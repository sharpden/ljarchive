using System;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms
{
	/// <summary>
	/// Makes the mouse cursor display the busy state.
	/// </summary>
	/// <remarks>Nice for putting into a using() statement.</remarks>
	public class Busy : IDisposable
	{
		public Busy()
		{
			Cursor.Current = Cursors.WaitCursor;
		}

		#region IDisposable Members

		public void Dispose()
		{
			Cursor.Current = Cursors.Default;
		}

		#endregion
	}
}
