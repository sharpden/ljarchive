using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EF.ljArchive.Engine.Collections
{
	/// <summary>
	/// Strongly typed collection of <see cref="Common.IPlugin"/> objects.
	/// </summary>
	public class PluginCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PluginCollection"/> class.
		/// </summary>
		/// <param name="path">The path to browse and load <see cref="Common.IPlugin"/> objects.</param>
		public PluginCollection(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);

			if (!di.Exists)
				return;

			foreach (FileInfo fi in di.GetFiles())
			{
				try
				{
					System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(fi.FullName);
					if (asm != null)
					{
						foreach (Type t in asm.GetTypes())
						{
							if (t.GetInterface("IPlugin") != null)
							{
								try
								{
									List.Add(Activator.CreateInstance(t));
								}
								catch (System.Reflection.TargetInvocationException) {}
							}
						}
					}
				}
				catch (System.BadImageFormatException) {}
				catch (System.IO.FileNotFoundException) {}
				catch (System.Reflection.ReflectionTypeLoadException) {}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Common.IPlugin"/> at the specified index.
		/// </summary>
		public Common.IPlugin this[int index]
		{
			get
			{
				return (Common.IPlugin) List[index];
			}
			set
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Inserts a <see cref="Common.IPlugin"/> into the <see cref="PluginCollection"/> at the
		/// specified position.
		/// </summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The <see cref="Common.IPlugin"/> to insert into the
		/// <see cref="PluginCollection"/>.</param>
		public void Insert( int index, Common.IPlugin value )
		{
			List.Insert( index, value );
		}

		/// <summary>
		/// Gets and sets a string that represents the serialized settings of all the
		/// <see cref="Common.IPlugin"/> objects it contains.
		/// </summary>
		public string Settings
		{
			get
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
				string s;
				using (MemoryStream ms = new MemoryStream())
				{
					for (int i = 0; i < List.Count; ++i)
					{
						object settings = ((Common.IPlugin) List[i]).Settings;
						if (settings != null && settings.GetType().IsSerializable)
							bf.Serialize(ms, settings);
					}
					s = Convert.ToBase64String(ms.ToArray());
				}
				return s;
			}
			set
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
				byte[] b = Convert.FromBase64String(value);
				System.AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
				using (MemoryStream ms = new MemoryStream(b))
				{
					while (ms.Position < ms.Length)
					{
						object settings;
						try { settings = bf.Deserialize(ms); }
						catch (SerializationException) {continue;}
						for (int i = 0; i < List.Count; ++i)
						{
							Common.IPlugin ip = (Common.IPlugin) List[i];
							if (ip.Settings != null && ip.Settings.GetType() == settings.GetType())
								ip.Settings = settings;
						}
					}
				}
				System.AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			}
		}

		/// <summary>
		/// Sets the selected event ID to each <see cref="Common.IPlugin"/> in the collection.
		/// </summary>
		public int SelectedEventID
		{
			set
			{
				foreach (Common.IPlugin plugin in List)
					plugin.SelectedEventID = value;
			}
		}

		// unbelievable!!  this is so poorly documented.
		// finally got an explanation of resolving types for serialization from external assemblies here:
		// http://weblogs.asp.net/grobinson/archive/2003/03/06/3521.aspx
		// also references a document here:
		// http://msdn.microsoft.com/msdnmag/issues/02/04/net/default.aspx
		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			foreach (System.Reflection.Assembly a in AppDomain.CurrentDomain.GetAssemblies())
				if (a.FullName.IndexOf(args.Name) > -1)
					return a;
			return null;
		}
	}
}
