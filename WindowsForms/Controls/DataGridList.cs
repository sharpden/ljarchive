using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EF.ljArchive.WindowsForms.Controls
{
	#region GridLayoutHelper
	/// <summary>
	/// Summary description for GridLayoutHelper.
	/// </summary>
	public class GridLayoutHelper
	{
		public GridLayoutHelper(DataGrid grid, DataGridTableStyle tableStyle, decimal[] percentages,
			int[] minimumWidths)
		{
			this.grid = grid;
			this.tableStyle = tableStyle;
			this.percentages = percentages;
			this.minimumWidths = minimumWidths;
			foreach(DataGridColumnStyle col in tableStyle.GridColumnStyles)
				col.WidthChanged += new EventHandler(this.Column_WidthChanged);
			grid.Resize += new System.EventHandler(this.Grid_SizeChanged);
		}

		public void SizeTheGrid()
		{
			if (tableStyle == null)	return;
			GridColumnStylesCollection colStyles = tableStyle.GridColumnStyles;
			if (colStyles.Count == 0) return;

			// Okay, we've got the data to do the sizing
			inManualSize = true;

			//get the target width
			int width = grid.ClientRectangle.Width - (grid.BorderStyle == BorderStyle.Fixed3D ? 4 : 0); // the 4 handles the borders
			if(IsScrollBarVisible(grid))
				width -= SystemInformation.VerticalScrollBarWidth;

			int nCols = tableStyle.GridColumnStyles.Count;
			int lastColIndex  = nCols - 1;

			// Set to zero so horizontal scroll does not show as your size
			// Frees up room in case leading cols grid during resize and try to flash teh HScroll
			colStyles[lastColIndex].Width = 0;

			decimal dWidth = (decimal)width;
			int totalWidth = width;
			int colWidth;

			// By Default we make colWidth equally distributed
			colWidth = width / nCols;
			for(int i = 0; i < lastColIndex; ++i)
			{
				if (i == 0)
				{
					colWidth = 20;
				}
				else if(null != percentages)
				{
					// We're using Percentages
					colWidth = (int)(dWidth * percentages[i]);
				}

				if(null != minimumWidths)
					totalWidth -= colStyles[i].Width = Math.Max(colWidth, minimumWidths[i]);
				else
					totalWidth -= colStyles[i].Width = colWidth;
			}

			// Add on any left over due to rounding
			if(null != minimumWidths)
				colStyles[lastColIndex].Width = Math.Max(totalWidth, minimumWidths[lastColIndex]);
			else
				colStyles[lastColIndex].Width = totalWidth;

			inManualSize = false;
		}

		public void FixupLastColumn()
		{
			if (tableStyle == null)	return;
			GridColumnStylesCollection colStyles = tableStyle.GridColumnStyles;
			if (colStyles.Count == 0) return;

			// Okay, we've got the data to do the sizing
			inManualSize = true;

			//get the target width
			int width = grid.ClientRectangle.Width - 4; // the 4 handles the borders
			if(IsScrollBarVisible(grid))
				width -= SystemInformation.VerticalScrollBarWidth;

			int lastColIndex  = tableStyle.GridColumnStyles.Count - 1;

			for(int i = 0; i < lastColIndex; ++i)
			{
				width -= colStyles[i].Width;
			}

			int minSizeLastCol = -1;
			if(null != minimumWidths)
				minSizeLastCol = minimumWidths[lastColIndex];

			// Last column always gets remainder of space
			if(width > minSizeLastCol)
				colStyles[lastColIndex].Width = width;
			inManualSize = false;
		}
			
		#region IsScrollBarVisible

		protected bool IsScrollBarVisible(Control aControl)
		{
			foreach(Control c in aControl.Controls)
			{
				if (c.GetType().Equals(typeof(VScrollBar)))
				{
					return c.Visible;
				}
			}
			return false;
		}

		#endregion

		#region Properties
		public decimal[] Percentages
		{
			get
			{
				return percentages;
			}
			set
			{
				percentages = value;
				SizeTheGrid();
			}
		}

		public int[] MinimumColumnWidths
		{
			get
			{
				return minimumWidths;
			}
			set
			{
				minimumWidths = value;
				SizeTheGrid();
			}
		}
		#endregion

		#region Event Handlers
		public void Grid_SizeChanged(object sender, System.EventArgs e)
		{
			this.Handle_SizeChanged(grid, tableStyle);
		}

		public void Column_WidthChanged(object sender, EventArgs e)
		{
			this.Handle_WidthChanged(grid, tableStyle);
		}

		public void Handle_SizeChanged(DataGrid grid, DataGridTableStyle ts)
		{
			this.SizeTheGrid();
		}

		public void Handle_WidthChanged(DataGrid grid, DataGridTableStyle ts)
		{
			if(!inManualSize) 
			{
				this.FixupLastColumn();
				if (percentages != null)
					for(int i = 0; i < ts.GridColumnStyles.Count; ++i)
						percentages[i] = ((decimal) ts.GridColumnStyles[i].Width) /
							((decimal) grid.ClientRectangle.Width);
			}
		}
		#endregion

		private DataGrid grid;
		private DataGridTableStyle tableStyle;
		private bool inManualSize = false;

		private decimal[] percentages   = null;
		private int[]     minimumWidths = null;
	}
	#endregion

	/// <summary>
	/// Summary description for DataGridList.
	/// </summary>
	public class DataGridList : DataGrid
	{
		public DataGridList()
		{
			foreach(Control c in this.Controls)
			{
				if (c.GetType().Equals(typeof(VScrollBar)))
				{
					VScrollBar v = (VScrollBar) c;
					v.VisibleChanged += new EventHandler(v_VisibleChanged);
				}
			}
		}

		[DefaultValue(null)]
		[Browsable(false)]
		public string ColumnSettings
		{
			get
			{
				StringBuilder sb;
				if (glh == null)
					return null;
				sb = new StringBuilder();
				foreach (decimal d in glh.Percentages)
				{
					sb.Append(d);
					sb.Append('+');
				}
				sb[sb.Length - 1] = ';';
				foreach (int i in glh.MinimumColumnWidths)
				{
					sb.Append(i);
					sb.Append('+');
				}
				sb.Length -= 1;
				return sb.ToString();
			}
			set
			{
				ArrayList percentages = new ArrayList(), minimumColumnWidths = new ArrayList();
				string[] sections = value.Split(';');
				
				foreach (string s in sections[0].Split('+'))
				{
					if (s.IndexOf('.') > -1)
						percentages.Add(decimal.Parse(s));
					else
						percentages.Add(0.2M); // fix for international users upgrading to 0.9.7
				}
				foreach (string s in sections[1].Split('+'))
					minimumColumnWidths.Add(int.Parse(s));
				if (glh == null)
				{
					glh = new GridLayoutHelper(this, this.TableStyles[0], (decimal[])
						percentages.ToArray(typeof(decimal)), (int[]) minimumColumnWidths.ToArray(typeof(int)));
				}
				else
				{
					glh.Percentages = (decimal[]) percentages.ToArray(typeof(decimal));
					glh.MinimumColumnWidths = (int[]) minimumColumnWidths.ToArray(typeof(int));
				}
			}
		}

		public DataView View
		{
			get
			{
				if (this.DataSource != null && this.DataMember != null)
					return (DataView) ((CurrencyManager) this.BindingContext[this.DataSource, this.DataMember]).List;
				else
					return null;
			}
		}

		public DataRow CurrentDataRow
		{
			get
			{
				BindingManagerBase bm;
				DataRowView drv;
				if (this.DataSource == null || this.DataMember == null)
					return null;
				bm = this.BindingContext[this.DataSource, this.DataMember];
				if (bm.Count < 1 || this.CurrentRowIndex < 0)
					return null;
				drv = (DataRowView) bm.Current;
				return drv.Row;
			}
		}

		private GridLayoutHelper glh;
		private int oldSelectedIndex = -1;
		private DataRow oldDataRow;

		public event EventHandler CurrentRowChanged;

		public void RefreshSelect()
		{
			OnCurrentCellChanged(EventArgs.Empty);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged (e);
			if (this.Visible && glh != null)
				glh.SizeTheGrid();
		}

		protected override void OnCurrentCellChanged(EventArgs e)
		{
			base.OnCurrentCellChanged (e);
			if (this.View != null && oldSelectedIndex < this.View.Count && oldSelectedIndex != -1)
				this.UnSelect(oldSelectedIndex);
			if (this.CurrentRowIndex > -1)
				this.Select(this.CurrentRowIndex);
			if ((this.CurrentDataRow == null || this.CurrentDataRow != oldDataRow) && CurrentRowChanged != null)
				CurrentRowChanged(this, EventArgs.Empty);
			oldDataRow = this.CurrentDataRow;
			oldSelectedIndex = this.CurrentRowIndex;
		}

		protected override void OnDataSourceChanged(EventArgs e)
		{
			oldSelectedIndex = -1;
			base.OnDataSourceChanged (e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			DataGrid.HitTestInfo h = this.HitTest(e.X, e.Y);

			switch (h.Type)
			{
				case DataGrid.HitTestType.ColumnHeader:
					CurrencyManager cm = (CurrencyManager) this.BindingContext[this.DataSource, this.DataMember];
					DataView dv = (DataView) cm.List;
					System.Data.DataRow currentRow = dv[this.CurrentRowIndex].Row;
					base.OnMouseUp (e);
					dv = (DataView) cm.List;
					for (int i = 0; i < dv.Count; ++i)
					{
						if (dv[i].Row == currentRow)
						{
							this.CurrentRowIndex = i;
							this.Select(this.CurrentRowIndex);
							return;
						}
					}
					break;
				case DataGrid.HitTestType.Cell:
					base.OnMouseUp (e);
					this.Select(this.CurrentRowIndex);
					break;
			}
		}

		private void v_VisibleChanged(object sender, EventArgs e)
		{
			if (glh != null)
				glh.SizeTheGrid();
		}
	}
}
