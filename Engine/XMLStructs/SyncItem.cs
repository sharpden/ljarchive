using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML SyncItem.
	/// </summary>
	public struct SyncItem
	{
		public SyncItem(string item, string action, string time)
		{
			this.item = item;
			this.action = action;
			this.time = time;
		}
		public string item;
		public string action;
		public string time;
	}
}
