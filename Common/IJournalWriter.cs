using System;
using System.Runtime.Serialization;
using System.IO;

namespace EF.ljArchive.Common
{
	/// <summary>
	/// Allows a class to implement a Journal Writer that writes selected entries and comments to a stream,
	/// and ultimately save the stream to a file.
	/// </summary>
	public interface IJournalWriter
	{
		/// <summary>
		/// Gets a name that accurately describes the class.
		/// </summary>
		/// <remarks>Keep the name short - one or two words.</remarks>
		string Name {get;}
		/// <summary>
		/// Gets a summary of what this <see cref="IJournalWriter"/> does.
		/// </summary>
		string Description {get;}
		/// <summary>
		/// Gets the version of the release.
		/// </summary>
		System.Version Version {get;}
		/// <summary>
		/// Gets the name of the author of this <see cref="IJournalWriter"/>.
		/// </summary>
		/// <remarks>That's you!</remarks>
		string Author {get;}
		/// <summary>
		/// Gets a URL where users can learn more about your class.
		/// </summary>
		string URL {get;}
		/// <summary>
		/// Gets the file name filter string, which determines the choices that appear in the "Save as" dialog box.
		/// </summary>
		/// <remarks><para>For each filtering option, the filter string contains a description of the filter, followed
		/// by the vertical bar (|) and the filter pattern. The strings for different filtering options are separated
		/// by the vertical bar.</para>
		/// <para>The following is an example of a filter string: "Text files (*.txt)|*.txt|All files (*.*)|*.*"</para>
		/// <para>You can add several filter patterns to a filter by separating the file types with semicolons. For
		/// example: "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"</para>
		/// </remarks>
		string Filter {get;}
		/// <summary>
		/// Writes selected <see cref="Journal.EventsRow"/> objects and <see cref="Journal.CommentsRow"/> objects
		/// to a stream.
		/// </summary>
		/// <param name="s">The <see cref="Stream"/> to be written to.</param>
		/// <param name="j">The <see cref="Journal"/> to use for writing.</param>
		/// <param name="eventIDs">An array of <see cref="Journal.EventsRow"/> ItemIDs that specify which
		/// <see cref="Journal.EventsRow"/> objects to write.</param>
		/// <param name="commentIDs">An array of <see cref="Journal.CommentsRow"/> IDs that specify which
		/// <see cref="Journal.CommentsRow"/> objects to write.</param>
		/// <param name="header">If <see langword="true"/>, write any required header for the stream format.</param>
		/// <param name="footer">If <see langword="true"/>, write any required footer for the stream format.</param>
		/// <remarks>Sample code:
		/// <code>
		/// public void WriteJournal(System.IO.Stream s, Journal j, int[] eventIDs, int[] commentIDs, bool header, bool footer)
		/// {
		///		System.Collections.IList i = eventIDs;
		///		foreach (Journal.EventsRow er in j.Events)
		///		{
		///			if (i.Contains(er.ID))
		///			{
		///				// do stuff
		///			}
		///		}
		/// }</code></remarks>
		void WriteJournal(Stream s, Journal j, int[] eventIDs, int[] commentIDs, bool header, bool footer);
		/// <summary>
		/// Gets or sets an object that allows the user to set and persist settings.
		/// </summary>
		object Settings {get; set;}
	}
}
