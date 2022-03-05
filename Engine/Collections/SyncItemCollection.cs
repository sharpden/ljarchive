using System;
using System.Collections;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine.Collections
{
	/// <summary>
	/// Strongly typed collection of <see cref="SyncItem"/> structs.
	/// </summary>
	internal class SyncItemCollection : CollectionBase
	{
		#region Public Instance Properties
		public SyncItem this[ int index ]
		{
			get
			{
				return( (SyncItem) List[index] );
			}
			set
			{
				List[index] = value;
			}
		}
		#endregion

		#region Public Instance Methods
		public void AddRangeLog( SyncItem[] values )
		{
			foreach (SyncItem value in values)
				if (value.item.StartsWith(_syncitemtypelogprefix))
					List.Add(value);
		}

		public void AddRange( SyncItem[] values )
		{
			foreach (SyncItem value in values)
				List.Add(value);
		}

		public int Add( SyncItem value )
		{
			return( List.Add( value ) );
		}

		public int IndexOf(SyncItem   value )
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, SyncItem value )
		{
			List.Insert( index, value );
		}

		public void Remove( SyncItem value )
		{
			List.Remove( value );
		}

		public bool Contains( SyncItem value )
		{
			// If value is not of type SyncItem, this will return false.
			return( List.Contains( value ) );
		}

		public DateTime GetMostRecentTime()
		{
			DateTime ret = DateTime.MinValue;
			foreach (SyncItem s in List)
			{
				DateTime dt = DateTime.Parse(s.time);
				if (dt > ret)
					ret = dt;
			}
			return ret;
		}

		public SyncItem GetOldest()
		{
			SyncItem ret = new SyncItem();
			DateTime dtMin = DateTime.MaxValue;
			foreach (SyncItem s in List)
			{
				DateTime dt = DateTime.Parse(s.time);
				if (dt < dtMin)
				{
					dtMin = dt;
					ret = s;
				}
			}
			return ret;
		}

		public void RemoveDownloaded(Event[] events)
		{
			ArrayList al = new ArrayList();
			foreach (Event e in events)
            {
				foreach (SyncItem s in List)
				{
					int sitemid = int.Parse(s.item.Substring(_syncitemtypelogprefix.Length));
					if (s.item.StartsWith(_syncitemtypelogprefix) && sitemid == e.itemid)
					{
						al.Add(s);
					}
				}
			}
			foreach (SyncItem s in al)
			{
				List.Remove(s);
			}
		}
		#endregion

		#region Protected Instance Methods
		protected override void OnInsert( int index, Object value )
		{
			if ( value.GetType() != typeof(EF.ljArchive.Engine.XMLStructs.SyncItem) )
				throw new ArgumentException( "value must be of type EF.ljArchive.Engine.XMLStructs.SyncItem.", "value" );
		}

		protected override void OnRemove( int index, Object value )
		{
			if ( value.GetType() != typeof(EF.ljArchive.Engine.XMLStructs.SyncItem) )
				throw new ArgumentException( "value must be of type EF.ljArchive.Engine.XMLStructs.SyncItem.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )
		{
			if ( newValue.GetType() != typeof(EF.ljArchive.Engine.XMLStructs.SyncItem) )
				throw new ArgumentException( "newValue must be of type EF.ljArchive.Engine.XMLStructs.SyncItem.", "newValue" );
		}

		protected override void OnValidate( Object value )
		{
			if ( value.GetType() != typeof(EF.ljArchive.Engine.XMLStructs.SyncItem) )
				throw new ArgumentException( "value must be of type EF.ljArchive.Engine.XMLStructs.SyncItem." );
		}
		#endregion

		#region Private Static Fields
		static private readonly string _syncitemtypelogprefix = ConstReader.GetString("_syncitemtypelogprefix");
		#endregion
	}
}
