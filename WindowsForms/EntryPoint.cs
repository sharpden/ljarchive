using System;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms
{
	/// <summary>
	/// Summary description for EntryPoint.
	/// </summary>
	public class EntryPoint
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Explorer e = null;

			// run internally system invariant to avoid date and decimal weirdness
			System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo) System.Globalization.CultureInfo.InvariantCulture.Clone();
			culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
			culture.DateTimeFormat.LongTimePattern = "hh:mm:ss";
			Application.CurrentCulture = culture;

			// https://www.koskila.net/how-to-force-an-outdated-net-project-to-use-tls-1-2/
			// https://stackoverflow.com/questions/47269609/system-net-securityprotocoltype-tls12-definition-not-found
			System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType) 0xC00;

			if (System.Environment.Version.Major > 1) // .NET 2.0 XP styles
				System.Windows.Forms.Application.EnableVisualStyles();
#if DEBUG
			e = new Explorer();
			Application.Run(e);
#else
			try
			{
				e = new Explorer();
				Application.Run(e);
			}
			catch (Exception ex)
			{
				Dialogs.GeneralError.Go("ljArchive encountered the following fatal error: " + ex.ToString(), null);
			}
#endif
		}
	}
}
