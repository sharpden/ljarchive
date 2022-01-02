using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The parameters for calling <see cref="ILJServer.GetEvents"/>.
	/// </summary>
	public struct GetEventsParams
	{
		public GetEventsParams(string username, string auth_method, string auth_challenge, string auth_response,
			int ver, int truncate, int prefersubject, int noprops, string selecttype, string lastsync, int year,
			int month, int day, int howmany, string beforedate, int itemid, string lineendings, string usejournal)
		{
			this.username = username;
			this.auth_method = auth_method;
			this.auth_challenge = auth_challenge;
			this.auth_response = auth_response;
			this.ver = ver;
			this.truncate = truncate;
			this.prefersubject = prefersubject;
			this.noprops = noprops;
			this.selecttype = selecttype;
			this.lastsync = lastsync;
			this.year = year;
			this.month = month;
			this.day = day;
			this.howmany = howmany;
			this.beforedate = beforedate;
			this.itemid = itemid;
			this.lineendings = lineendings;
			this.usejournal = usejournal;
		}
		public string username;
		public string auth_method;
		public string auth_challenge;
		public string auth_response;
		public int ver;
		public int truncate;
		public int prefersubject;
		public int noprops;
		public string selecttype;
		public string lastsync;
		public int year;
		public int month;
		public int day;
		public int howmany;
		public string beforedate;
		public int itemid;
		public string lineendings;
		public string usejournal;
	}
}
