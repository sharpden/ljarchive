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

namespace CookComputing.XmlRpc
{
  using System;
  using System.Collections;
  using System.IO;
  using System.Reflection;
  using System.Text.RegularExpressions;

  public class XmlRpcServerProtocol
  {
    public Stream Invoke(Stream requestStream)
    {
      try
      {
        XmlRpcSerializer serializer = new XmlRpcSerializer();
        XmlRpcRequest xmlRpcReq 
          = serializer.DeserializeRequest(requestStream, this.GetType());
        XmlRpcResponse xmlRpcResp = Invoke(xmlRpcReq);
        Stream responseStream = new MemoryStream();
        serializer.SerializeResponse(responseStream, xmlRpcResp);
        responseStream.Seek(0, SeekOrigin.Begin);
        return responseStream;
      }
      catch (Exception ex)
      {
        XmlRpcFaultException fex;
        if (ex is XmlRpcException)
          fex = new XmlRpcFaultException("0", ((XmlRpcException)ex).Message);
        else if (ex is XmlRpcFaultException)
          fex = (XmlRpcFaultException)ex;
        else 
          fex = new XmlRpcFaultException("0", ex.Message);
        XmlRpcSerializer serializer = new XmlRpcSerializer();
        Stream responseStream = new MemoryStream();
        serializer.SerializeFaultResponse(responseStream, fex);
        responseStream.Seek(0, SeekOrigin.Begin);
        return responseStream;      
      }
    }

    public XmlRpcResponse Invoke(XmlRpcRequest request)
    {
      MethodInfo mi = null;
      if (request.mi != null)
      {
        mi = request.mi;
      }
      else
      {
        mi = this.GetType().GetMethod(request.method);
      }
      // exceptions thrown during an MethodInfo.Invoke call are
      // package as inner of 
      Object reto;
      try
      {
        reto = mi.Invoke(this, request.args);
      }
      catch(Exception ex)
      {
        if (ex.InnerException != null)
          throw ex.InnerException;
        throw ex;
      }
      XmlRpcResponse response = new XmlRpcResponse(reto);
      return response;
    }

    bool IsVisibleXmlRpcMethod(MethodInfo mi)
    {
      bool ret = false;
      Attribute attr = Attribute.GetCustomAttribute(mi, 
        typeof(XmlRpcMethodAttribute));
      if (attr != null)
      {
        XmlRpcMethodAttribute mattr = (XmlRpcMethodAttribute)attr;
        ret = !mattr.Hidden;
      }
      return ret;
    }

    [XmlRpcMethod("system.listMethods",  IntrospectionMethod = true,
       Description=
       "Return an array of all available XML-RPC methods on this Service.")]
    public string[] System__List__Methods___()
    {
      XmlRpcServiceInfo svcInfo = XmlRpcServiceInfo.CreateServiceInfo(
                                    this.GetType());
      ArrayList alist = new ArrayList();
      foreach (XmlRpcMethodInfo mthdInfo in svcInfo.Methods)
      {
        if (!mthdInfo.IsHidden)
          alist.Add(mthdInfo.XmlRpcName);
      }
      return (String[])alist.ToArray(typeof(string));
    }

    [XmlRpcMethod("system.methodSignature",  IntrospectionMethod = true,
       Description=
       "Given the name of a method, return an array of legal signatures. "+
       "Each signature is an array of strings. The first item of each "+
       "signature is the return type, and any others items are parameter "+
       "types.")]
    public Array System__Method__Signature___(string MethodName)
    {
      //TODO: support overloaded methods
      XmlRpcServiceInfo svcInfo = XmlRpcServiceInfo.CreateServiceInfo(
        this.GetType());
      XmlRpcMethodInfo mthdInfo = svcInfo.GetMethod(MethodName);
      if (mthdInfo == null)
      {
        throw new XmlRpcFaultException("880", 
          "Request for information on unsupported method");
      }
      if (mthdInfo.IsHidden)
      {
        throw new XmlRpcFaultException("881", 
          "Information not available on this method");
      }
      //XmlRpcTypes.CheckIsXmlRpcMethod(mi);
      ArrayList alist = new ArrayList();
      alist.Add(XmlRpcServiceInfo.GetXmlRpcTypeString(mthdInfo.ReturnType));
      foreach (XmlRpcParameterInfo paramInfo in mthdInfo.Parameters)
      {
        alist.Add(XmlRpcServiceInfo.GetXmlRpcTypeString(paramInfo.Type));
      }
      string[] types = (string[])alist.ToArray(typeof(string));
      ArrayList retalist = new ArrayList();
      retalist.Add(types);
      Array retarray = retalist.ToArray(typeof(string[]));
      return retarray;
    }

    [XmlRpcMethod("system.methodHelp", IntrospectionMethod = true,
       Description=
       "Given the name of a method, return a help string.")]
    public string System__Method__Help___(string MethodName)
    {
      //TODO: support overloaded methods?
      XmlRpcServiceInfo svcInfo = XmlRpcServiceInfo.CreateServiceInfo(
        this.GetType());
      XmlRpcMethodInfo mthdInfo = svcInfo.GetMethod(MethodName);
      if (mthdInfo == null)
      {
        throw new XmlRpcFaultException("880", 
          "Request for information on unsupported method");
      }
      if (mthdInfo.IsHidden)
      {
		  throw new XmlRpcFaultException("881", 
          "Information not available for this method");
      }
      return mthdInfo.Doc;
    }
  }
}
