using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;

namespace EF.ljArchive.WindowsForms.Update
{
	/// <summary>
	/// Summary description for ApplicationWindow.
	/// </summary>
	public class ApplicationWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar pgb;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblBytesDownloaded;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ApplicationWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeForm();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pgb = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblBytesDownloaded = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pgb
			// 
			this.pgb.Location = new System.Drawing.Point(10, 10);
			this.pgb.Name = "pgb";
			this.pgb.Size = new System.Drawing.Size(382, 23);
			this.pgb.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(320, 44);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblBytesDownloaded
			// 
			this.lblBytesDownloaded.Location = new System.Drawing.Point(16, 48);
			this.lblBytesDownloaded.Name = "lblBytesDownloaded";
			this.lblBytesDownloaded.Size = new System.Drawing.Size(168, 16);
			this.lblBytesDownloaded.TabIndex = 2;
			// 
			// ApplicationWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(402, 74);
			this.Controls.Add(this.lblBytesDownloaded);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pgb);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ApplicationWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Updating ljArchive...";
			this.ResumeLayout(false);

		}
		#endregion

		#region Private Instance Methods
		private void InitializeForm()
		{
			Localizer.SetControlText(this.GetType(), new Control[] {this, btnCancel});
			WebDownloader.Start(_downloadURL, new WebDownloadCallBack(WebDownloader_CallBack));
		}

		private void WebDownloader_CallBack(WebDownloadEventArgs wdea)
		{
			if (this.IsHandleCreated)
				this.Invoke(new TS_CallBackDelegate(TS_CallBack), new object[] {wdea});
		}

		private void TS_CallBack(WebDownloadEventArgs wdea)
		{
			switch (wdea.Status)
			{
				case WebDownloadStatus.Downloading:
					pgb.Value = wdea.PercentComplete;
					lblBytesDownloaded.Text = string.Format(Localizer.GetString(this.GetType(),
						"lblBytesDownloaded.Text"), wdea.BytesDownloaded);
					break;
				case WebDownloadStatus.Fail:
					if (WebDownloader.DownloadException.GetType() == typeof(WebException))
						MessageBox.Show("Error downloading update from server.");
					else
                        MessageBox.Show("General Error: " + WebDownloader.DownloadException.ToString());
					this.Close();
					break;
				case WebDownloadStatus.Success:
					try
					{
						string[] deletePaths;
						using (ZipInputStream zs = new ZipInputStream(WebDownloader.DownloadStream))
						{
							ZipEntry ze;

							while ((ze = zs.GetNextEntry()) != null)
							{
								int size;
								byte[] data = new byte[2048];
								string path = Path.Combine(Application.StartupPath, ze.Name);
								string dir = Path.GetDirectoryName(path);
								if (ze.IsDirectory)
									continue;
								if (!Directory.Exists(dir))
									Directory.CreateDirectory(dir);
								using (FileStream fs = File.Open(path, FileMode.Create))
								{
									do
									{
										size = zs.Read(data, 0, data.Length);
										if (size > 0)
											fs.Write(data, 0, size);
									} while (size > 0);
								}
							}
						}
						if (File.Exists(Path.Combine(Application.StartupPath, _deleteFile)))
						{
							using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath,
									   _deleteFile)))
								deletePaths = sr.ReadToEnd().Split('|');
							if (deletePaths != null)
								foreach (string deletePath in deletePaths)
									if (File.Exists(Path.Combine(Application.StartupPath, deletePath)))
										File.Delete(Path.Combine(Application.StartupPath, deletePath));
							File.Delete(Path.Combine(Application.StartupPath, _deleteFile));
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
					this.Close();
					break;
			}
		}

		private void ApplicationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (WebDownloader.IsAlive)
				WebDownloader.Abort();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, _ljExecutable));
		}
		#endregion

		#region Private Static Methods
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
			Application.Run(new ApplicationWindow());
		}
		#endregion

		private delegate void TS_CallBackDelegate(WebDownloadEventArgs wdea);
		private const string _downloadURL = "http://http://ljarchive.sourceforge.net/CurrentVersion/ljArchive.zip";
		private const string _ljExecutable = "ljArchive.exe";
		private const string _deleteFile = "_delete.txt";
	}
}
