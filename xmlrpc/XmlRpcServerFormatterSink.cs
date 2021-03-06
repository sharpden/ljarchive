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
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

using CookComputing.XmlRpc;

namespace CookComputing.XmlRpc
{
  public class XmlRpcServerFormatterSink : IServerChannelSink
  {
    //constructors
    //
    public XmlRpcServerFormatterSink(
      IServerChannelSink Next)
    {
      m_next = Next;
    }

    // properties
    //
    public IServerChannelSink NextChannelSink 
    {
      get { return m_next; }
    }
  
    public IDictionary Properties
    {
      get { return null; }
    }

    // public methods
    //
    public void AsyncProcessResponse(
      IServerResponseChannelSinkStack sinkStack,
      object state,
      IMessage msg,
      ITransportHeaders headers,
      Stream stream
      )
    {
      throw new NotSupportedException();
    }

    public Stream GetResponseStream(
      IServerResponseChannelSinkStack sinkStack, 
      object state, 
      IMessage msg, 
      ITransportHeaders headers) 
    {
      throw new NotSupportedException();
    }

    public ServerProcessing ProcessMessage(
      IServerChannelSinkStack sinkStack,
      IMessage requestMsg,
      ITransportHeaders requestHeaders,
      Stream requestStream,
      out IMessage responseMsg,
      out ITransportHeaders responseHeaders,
      out Stream responseStream
      )
    {
      // use presence of SOAPAction header to determine if this is a SOAP
      // request - if so pass onto next sink in chain
      string soapAction = (string) requestHeaders["SOAPAction"];
      if (soapAction != null)
      {
        return m_next.ProcessMessage(sinkStack, requestMsg, requestHeaders, 
          requestStream, out responseMsg, out responseHeaders, 
          out responseStream);
      }
      // for time being assume we have an XML-RPC request (need to improve
      // this in case there are other non-SOAP formatters in the chain)
      MethodCall mthdCall = DeserializeRequest(requestHeaders, requestStream);
      sinkStack.Push(this, mthdCall);
      // forward to next sink in chain - pass request stream as null to 
      // indicate that we have deserialized the request
      m_next.ProcessMessage(sinkStack, mthdCall, requestHeaders, null, 
          out responseMsg, out responseHeaders, out responseStream);
      SerializeResponse(responseMsg, ref responseHeaders, ref responseStream);
      return ServerProcessing.Complete;
    }

    // private methods
    //
    MethodCall DeserializeRequest(
      ITransportHeaders requestHeaders, 
      Stream requestStream)
    {
      string requestUri = (string) requestHeaders["__RequestUri"];
      string typeName = GetTypeName(requestUri);
      Type svcType = Type.GetType(typeName);
      XmlRpcSerializer serializer = new XmlRpcSerializer();
      XmlRpcRequest xmlRpcReq 
        = serializer.DeserializeRequest(requestStream, svcType);
      // TODO: optimise out two calls to GetRegisteredWellKnownServiceTypes
      Header[] headers = GetChannelHeaders(requestHeaders, xmlRpcReq);
      MethodCall mthdCall = new MethodCall(headers);
      mthdCall.ResolveMethod();
      return mthdCall;
    }

    void SerializeResponse(
      IMessage responseMsg,
      ref ITransportHeaders responseHeaders, 
      ref Stream responseStream)
    {
      XmlRpcSerializer serializer = new XmlRpcSerializer();
      responseStream = new MemoryStream();
      responseHeaders = new TransportHeaders();

      ReturnMessage retMsg = (ReturnMessage)responseMsg;
      if (retMsg.Exception == null)
      {
        XmlRpcResponse xmlRpcResp = new XmlRpcResponse(retMsg.ReturnValue);
        serializer.SerializeResponse(responseStream, xmlRpcResp);
      }
      else if (retMsg.Exception is XmlRpcFaultException)
      {
        serializer.SerializeFaultResponse(responseStream, 
          (XmlRpcFaultException)retMsg.Exception);
      }
      else
      {
        serializer.SerializeFaultResponse(responseStream,
          new XmlRpcFaultException("1", retMsg.Exception.Message));
      }
      responseHeaders["Content-Type"] = "text/xml; charset=\"utf-8\"";
    }

    Header[] GetChannelHeaders(
      ITransportHeaders requestHeaders,
      XmlRpcRequest xmlRpcReq) 
    {
      string requestUri = (string) requestHeaders["__RequestUri"];
      string typeName = GetTypeName(requestUri);
      Type svcType = Type.GetType(typeName);
      XmlRpcServiceInfo svcInfo = XmlRpcServiceInfo.CreateServiceInfo(svcType);

      ArrayList hdrList = new ArrayList();
      hdrList.Add(new Header("__Uri", requestUri));
      hdrList.Add(new Header("__TypeName", typeName));
      hdrList.Add(new Header("__MethodName", 
        svcInfo.GetMethodName(xmlRpcReq.method)));
      hdrList.Add(new Header("__Args", xmlRpcReq.args));
      return (Header[])hdrList.ToArray(typeof(Header));
    }
       
    public static String GetTypeName(
      String Uri) 
    {
      WellKnownServiceTypeEntry[] svcTypes 
        = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
      foreach (WellKnownServiceTypeEntry svcType in svcTypes) 
      {
        if (Uri.Equals ("/" + svcType.ObjectUri)) 
        {
          // we need to pass type of service to deserializer and Type.GetType 
          // requires assembly qualified name
          return svcType.ObjectType.AssemblyQualifiedName;
        }
      }
      throw new Exception("couldn't find type");
    }

    // data
    //
    IServerChannelSink m_next;
  }


}
