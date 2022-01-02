using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The parameters for calling <see cref="ILJServer.SyncItems"/>.
	/// </summary>
	public struct SyncItemsParams
	{
		public SyncItemsParams(string username, string auth_method, string auth_challenge, string auth_response,
			int ver, string lastsync, string usejournal)
		{
			this.username = username;
			this.auth_method = auth_method;
			this.auth_challenge = auth_challenge;
			this.auth_response = auth_response;
			this.ver = ver;
			this.lastsync = lastsync;
			this.usejournal = usejournal;
		}
		public string username;
		public string auth_method;
		public string auth_challenge;
		public string auth_response;
		public int ver;
		public string lastsync;
		public string usejournal;
	}
}
