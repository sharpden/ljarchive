using System;
using CookComputing.XmlRpc;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML FriendGroup.
	/// </summary>
	public struct FriendGroup
	{
		public FriendGroup(int id, string name, int sortorder, int isPublic)
		{
			this.id = id;
			this.name = name;
			this.sortorder = sortorder;
			this.isPublic = isPublic;
		}
		public int id;
		public string name;
		public int sortorder;
		[XmlRpcMember("public")]
		public int isPublic;
	}
}
