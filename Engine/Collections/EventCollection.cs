using System;
using System.Collections;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine.Collections
{
	/// <summary>
	/// Strongly typed collection of <see cref="Event"/> structs.
	/// </summary>
	internal class EventCollection : CollectionBase
	{
		#region Public Instance Properties
		public Event this[ int index ]
		{
			get
			{
				return( (Event) List[index] );
			}
			set
			{
				List[index] = value;
			}
		}
		#endregion
		
		#region Public Instance Methods
		public void AddRange( Event[] values )
		{
			foreach (Event value in values)
				this.Add( value );
		}

		public int Add( Event value )
		{
			int i = IndexOfID(value.itemid);
			if (i != -1)
				List.RemoveAt(i);
			return( List.Add( value ) );
		}
	
		public int IndexOf(Event   value )
		{
			return( List.IndexOf( value ) );
		}
	
		public void Insert( int index, Event value )
		{
			List.Insert( index, value );
		}
	
		public void Remove( Event value )
		{
			List.Remove( value );
		}
	
		public bool Contains( Event value )
		{
			// If value is not of type Event, this will return false.
			return( List.Contains( value ) );
		}

		public int IndexOfID (int ID)
		{
			int i = -1;
			foreach (Event e in this)
				if (e.itemid == ID)
				{
					i = List.IndexOf(e);
					break;
				}
			return i;
		}

		public DateTime GetMostRecentTime()
		{
			DateTime ret = DateTime.MinValue;
			foreach (Event e in this)
			{
				DateTime dt = DateTime.Parse(e.eventtime);
				if (dt > ret)
					ret = dt;
			}
			return ret;
		}
		#endregion
		
		#region Protected Instance Methods
		protected override void OnInsert( int index, Object value )
		{
			if ( value.GetType() != typeof(Event) )
				throw new ArgumentException( "value must be of type Event.", "value" );
		}

		protected override void OnRemove( int index, Object value )
		{
			if ( value.GetType() != typeof(Event) )
				throw new ArgumentException( "value must be of type Event.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )
		{
			if ( newValue.GetType() != typeof(Event) )
				throw new ArgumentException( "newValue must be of type Event.", "newValue" );
		}

		protected override void OnValidate( Object value )
		{
			if ( value.GetType() != typeof(Event) )
				throw new ArgumentException( "value must be of type Event." );
		}
		#endregion
	}
}
