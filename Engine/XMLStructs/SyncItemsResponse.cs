using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The response from calling <see cref="ILJServer.SyncItems"/>.
	/// </summary>
	public struct SyncItemsResponse
	{
		public SyncItemsResponse(SyncItem[] syncitems, int count, int total)
		{
			this.syncitems = syncitems;
			this.count = count;
			this.total = total;
		}
		public SyncItem[] syncitems;
		public int count;
		public int total;
	}
}
