using System;
using System.Collections;
using System.IO;

namespace EF.ljArchive.WindowsForms
{
	public class TemplateCollection : CollectionBase
	{
		public TemplateCollection(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			names = new ArrayList();

			if (!di.Exists)
				throw new ArgumentException("path not found.", "path");

			foreach (FileInfo fi in di.GetFiles("*.ljt"))
			{
				using (StreamReader sr = new StreamReader(fi.FullName))
				{
					List.Add(sr.ReadToEnd());
					names.Add(Path.GetFileNameWithoutExtension(fi.FullName));
				}
			}
		}

		#region Public Instance Properties
		public string this[ int index ]
		{
			get
			{
				return( (string) List[index] );
			}
			set
			{
				List[index] = value;
			}
		}

		public string[] Names
		{
			get
			{
				return (string[]) names.ToArray(typeof(string));
			}
		}
		#endregion
		
		#region Public Instance Methods
		public int Add( string value )
		{
			return( List.Add( value ) );
		}
	
		public int IndexOf(string   value )
		{
			return( List.IndexOf( value ) );
		}
	
		public void Insert( int index, string value )
		{
			List.Insert( index, value );
		}
	
		public void Remove( string value )
		{
			List.Remove( value );
		}
	
		public bool Contains( string value )
		{
			// If value is not of type string, this will return false.
			return( List.Contains( value ) );
		}
		#endregion
		
		#region Protected Instance Methods
		protected override void OnInsert( int index, Object value )
		{
			if ( value.GetType() != typeof(string) )
				throw new ArgumentException( "value must be of type string.", "value" );
		}
		protected override void OnRemove( int index, Object value )
		{
			if ( value.GetType() != typeof(string) )
				throw new ArgumentException( "value must be of type string.", "value" );
		}
		protected override void OnSet( int index, Object oldValue, Object newValue )
		{
			if ( newValue.GetType() != typeof(string) )
				throw new ArgumentException( "newValue must be of type string.", "newValue" );
		}
		protected override void OnValidate( Object value )
		{
			if ( value.GetType() != typeof(string) )
				throw new ArgumentException( "value must be of type string." );
		}
		#endregion

		private ArrayList names;
	}
}
