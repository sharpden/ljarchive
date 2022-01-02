using System;
using System.Drawing;
using System.Data;
using System.Collections;
using ZedGraph;
using EF.ljArchive.Common;

namespace EF.ljArchive.Plugins.Core
{
	/// <summary>
	/// Creates a graph of comment counts over the life of the journal.
	/// </summary>
	public class CommentCount
	{
		#region Public Instance Constructors
		/// <summary>
		/// Intializes a new instance of <see cref="CommentCount"/>.
		/// </summary>
		/// <param name="j">The <see cref="Journal"/> to count comments.</param>
		public CommentCount(Journal j)
		{
			DataRow[] drs = j.Users.Select("User = '" + j.Options[0].UserName + "'");
			this.j = j;
			users = new ArrayList();
			commentsReceived = new Hashtable();
			commentsGiven = new Hashtable();
			if (drs != null && drs.Length > 0)
				myID = (int) drs[0]["ID"];
			else
				myID = -1;

			foreach (Journal.UsersRow ur in j.Users)
			{
				commentsReceived.Add(ur.ID, 0);
				commentsGiven.Add(ur.ID, 0);
				users.Add(ur.ID);
			}
			foreach (Journal.CommentsRow cr in j.Comments)
			{
				if (cr.PosterID == myID)
				{
					if (cr.ParentID == 0)
						continue;
					foreach (Journal.CommentsRow crGiven in j.Comments)
					{
						if (crGiven.ID == cr.ParentID)
						{
							if (commentsGiven.ContainsKey(crGiven.PosterID))
								commentsGiven[crGiven.PosterID] = ((int) commentsGiven[crGiven.PosterID]) + 1;
							break;
						}
					}
				}
				else
				{
					if (commentsReceived.ContainsKey(cr.PosterID))
						commentsReceived[cr.PosterID] = ((int) commentsReceived[cr.PosterID]) + 1;
				}
			}
			users.Sort(new CommentCountCompare(commentsReceived, commentsGiven));
		}
		#endregion

		#region Public Instance Methods
		/// <summary>
		/// Returns a graph of comment counts over the life of the journal.
		/// </summary>
		/// <param name="topNum">The top number of users to graph.</param>
		/// <param name="width">The width of the graph.</param>
		/// <param name="height">The height of the graph.</param>
		/// <returns>a graph of comment counts over the life of the journal.</returns>
		public GraphPane GetGraph(int topNum, int width, int height)
		{
			int num = Math.Min(topNum, users.Count);
			GraphPane pane;
			CurveItem curve;
			string[] labels;
			double[] given, received;
			
			labels = new string[num];
			given = new double[num];
			received = new double[num];
			for (int i = 0; i < topNum && i < users.Count; ++i)
			{
				int userid = (int) users[i];
				labels[i] = j.Users.FindByID(userid).User;
				given[i] = (double) (int) commentsGiven[userid];
				received[i] = (double) (int) commentsReceived[userid];
			}
			pane = new GraphPane(new Rectangle(0, 0, width, height),
				string.Empty, string.Empty, "Comment Amounts");
			curve = pane.AddCurve("Received", null, received, Color.Red);
			curve.IsBar = true;
			curve = pane.AddCurve("Given", null, given, Color.Green);
			curve.IsBar = true;
			pane.XAxis.IsTicsBetweenLabels = true;
			pane.XAxis.TextLabels = labels;
			pane.XAxis.Step = 1;
			pane.XAxis.Type = AxisType.Text;
			pane.XAxis.ScaleFontSpec.Angle = 90;
			pane.XAxis.ScaleFontSpec.Size = 8.0F;
			pane.YAxis.IsShowGrid = true;
			pane.YAxis.GridColor = Color.DarkGray;
			pane.AxisChange();
			return pane;
		}
		#endregion

		#region Private Instance Fields
		int myID;
		private Journal j;
		private ArrayList users;
		private Hashtable commentsReceived;
		private Hashtable commentsGiven;
		#endregion

		#region CommentCountCompare
		private class CommentCountCompare : IComparer
		{
			public CommentCountCompare(Hashtable commentsReceived, Hashtable commentsGiven)
			{
				this.commentsReceived = commentsReceived;
				this.commentsGiven = commentsGiven;
			}

			public int Compare(object x, object y)
			{
				int commentsX = (commentsReceived.ContainsKey(x) ? (int) commentsReceived[x] : 0) +
								(commentsGiven.ContainsKey(x) ? (int) commentsGiven[x] : 0);
				int commentsY = (commentsReceived.ContainsKey(y) ? (int) commentsReceived[y] : 0) +
								(commentsGiven.ContainsKey(y) ? (int) commentsGiven[y] : 0);
				return commentsY - commentsX;
			}

			private Hashtable commentsReceived;
			private Hashtable commentsGiven;
		}
		#endregion
	}
}
