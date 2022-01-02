using System;
using CookComputing.XmlRpc;

namespace EF.ljArchive.Engine.XMLStructs
{
	/// <summary>
	/// An XML EventProps.
	/// </summary>
	/// <remarks>Metadata for an <see cref="Event"/>.</remarks>
	public struct EventProps
	{
		public EventProps(string current_mood, int current_moodid, string current_music, int opt_preformatted,
			int opt_nocomments, string picture_keyword, int opt_backdated, int opt_noemail, int unknown8bit,
			int hasscreened, int revnum, int commentalter, string syn_link, int revtime, string syn_id, string taglist)
		{
			this.current_mood = current_mood;
			this.current_moodid = current_moodid;
			this.current_music = current_music;
			this.opt_preformatted = opt_preformatted;
			this.opt_nocomments = opt_nocomments;
			this.picture_keyword = picture_keyword;
			this.opt_backdated = opt_backdated;
			this.opt_noemail = opt_noemail;
			this.unknown8bit = unknown8bit;
			this.hasscreened = hasscreened;
			this.revnum = revnum;
			this.commentalter = commentalter;
			this.syn_link = syn_link;
			this.revtime = revtime;
			this.syn_id = syn_id;
			this.taglist = taglist;
		}
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string current_mood;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int current_moodid;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string current_music;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int opt_preformatted;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int opt_nocomments;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string picture_keyword;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int opt_backdated;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int opt_noemail;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int unknown8bit;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int hasscreened;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int revnum;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int commentalter;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string syn_link;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public int revtime;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string syn_id;
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public string taglist;
	}
}
