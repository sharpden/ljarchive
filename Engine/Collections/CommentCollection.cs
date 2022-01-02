using System;
using System.Collections;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine.Collections
{
	/// <summary>
	/// Strongly typed collection of <see cref="Comment"/> structs.
	/// </summary>
	internal class CommentCollection : CollectionBase
	{
		#region Public Instance Properties
		public Comment this[ int index ]
		{
			get
			{
				return( (Comment) List[index] );
			}
			set
			{
				List[index] = value;
			}
		}
		#endregion
		
		#region Public Instance Methods
		public int Add( Comment value )
		{
			return( List.Add( value ) );
		}

		public int IndexOf(Comment   value )
		{
			return( List.IndexOf( value ) );
		}
	
		public void Insert( int index, Comment value )
		{
			List.Insert( index, value );
		}
	
		public void Remove( Comment value )
		{
			List.Remove( value );
		}

		public bool Contains( Comment value )
		{
			// If value is not of type Comment, this will return false.
			return( List.Contains( value ) );
		}

		public int GetMaxID()
		{
			int ret = -1;
			foreach (Comment c in List)
				if (c.id > ret)
					ret = c.id;
			return ret;
		}
		#endregion
		
		#region Protected Instance Methods
		protected override void OnInsert( int index, Object value )
		{
			if ( value.GetType() != typeof(Comment) )
				throw new ArgumentException( "value must be of type Comment.", "value" );
		}

		protected override void OnRemove( int index, Object value )
		{
			if ( value.GetType() != typeof(Comment) )
				throw new ArgumentException( "value must be of type Comment.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )
		{
			if ( newValue.GetType() != typeof(Comment) )
				throw new ArgumentException( "newValue must be of type Comment.", "newValue" );
		}

		protected override void OnValidate( Object value )
		{
			if ( value.GetType() != typeof(Comment) )
				throw new ArgumentException( "value must be of type Comment." );
		}
		#endregion
	}
}
