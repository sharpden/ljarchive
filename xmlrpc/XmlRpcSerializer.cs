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

// TODO: overriding default mapping action in a struct should not affect nested structs

namespace CookComputing.XmlRpc
{
  using System;
  using System.Collections;
  using System.Globalization;
  using System.IO;
  using System.Reflection;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Xml;

  struct Fault
  {
    // At least for right now, LJ appears to send us fault codes as strings.
    public string faultCode;
    public string faultString;
  }

  public class XmlRpcSerializer
  {
    // properties
    public bool UseIndentation 
    {
      get { return m_bUseIndentation; }
      set { m_bUseIndentation = value; }
    }
    bool m_bUseIndentation = true;

    public int Indentation 
    {
      get { return m_indentation; }
      set { m_indentation = value; }
    }
    int m_indentation = 2;

    public Encoding XmlEncoding 
    {
      get { return m_encoding; }
      set { m_encoding = value; }
    }
    Encoding m_encoding = null;

    public Encoding XmlDecoding
    {
      get { return m_decoding; }
      set { m_decoding = value; }
    }
    Encoding m_decoding = null;
    // todo:
    //  add indent bool property
    //  add indent size property
    //
    public void SerializeRequest(Stream stm, XmlRpcRequest request) 
    {
      XmlTextWriter xtw = new XmlTextWriter(stm, m_encoding);
      ConfigureXmlFormat(xtw);
      xtw.WriteStartDocument();
      xtw.WriteStartElement("", "methodCall", "");
    {
      // TODO: use global action setting
      MappingAction mappingAction = MappingAction.Error;  
      xtw.WriteElementString("methodName", request.method);
      xtw.WriteStartElement("", "params", "");
      for (int i=0; i< request.args.Length; i++)
      {
        xtw.WriteStartElement("", "param", "");
        Serialize(xtw, request.args[i], mappingAction);
        xtw.WriteEndElement();
      }
      xtw.WriteEndElement();
    }
      xtw.WriteEndElement();
      xtw.Flush();
    }

    public XmlRpcRequest DeserializeRequest(Stream stm, Type svcType)
    {
      TextReader trdr = new StreamReader(stm, new UTF8Encoding(), true, 4096);
      return DeserializeRequest(trdr, svcType);
    }

    public XmlRpcRequest DeserializeRequest(TextReader trdr, Type svcType)
    {
      XmlRpcRequest request = new XmlRpcRequest();
      XmlDocument xdoc = new XmlDocument();
      //xdoc.PreserveWhitespace = true;
      try 
      {
        xdoc.Load(trdr);
      }
      catch (Exception ex)
      {
        throw new XmlRpcIllFormedXmlException(
          "Request from client does not contain valid XML.", ex);
      }
      XmlNode callNode = xdoc.SelectSingleNode("/methodCall");
      if (callNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(
          "Request XML not valid XML-RPC - missing methodCall element.");
      }
      XmlNode methodNode = callNode.SelectSingleNode("methodName");
      if (methodNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(
          "Request XML not valid XML-RPC - missing methodName element.");
      }
      request.method = methodNode.FirstChild.Value;
      if (request.method == "")
      {
        throw new XmlRpcInvalidXmlRpcException(
          "Request XML not valid XML-RPC - empty methodName.");
      }
      request.mi = null;
      ParameterInfo[] pis = new ParameterInfo[1];
      if (svcType != null)
      {
        // retrieve info for the method which handles this XML-RPC method
        XmlRpcServiceInfo svcInfo 
          = XmlRpcServiceInfo.CreateServiceInfo(svcType);
        request.mi = svcInfo.GetMethodInfo(request.method);
        // if a service type has been specified and we cannot find the requested
        // method then we must throw an exception
        if (request.mi == null)
        {
          string msg = String.Format("unsupported method called: {0}", 
                                      request.method);
          throw new XmlRpcUnsupportedMethodException(msg);
        }
        // method must be marked with XmlRpcMethod attribute
        Attribute attr = Attribute.GetCustomAttribute(request.mi, 
          typeof(XmlRpcMethodAttribute));
        if (attr == null)
        {
          throw new XmlRpcMethodAttributeException(
            "Method must be marked with the XmlRpcMethod attribute.");
        }
        pis = request.mi.GetParameters();
      }
      XmlNode paramsNode = callNode.SelectSingleNode("params");
      if (paramsNode == null)
      {
        if (svcType != null)
        {
          if (pis.Length == 0)
          {
            request.args = new object[0];
            return request;
          }
          else
          {
            throw new XmlRpcInvalidParametersException(
              "Method takes parameters and params element is missing.");
          }
        }
        else
        {
          request.args = new object[0];
          return request;
        }
      }
      XmlNodeList paramNodes = paramsNode.ChildNodes; 
      if (svcType != null && paramNodes.Count != pis.Length)
      {
        throw new XmlRpcInvalidParametersException(
          "Method takes parameters and there is incorrect number of param elements.");
      }
      ParseStack parseStack = new ParseStack("request");
      // TODO: use global action setting
      MappingAction mappingAction = MappingAction.Error;  
      Object[] paramObjs = new Object[paramNodes.Count];
      for (int i = 0; i < paramNodes.Count; i++) 
      {
        XmlNode paramNode = paramNodes[i];
        XmlNode valueNode = paramNode.FirstChild;
        XmlNode node = valueNode.FirstChild;
        if (svcType != null)
        {
          parseStack.Push(String.Format("parameter {0}", i));
//          parseStack.Push(String.Format("parameter {0} mapped to type {1}", 
//            i, pis[i].ParameterType.Name));
          paramObjs[i] = ParseValue(node, pis[i].ParameterType, parseStack, 
            mappingAction);
        }
        else
        {
          parseStack.Push(String.Format("parameter {0}", i));
          paramObjs[i] = ParseValue(node, null, parseStack, mappingAction);
        }
        parseStack.Pop();
      }
      request.args = paramObjs;
      return request;
    }

    public void SerializeResponse(Stream stm, XmlRpcResponse response)
    {
      Object ret = response.retVal;
      if (ret is XmlRpcFaultException)
      {
        SerializeFaultResponse(stm, (XmlRpcFaultException)ret);
        return;
      }

      XmlTextWriter xtw = new XmlTextWriter(stm, m_encoding);
      ConfigureXmlFormat(xtw);
      xtw.WriteStartDocument();
      xtw.WriteStartElement("", "methodResponse", "");
      xtw.WriteStartElement("", "params", "");
      // "void" methods actually return an empty string value
      if (ret == null)
      {
        ret = "";
      }
      xtw.WriteStartElement("", "param", "");
      // TODO: use global action setting
      MappingAction mappingAction = MappingAction.Error;  
      Serialize(xtw, ret, mappingAction);
      xtw.WriteEndElement();
      xtw.WriteEndElement();
      xtw.WriteEndElement();
      xtw.Flush();
    }

    public XmlRpcResponse DeserializeResponse(Stream stm, Type returnType)
    {
      TextReader trdr = new StreamReader(stm, new UTF8Encoding(), true, 4096);
      return DeserializeResponse(trdr, returnType);
    }

    public XmlRpcResponse DeserializeResponse(TextReader trdr, Type returnType)
    {
      XmlRpcResponse response = new XmlRpcResponse();
      Object retObj = null;
      XmlDocument xdoc = new XmlDocument();
      //xdoc.PreserveWhitespace = true;
      try
      {
        xdoc.Load(trdr);
      }
      catch (Exception ex)
      {
        throw new XmlRpcIllFormedXmlException(
          "Response from server does not contain valid XML.", ex);
      }
      // check for fault response
      XmlNode faultNode = xdoc.SelectSingleNode("/methodResponse/fault");
      if (faultNode != null)
      {
        ParseStack parseStack = new ParseStack("fault response");
        // TODO: use global action setting
        MappingAction mappingAction = MappingAction.Error;  
        XmlRpcFaultException faultEx = ParseFault(faultNode, parseStack, 
          mappingAction);
        throw faultEx;
      }
      XmlNode methodResponseNode = xdoc.SelectSingleNode("/methodResponse");
      if (methodResponseNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(
          "Response XML not valid XML-RPC - missing methodResponse element.");
      }
      XmlNode paramsNode = methodResponseNode.SelectSingleNode("params");
      if (paramsNode == null && returnType != null)
      {
        if (returnType == typeof(void))
          return null;
        else
          throw new XmlRpcInvalidXmlRpcException(
            "Response XML not valid XML-RPC - missing params element.");
      }
      XmlNode paramNode = paramsNode.SelectSingleNode("param"); // TODO: verify single params node?
      if (paramNode == null && returnType != null)
      {
        if (returnType == typeof(void))
          return null;
        else
          throw new XmlRpcInvalidXmlRpcException(
            "Response XML not valid XML-RPC - missing params element.");
      }
      XmlNode valueNode = paramNode.SelectSingleNode("value");
      if (valueNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(
          "Response XML not valid XML-RPC - missing value element.");
      }
      if (returnType == typeof(void))
      {
        retObj = null;
      }
      else
      {
        ParseStack parseStack = new ParseStack("response");
        // TODO: use global action setting
        MappingAction mappingAction = MappingAction.Error;  
        XmlNode node = valueNode.FirstChild;
        retObj = ParseValue(node, returnType, parseStack, mappingAction);
      }
      response.retVal = retObj;
      return response;
    }
    
    #if (DEBUG)
    public
    #endif
    void Serialize(
      XmlTextWriter xtw, 
      Object o,
      MappingAction mappingAction)
    {
      try
      {
        xtw.WriteStartElement("", "value", "");
        XmlRpcType xType = XmlRpcServiceInfo.GetXmlRpcType(o.GetType());
        switch (xType)
        {
          case XmlRpcType.tArray:
            xtw.WriteStartElement("", "array", "");
            xtw.WriteStartElement("", "data", "");
            Array a = (Array) o;
            foreach (Object aobj in a)
            {
              Serialize(xtw, aobj, mappingAction);
            }
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            break;
          case XmlRpcType.tMultiDimArray:
            Array mda = (Array)o;
            int[] indices = new int[mda.Rank];
            BuildArrayXml(xtw, mda, 0, indices, mappingAction);
            break;
          case XmlRpcType.tBase64:
            byte[] buf = (byte[])o;
            xtw.WriteStartElement("", "base64", "");
            xtw.WriteBase64(buf, 0, buf.Length);
            xtw.WriteEndElement();
            break;
          case XmlRpcType.tBoolean:
            bool boolVal;
            if (o is bool)
              boolVal = (bool)o;
            else
              boolVal = (XmlRpcBoolean)o;
            if (boolVal)
              xtw.WriteElementString("boolean", "1");
            else
              xtw.WriteElementString("boolean", "0");
            break;
          case XmlRpcType.tDateTime:
            DateTime dt;
            if (o is DateTime)
              dt = (DateTime)o;
            else
              dt = (XmlRpcDateTime)o;
            string sdt = dt.ToString("yyyyMMddTHH:mm:ss", 
              null);
            xtw.WriteElementString("dateTime.iso8601", sdt);
            break;
          case XmlRpcType.tDouble:
            double doubleVal;
            if (o is double)
              doubleVal = (double)o;
            else
              doubleVal = (XmlRpcDouble)o;
            xtw.WriteElementString("double", doubleVal.ToString(null, 
              CultureInfo.InvariantCulture));
            break;
          case XmlRpcType.tHashtable:
            xtw.WriteStartElement("", "struct", "");
            XmlRpcStruct xrs = o as XmlRpcStruct;
            foreach (object obj in xrs.Keys)
            {
              string skey = obj as string;
              xtw.WriteStartElement("", "member", "");
              xtw.WriteElementString("name", skey);
              Serialize(xtw, xrs[skey], mappingAction);
              xtw.WriteEndElement();
            }
            xtw.WriteEndElement();
            break;
          case XmlRpcType.tInt32:
            xtw.WriteElementString("i4", o.ToString());
            break;
          case XmlRpcType.tString:
            xtw.WriteElementString("string", (string)o);
            break;
          case XmlRpcType.tStruct:
            MappingAction structAction 
              = StructMappingAction(o.GetType(), mappingAction);
            xtw.WriteStartElement("", "struct", "");
            MemberInfo[] mis = o.GetType().GetMembers();
            foreach (MemberInfo mi in mis)
            {
              if (mi.MemberType == MemberTypes.Field)
              {
                FieldInfo fi = (FieldInfo)mi;
                string member = fi.Name;
                Attribute attrchk = Attribute.GetCustomAttribute(fi, 
                  typeof(XmlRpcMemberAttribute));
                if (attrchk != null && attrchk is XmlRpcMemberAttribute)
                {
                  string mmbr = ((XmlRpcMemberAttribute)attrchk).Member;
                  if (mmbr != "")
                    member = member;
                }
                if (fi.GetValue(o) == null)
                {
                  MappingAction memberAction = MemberMappingAction(o.GetType(),
                    member, structAction);
                  if (memberAction == MappingAction.Ignore)
                    continue;
                }
                xtw.WriteStartElement("", "member", "");
                xtw.WriteElementString("name", member);
                Serialize(xtw, fi.GetValue(o), mappingAction);
                xtw.WriteEndElement();
              }    
            }
            xtw.WriteEndElement();
            break;
          default:
            throw new Exception();
        }
        xtw.WriteEndElement();
      }
      catch(System.NullReferenceException)
      {
        throw new XmlRpcNullReferenceException("Attempt to serialize data containing null reference");
      }
    }

    void BuildArrayXml(
      XmlTextWriter xtw, 
      Array ary, 
      int CurRank, 
      int[] indices,
      MappingAction mappingAction)
    {
      xtw.WriteStartElement("", "array", "");
      xtw.WriteStartElement("", "data", "");
      if (CurRank < (ary.Rank-1))
      {
        for (int i=0; i<ary.GetLength(CurRank); i++)
        {
          indices[CurRank] = i;
          BuildArrayXml(xtw, ary, CurRank+1, indices, mappingAction);
        }      
      }
      else
      {
        for (int i=0; i<ary.GetLength(CurRank); i++)
        {
          indices[CurRank] = i;
          Serialize(xtw, ary.GetValue(indices), mappingAction);
        }
      }
      xtw.WriteEndElement();
      xtw.WriteEndElement();
    }

    Object ParseValue(
      XmlNode node, 
      Type ValueType, 
      ParseStack parseStack,
      MappingAction mappingAction)
    {
      Type parsedType;
      Type parsedArrayType;
      return ParseValue(node, ValueType, parseStack, mappingAction, 
        out parsedType, out parsedArrayType);
    }

    #if (DEBUG)
    public
    #endif
    Object ParseValue(
      XmlNode node, 
      Type ValueType, 
      ParseStack parseStack,
      MappingAction mappingAction,
      out Type ParsedType,
      out Type ParsedArrayType)
    {
      ParsedType = null;
      ParsedArrayType = null;
      // if suppplied type is System.Object then ignore it because
      // if doesn't provide any useful information (parsing methods
      // expect null in this case)
      Type valType = ValueType;
      if (valType != null && valType.BaseType == null)
        valType = null;
      
      Object retObj = null;
      if (node == null)
      {
        retObj = "";
      }
      else if (node is XmlText)
      {
        if (valType != null && valType != typeof(string))
        {
          throw new XmlRpcTypeMismatchException(parseStack.ParseType 
            + " contains implicit string value where " 
            + XmlRpcServiceInfo.GetXmlRpcTypeString(valType) 
            + " expected " + StackDump(parseStack));
        }
        retObj = node.Value;
      }
      else 
      {
        switch (node.Name)
        {
          case "array":
            retObj = ParseArray(node, valType, parseStack, mappingAction);
            break;
          case "base64":
            retObj = ParseBase64(node, valType, parseStack, mappingAction);
            break;
          case "struct":
            // if we don't know the expected struct type then we must
            // parse the XML-RPC struct as an instance of XmlRpcStruct
            if (valType != null && valType != typeof(XmlRpcStruct)
              && !valType.IsSubclassOf(typeof(XmlRpcStruct)))
            {
              retObj = ParseStruct(node, valType, parseStack, mappingAction);
            }
            else
            {
              if (valType == null || valType == typeof(object))
                valType = typeof(XmlRpcStruct);
              // TODO: do we need to validate type here?
              retObj = ParseHashtable(node, valType, parseStack, mappingAction);
            }
            break;
          case "i4":  // integer has two representations in XML-RPC spec
          case "int":
            retObj = ParseInt(node, valType, parseStack, mappingAction);
            ParsedType = typeof(int);
            ParsedArrayType = typeof(int[]);
            break;
          case "string":
            retObj = ParseString(node, valType, parseStack, mappingAction);
            ParsedType = typeof(string);
            ParsedArrayType = typeof(string[]);
            break;
          case "boolean":
            retObj = ParseBoolean(node, valType, parseStack, mappingAction);
            ParsedType = typeof(bool);
            ParsedArrayType = typeof(bool[]);
            break;
          case "double":
            retObj = ParseDouble(node, valType, parseStack, mappingAction);
            ParsedType = typeof(double);
            ParsedArrayType = typeof(double[]);
            break;
          case "dateTime.iso8601":
            retObj = ParseDateTime(node, valType, parseStack, mappingAction);
            ParsedType = typeof(DateTime);
            ParsedArrayType = typeof(DateTime[]);
            break;
          default:
            break;
        }
      }
      return retObj;
    }
  
    Object ParseArray(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction) 
      //!! add support for multi dim
    {
      // required type must be an array
      if (ValueType != null 
        && !(ValueType.IsArray == true 
            || ValueType == typeof(Array)
            || ValueType == typeof(object)))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains array value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      if (ValueType != null)
      {
        XmlRpcType xmlRpcType = XmlRpcServiceInfo.GetXmlRpcType(ValueType);
        if (xmlRpcType == XmlRpcType.tMultiDimArray)
        {
          Object ret = ParseMultiDimArray(node, ValueType);
          return ret;
        }
        parseStack.Push("array mapped to type " + ValueType.Name);
      }
      else
        parseStack.Push("array");
      XmlNode dataNode = node.SelectSingleNode("data");
      int nodeCount = dataNode.ChildNodes.Count; 
      Object[] elements = new Object[nodeCount];
      // determine type of array elements
      Type elemType = null;
      if (ValueType != null 
        && ValueType != typeof(Array)
        && ValueType != typeof(object))
      {
        string[] checkMultiDim = Regex.Split(ValueType.FullName, 
          "\\[\\]$");
        // determine assembly of array element type
        Assembly asmbly = ValueType.Assembly;
        string[] asmblyName = asmbly.FullName.Split(',');
        string elemTypeName = checkMultiDim[0] + ", " + asmblyName[0]; 
        
        elemType = Type.GetType(elemTypeName);
      }
      else 
      {
        elemType = typeof(object);
      }
      bool bGotType = false;
      Type useType = null;
      int i = 0;
      foreach (XmlNode vNode in dataNode.ChildNodes)
      {
        parseStack.Push(String.Format("element {0}", i));
        XmlNode vvNode = vNode.FirstChild;
        Type parsedType;
        Type parsedArrayType;
        elements[i++] = ParseValue(vvNode, elemType, parseStack, mappingAction,
                                    out parsedType, out parsedArrayType);
        if (bGotType == false)
        {
          useType = parsedArrayType;
          bGotType = true;
        }
        else
        {
          if (useType != parsedArrayType)
            useType = null;
        }
        parseStack.Pop();
      }
      Object[] args = new Object[1]; args[0] = nodeCount;
      Object retObj = null;
      if (ValueType != null 
        && ValueType != typeof(Array) 
        && ValueType != typeof(object))
      {
        retObj = Activator.CreateInstance(ValueType, args);
      }
      else
      {
        if (useType == null)
          retObj = Activator.CreateInstance(typeof(object[]), args);
        else
          retObj = Activator.CreateInstance(useType, args);          
      }
      for (int j=0; j < elements.Length; j++)
      {
        ((Array)retObj).SetValue(elements[j], j);
      }
      parseStack.Pop();
      return retObj;
    }

    Object ParseMultiDimArray(XmlNode node, Type ValueType)
    {
      // parse the type name to get element type and array rank
      string[] checkMultiDim = Regex.Split(ValueType.FullName, 
        "\\[,[,]*\\]$");
      Type elemType = Type.GetType(checkMultiDim[0]);
      string commas = ValueType.FullName.Substring(checkMultiDim[0].Length+1, 
        ValueType.FullName.Length-checkMultiDim[0].Length-2);
      int rank = commas.Length+1;
      // elements will be stored sequentially as nested arrays are parsed
      ArrayList elements = new ArrayList();
      // create array to store length of each dimension - initialize to 
      // all zeroes so that when parsing we can determine if an array for 
      // that dimension has been parsed already
      int[] dimLengths = new int[rank];
      dimLengths.Initialize(); 
      ParseMultiDimElements(node, rank, 0, elemType, elements, dimLengths);
      // build arguments to define array dimensions and create the array
      Object[] args = new Object[dimLengths.Length];
      for (int argi=0; argi<dimLengths.Length; argi++)
      {
        args[argi] = dimLengths[argi];
      }
      Array ret = (Array)Activator.CreateInstance(ValueType, args);
      // copy elements into new multi-dim array
      //!! make more efficient
      int length = ret.Length;
      for (int e=0; e<length; e++)
      {
        int[] indices = new int[dimLengths.Length];
        int div = 1;
        for (int f=(indices.Length-1); f>=0; f--)
        {
          indices[f] = (e/div)%dimLengths[f];
          div*=dimLengths[f];
        }
        ret.SetValue(elements[e], indices);
      }
      return ret;
    }

    void ParseMultiDimElements(XmlNode node, int Rank, int CurRank, 
      Type elemType, ArrayList elements, int[] dimLengths)
    {
      if (node.Name != "array")
      {
        throw new XmlRpcTypeMismatchException(
          "param element does not contain array element.");
      }
      XmlNode dataNode = node.SelectSingleNode("data");
      int nodeCount = dataNode.ChildNodes.Count;
      //!! check that multi dim array is not jagged
      if (dimLengths[CurRank] != 0 && nodeCount != dimLengths[CurRank])
      {
        throw new XmlRpcNonRegularArrayException(
          "Multi-dimensional array must not be jagged.");
      }
      dimLengths[CurRank] = nodeCount;  // in case first array at this rank
      if (CurRank < (Rank-1))
      {
        foreach (XmlNode vNode in dataNode.ChildNodes)
        {
          ParseMultiDimElements(vNode, Rank, CurRank+1, elemType, 
            elements, dimLengths);
        }
      }
      else
      {
        foreach (XmlNode vNode in dataNode.ChildNodes)
        {
          XmlNode vvNode = vNode.FirstChild;
          elements.Add(ParseValue(vvNode, elemType, null, MappingAction.Error));
        }
      }
    }

    Object ParseStruct(
      XmlNode node, 
      Type valueType,
      ParseStack parseStack,
      MappingAction mappingAction) 
    {
      if (valueType.IsValueType == false || valueType.IsPrimitive == true)
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains struct value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(valueType) 
          + " expected " + StackDump(parseStack));
      }
      // Note: mapping action on a struct is only applied locally - it 
      // does not override the global mapping action when members of the 
      // struct are parsed
      MappingAction localAction = mappingAction;
      if (valueType != null)
      {
        parseStack.Push("struct mapped to type " + valueType.Name);
        localAction = StructMappingAction(valueType, mappingAction);
      }
      else
      {
        parseStack.Push("struct");
      }
      // create map of field names and remove each name from it as 
      // processed so we can determine which fields are missing
      // TODO: replace HashTable with lighter collection
      Hashtable names = new Hashtable();
      foreach (FieldInfo fi in valueType.GetFields())
      {
        names.Add(fi.Name, fi.Name);
      }
      XmlNodeList members = node.ChildNodes;
      Object retObj = Activator.CreateInstance(valueType);
      int fieldCount = 0;
      foreach (XmlNode member in members)
      {
        XmlNode nameNode = member.SelectSingleNode("name");
        XmlNode valueNode = member.SelectSingleNode("value");
        string name = nameNode.FirstChild.Value;
        string structName = GetStructName(valueType, name);
        if (structName != null)
          name = structName;
        names.Remove(name);
        MemberInfo[] mis = valueType.GetMember(name);
        if (mis.Length == 0)
        {
          continue;   // allow unexpected members 
        }
        FieldInfo fi = (FieldInfo)mis[0];
        if (valueType == null)
          parseStack.Push(String.Format("member {0}", name));
        else
          parseStack.Push(String.Format("member {0} mapped to type {1}", 
            name,fi.FieldType.Name));
        Object valObj = ParseValue(valueNode.FirstChild, fi.FieldType, 
          parseStack, mappingAction);
        parseStack.Pop();
        fi.SetValue(retObj, valObj);
        fieldCount++;
      }
      if (localAction == MappingAction.Error && names.Count > 0)
        ReportMissingMembers(valueType, names, parseStack);
      parseStack.Pop();
      return retObj;
    }

    void ReportMissingMembers(
      Type valueType,
      Hashtable names,
      ParseStack parseStack)
    {
      StringBuilder sb = new StringBuilder();
      int errorCount = 0;
      string sep = "";
      foreach (string s in names.Keys)
      {
        MappingAction memberAction = MemberMappingAction(valueType, s, 
          MappingAction.Error);
        if (memberAction == MappingAction.Error)
        {
          sb.Append(sep);
          sb.Append(s);
          sep = " ";
          errorCount++;
        }
      }
      if (errorCount > 0)
      {
        string plural = "";
        if (errorCount > 1)
          plural = "s";
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains struct value with missing non-optional member" 
          + plural + ": " + sb.ToString() + " " + StackDump(parseStack));
      }
    }

    string GetStructName(Type ValueType, string XmlRpcName)
    {
      // given a member name in an XML-RPC struct, check to see whether
      // a field has been associated with this XML-RPC member name, return
      // the field name if it has else return null
      if (ValueType == null)
        return null;
      foreach (FieldInfo fi in ValueType.GetFields())
      {
        Attribute attr = Attribute.GetCustomAttribute(fi, 
          typeof(XmlRpcMemberAttribute));
        if (attr != null 
          && attr is XmlRpcMemberAttribute
          && ((XmlRpcMemberAttribute)attr).Member == XmlRpcName)
        {
          string ret = fi.Name;
          return ret;
        }
      }
      return null;
    }

    MappingAction StructMappingAction(
      Type type,
      MappingAction currentAction)
    {
      // if struct member has mapping action attribute, override the current
      // mapping action else just return the current action
      if (type == null)
        return currentAction;
      Attribute attr = Attribute.GetCustomAttribute(type, 
        typeof(XmlRpcMissingMappingAttribute));
      if (attr != null)
        return ((XmlRpcMissingMappingAttribute)attr).Action;
      return currentAction;
    }

    MappingAction MemberMappingAction(
      Type type,
      string memberName,
      MappingAction currentAction)
    {
      // if struct member has mapping action attribute, override the current
      // mapping action else just return the current action
      if (type == null)
        return currentAction;
      FieldInfo fi = type.GetField(memberName);
      Attribute attr = Attribute.GetCustomAttribute(fi, 
        typeof(XmlRpcMissingMappingAttribute));
      if (attr != null)
        return ((XmlRpcMissingMappingAttribute)attr).Action;
      return currentAction;
    }

    Object ParseHashtable(
      XmlNode node,
      Type valueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
      parseStack.Push("struct");
      // create map of field names and remove each name from it as 
      // processed so we can determine which fields are missing
      Hashtable names = new Hashtable();
      foreach (FieldInfo fi in valueType.GetFields())
      {
        names.Add(fi.Name, fi.Name);
      }
      XmlNodeList members = node.ChildNodes;
      Object retObj = Activator.CreateInstance(valueType);
      foreach (XmlNode member in members)
      {
        XmlNode nameNode = member.SelectSingleNode("name");
        XmlNode valueNode = member.SelectSingleNode("value");
        string rpcName = nameNode.FirstChild.Value;
        string mbrName = GetStructName(valueType, rpcName);
        if (mbrName == null)
          mbrName = rpcName;
        names.Remove(mbrName);
        MemberInfo[] mis = valueType.GetMember(mbrName);
        object valObj;
        if (mis.Length != 0)
        {
          FieldInfo fi = (FieldInfo)mis[0];
          parseStack.Push(String.Format("member {0} mapped to type {1}", 
            rpcName, fi.FieldType.Name));
          valObj = ParseValue(valueNode.FirstChild, fi.FieldType, 
            parseStack, mappingAction);
          parseStack.Pop();
          fi.SetValue(retObj, valObj);
        }
        else
        {
          parseStack.Push(String.Format("member {0}", rpcName));
          valObj = ParseValue(valueNode.FirstChild, null, parseStack, 
            mappingAction);
          parseStack.Pop();
        }
        (retObj as XmlRpcStruct).Add(rpcName, valObj);
      }
      if (mappingAction == MappingAction.Error && names.Count > 0)
        ReportMissingMembers(valueType, names, parseStack);
      parseStack.Pop();
      return retObj;
    }
    
    Object ParseInt(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
		// edit by Erik Frey on May 16, 2004
		// to allow casting direct to string
		// if server is not returning the expected type
      if (ValueType != null && ValueType != typeof(Object)
        && ValueType != typeof(System.Int32)
        && ValueType != typeof(XmlRpcInt) && ValueType != typeof(string))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType +
          " contains int value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      parseStack.Push("integer");
      XmlNode valueNode = node.FirstChild;
      if (valueNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(parseStack.ParseType 
          +" contains empty int value " + StackDump(parseStack));
      }
      int retVal = 0;
	  String strValue; // edit by Erik Frey on May 16
      try
      {
        strValue = valueNode.Value;
		if (ValueType == typeof(XmlRpcInt) || ValueType == typeof(int)) // edit by Erik Frey on August 16
          retVal = Int32.Parse(strValue);
      }
      catch(Exception)
      {
        throw new XmlRpcInvalidXmlRpcException(parseStack.ParseType 
          + " contains invalid int value " + StackDump(parseStack));
      }
      parseStack.Pop();
      if (ValueType == typeof(XmlRpcInt))
        return new XmlRpcInt(retVal);
	  else if (ValueType == typeof(string)) // edit by Erik Frey on May 16
		return strValue;
      else 
        return retVal;
    }

    Object ParseString(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
      if (ValueType != null && ValueType != typeof(System.String) && ValueType != typeof(Object))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains string value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      parseStack.Push("string");
      string ret;
      if (node.FirstChild == null) //!!??
        ret = "";
      else
        ret = node.FirstChild.Value;
      parseStack.Pop();
      if (m_decoding != null)
      	ret = m_decoding.GetString((new UTF8Encoding()).GetBytes(ret));
      return ret;
    }

    Object ParseBoolean(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
		// edit by Erik Frey on May 16, 2004
		// to allow casting direct to string
		// if server is not returning the expected type
      if (ValueType != null && ValueType != typeof(Object)
        && ValueType != typeof(System.Boolean)
        && ValueType != typeof(XmlRpcBoolean) && ValueType != typeof(string))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains boolean value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      parseStack.Push("boolean");
      string s = node.FirstChild.Value;
      bool retVal;
      if (s == "1")
      {
        retVal = true;
      }
      else if (s == "0")
      {
        retVal = false;
      }
      else
      {
        throw new XmlRpcInvalidXmlRpcException(
          "reponse contains invalid boolean value " 
          + StackDump(parseStack));
      }
      parseStack.Pop();
      if (ValueType == typeof(XmlRpcBoolean))
        return new XmlRpcBoolean(retVal);
	  else if (ValueType == typeof(string)) // edit by Erik Frey on May 16
	    return s;
      else
        return retVal;
    }

    Object ParseDouble(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
		// edit by Erik Frey on May 16, 2004
		// to allow casting direct to string
		// if server is not returning the expected type
      if (ValueType != null && ValueType != typeof(Object)
        && ValueType != typeof(System.Double)
        && ValueType != typeof(XmlRpcDouble) && ValueType != typeof(string))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains double value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
	  string s = node.FirstChild.Value; // edit by Erik Frey on May 16
      Double retVal;
      parseStack.Push("double");
      try
      {
        retVal = Double.Parse(s, // edit by Erik Frey on May 16
          CultureInfo.InvariantCulture.NumberFormat);
      }
      catch(Exception)
      {
        throw new XmlRpcInvalidXmlRpcException(parseStack.ParseType 
          + " contains invalid double value " + StackDump(parseStack));
      }
      parseStack.Pop();
      if (ValueType == typeof(XmlRpcDouble))
        return new XmlRpcDouble(retVal);
	  else if (ValueType == typeof(string)) // edit by Erik Frey on May 16
	    return s;
      else
        return retVal;
    }

    Object ParseDateTime(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
		// edit by Erik Frey on May 16, 2004
		// to allow casting direct to string
		// if server is not returning the expected type
      if (ValueType != null && ValueType != typeof(Object)
        && ValueType != typeof(System.DateTime)
        && ValueType != typeof(XmlRpcDateTime) && ValueType != typeof(string))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains dateTime.iso8601 value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      parseStack.Push("dateTime");
      string s = node.FirstChild.Value;
      DateTime retVal;
      try 
      {
        //!! there must be a better way of doing this
        DateTime dt = new DateTime(
          Int32.Parse(s.Substring(0,4)),
          Int32.Parse(s.Substring(4,2)),
          Int32.Parse(s.Substring(6,2)),
          Int32.Parse(s.Substring(9,2)),
          Int32.Parse(s.Substring(12,2)),
          Int32.Parse(s.Substring(15,2)));
        retVal = dt;
      }
      catch(Exception)
      {
        throw new XmlRpcInvalidXmlRpcException(parseStack.ParseType 
          + " contains invalid dateTime value " 
          + StackDump(parseStack));
      }
      parseStack.Pop();
      if (ValueType == typeof(XmlRpcDateTime))
        return new XmlRpcDateTime(retVal);
	  else if (ValueType == typeof(string)) // edit by Erik Frey on May 16
		return s;
      else
        return retVal;
    }

    Object ParseBase64(
      XmlNode node, 
      Type ValueType,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
      Encoding ec = (m_decoding == null ? new UTF8Encoding() : m_decoding);
		// edit by Erik Frey on May 16, 2004
		// to allow casting direct to string
		// if server is not returning the expected type
      if (ValueType != null && ValueType != typeof(byte[]) 
        && ValueType != typeof(Object) && ValueType != typeof(string))
      {
        throw new XmlRpcTypeMismatchException(parseStack.ParseType 
          + " contains base64 value where " 
          + XmlRpcServiceInfo.GetXmlRpcTypeString(ValueType) 
          + " expected " + StackDump(parseStack));
      }
      parseStack.Push("base64");
      string s = node.FirstChild.Value;
      byte[] ret;
      try
      {
        ret = Convert.FromBase64String(s);
      }
      catch(Exception)
      {
        throw new XmlRpcInvalidXmlRpcException(parseStack.ParseType 
          + " contains invalid base64 value " 
          + StackDump(parseStack));
      }
      parseStack.Pop();
	  if (ValueType == typeof(string)) // edit by Erik Frey on May 16
		return ec.GetString(ret);
	  else
		return ret;
    }

    XmlRpcFaultException ParseFault(
      XmlNode faultNode,
      ParseStack parseStack,
      MappingAction mappingAction)
    {
      XmlNode structNode = faultNode.SelectSingleNode("value/struct");
      if (structNode == null)
      {
        throw new XmlRpcInvalidXmlRpcException(
          "struct element missing from fault response.");
      }
      Fault fault;
      fault.faultCode = "0";
      fault.faultString = "";
      fault = (Fault)ParseValue(structNode, typeof(Fault), parseStack, 
        mappingAction);
      return new XmlRpcFaultException(fault.faultCode, fault.faultString);
    }

    struct FaultStruct
    {
      public string faultCode;
      public string faultString;
    }

    public void SerializeFaultResponse(
      Stream stm, 
      XmlRpcFaultException faultEx)
    {
      FaultStruct fs;
      fs.faultCode = faultEx.FaultCode;
      fs.faultString = faultEx.FaultString;

      XmlTextWriter xtw = new XmlTextWriter(stm, m_encoding);
      ConfigureXmlFormat(xtw);
      xtw.WriteStartDocument();
      xtw.WriteStartElement("", "methodResponse", "");
      xtw.WriteStartElement("", "fault", "");
      Serialize(xtw, fs, MappingAction.Error);
      xtw.WriteEndElement();
      xtw.WriteEndElement();
      xtw.Flush();
    }

    void ConfigureXmlFormat(
      XmlTextWriter xtw)
    {
      if (m_bUseIndentation)
      {
        xtw.Formatting = Formatting.Indented;
        xtw.Indentation = m_indentation;
      }
      else
      {
        xtw.Formatting = Formatting.None;
      }
    }

    string StackDump(ParseStack parseStack)
    {
      StringBuilder sb = new StringBuilder();
      while (parseStack.Count > 0)
      {
        sb.Insert(0, (string)parseStack.Pop());
        sb.Insert(0, " : ");
      }
      sb.Insert(0, parseStack.ParseType);
      sb.Insert(0, "[");
      sb.Append("]");
      return sb.ToString();
    }

    #if (DEBUG)
    public
    #endif
    class ParseStack : Stack
    {
      public ParseStack(string parseType)
      {
        m_parseType = parseType;
      }

      void Push(string str)
      {
        base.Push(str);
      }
 
      public string ParseType 
      {
        get { return m_parseType; }
      }
   
      public string m_parseType = "";
    }
  }

}
