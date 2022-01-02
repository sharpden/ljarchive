using System;
using CookComputing.XmlRpc;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML Menu.
	/// </summary>
	public class Menu
	{
		public Menu(string text, string url, Menu sub)
		{
			this.text = text;
			this.url = url;
			this.sub = sub;
		}
		public string text;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string url;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public Menu sub;
	}
}
