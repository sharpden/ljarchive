using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML UserMap.
	/// </summary>
	public struct UserMap
	{
		public UserMap(int id, string user)
		{
			this.id = id;
			this.user = user;
		}
		public int id;
		public string user;
	}
}
