using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace EF.ljArchive.WindowsForms.Controls
{
	/// <summary>
	/// Summary description for DataGridImageColumn.
	/// </summary>
	public class DataGridImageColumn : DataGridTextBoxColumn
	{
		public DataGridImageColumn(string mappingName, string headerText, int width, Hashtable imageMap)
		{
			this.MappingName = mappingName;
			this.HeaderText = headerText;
			this.Width = width;
			this.imageMap = imageMap;
		}

		public DataGridImageColumn(Hashtable imageMap)
		{
			this.imageMap = imageMap;
		}

		public DataGridImageColumn() : this(null)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void Edit(CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			return;
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
		{
			DataRowView drv = source.List[rowNum] as DataRowView;
			Image img = null;

			if (drv != null)
				img = imageMap[drv[this.MappingName]] as Image;
			g.FillRectangle(backBrush, bounds);
			bounds.Offset(2, 1);
			if (img != null)
				g.DrawImageUnscaled(img, bounds);
		}


		public Hashtable ImageMap
		{
			get
			{
				return imageMap;
			}
			set
			{
				imageMap = value;
			}
		}

		private Hashtable imageMap;
	}
}
