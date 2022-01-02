using System;
using System.Drawing;
using System.Collections;
using ZedGraph;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.Core
{
	/// <summary>
	/// Categories designating posts be split.
	/// </summary>
	public enum SplitPosts
	{
		/// <summary>
		/// Split posts per day.
		/// </summary>
		Day,
		/// <summary>
		/// Split posts per month.
		/// </summary>
		Month,
		/// <summary>
		/// Split posts per year.
		/// </summary>
		Year
	}

	/// <summary>
	/// Creates a graph of post frequency over the life of the journal.
	/// </summary>
	public class PostFrequency
	{
		/// <summary>
		/// this is a static class.
		/// </summary>
		private PostFrequency() {}

		/// <summary>
		/// Returns a graph of post frequency over the life of the journal.
		/// </summary>
		/// <param name="j">The <see cref="Journal"/> to analyze frequencies.</param>
		/// <param name="width">The width of the graph.</param>
		/// <param name="height">The height of the graph.</param>
		/// <param name="splitPosts">A category designating posts be split per day, month, or year.</param>
		/// <returns>a graph of post frequency over the life of the journal.</returns>
		static public GraphPane GetGraph(Journal j, int width, int height, SplitPosts splitPosts)
		{
			SortedList sl = new SortedList();
			GraphPane pane;
			CurveItem curve;
			double[] x, y;
			foreach (Journal.EventsRow er in j.Events)
			{
				DateTime dt;
				if (er.IsDateNull())
					continue;
				switch (splitPosts)
				{
					case SplitPosts.Day: dt = er.Date.Date; break;
					case SplitPosts.Month: dt = new DateTime(er.Date.Year, er.Date.Month, 1); break;
					default: dt = new DateTime(er.Date.Year, 1, 1); break;
				}
				if (sl.ContainsKey(dt))
					sl[dt] = (int) sl[dt] + 1;
				else
					sl.Add(dt, 1);
			}
			pane = new GraphPane(new Rectangle(0, 0, width, height),
				"Post Frequencies Per " + splitPosts.ToString(), "Date", "Post Frequency");
			x = new double[sl.Count];
			y = new double[sl.Count];
			for (int i = 0; i < sl.Count; ++i)
			{
				DateTime dt = (DateTime) sl.GetKey(i);
				x[i] = (double) new XDate(dt.Year, dt.Month, dt.Day);
				y[i] = (int) sl.GetByIndex(i);
			}
			curve = pane.AddCurve("Frequency", x, y, Color.Red, SymbolType.Diamond);
			pane.XAxis.Type = AxisType.Date;
			pane.XAxis.ScaleFormat = "&yyyy";
			pane.XAxis.IsShowGrid = true;
			pane.YAxis.IsShowGrid = true;
			pane.XAxis.GridColor = Color.LightGray;
			pane.YAxis.GridColor = Color.LightGray;
			pane.AxisBackColor = Color.LightCyan;
			pane.Legend.IsVisible = false;
			pane.AxisChange();
			return pane;
		}
	}
}
