using System;
using System.Collections;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine.Collections
{
	/// <summary>
	/// Strongly typed collection of <see cref="UserMap"/> structs.
	/// </summary>
	internal class UserMapCollection : CollectionBase
	{
		#region Public Instance Properties
		public UserMap this[ int index ]
		{
			get
			{
				return( (UserMap) List[index] );
			}
			set
			{
				List[index] = value;
			}
		}
		#endregion
		
		#region Public Instance Methods
		public int Add( UserMap value )
		{
			return( List.Add( value ) );
		}
	
		public int IndexOf(UserMap   value )
		{
			return( List.IndexOf( value ) );
		}
	
		public void Insert( int index, UserMap value )
		{
			List.Insert( index, value );
		}
	
		public void Remove( UserMap value )
		{
			List.Remove( value );
		}
	
		public bool Contains( UserMap value )
		{
			// If value is not of type UserMap, this will return false.
			return( List.Contains( value ) );
		}
		#endregion
		
		#region Protected Instance Methods
		protected override void OnInsert( int index, Object value )
		{
			if ( value.GetType() != typeof(UserMap) )
				throw new ArgumentException( "value must be of type UserMap.", "value" );
		}

		protected override void OnRemove( int index, Object value )
		{
			if ( value.GetType() != typeof(UserMap) )
				throw new ArgumentException( "value must be of type UserMap.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )
		{
			if ( newValue.GetType() != typeof(UserMap) )
				throw new ArgumentException( "newValue must be of type UserMap.", "newValue" );
		}

		protected override void OnValidate( Object value )
		{
			if ( value.GetType() != typeof(UserMap) )
				throw new ArgumentException( "value must be of type UserMap." );
		}
		#endregion
	}
}
