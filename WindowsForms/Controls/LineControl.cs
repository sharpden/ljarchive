using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	/// <summary>
	/// Summary description for LineControl.
	/// </summary>
	public class LineControl : Control
	{
		public LineControl()
		{
			this.Size = new Size(50, 50);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
		}

		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Horizontal
		{
			get
			{
				return horizontal;
			}
			set
			{
				horizontal = value;
				this.Size = new Size(this.Height, this.Width);
				this.Invalidate(this.ClientRectangle);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Pen p1, p2;
			p1 = new Pen(SystemColors.ControlDark);
			p2 = new Pen(Color.White);

			if (horizontal)
			{
				e.Graphics.DrawLine(p1, 0, 0, Width, 0);
				e.Graphics.DrawLine(p2, 0, 1, Width, 1);
			}
			else
			{
				e.Graphics.DrawLine(p1, 0, 0, 0, Height);
				e.Graphics.DrawLine(p2, 1, 0, 1, Height);
			}
		}


		private bool horizontal = true;
	}
}
