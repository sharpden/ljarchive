using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// The response from calling <see cref="ILJServer.GetEvents"/>.
	/// </summary>
	public struct GetEventsResponse
	{
		public GetEventsResponse(Event[] events)
		{
			this.events = events;
		}
		public Event[] events;
	}
}
