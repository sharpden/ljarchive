using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.Core
{
	/// <summary>
	/// Creates a Hashtable of words and their word counts in the journal.
	/// </summary>
	public class WordCount
	{
		/// <summary>
		/// this is a static class.
		/// </summary>
		private WordCount() {}

		/// <summary>
		/// Static constructor for <see cref="WordCount"/>.
		/// </summary>
		static WordCount()
		{
			using (StreamReader sr = new StreamReader(
					   Assembly.GetExecutingAssembly().GetManifestResourceStream("EF.ljArchive.Plugins.Core.CommonWords.txt")))
				commonWordList = sr.ReadToEnd().Split(' ');
		}

		/// <summary>
		/// Returns a Hashtable of words and their word counts in the journal.
		/// </summary>
		/// <param name="j">The <see cref="Journal"/> to analyze word counts.</param>
		/// <param name="countComments">If true, counts comments, also.</param>
		/// <param name="ignoreCommonWords">If true, ignores common english words.</param>
		/// <returns>a Hashtable of words and their word counts in the journal.</returns>
		static public Hashtable GetWordCount(Journal j, bool countComments, bool ignoreCommonWords)
		{
			Hashtable ht = new Hashtable();
			Regex r = new Regex(@"[\w']+");
			Regex rTag = new Regex("<.+?>");
			StringBuilder sb = new StringBuilder();
			Match m;
			foreach (Journal.EventsRow er in j.Events)
			{
				sb.Length = 0;
				if (!er.IsBodyNull())
					sb.Append(rTag.Replace(er.Body, string.Empty));
				sb.Append(' ');
				if (!er.IsSubjectNull())
					sb.Append(rTag.Replace(er.Subject, string.Empty));
				if (countComments)
				{
					foreach (Journal.CommentsRow cr in j.Comments.Select("JItemID = " + er.ID.ToString()))
					{
						if (!cr.IsBodyNull())
							sb.Append(rTag.Replace(cr.Body, string.Empty));
						if (!cr.IsSubjectNull())
							sb.Append(rTag.Replace(cr.Subject, string.Empty));
					}
				}
				m = r.Match(sb.ToString());
				while (m.Success)
				{
					string s = m.Value.ToLower();
					if (!ignoreCommonWords || !((IList) commonWordList).Contains(s))
					{
						if (ht.ContainsKey(s))
							ht[s] = (int) ht[s] + 1;
						else
							ht.Add(s, 1);
					}
					m = m.NextMatch();
				}
			}
			return ht;
		}

		static private string[] commonWordList;
	}
}
