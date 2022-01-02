using System;
using System.IO;
using System.Net;
using System.Threading;

namespace EF.ljArchive.WindowsForms
{
	public delegate void UpdateExistsCallBack();

	/// <summary>
	/// Summary description for UpdateChecker.
	/// </summary>
	public class UpdateChecker
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private UpdateChecker() {}

		static public void Start(UpdateExistsCallBack uccb)
		{
			UpdateChecker.uccb = uccb;
			t = new Thread(new ThreadStart(ThreadStart));
			t.Start();
		}

		static public bool IsAlive
		{
			get { return (t != null && t.IsAlive); }
		}

		static public void Abort()
		{
			if (t != null)
				t.Abort();
		}

		static private void ThreadStart()
		{
			if (UpdateAvailable())
				uccb();
		}

		static public bool UpdateAvailable()
		{
			try
			{
				WebRequest request = WebRequest.Create(_manifestURL);
				WebResponse response = request.GetResponse();
				Version serverVersion;
				using (StreamReader sr = new StreamReader(response.GetResponseStream()))
					serverVersion = new Version(sr.ReadToEnd());
				if (serverVersion > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)
					return true;
			}
			catch {}
			return false;
		}

		private const string _manifestURL = "http://ljarchive.sourceforge.net/CurrentVersion/Manifest";
		static private Thread t;
		static private UpdateExistsCallBack uccb;
	}
}
