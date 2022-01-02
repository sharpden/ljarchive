using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The parameters for calling <see cref="ILJServer.SessionGenerate"/>.
	/// </summary>
	public struct SessionGenerateParams
	{
		public SessionGenerateParams(string username, string auth_method, string auth_challenge, string auth_response,
			int ver, string expiration, int ipfixed)
		{
			this.username = username;
			this.auth_method = auth_method;
			this.auth_challenge = auth_challenge;
			this.auth_response = auth_response;
			this.ver = ver;
			this.expiration = expiration;
			this.ipfixed = ipfixed;
		}
		public string username;
		public string auth_method;
		public string auth_challenge;
		public string auth_response;
		public int ver;
		public string expiration;
		public int ipfixed;
	}
}
