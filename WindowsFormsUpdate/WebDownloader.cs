using System;
using System.IO;
using System.Net;
using System.Threading;

namespace EF.ljArchive.WindowsForms.Update
{
	/// <summary>
	/// Callback for the <see cref="Sync"/> class.
	/// </summary>
	public delegate void WebDownloadCallBack(WebDownloadEventArgs soe);

	public enum WebDownloadStatus
	{
		Downloading,
		Success,
		Fail
	}

	#region WebDownloadEventArgs
	public class WebDownloadEventArgs : System.EventArgs
	{
		public WebDownloadEventArgs(WebDownloadStatus status, int percentComplete, long bytesDownloaded)
		{
			this.status = status;
			this.percentComplete = percentComplete;
			this.bytesDownloaded = bytesDownloaded;
		}

		public EF.ljArchive.WindowsForms.Update.WebDownloadStatus Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public int PercentComplete
		{
			get { return this.percentComplete; }
			set { this.percentComplete = value; }
		}

		public long BytesDownloaded
		{
			get { return this.bytesDownloaded; }
			set { this.bytesDownloaded = value; }
		}

		private WebDownloadStatus status;
		private int percentComplete;
		private long bytesDownloaded;
	}
	#endregion

	/// <summary>
	/// Summary description for WebDownloader.
	/// </summary>
	public class WebDownloader
	{
		/// <summary>
		/// This is a static class.
		/// </summary>
		private WebDownloader() {}

		static public void Start(string url, WebDownloadCallBack wdcb)
		{
			WebDownloader.url = url;
			WebDownloader.wdcb = wdcb;
			t = new Thread(new ThreadStart(ThreadStart));
			t.Start();
		}

		static private void ThreadStart()
		{
			WebRequest request = WebRequest.Create(url);
			WebResponse response;
			Stream responseStream;
			long totalBytes;
			long downloadedBytes = 0;
			byte[] buffer = new byte[1024];
			int readBytes, percentComplete = 0;

			try
			{
				response = request.GetResponse();
				totalBytes = response.ContentLength;
				responseStream = response.GetResponseStream();
			

				downloadStream = new MemoryStream();
				while ((readBytes = responseStream.Read(buffer, 0, buffer.Length)) > 0)
				{
					int i;
					downloadStream.Write(buffer, 0, readBytes);
					downloadedBytes += readBytes;
					i = (int) ((downloadedBytes * 100) / totalBytes);
					if (percentComplete != i)
					{
						percentComplete = i;
						wdcb(new WebDownloadEventArgs(WebDownloadStatus.Downloading, percentComplete, downloadedBytes));
					}
				}
			

				downloadStream.Seek(0L, SeekOrigin.Begin);
				wdcb(new WebDownloadEventArgs(WebDownloadStatus.Success, 0, 0));
			}
			catch (Exception ex)
			{
				downloadException = ex;
				wdcb(new WebDownloadEventArgs(WebDownloadStatus.Fail, 0, 0));
				return;
			}
		}

		static public void Abort()
		{
			if (t != null)
				t.Abort();
		}

		static public bool IsAlive
		{
			get { return (t != null && t.IsAlive); }
		}

		static public Stream DownloadStream
		{
			get { return downloadStream; }
		}

		static public Exception DownloadException
		{
			get { return downloadException; }
		}

		static private Exception downloadException;
		static private Stream downloadStream;
		static private Thread t;
		static private string url;
		static private WebDownloadCallBack wdcb;
	}
}
