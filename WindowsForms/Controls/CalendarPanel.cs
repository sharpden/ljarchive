using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	/// <summary>
	/// Summary description for CalendarPanel.
	/// </summary>
	public class CalendarPanel : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.MonthCalendar calendar;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CalendarPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.calendar = new System.Windows.Forms.MonthCalendar();
			this.panel = new System.Windows.Forms.Panel();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(320, 132);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 20);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// calendar
			// 
			this.calendar.CalendarDimensions = new System.Drawing.Size(2, 1);
			this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.calendar.Location = new System.Drawing.Point(0, 0);
			this.calendar.MaxSelectionCount = 1;
			this.calendar.Name = "calendar";
			this.calendar.TabIndex = 3;
			this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.eventCalendar_DateSelected);
			// 
			// panel
			// 
			this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel.Controls.Add(this.btnClose);
			this.panel.Controls.Add(this.calendar);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(392, 160);
			this.panel.TabIndex = 5;
			// 
			// CalendarPanel
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.panel);
			this.Name = "CalendarPanel";
			this.Size = new System.Drawing.Size(392, 160);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (CloseClicked != null)
				CloseClicked(this, EventArgs.Empty);
		}

		private void eventCalendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			if (DateClicked != null)
				DateClicked(this, EventArgs.Empty);
		}

		public DateTime[] BoldedDates
		{
			get { return calendar.BoldedDates; }
			set { calendar.BoldedDates = value; }
		}

		public SelectionRange SelectionRange
		{
			get { return calendar.SelectionRange; }
			set { calendar.SelectionRange = value; }
		}

		public event System.EventHandler DateClicked;
		public event System.EventHandler CloseClicked;
	}
}
