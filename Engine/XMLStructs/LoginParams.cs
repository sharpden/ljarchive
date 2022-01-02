using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The parameters for calling <see cref="ILJServer.Login"/>.
	/// </summary>
	public struct LoginParams
	{
		public LoginParams(string username, string auth_method, string auth_challenge, string auth_response, int ver,
			string clientversion, int getmoods, int getmenus, int getpickws, int getpickwurls)
		{
			this.username = username;
			this.auth_method = auth_method;
			this.auth_challenge = auth_challenge;
			this.auth_response = auth_response;
			this.ver = ver;
			this.clientversion = clientversion;
			this.getmoods = getmoods;
			this.getmenus = getmenus;
			this.getpickws = getpickws;
			this.getpickwurls = getpickwurls;
		}
		public string username;
		public string auth_method;
		public string auth_challenge;
		public string auth_response;
		public int ver;
		public string clientversion;
		public int getmoods;
		public int getmenus;
		public int getpickws;
		public int getpickwurls;
	}
}
