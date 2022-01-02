using System;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML Mood.
	/// </summary>
	public struct Mood
	{
		public Mood(int id, string name, int parent)
		{
			this.id = id;
			this.name = name;
			this.parent = parent;
		}
		public int id;
		public string name;
		public int parent;
	}
}
