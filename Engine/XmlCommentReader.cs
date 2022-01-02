using System;
using System.Xml;
using EF.ljArchive.Engine.XMLStructs;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// Reads comments from an XML stream in sequence.
	/// </summary>
	internal class XmlCommentReader : XmlTextReader
	{
		/// <summary>
		/// Creates a new instance of <see cref="XmlCommentReader"/>.
		/// </summary>
		/// <param name="s">The stream containing the XML data to read.</param>
		public XmlCommentReader(System.IO.Stream s) : base(s) {}
		
		/// <summary>
		/// Creates a new instance of <see cref="XmlCommentReader"/>.
		/// </summary>
		/// <param name="tr">The TextReader containing the XML data to read.</param>
		public XmlCommentReader(System.IO.TextReader tr) : base(tr) {}
		
		/// <summary>
		/// Reads a comment.
		/// </summary>
		/// <returns>True if the stream is still open.  Otherwise, false.</returns>
		public override bool Read()
		{
			while (base.Read())
			{
				if (base.NodeType == XmlNodeType.Element && base.Name == "comment")
				{
					string id = base.GetAttribute("id");
					string posterid = base.GetAttribute("posterid");
					string state = base.GetAttribute("state");
					string jitemid = base.GetAttribute("jitemid");
					string parentid = base.GetAttribute("parentid");
					string subject = string.Empty;
					string body = string.Empty;
					string date = XmlConvert.ToString(DateTime.MinValue);
					if (state == null) state = "A";
					if (posterid == null) posterid = "0";
					if (parentid == null) parentid = "0";
					if (!base.IsEmptyElement)
					{
						while (base.Read() && (base.NodeType != XmlNodeType.EndElement || base.Name != "comment"))
						{
							if (base.NodeType == XmlNodeType.Element && base.Name == "subject")
							{
								base.Read();
								subject = base.Value;
							}
							else if (base.NodeType == XmlNodeType.Element && base.Name == "body")
							{
								base.Read();
								body = base.Value;
							}
							else if (base.NodeType == XmlNodeType.Element && base.Name == "date")
							{
								base.Read();
								date = base.Value;
							}
						}
					}
					if (id != null && jitemid != null)
					{
						comment = new Comment(XmlConvert.ToInt32(id), XmlConvert.ToInt32(posterid),
							state, XmlConvert.ToInt32(jitemid), XmlConvert.ToInt32(parentid), body,
							subject, XmlConvert.ToDateTime(date));
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// The current comment node.
		/// </summary>
		public Comment Comment
		{
			get
			{
				return comment;
			}
		}

		/// <summary>
		/// Private store for the comment property.
		/// </summary>
		private Comment comment;
	}
}
