using System;
using System.IO;
using System.Collections;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// Exports events and comments to a single file, or series of files.
	/// </summary>
	public class Exporter
	{
		#region Private Instance Constructor
		private Exporter() {} // hiding the constructor
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Begins the comment export.
		/// </summary>
		/// <param name="splitExport">A <see cref="SplitExport"/> that specifies how to split the export.</param>
		/// <param name="basePath">The base file name to use.</param>
		/// <param name="j">The <see cref="Journal"/> to export from.</param>
		/// <param name="ijw">The <see cref="Common.IJournalWriter"/> to use.</param>
		static public void Export(SplitExport splitExport, string basePath, Journal j,
			Common.IJournalWriter ijw)
		{
			ArrayList erIDs = null;
			int currentYear = int.MinValue;
			int currentMonth = int.MinValue;


			switch (splitExport)
			{
				case SplitExport.Single:
					using (FileStream fs = new FileStream(basePath, FileMode.Create))
					{
						ijw.WriteJournal(fs, j, null, null, true, true);
					}
					break;
				case SplitExport.PerYear:
					foreach (Common.Journal.EventsRow er in j.Events.Select(string.Empty, "Date"))
					{
						if (er.Date.Year != currentYear)
						{
							if (erIDs != null)
								SaveWithYear(ijw, j, (int[]) erIDs.ToArray(typeof(int)), currentYear, basePath);
							erIDs = new ArrayList();
							currentYear = er.Date.Year;
						}
						erIDs.Add(er.ID);
					}
					SaveWithYear(ijw, j, (int[]) erIDs.ToArray(typeof(int)), currentYear, basePath);
					break;
				case SplitExport.PerMonth:
					foreach (Common.Journal.EventsRow er in j.Events.Select(string.Empty, "Date"))
					{
						if (er.Date.Year != currentYear || er.Date.Month != currentMonth)
						{
							if (erIDs != null)
								SaveWithYearAndMonth(ijw, j, (int[]) erIDs.ToArray(typeof(int)), currentYear, currentMonth,
									basePath);
							erIDs = new ArrayList();
							currentYear = er.Date.Year;
							currentMonth = er.Date.Month;
						}
						erIDs.Add(er.ID);
					}
					SaveWithYearAndMonth(ijw, j, (int[]) erIDs.ToArray(typeof(int)), currentYear, currentMonth, basePath);
					break;
				case SplitExport.PerEntry:
					string s = j.GetMaxEventID().ToString();
					string numFormat = string.Empty.PadLeft(s.Length, '0');
					foreach (Common.Journal.EventsRow er in j.Events)
						SaveWithID(ijw, j, er.ID, numFormat, basePath);
					break;
			}
		}
		#endregion

		#region Private Static Methods
		static private void SaveWithID(Common.IJournalWriter ijw, Journal j, int erID, string numFormat, string basePath)
		{
			string fileName = string.Format("{0} - {1:" + numFormat + "}{2}",
				Path.Combine(Path.GetDirectoryName(basePath),
				Path.GetFileNameWithoutExtension(basePath)), erID,
				Path.GetExtension(basePath));
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
				ijw.WriteJournal(fs, j, new int[] {erID}, null, true, true);
		}

		static private void SaveWithYear(Common.IJournalWriter ijw, Journal j, int[] erIDs, int year, string basePath)
		{
			string fileName = string.Format("{0} - {1}{2}",
				Path.Combine(Path.GetDirectoryName(basePath),
				Path.GetFileNameWithoutExtension(basePath)), year,
				Path.GetExtension(basePath));
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
				ijw.WriteJournal(fs, j, erIDs, null, true, true);
		}

		static private void SaveWithYearAndMonth(Common.IJournalWriter ijw, Journal j, int[] erIDs, int year, int month, string basePath)
		{
			string stamp = new DateTime(year, month, 1).ToString("yyyy MM (MMMM)");
			string fileName = string.Format("{0} - {1}{2}",
				Path.Combine(Path.GetDirectoryName(basePath),
				Path.GetFileNameWithoutExtension(basePath)), stamp,
				Path.GetExtension(basePath));
			using (FileStream fs = new FileStream(fileName, FileMode.Create))
				ijw.WriteJournal(fs, j, erIDs, null, true, true);
		}
		#endregion
	}
}
