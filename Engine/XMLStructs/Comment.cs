using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML Comment.
	/// </summary>
	public struct Comment
	{
		public Comment(int id, int posterid, string state, int jitemid, int parentid, string body, string subject,
			DateTime date)
		{
			this.id = id;
			this.posterid = posterid;
			this.state = state;
			this.jitemid = jitemid;
			this.parentid = parentid;
			this.body = body;
			this.subject = subject;
			this.date = date;
		}
		public int id;
		public int posterid;
		public string state;
		public int jitemid;
		public int parentid;
		public string body;
		public string subject;
		public DateTime date;
	}
}
