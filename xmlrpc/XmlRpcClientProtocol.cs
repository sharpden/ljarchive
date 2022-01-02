/* 
XML-RPC.NET library
Copyright (c) 2001-2003, Charles Cook <ccook@cookcomputing.com>

Permission is hereby granted, free of charge, to any person 
obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Services.Protocols;

namespace CookComputing.XmlRpc
{
  public class XmlRpcClientProtocol : WebClientProtocol
  {
    public XmlRpcClientProtocol(System.ComponentModel.IContainer container)
    {
      container.Add(this);
      InitializeComponent();
    }

    public XmlRpcClientProtocol()
    {
      InitializeComponent();
    }

    public object Invoke(
      string MethodName,
      params object[]Parameters)
    {
      return Invoke(this, MethodName, Parameters);
    }

    public object Invoke(
      Object clientObj, 
      string methodName, 
      params object[] parameters)
    {
      WebRequest webReq = null;
      object reto = null;
      try
      {
        SetUrl(clientObj);
        webReq = GetWebRequest(new Uri(Url));
        XmlRpcRequest req = MakeXmlRpcRequest(webReq, methodName, parameters, clientObj);
        SetProperties(webReq);
        SetRequestHeaders(headers, webReq);
        SetClientCertificates(clientCertificates, webReq);
        Stream reqStm = webReq.GetRequestStream();
        try
        {
          XmlRpcSerializer serializer = new XmlRpcSerializer();
          if (xmlEncoding != null)
            serializer.XmlEncoding = xmlEncoding;
          serializer.UseIndentation = _useIndentation;
          serializer.Indentation = _indentation;
		  serializer.SerializeRequest(reqStm, req);
        }
        finally
        {
          reqStm.Close();
        }
        WebResponse webResp = GetWebResponse(webReq);
        Stream respStm = null;
        try
        { 
          respStm = webResp.GetResponseStream();
          XmlRpcResponse resp = ReadResponse(req, webResp, respStm, null);
          reto = resp.retVal;
        }
        finally
        {
          if (respStm != null)
            respStm.Close();
        }
      }
      finally
      {
        if (webReq != null)
          webReq = null;
      }
      return reto;
    }

    public string UserAgent
    {
      get { return userAgent; }
      set { userAgent = value; }
    }

    public IWebProxy Proxy
    {
      get { return proxy; }
      set { proxy = value; }
    }

    public Encoding XmlEncoding
    {
      get { return xmlEncoding; }
      set { xmlEncoding = value; }
    }
    
    public Encoding XmlDecoding
    {
      get { return xmlDecoding; }
      set { xmlDecoding = value; }    	
    }

    // added by Erik Frey 4/3/2004
    public Version ProtocolVersion
    {
      get { return protocolVersion; }
      set { protocolVersion = value; }
    }

    // added by Erik Frey 1/5/2005
    public bool KeepAlive
    {
      get { return keepAlive; }
      set { keepAlive = value; }
    }

    public bool UseIndentation
    {
      get { return _useIndentation; }
      set { _useIndentation = value; }
    }

    public int Indentation
    {
      get { return _indentation; }
      set { _indentation = value; }
    }

    public void SetProperties(WebRequest webReq)
    {
      if (proxy != null)
        webReq.Proxy = proxy;
      HttpWebRequest httpReq = (HttpWebRequest) webReq;
      httpReq.KeepAlive = keepAlive;
      httpReq.ProtocolVersion = protocolVersion;
      httpReq.UserAgent = userAgent;
    }

    public WebHeaderCollection Headers
    {
      get { return headers; }
    }

    private void SetRequestHeaders(
      WebHeaderCollection headers,
      WebRequest webReq)
    {
      foreach (string key in headers)
      {
        webReq.Headers.Add(key, headers[key]); 
      }
    }

    public X509CertificateCollection ClientCertificates
    {
      get { return clientCertificates; }
    }

    private void SetClientCertificates(
      X509CertificateCollection certificates,
      WebRequest webReq)
    {
      foreach (X509Certificate certificate in certificates)
      {
        HttpWebRequest httpReq = (HttpWebRequest)webReq;
        httpReq.ClientCertificates.Add(certificate); 
      }
    }

    XmlRpcRequest MakeXmlRpcRequest(WebRequest webReq, string methodName, object[] parameters, object clientObj)
    {
      webReq.Method = "POST";
      webReq.ContentType = "text/xml";
      string rpcMethodName = GetRpcMethodName(clientObj, methodName);
      MethodInfo mi = null;
      if (clientObj != null)
        mi = clientObj.GetType().GetMethod(methodName);
      XmlRpcRequest req = new XmlRpcRequest(rpcMethodName, parameters, mi);
      return req;
    }

    XmlRpcResponse ReadResponse(
      XmlRpcRequest req, 
      WebResponse webResp, 
      Stream respStm,
      Type returnType)
    {
      HttpWebResponse httpResp = (HttpWebResponse)webResp;
      if (httpResp.StatusCode != HttpStatusCode.OK)
      {
        // status 400 is used for errors caused by the client
        // status 500 is used for server errors (not server application
        // errors which are returned as fault responses)
        if (httpResp.StatusCode == HttpStatusCode.BadRequest)
          throw new XmlRpcException(httpResp.StatusDescription);
        else
          throw new XmlRpcServerException(httpResp.StatusDescription);
      }
      XmlRpcSerializer serializer = new XmlRpcSerializer();
      if (xmlDecoding != null)
          	serializer.XmlDecoding = xmlDecoding;
      Type retType = returnType;
      if (retType == null)
        retType = req.mi.ReturnType;
      XmlRpcResponse xmlRpcResp 
        = serializer.DeserializeResponse(respStm, retType);
      return xmlRpcResp;
    }

    string GetRpcMethodName(object clientObj, string MethodName)
    {
      string rpcMethod;
      // extract method name from Proxy type info
      Type type = clientObj.GetType();
      MethodInfo mi = type.GetMethod(MethodName);
      if (mi == null)
      {
        throw new Exception("Invoke on non-existent or non-public proxy method");
      }
      // first check if specified method is a Begin async method
      Attribute attr = Attribute.GetCustomAttribute(mi, 
        typeof(XmlRpcBeginAttribute));
      if (attr != null)
      {
        rpcMethod = ((XmlRpcBeginAttribute)attr).Method;
        if (rpcMethod == "")  
        {
          if (!MethodName.StartsWith("Begin") || MethodName.Length <= 5)
            throw new Exception(String.Format(
              "method {0} has invalid signature for begin method", 
              MethodName));
          rpcMethod = MethodName.Substring(5);
        }
        return rpcMethod;
      }
      // if no XmlRpcBegin attribute, must have XmlRpcMethod attribute   
      attr = Attribute.GetCustomAttribute(mi, typeof(XmlRpcMethodAttribute));
      if (attr == null)
      {
        throw new Exception("missing method attribute");
      }
      XmlRpcMethodAttribute xrmAttr = attr as XmlRpcMethodAttribute;
      rpcMethod = xrmAttr.Method;
      if (rpcMethod == "")
      {
        rpcMethod = mi.Name;
      }
      return rpcMethod;
    }

    public IAsyncResult BeginInvoke(
      string methodName, 
      object[] parameters, 
      AsyncCallback callback, 
      object outerAsyncState)
    {
      return BeginInvoke(methodName, parameters, this, callback, 
        outerAsyncState);
    }

    public IAsyncResult BeginInvoke(
      string methodName, 
      object[] parameters, 
      object clientObj, 
      AsyncCallback callback, 
      object outerAsyncState)
    {
      SetUrl(clientObj);
      WebRequest webReq = GetWebRequest(new Uri(Url));
      XmlRpcRequest xmlRpcReq = MakeXmlRpcRequest(webReq, methodName, 
        parameters, clientObj);
      SetProperties(webReq);
      SetRequestHeaders(headers, webReq);
      SetClientCertificates(clientCertificates, webReq);
      Encoding useEncoding = null;
      if (xmlEncoding != null)
        useEncoding = xmlEncoding;
      XmlRpcAsyncResult asr = new XmlRpcAsyncResult(this, xmlRpcReq, 
        useEncoding, _useIndentation, _indentation, webReq, callback, 
        outerAsyncState, 0);
      webReq.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), asr);
      if (!asr.IsCompleted)
        asr.CompletedSynchronously = false;
      return asr;
    }

    static void GetRequestStreamCallback(IAsyncResult asyncResult)
    {
      XmlRpcAsyncResult clientResult 
        = (XmlRpcAsyncResult)asyncResult.AsyncState;
      clientResult.CompletedSynchronously = asyncResult.CompletedSynchronously;
      try
      {
        Stream stm = clientResult.Request.EndGetRequestStream(asyncResult);
        try
        {
          XmlRpcRequest req = clientResult.XmlRpcRequest;
          XmlRpcSerializer serializer = new XmlRpcSerializer();
          if (clientResult.XmlEncoding != null)
            serializer.XmlEncoding = clientResult.XmlEncoding;
          serializer.UseIndentation = clientResult.UseIndentation;
          serializer.Indentation = clientResult.Indentation;
          serializer.SerializeRequest(stm, req); 
        }
        finally
        {
          stm.Close();
        }
        clientResult.Request.BeginGetResponse(new AsyncCallback(GetResponseCallback), clientResult);
      }
      catch (Exception ex)
      {
        ProcessAsyncException(clientResult, ex);
      }
    }

    static void GetResponseCallback(IAsyncResult asyncResult)
    {
      XmlRpcAsyncResult result = (XmlRpcAsyncResult)asyncResult.AsyncState;
      result.CompletedSynchronously = asyncResult.CompletedSynchronously;
      try
      {
        result.Response = result.ClientProtocol.GetWebResponse(result.Request, asyncResult);
      }
      catch (Exception ex)
      {
        ProcessAsyncException(result, ex);
        if (result.Response == null)
          return;
      }
      ReadAsyncResponse(result);
    }

    static void ReadAsyncResponse(XmlRpcAsyncResult result)
    {
      if (result.Response.ContentLength == 0)
      {
        result.Complete();
        return;
      }
      try
      {
        result.ResponseStream = result.Response.GetResponseStream();
        ReadAsyncResponseStream(result);
      }
      catch (Exception ex)
      {
        ProcessAsyncException(result, ex);
      }
    }

    static void ReadAsyncResponseStream(XmlRpcAsyncResult result)
    {
      IAsyncResult asyncResult;
      do
      {
        byte[] buff = result.Buffer;
        long contLen = result.Response.ContentLength;
        if (buff == null)
        {
          if (contLen == -1)
            result.Buffer = new Byte[1024];
          else
            result.Buffer = new Byte[contLen];
        }
        else
        {
          if (contLen != -1 && contLen > result.Buffer.Length)
            result.Buffer = new Byte[contLen];
        }
        buff = result.Buffer;
        asyncResult = result.ResponseStream.BeginRead(buff, 0, buff.Length, new AsyncCallback(ReadResponseCallback), result);
        if (!asyncResult.CompletedSynchronously)
          return;
      }
      while (!(ProcessAsyncResponseStreamResult(result, asyncResult)));
    }

    static bool ProcessAsyncResponseStreamResult(XmlRpcAsyncResult result, IAsyncResult asyncResult)
    {
      int endReadLen = result.ResponseStream.EndRead(asyncResult);
      long contLen = result.Response.ContentLength;
      bool completed;
      if (endReadLen == 0)
        completed = true;
      else if (contLen > 0 && endReadLen == contLen)
      {
        result.ResponseBufferedStream = new MemoryStream(result.Buffer);
        completed = true;
      }
      else
      {
        if (result.ResponseBufferedStream == null)
        {
          result.ResponseBufferedStream = new MemoryStream(result.Buffer.Length);
        }
        result.ResponseBufferedStream.Write(result.Buffer, 0, endReadLen);
        completed = false;
      }
      if (completed)
        result.Complete();
      return completed;
    }


    static void ReadResponseCallback(IAsyncResult asyncResult)
    {
      XmlRpcAsyncResult result = (XmlRpcAsyncResult)asyncResult.AsyncState;
      result.CompletedSynchronously = asyncResult.CompletedSynchronously;
      if (asyncResult.CompletedSynchronously)
        return;
      try
      {
        bool completed = ProcessAsyncResponseStreamResult(result, asyncResult);
        if (!completed)
          ReadAsyncResponseStream(result);
      }
      catch(Exception ex)
      {
        ProcessAsyncException(result, ex);
      }
    }

    static void ProcessAsyncException(XmlRpcAsyncResult clientResult, Exception ex)
    {
      WebException webex = ex as WebException;
      if (webex != null && webex.Response != null)
      {
        clientResult.Response = webex.Response;
        return;
      }
      if (clientResult.IsCompleted)
        throw new Exception("error during async processing");
      clientResult.Complete(ex);
    }

    public object EndInvoke(
      IAsyncResult asr)
    {
      return EndInvoke(asr, null);
    }

    public object EndInvoke(
      IAsyncResult asr, 
      Type returnType)
    {
      object reto = null;
      WebResponse webResp = null;
      Stream responseStream = null;
      try
      {
        XmlRpcAsyncResult clientResult = (XmlRpcAsyncResult)asr;
        if (clientResult.Exception != null)
          throw clientResult.Exception;
        if (clientResult.EndSendCalled)
          throw new Exception("dup call to EndSend");
        clientResult.EndSendCalled = true;
        webResp = clientResult.WaitForResponse();
        responseStream = clientResult.ResponseBufferedStream;

        XmlRpcResponse resp = ReadResponse(clientResult.XmlRpcRequest, 
          webResp, responseStream, returnType);
        reto = resp.retVal;
      }
      finally
      {
        if (responseStream != null)
          responseStream.Close();
        if (webResp != null)
          webResp = null;
      }
      return reto;
    }

    void SetUrl(object clientObj)
    {
      Type type = clientObj.GetType();
      // client can either have define URI in attribute or have set it
      // via proxy's ServiceURI property - but must exist by now
      string useUrl = "";
      if (Url == "")
      {
        Attribute urlAttr = Attribute.GetCustomAttribute(type, 
          typeof(XmlRpcUrlAttribute));
        if (urlAttr != null)
        {
          XmlRpcUrlAttribute xrsAttr = urlAttr as XmlRpcUrlAttribute;
          useUrl = xrsAttr.Uri;
        }
      }
      else
      {
        useUrl = Url;
      }
      if (useUrl == "")
      {
        throw new Exception("Url not specified.");
      }
      Url = useUrl;
    }
 
    // introspection methods
    [XmlRpcMethod("system.listMethods")]
    public string[] SystemListMethods()
    {
      return (string[])Invoke("SystemListMethods", new Object[0]);
    }

    [XmlRpcMethod("system.listMethods")]
    public IAsyncResult BeginSystemListMethods(
      AsyncCallback Callback,
      object State)
    {
      return BeginInvoke("SystemListMethods", new object[0], this, Callback, State);
    }

    public string[] EndSystemListMethods(IAsyncResult AsyncResult)
    {
      return (string[])EndInvoke(AsyncResult);
    }

    [XmlRpcMethod("system.methodSignature")]
    public object[] SystemMethodSignature(string MethodName)
    {
      return (object[])Invoke("SystemMethodSignature", 
        new Object[]{MethodName});
    }

    [XmlRpcMethod("system.methodSignature")]
    public IAsyncResult BeginSystemMethodSignature(
      string MethodName,
      AsyncCallback Callback,
      object State)
    {
      return BeginInvoke("SystemMethodSignature", 
        new Object[]{MethodName}, this, Callback, State);
    }

    public Array EndSystemMethodSignature(IAsyncResult AsyncResult)
    {
      return (Array)EndInvoke(AsyncResult);
    }

    [XmlRpcMethod("system.methodHelp")]
    public string SystemMethodHelp(string MethodName)
    {
      return (string)Invoke("SystemMethodHelp", 
        new Object[]{MethodName});
    }

    [XmlRpcMethod("system.methodHelp")]
    public IAsyncResult BeginSystemMethodHelp(
      string MethodName,
      AsyncCallback Callback,
      object State)
    {
      return BeginInvoke("SystemMethodHelp", 
        new Object[]{MethodName}, this, Callback, State);
    }

    public string EndSystemMethodHelp(IAsyncResult AsyncResult)
    {
      return (string)EndInvoke(AsyncResult);
    }

    private string userAgent = "XML-RPC.NET";
    private WebHeaderCollection headers = new WebHeaderCollection();
    private X509CertificateCollection clientCertificates 
      = new X509CertificateCollection();
    private IWebProxy proxy = null;
    private Encoding xmlEncoding = null;
    private Encoding xmlDecoding = null;
    private Version protocolVersion = new Version(1,1);
    private bool keepAlive = true;
    private bool _useIndentation = false;
    private int _indentation = 2;
		#region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }
		#endregion
  }
}
