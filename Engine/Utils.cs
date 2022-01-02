using System;
using System.Resources;
using System.Drawing;
using System.Reflection;
using CookComputing.XmlRpc;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// Factory for creating <see cref="ILJServer"/> classes.
	/// </summary>
	internal class LJServerFactory
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private LJServerFactory() {}

		internal static ILJServer Create(string url)
		{
			ILJServer iLJ = (ILJServer) XmlRpcProxyGen.Create(typeof(ILJServer));
			XmlRpcClientProtocol xpc = (CookComputing.XmlRpc.XmlRpcClientProtocol) iLJ;
			xpc.UserAgent = _useragent;
			Uri baseUri = new Uri(url), uri = new Uri(baseUri, _xmlrpcpath);
			
			xpc.Url =  uri.AbsoluteUri;
			xpc.ProtocolVersion = new Version(1, 0);
			xpc.KeepAlive = false;
			xpc.RequestEncoding = System.Text.Encoding.UTF8;
			xpc.XmlEncoding = System.Text.Encoding.UTF8;
			if (System.Environment.Version.Major == 1)
				xpc.Proxy = System.Net.WebProxy.GetDefaultProxy(); // need to test this in 1.1
			if (System.Environment.Version.Major == 1) // .NET 2.0 utf8 cleans strings, so we don't have to
				xpc.XmlDecoding = new UTF8Clean();
			return iLJ;
		}

		static private readonly string _xmlrpcpath = ConstReader.GetString("_xmlrpcpath");
		static private readonly string _useragent = ConstReader.GetString("_useragent");
	}

	/// <summary>
	/// Factory for creating <see cref="HttpWebRequest"/> classes.
	/// </summary>
	internal class HttpWebRequestFactory
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private HttpWebRequestFactory() {}

		internal static HttpWebRequest Create(string url, string ljsession)
		{
			Uri uri = new Uri(url);
			HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(uri.AbsoluteUri);
			wr.ProtocolVersion = new Version(1, 0);
			wr.KeepAlive = false;
			wr.UserAgent = _useragent;
			if (System.Environment.Version.Major == 1)
				wr.Proxy = System.Net.WebProxy.GetDefaultProxy(); // need to test this in 1.1
			
			if (ljsession != null && ljsession.Length > 0)
			{
				wr.CookieContainer = new CookieContainer();
				// Old-style session cookies, just in case.
				wr.CookieContainer.Add(new Cookie("ljsession", ljsession, "/", uri.Host));
				// New-style session cookies.
				//wr.CookieContainer.Add(new Cookie("ljmastersession", ljsession, "/", uri.Host));
				//wr.CookieContainer.Add(new Cookie("ljloggedin", ljsession.Substring(ljsession.IndexOf(":") + 1, ljsession.LastIndexOf(":") - ljsession.IndexOf(":") - 1), "/", uri.Host));
			}
			return wr;
		}
		static private readonly string _useragent = ConstReader.GetString("_useragent");
	}

	/// <summary>
	/// Reads internal global consts.
	/// </summary>
	internal class ConstReader
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private ConstReader() {}

		static ConstReader()
		{
			rm = new ResourceManager(_constsresource, Assembly.GetExecutingAssembly());
		}

		internal static object GetObject(string name)
		{
			return rm.GetObject(name);
		}

		internal static int GetInt(string name)
		{
			return (int) GetObject(name);
		}

		internal static string GetString(string name)
		{
			return (string) GetObject(name);
		}

		static private ResourceManager rm;
		private const string _constsresource = "EF.ljArchive.Engine.res.Consts";
	}

	internal class MD5Hasher
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private MD5Hasher() {}

		static MD5Hasher()
		{
			md5 = new MD5CryptoServiceProvider();
		}

		static public string Compute(string plainText)
		{
			byte[] plainTextBytes = Encoding.ASCII.GetBytes(plainText);
			byte[] hashBytes = md5.ComputeHash(plainTextBytes);
			StringBuilder sb = new StringBuilder();
			foreach (byte hashByte in hashBytes)
				sb.Append(Convert.ToString(hashByte, 16).PadLeft(2, '0'));
			return sb.ToString();
		}

		static private MD5CryptoServiceProvider md5;
	}
	
	internal class UTF8Clean : System.Text.UTF8Encoding
	{
		public UTF8Clean()
		{
			d = new Decoder();
		}
		
		public override System.Text.Decoder GetDecoder()
		{
			return d;
		}
		
		public override string GetString(byte[] bytes)
		{
			return new string(GetChars(bytes));
		}
		
		public override string GetString(byte[] bytes, int index, int count)
		{
			return new string(GetChars(bytes, index, count));
		}
		
		public override char[] GetChars(byte[] bytes)
		{
			return GetChars(bytes, 0, bytes.Length);
		}
		
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return d.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}
		
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] chars = new char[d.GetCharCount(bytes, index, count)];
			d.GetChars(bytes, index, count, chars, 0);
			return chars;
		}

		public override int GetCharCount(byte[] bytes)
		{
			return d.GetCharCount(bytes, 0, bytes.Length);
		}
		
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return d.GetCharCount(bytes, index, count);
		}
		
		private class Decoder : System.Text.Decoder
		{
			public Decoder()
			{
				decoder = System.Text.Encoding.UTF8.GetDecoder();
			}
			
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int ret = decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
				for (int i=0; i < chars.Length; ++i)
					if (!IsValidUTF8(chars[i]))
						chars[i] = (char) 0xFFFD; // replacement char
				return ret;
			}
			
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return decoder.GetCharCount(bytes, index, count);
			}
			
			private bool IsValidUTF8(char c)
			{
				return (c >= 0x000020 && c <= 0x00d7ff)
			    		|| (c >= 0x00e000 && c <= 0x00fffd)
			    		|| (c == 0x09)
			    		|| (c == 0x0A)
			    		|| (c == 0x0D);
			}
			
			private System.Text.Decoder decoder;
		}
		
		private Decoder d;
	}

	/// <summary>
	/// Miscellaneous functions for talking to an LJ server.
	/// </summary>
	public class Server
	{
		/// <summary>
		/// Get journals a user has community access to.
		/// </summary>
		/// <param name="username">The user's username.</param>
		/// <param name="password">The user's password.</param>
		/// <param name="serverURL">The server's URL.</param>
		/// <returns></returns>
		static public string[] GetUseJournals(string username, string password, string serverURL)
		{
			ILJServer iLJ;
			XMLStructs.GetChallengeResponse gcr;
			string auth_response;
			XMLStructs.LoginParams lp;
			XMLStructs.LoginResponse lr;

			try
			{
				iLJ = LJServerFactory.Create(serverURL);
				gcr = iLJ.GetChallenge();
				auth_response = MD5Hasher.Compute(gcr.challenge + MD5Hasher.Compute(password));
				lp = new XMLStructs.LoginParams(username, "challenge", gcr.challenge, auth_response, 1, clientVersion,
				0, 0, 1, 1);
				lr = iLJ.Login(lp);
			}
			catch (CookComputing.XmlRpc.XmlRpcFaultException xfe)
			{
				throw new ExpectedSyncException(ExpectedError.InvalidPassword, xfe);
			}
			catch (System.Net.WebException we)
			{
				throw new ExpectedSyncException(ExpectedError.ServerNotResponding, we);
			}
			catch (CookComputing.XmlRpc.XmlRpcServerException xse)
			{
				throw new ExpectedSyncException(ExpectedError.ServerNotResponding, xse);
			}
			return lr.usejournals;
		}
		
		static public string GetDefaultPicURL(string journalName, string serverURL, bool community)
		{
			Uri uri = new Uri(new Uri(serverURL), string.Format(_foafpath,
			                                                       (community ? "community" : "users"),
			                                                       journalName));
			HttpWebRequest w = HttpWebRequestFactory.Create(uri.AbsoluteUri, null);
			string picURL = null;
			using (System.IO.Stream s = w.GetResponse().GetResponseStream())
			{
				System.Xml.XmlTextReader xr = new System.Xml.XmlTextReader(s);
				while (xr.Read())
				{
					if (xr.NodeType == System.Xml.XmlNodeType.Element && xr.Name == "foaf:img")
					{
						picURL = xr.GetAttribute("rdf:resource");
						break;
					}
				}
				xr.Close();
			}
			return picURL;
		}
		
		static private readonly string clientVersion = Environment.OSVersion.Platform.ToString() + "-.NET/" +
			System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
		static private readonly string _foafpath = ConstReader.GetString("_foafpath");
	}
}
