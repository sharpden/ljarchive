using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using ZedGraph;
using EF.ljArchive.Common;
using System.Text.RegularExpressions;

namespace EF.ljArchive.Plugins.Core
{

	#region Public Enums
	/// <summary>
	/// Possible baseline comparisons.
	/// </summary>
	public enum RIDBaselines
	{
		/// <summary>
		/// All of LiveJournal
		/// </summary>
		All = 0,
		/// <summary>
		/// Same gender
		/// </summary>
		Gender = 1,
		/// <summary>
		/// Same age group
		/// </summary>
		Age = 2
	}

	/// <summary>
	/// The possible states of an RID Analysis.
	/// </summary>
	public enum AnalysisStatus
	{
		/// <summary>
		/// Analyzing - in progress
		/// </summary>
		Analyzing,
		/// <summary>
		/// Couldn't reach the server
		/// </summary>
		NetFailed,
		/// <summary>
		/// Success
		/// </summary>
		Success
	}
	#endregion

	#region AnalysisStatusEventArgs
	/// <summary>
	/// <see cref="RIDAnalysis"/> callback event arguments.
	/// </summary>
	public class AnalysisStatusEventArgs : System.EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AnalysisStatusEventArgs"/> class.
		/// </summary>
		/// <param name="status">The <see cref="AnalysisStatus"/> designating the current status.</param>
		/// <param name="percentComplete">The percent complete of the analysis.</param>
		public AnalysisStatusEventArgs(AnalysisStatus status, int percentComplete)
		{
			this.status = status;
			this.percentComplete = percentComplete;
		}

		/// <summary>
		/// The <see cref="AnalysisStatus"/> designating the current status.
		/// </summary>
		public AnalysisStatus Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		/// <summary>
		/// The percent complete of the analysis.
		/// </summary>
		public int PercentComplete
		{
			get { return this.percentComplete; }
			set { this.percentComplete = value; }
		}

		private AnalysisStatus status;
		private int percentComplete;
	}
	#endregion

	/// <summary>
	/// Callback delegate for the <see cref="RIDAnalysis"/> class.
	/// </summary>
	public delegate void AnalysisStatusCallBack(AnalysisStatusEventArgs asea);

	/// <summary>
	/// Performs a Regressive Imagery Analysis of a <see cref="Journal"/>, and creates resultant graphs and HTML
	/// summaries.
	/// </summary>
	public class RIDAnalysis
	{
		#region Private Instance Constructors
		/// <summary>
		/// This is a static class.
		/// </summary>
		private RIDAnalysis() {}
		#endregion

		#region Public Static Properties
		/// <summary>
		/// If true, the analysis is still running.
		/// </summary>
		static public bool IsAlive
		{
			get { return (t != null && t.IsAlive); }
		}

		/// <summary>
		/// The categories available for analysis and graphing.
		/// </summary>
		static public string[] Categories
		{
			get { return categories; }
		}

		/// <summary>
		/// The average values returned from the server.
		/// </summary>
		static public Hashtable[] Averages
		{
			get { return averages; }
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Begin an analysis.
		/// </summary>
		/// <param name="j">The <see cref="Journal"/> to analyze.</param>
		/// <param name="gender">The gender of the journal owner.</param>
		/// <param name="age">The age of the journal owner.</param>
		/// <param name="ascb">The <see cref="AnalysisStatusCallBack"/> to notify the interface of the plugin's progress.</param>
		/// <remarks>Providing an age of -1 to this method will make the analysis ignore age and gender.</remarks>
		static public void Start(Journal j, char gender, int age, AnalysisStatusCallBack ascb)
		{
			RIDAnalysis.j = j;
			RIDAnalysis.gender = gender;
			RIDAnalysis.age = age;
			RIDAnalysis.ascb = ascb;
			t = new Thread(new ThreadStart(RIDAnalysis.ThreadStart));
			t.Start();
		}

		/// <summary>
		/// Retries the portion of the analysis that connects to a server.
		/// </summary>
		static public void RetryServer()
		{
			t = new Thread(new ThreadStart(RIDAnalysis.BuildBaseline));
			t.Start();
		}

		/// <summary>
		/// Aborts an analysis.
		/// </summary>
		static public void Abort()
		{
			t.Abort();
		}

		/// <summary>
		/// Returns an HTML string that provides a summary of the RID Analysis.
		/// </summary>
		/// <param name="categories">The categories to report.</param>
		/// <param name="title">The title of the summary.</param>
		/// <param name="baseline">The baseline to use.</param>
		/// <returns>an HTML string that provides a summary of the RID Analysis.</returns>
		static public string GetHTMLSummary(string[] categories, string title, RIDBaselines baseline)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<!-- BEGIN RID ANALYSIS CODE -->");
			sb.Append("<div style='text-align: center'>");
			sb.Append("<div style='width: 450px; background-color: #666; color: #fff; border: 5px solid #ccc; margin-left: auto; margin-right: auto; font-family: Verdana, Arial, Helvetica; font-size: 10px'><b>");
			sb.Append(title);
			sb.Append("</b><br/>Regressive Imagery Analysis for ");
			sb.Append(j.Options[0].UserName);
			sb.Append("'s journal<br />Compared to: <b>");
			switch (baseline)
			{
				case RIDBaselines.All: sb.Append("Everyone"); break;
				case RIDBaselines.Gender: sb.Append("Same Gender"); break;
				case RIDBaselines.Age: sb.Append("Same Age Group"); break;
			}
			sb.Append("</b><table>");
			for (int i = 0; i < categories.Length; ++i)
			{
				string category = categories[i];
				double d = GetAverage(category, baseline);
				string bar = string.Format("<tr valign='middle' style='font-size:10px; color: #fff'>" +
				"<td align='right'>{0}</td>" +
				"<td align='left'><img src='http://stat.livejournal.com/img/poll/leftbar.gif' align='absmiddle' height='14' width='7' />" +
				"<img src='http://stat.livejournal.com/img/poll/mainbar.gif' align='absmiddle' height='14' width='{1}' alt='{2:00.0}%' />" +
				"<img src='http://stat.livejournal.com/img/poll/rightbar.gif' align='absmiddle' height='14' width='7' /></td><td><b>{2:0.0}%</b></td></tr>",
					category.Substring(category.LastIndexOf(':') + 1),
					Convert.ToInt32((double.IsNaN(d) ? 0F : d) * 2.7F), d);
				sb.Append(bar);
			}
			sb.Append("</table>");
			sb.Append("<a style='color: #fff' href='http://fawx.com/software/ljarchive/rid'>What does this mean?</a><br />&nbsp;");
			sb.Append("</div>");
			sb.Append("</div>");
			sb.Append("<!-- END RID ANALYSIS CODE -->");
			return sb.ToString();
		}

		/// <summary>
		/// Returns a graph that provides an over-time RID Analysis.
		/// </summary>
		/// <param name="categories">The categories to report.</param>
		/// <param name="title">The title of the graph.</param>
		/// <param name="width">The width of the graph to return.</param>
		/// <param name="height">The height of the graph to return.</param>
		/// <param name="baseline">The baseline to use.</param>
		/// <returns>a graph that provides an over-time RID Analysis.</returns>
		static public GraphPane GetOverTimeGraph(string[] categories, string title, int width, int height,
			RIDBaselines baseline)
		{
			GraphPane pane;
			CurveItem curve;
			Random r = new Random();
			pane = new GraphPane(new Rectangle(0, 0, width, height),
				title + " Over Time (%)", string.Empty, string.Empty);

			foreach (string category in categories)
			{
				NormalDist nd;
				SortedList sl = new SortedList();
				Hashtable counts = new Hashtable();
				double mean = 0, variance = 0;
				double[] x, y;

				foreach (string findCategory in RIDAnalysis.categories)
				{
					if (findCategory.StartsWith(category))
					{
						mean += (double) averages[(int) baseline][findCategory];
						variance += Math.Pow((double) stdDev[(int) baseline][findCategory], 2);
					}
				}
				for (int i = 0; i < j.Events.Count; ++i)
				{
					Journal.EventsRow er = j.Events[i];
					double d = 0F;
					DateTime dt;
					if (er.IsDateNull())
						continue;
					dt = new DateTime(er.Date.Year, er.Date.Month, 1);
				
					foreach (string findCategory in RIDAnalysis.categories)
						if (findCategory.StartsWith(category))
							d += ((double[]) frequencies[findCategory])[i];

					if (sl.ContainsKey(dt))
					{
						sl[dt] = (double) sl[dt] + d;
						counts[dt] = (int) counts[dt] + 1;
					}
					else
					{
						sl.Add(dt, d);
						counts.Add(dt, 1);
					}
				}
				if (variance > 0F)
				{
					nd = new NormalDist(mean, variance);
					foreach (object key in counts.Keys)
						sl[key] = nd.CDF(((double) sl[key]) / ((double) (int) counts[key])) * 100F;
				}
				else
				{
					foreach (object key in counts.Keys)
						sl[key] = (double) 50F;
				}

				x = new double[sl.Count];
				y = new double[sl.Count];
				for (int i = 0; i < sl.Count; ++i)
				{
					DateTime dt = (DateTime) sl.GetKey(i);
					double d = (double) sl.GetByIndex(i);
					x[i] = (double) new XDate(dt.Year, dt.Month, dt.Day);
					y[i] = double.IsNaN(d) ? 0F : d;
				}
				curve = pane.AddCurve(category.Substring(category.LastIndexOf(':') + 1), x, y,
					Color.FromArgb(r.Next(200), r.Next(200), r.Next(200)),
					SymbolType.Diamond);
			}
			pane.XAxis.Type = AxisType.Date;
			pane.XAxis.ScaleFormat = "&yyyy";
			pane.XAxis.IsShowGrid = true;
			pane.YAxis.IsShowGrid = true;
			pane.YAxis.Min = 0;
			pane.YAxis.Max = 100;
			pane.XAxis.GridColor = Color.LightGray;
			pane.YAxis.GridColor = Color.LightGray;
			pane.AxisBackColor = Color.LightCyan;
			pane.Legend.IsVisible = true;
			pane.AxisChange();
			return pane;
		}

		/// <summary>
		/// Returns a graph that provides a summary of the RID Analysis.
		/// </summary>
		/// <param name="categories">The categories to report.</param>
		/// <param name="title">The title of the graph.</param>
		/// <param name="width">The width of the graph to return.</param>
		/// <param name="height">The height of the graph to return.</param>
		/// <param name="baseline">The baseline to use.</param>
		/// <returns>a graph that provides a summary of the RID Analysis.</returns>
		static public GraphPane GetSummaryGraph(string[] categories, string title, int width, int height,
			RIDBaselines baseline)
		{
			GraphPane pane;
			CurveItem curve;
			TextItem text;
			string[] labels;
			double[] values;
			double[] averageLine;
			labels = new string[categories.Length];
			values = new double[categories.Length];
			averageLine = new double[categories.Length];
			pane = new GraphPane(new Rectangle(0, 0, width, height),
				title + " (%)", string.Empty, string.Empty);
			for (int i = 0; i < categories.Length; ++i)
			{
				string category = categories[i];
				double d = GetAverage(category, baseline);
				values[i] = double.IsNaN(d) ? 0F : d;
				labels[i] = category.Substring(category.LastIndexOf(':') + 1);
				averageLine[i] = 50F;
				text = new TextItem(string.Format("{0:0.0}%", d), i + 1, (float) d / 2F);
				text.FontSpec.IsFramed = false;
				text.FontSpec.IsFilled = false;
				pane.TextList.Add(text);
			}
			curve = pane.AddCurve("Average", null, averageLine, Color.Green);
			curve.Symbol.Type = SymbolType.Diamond;
			curve.Symbol.Size *= 2;
			curve.Line.Width = 2.0F;
			curve = pane.AddCurve("Bar Values", null, values, Color.Red);
			curve.IsBar = true;
			pane.Legend.IsVisible = false;
			pane.XAxis.IsTicsBetweenLabels = true;
			pane.XAxis.TextLabels = labels;
			pane.XAxis.ScaleFontSpec.Angle = 45F;
			pane.XAxis.Step = 1;
			pane.XAxis.Type = AxisType.Text;
			pane.YAxis.Min = 0;
			pane.YAxis.Max = 100;
			pane.AxisChange();
			return pane;
		}

		/// <summary>
		/// Returns the bottom three categories averaged from the RID Analysis.
		/// </summary>
		/// <param name="baseline">The baseline to use.</param>
		/// <returns>the bottom three categories averaged from the RID Analysis.</returns>
		static public string[] GetBottomThreeCategories(RIDBaselines baseline)
		{
			SortedList sl = new SortedList();

			foreach (string category in categories)
			{
				double d = GetAverage(category, baseline);
				sl.Add(d, category);
			}
			return new string[] {(string) sl.GetByIndex(0),
									(string) sl.GetByIndex(1),
									(string) sl.GetByIndex(2)};
		}

		/// <summary>
		/// Returns the top three categories averaged from the RID Analysis.
		/// </summary>
		/// <param name="baseline">The baseline to use.</param>
		/// <returns>the top three categories averaged from the RID Analysis.</returns>
		static public string[] GetTopThreeCategories(RIDBaselines baseline)
		{
			SortedList sl = new SortedList();

			foreach (string category in categories)
			{
				double d = GetAverage(category, baseline);
				sl.Add(d, category);
			}
			return new string[] {(string) sl.GetByIndex(sl.Count - 1),
								 (string) sl.GetByIndex(sl.Count - 2),
								 (string) sl.GetByIndex(sl.Count - 3)};
		}
		#endregion

		#region Private Static Methods
		static private void ThreadStart()
		{
			BuildHitCount();
			BuildBaseline();
		}

		static private double GetAverage(string category, RIDBaselines baseline)
		{
			double d = 0, mean = 0, variance = 0;
			foreach (string findCategory in categories)
			{
				if (findCategory.StartsWith(category))
				{
					d += (double) localAverages[findCategory];
					mean += (double) averages[(int) baseline][findCategory];
					variance += Math.Pow((double) stdDev[(int) baseline][findCategory], 2);
				}
			}
			if (variance > 0F)
				return (new NormalDist(mean, variance)).CDF(d) * 100F;
			else
				return 50;
		}

		static private void BuildBaseline()
		{
			WebRequest wr;
			int badCategories = 0;
			string response;
			string[] sections;
			double entryCount = j.Events.Count;
			IDictionaryEnumerator ide = frequencies.GetEnumerator();
			StringBuilder uri = new StringBuilder();

			// build local averages
			localAverages = new Hashtable();
			while (ide.MoveNext())
			{
				double total = 0;
				foreach (double d in (IEnumerable) ide.Value)
					total += d;
				localAverages.Add(ide.Key, total / entryCount);
			}

            // build uri string
			ide = localAverages.GetEnumerator();
			while (ide.MoveNext())
				if ((double) ide.Value < 0.0000001F || double.IsNaN((double) ide.Value))
					badCategories++;
			if (badCategories < 20)
			{
				ide.Reset();
				uri.Append(string.Format("{0}?mode=update&id={1}", ridServerURL, GetID(j)));
				if (age > -1)
					uri.Append(string.Format("&gender={0}&age={1}", gender, age));
				else
					uri.Append("&gender=&age=");
				while (ide.MoveNext())
				{
					string key = (string) ide.Key;
					key = key.Substring(key.LastIndexOf(':') + 1).Replace(" ", "_").Replace("-", "_");
					uri.Append(string.Format("&{0}={1:0.0000000}", key, ide.Value));
				}
			}
			else // don't submit values to server if they're awry (like a different language)
			{
				uri.Append(string.Format("{0}?mode=check", ridServerURL));
				if (age > -1)
					uri.Append(string.Format("&gender={0}&age={1}", gender, age));
				else
					uri.Append("&gender=&age=");
			}
			wr = WebRequest.Create(uri.ToString());
			averages = null;
			stdDev = null;
			try
			{
				using (StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream()))
					response = sr.ReadToEnd();
			}
			catch (WebException)
			{
				ascb(new AnalysisStatusEventArgs(AnalysisStatus.NetFailed, 100));
				return;
			}
			sections = response.Split('\n');
			averages = new Hashtable[sections[sections.Length - 1] == string.Empty ? sections.Length - 1
				: sections.Length];
			stdDev = new Hashtable[sections[sections.Length - 1] == string.Empty ? sections.Length - 1
				: sections.Length];
			for (int i = 0; i < sections.Length; ++i)
			{
				string[] nums = sections[i].Split(' ');
				if (sections[i] == string.Empty)
					continue;
				averages[i] = new Hashtable();
				stdDev[i] = new Hashtable();
				for (int c = 0; c < nums.Length; ++c)
				{
					if (c % 2 == 0)
						averages[i].Add(categories[c / 2], double.Parse(nums[c]));
					else
						stdDev[i].Add(categories[c / 2], double.Parse(nums[c]));
				}
			}
			ascb(new AnalysisStatusEventArgs(AnalysisStatus.Success, 100));
		}

		static private string GetID(Journal j)
		{
			Uri serverURI = new Uri(j.Options[0].ServerURL);
			string idPlainText = serverURI.Host.ToLower() + j.Options[0].UserName.ToLower();
			return ComputeMD5(idPlainText);
		}

		static private string ComputeMD5(string plainText)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] plainTextBytes = Encoding.ASCII.GetBytes(plainText);
			byte[] hashBytes = md5.ComputeHash(plainTextBytes);
			StringBuilder sb = new StringBuilder();
			foreach (byte hashByte in hashBytes)
				sb.Append(Convert.ToString(hashByte, 16).PadLeft(2, '0'));
			return sb.ToString();
		}

		static private void BuildHitCount()
		{
			ArrayList al = null;
			ArrayList categoryList = new ArrayList();
			Regex r = new Regex("<.+>", RegexOptions.Compiled | RegexOptions.Multiline);
			StringBuilder sb = new StringBuilder();

			// wordlists
			wordLists = new Hashtable();
			using (StreamReader sr = new StreamReader(
					   Assembly.GetExecutingAssembly().GetManifestResourceStream("EF.ljArchive.Plugins.Core.RID.txt")))
			{
				while (sr.Peek() > -1)
				{
					string s = sr.ReadLine();
					if (s.StartsWith("\t"))
					{
						al = new ArrayList();
						wordLists.Add(s.Substring(1), al);
						categoryList.Add(s.Substring(1));
					}
					else
					{
						al.Add(s);
					}
				}
			}
			categories = (string[]) categoryList.ToArray(typeof(string));
			hitCounts = new Hashtable();
			frequencies = new Hashtable();
			foreach (string category in categories)
			{
				hitCounts.Add(category, new int[j.Events.Count]);
				frequencies.Add(category, new double[j.Events.Count]);
			}
			wordLengths = new int[j.Events.Count];
			
			for (int row = 0; row < j.Events.Count; ++row)
			{
				int lastPercentComplete = 0;
				IDictionaryEnumerator ide;
				Journal.EventsRow er = j.Events[row];
				string[] words;
				sb.Length = 0;
				if (!er.IsSubjectNull())
				{
					sb.Append(r.Replace(er.Subject, string.Empty).ToLower());
					sb.Append(' ');
				}
				if (!er.IsBodyNull())
					sb.Append(r.Replace(er.Body, string.Empty).ToLower());
				words = sb.ToString().Split(' ', '\n', '\t', '.', ',', '?');
				wordLengths[row] = words.Length;
				ide = wordLists.GetEnumerator();
				while (ide.MoveNext())
				{
					al = (ArrayList) ide.Value;
					int[] hitCount = (int[]) hitCounts[ide.Key];
					double[] frequency = (double[]) frequencies[ide.Key];
					foreach (string word in words)
					{
						foreach (string catWord in al)
						{
							int i;
							if (word == null || word.Length == 0 || catWord[0] > word[0])
								break;
							if (catWord[0] != word[0])
								continue;
							i = catWord.Length - 1;
							// this is poor lemmatization, but an eyeball guesstimate of the dictionary
							// shows it should work for about 95% of all cases
							if (catWord[i] == '*')
							{
								if (word.Length >= i && word.Length < i + 3 && word.Substring(0, i) ==
									catWord.Substring(0, i))
								{
									hitCount[row]++;
									break;
								}
							}
							else if (catWord == word)
							{
								hitCount[row]++;
								break;
							}
						}
					}
					frequency[row] = ((double) hitCount[row]) / ((double) wordLengths[row]);
				}
				if (((95 * row) / j.Events.Count) - lastPercentComplete >= 5)
				{
					lastPercentComplete = (95 * row) / j.Events.Count;
					ascb(new AnalysisStatusEventArgs(AnalysisStatus.Analyzing, lastPercentComplete));
				}
			}
		}
		#endregion

		#region Static Private Fields
		static private Thread t;
		static private Journal j;
		static private int age;
		static private char gender;
		static private AnalysisStatusCallBack ascb;
		static private Hashtable localAverages;
		static private Hashtable[] averages, stdDev;
		static private Hashtable wordLists;
		static private Hashtable hitCounts, frequencies;
		static private int[] wordLengths;
		static private string[] categories;
		private const string ridServerURL = "http://fawx.com/ljArchive/pl/rid.pl";
		#endregion
	}
}
