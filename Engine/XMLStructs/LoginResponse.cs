using System;
using CookComputing.XmlRpc;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The response from calling <see cref="ILJServer.Login"/>.
	/// </summary>
	public struct LoginResponse
	{
		public LoginResponse(string fullname, string message, FriendGroup[] friendgroups, string[] usejournals,
			Mood[] moods, string[] pickws, string[] pickwurls, string defaultpicurl, int fastserver, int userid,
			Menu[] menus)
		{
			this.fullname = fullname;
			this.message = message;
			this.friendgroups = friendgroups;
			this.usejournals = usejournals;
			this.moods = moods;
			this.pickws = pickws;
			this.pickwurls = pickwurls;
			this.defaultpicurl = defaultpicurl;
			this.fastserver = fastserver;
			this.userid = userid;
			this.menus = menus;
		}
		public string fullname;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string message;
		public FriendGroup[] friendgroups;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string[] usejournals;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public Mood[] moods;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string[] pickws;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string[] pickwurls;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string defaultpicurl;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int fastserver;
		public int userid;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public Menu[] menus;
	}
}
