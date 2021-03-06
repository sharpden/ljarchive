using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EF.ljArchive.Engine
{
	/// <summary>
	/// Adds functionality to the <see cref="Common.Journal"/> typed dataset.
	/// </summary>
	/// <remarks>Inherits from <see cref="Common.Journal"/> so that plugin writers need not be concerned with the
	/// extra functionality that the ljArchive engine requires.  Supports fast serialization (much faster than native
	/// XML serialization), loading and saving to a path, setting the MD5-hashed password, and a few other features.
	/// </remarks>
	[Serializable]
	public class Journal : Common.Journal, ISerializable
	{
		#region Public Instance Constructors
		/// <summary>
		/// Creates a new instance of the <see cref="Journal"/> class.
		/// </summary>
		public Journal() {}

		/// <summary>
		/// Creates a new instance of the <see cref="Journal"/> class.
		/// </summary>
		public Journal(string serverURL, string userName, string password, bool getComments, string useJournal)
		{
			this.Relations.Add("FK_Users_Comments", this.Users.Columns["ID"],
				this.Comments.Columns["PosterID"], false);
			this.Comments.Columns.Add("PosterUserName", typeof(string), "Parent.User");
			base.Options.AddOptionsRow(serverURL, null, null, null, userName, null,
				DateTime.MinValue, getComments, useJournal);
			SetPassword(password);
			this.version = this.GetType().Assembly.GetName().Version;
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Loads a <see cref="Journal"/> from a specified path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static Journal Load(string path)
		{
			BinaryFormatter bf = new BinaryFormatter();
			Journal j;
			using (StreamReader sr = new StreamReader(path))
				j = (Journal) bf.Deserialize(sr.BaseStream);
			j.Relations.Add("FK_Users_Comments", j.Users.Columns["ID"], j.Comments.Columns["PosterID"], false);
			j.Comments.Columns.Add("PosterUserName", typeof(string), "Parent.User");
			j.path = path;
			return j;
		}
		#endregion

		#region Public Instance Methods
		/// <summary>
		/// Saves the <see cref="Journal"/> to its default path.
		/// </summary>
		public void Save()
		{
			Save(path);
		}

		/// <summary>
		/// Saves the <see cref="Journal"/> to a new path.
		/// </summary>
		/// <param name="path">The path to save the journal.</param>
		public void Save(string path)
		{
			this.path = path;
			this.Comments.Columns.Remove("PosterUserName");
			BinaryFormatter bf = new BinaryFormatter();
			using (StreamWriter sw = new StreamWriter(path))
				bf.Serialize(sw.BaseStream, this);
			this.Comments.Columns.Add("PosterUserName", typeof(string), "Parent.User");
		}

		/// <summary>
		/// Sets the password of the journal.
		/// </summary>
		/// <param name="password">The new password to set.</param>
		public void SetPassword(string password)
		{
			if (base.Options.Rows.Count < 1)
				throw new InvalidOperationException("Cannot set password without options row");
			base.Options[0].HPassword = MD5Hasher.Compute(password);
		}
		#endregion

		#region Internal Instance Methods
		/// <summary>
		/// Gets the maximum mood id stored in the journal.
		/// </summary>
		/// <returns>The maximum mood id stored in the journal.</returns>
		internal int GetMaxMoodID()
		{
			int ret = 0;
			foreach (Journal.MoodsRow mr in this.Moods)
				if (mr.ID > ret)
					ret = mr.ID;
			return ret;
		}

		/// <summary>
		/// Gets the maximum comment id stored in the journal.
		/// </summary>
		/// <returns>The maximum comment id stored in the journal.</returns>
		internal int GetMaxCommentID()
		{
			int ret = 0;
			foreach (Journal.CommentsRow cr in this.Comments)
				if (cr.ID > ret)
					ret = cr.ID;
			return ret;
		}

		/// <summary>
		/// Gets the maximum comment id stored in the journal.
		/// </summary>
		/// <returns>The maximum comment id stored in the journal.</returns>
		internal int GetMaxEventID()
		{
			int ret = 0;
			foreach (Journal.EventsRow er in this.Events)
				if (er.ID > ret)
					ret = er.ID;
			return ret;
		}
		#endregion
		
		#region Protected Instance Methods
		/// <summary>
		/// Protected serialization constructor.
		/// </summary>
		/// <param name="si">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		protected Journal(SerializationInfo si, StreamingContext context) : base()
		{
			int i;
			object[][] optionsRows = (object[][]) si.GetValue("optionsRows", typeof(object[][]));
			object[][] moodsRows = (object[][]) si.GetValue("moodsRows", typeof(object[][]));
			object[][] userpicsRows = (object[][]) si.GetValue("userpicsRows", typeof(object[][]));
			object[][] usersRows = (object[][]) si.GetValue("usersRows", typeof(object[][]));
			object[][] eventsRows = (object[][]) si.GetValue("eventsRows", typeof(object[][]));
			object[][] commentsRows = (object[][]) si.GetValue("commentsRows", typeof(object[][]));

			try
			{
				this.version = (Version) si.GetValue("Version", typeof(Version));
			}
			catch (SerializationException)
			{
				this.version = new Version(0, 0, 0, 0); // 0.9.6 and before didn't serialize versions
			}
			if (this.version != this.GetType().Assembly.GetName().Version)
				Migrate(ref optionsRows, ref moodsRows, ref userpicsRows, ref usersRows, ref eventsRows, ref commentsRows);
				
			for (i = 0; i < optionsRows.Length; ++i)
				base.Options.Rows.Add((object[]) optionsRows[i]);
			for (i = 0; i < moodsRows.Length; ++i)
				this.Moods.Rows.Add((object[]) moodsRows[i]);
			for (i = 0; i < userpicsRows.Length; ++i)
				this.UserPics.Rows.Add((object[]) userpicsRows[i]);
			for (i = 0; i < usersRows.Length; ++i)
				this.Users.Rows.Add((object[]) usersRows[i]);
			for (i = 0; i < eventsRows.Length; ++i)
				this.Events.Rows.Add((object[]) eventsRows[i]);
			for (i = 0; i < commentsRows.Length; ++i)
				this.Comments.Rows.Add((object[]) commentsRows[i]);
		}
		
		protected void Migrate(ref object[][] optionsRows, ref object[][] moodsRows, ref object[][] userpicsRows, ref object[][] usersRows, ref object[][] eventsRows, ref object[][] commentsRows)
		{
			if (this.version < new Version(0, 9, 7, 0))
			{
				for (int i = 0; i < optionsRows.Length; ++i)
					optionsRows[i] = new object[] {optionsRows[i][0], optionsRows[i][1], null, optionsRows[i][2], 
					optionsRows[i][3], optionsRows[i][4], optionsRows[i][5], optionsRows[i][6], null};
				if (eventsRows.Length > 0 && eventsRows[0] != null)
				{
					switch (eventsRows[0].Length)
					{
						case 23:  // 0.9.5, 0.9.6
							for (int i = 0; i < eventsRows.Length; ++i)
								eventsRows[i] = new object[] {eventsRows[i][0], eventsRows[i][1], eventsRows[i][2],
								eventsRows[i][3], eventsRows[i][4], eventsRows[i][5],  eventsRows[i][6], null,
								eventsRows[i][7], eventsRows[i][8], eventsRows[i][9], eventsRows[i][10],
								eventsRows[i][11], eventsRows[i][12], eventsRows[i][13], eventsRows[i][14],
								eventsRows[i][15], eventsRows[i][16], eventsRows[i][17], eventsRows[i][18],
								eventsRows[i][19], eventsRows[i][20], eventsRows[i][21], eventsRows[i][22]};
							break;
						case 22: // 0.9.4.3
							for (int i = 0; i < eventsRows.Length; ++i)
								eventsRows[i] = new object[] {eventsRows[i][0], eventsRows[i][1], eventsRows[i][2],
								eventsRows[i][3], eventsRows[i][4], eventsRows[i][5],  eventsRows[i][6], null,
								eventsRows[i][7], eventsRows[i][8], eventsRows[i][9], eventsRows[i][10],
								eventsRows[i][11], eventsRows[i][12], eventsRows[i][13], eventsRows[i][14],
								eventsRows[i][15], eventsRows[i][16], eventsRows[i][17], eventsRows[i][18],
								eventsRows[i][19], eventsRows[i][20], eventsRows[i][21], null};
							break;
					}
				}
			}
			this.version = this.GetType().Assembly.GetName().Version;
		}
		#endregion

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			int i;
			object[][] optionsRows = new object[base.Options.Rows.Count][];
			object[][] moodsRows = new object[this.Moods.Rows.Count][];
			object[][] userpicsRows = new object[this.UserPics.Rows.Count][];
			object[][] usersRows = new object[this.Users.Rows.Count][];
			object[][] eventsRows = new object[this.Events.Rows.Count][];
			object[][] commentsRows = new object[this.Comments.Rows.Count][];

			for (i = 0; i < base.Options.Rows.Count; ++i)
				optionsRows[i] = base.Options.Rows[i].ItemArray;
			for (i = 0; i < this.Moods.Rows.Count; ++i)
				moodsRows[i] = this.Moods.Rows[i].ItemArray;
			for (i = 0; i < this.UserPics.Rows.Count; ++i)
				userpicsRows[i] = this.UserPics.Rows[i].ItemArray;
			for (i = 0; i < this.Users.Rows.Count; ++i)
				usersRows[i] = this.Users.Rows[i].ItemArray;
			for (i = 0; i < this.Events.Rows.Count; ++i)
				eventsRows[i] = this.Events.Rows[i].ItemArray;
			for (i = 0; i < this.Comments.Rows.Count; ++i)
				commentsRows[i] = this.Comments.Rows[i].ItemArray;

			si.AddValue("Version", this.GetType().Assembly.GetName().Version);
			si.AddValue("optionsRows", optionsRows);
			si.AddValue("moodsRows", moodsRows);
			si.AddValue("userpicsRows", userpicsRows);
			si.AddValue("usersRows", usersRows);
			si.AddValue("eventsRows", eventsRows);
			si.AddValue("commentsRows", commentsRows);
		}
		#endregion

		#region Public Instance Properties
		/// <summary>
		/// Gets the current path of the journal.
		/// </summary>
		public string Path
		{
			get
			{
				return path;
			}
		}

		/// <summary>
		/// Gets the <see cref="Common.Journal.OptionsRow"/> object for this <see cref="Journal"/>.
		/// </summary>
		new public Common.Journal.OptionsRow Options
		{
			get
			{
				return base.Options[0];
			}
		}
		
		/// <summary>
		/// Gets the <see cref="System.Version"/> of EF.ljArchive.Engine that created this journal.
		/// </summary>
		public Version Version
		{
			get
			{
				return version;
			}
		}
		#endregion

		#region Private Instance Fields
		private string path;
		private Version version;
		#endregion
	}
}
