using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The response from calling <see cref="ILJServer.GetChallenge"/>.
	/// </summary>
	public struct GetChallengeResponse
	{
		public GetChallengeResponse(string auth_scheme, string challenge, int expire_time, int server_time)
		{
			this.auth_scheme = auth_scheme;
			this.challenge = challenge;
			this.expire_time = expire_time;
			this.server_time = server_time;
		}
		public string auth_scheme;
		public string challenge;
		public int expire_time;
		public int server_time;
	}
}
