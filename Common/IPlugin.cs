using System;
using System.Drawing;

namespace EF.ljArchive.Common
{
	/// <summary>
	/// Allows a class to implement an ljArchive Plugin that does just about anything.
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// Gets a title that accurately describes the class.
		/// </summary>
		/// <remarks>Keep the title short - one or two words.</remarks>
		string Title {get;}
		/// <summary>
		/// Gets a summary of what this <see cref="IPlugin"/> does.
		/// </summary>
		string Description {get;}
		/// <summary>
		/// Gets the version of the release.
		/// </summary>
		System.Version Version {get;}
		/// <summary>
		/// Gets the name of the author of this <see cref="IPlugin"/>.
		/// </summary>
		/// <remarks>That's you!</remarks>
		string Author {get;}
		/// <summary>
		/// Gets a URL where users can learn more about your class.
		/// </summary>
		string URL {get;}
		/// <summary>
		/// Gets a <see cref="Image"/> to be displayed in the menu item for this <see cref="IPlugin"/>.
		/// </summary>
		/// <remarks>The image should be 16x16 pixels.</remarks>
		Image MenuIcon {get;}
		/// <summary>
		/// Executes the <see cref="IPlugin"/>.
		/// </summary>
		/// <param name="j">The <see cref="Journal"/> on which to operate.</param>
		void Go(Journal j);
		/// <summary>
		/// Gets or sets an object that allows the user to set and persist settings.
		/// </summary>
		object Settings {get; set;}
		/// <summary>
		/// Sets the index of the currently selected journal entry.
		/// </summary>
		int SelectedEventID {set;}
	}
}
