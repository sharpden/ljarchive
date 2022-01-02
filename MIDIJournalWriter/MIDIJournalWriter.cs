using System;
using System.Collections;
using EF.ljArchive.Common;
using EF.SimpleMIDI;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EF.ljArchive.MIDIJournalWriter
{
	#region MIDIJournalWriterSettings
	[Serializable()]
	public class MIDIJournalWriterSettings
	{
		public MIDIJournalWriterSettings(bool pauseBetweenEntries, int program, int noteLength)
		{
			this.pauseBetweenEntries = pauseBetweenEntries;
			this.program = program;
			this.noteLength = noteLength;
		}

		[Description("Insert an brief pause to mark a new journal entry.")]
		[DefaultValue(true)]
		public bool PauseBetweenEntries
		{
			get
			{
				return pauseBetweenEntries;
			}
			set
			{
				pauseBetweenEntries = value;
			}
		}

		[Description("Program (instrument sound) to use for this MIDI.")]
		[DefaultValue(12)]
		public int Program
		{
			get
			{
				return program;
			}
			set
			{
				if (value > 0x7F)
					throw new ArgumentException("Program cannot be greater than 127", "Program");
				program = value;
			}
		}

		[Description("Duration to play each note (in ticks).")]
		[DefaultValue(50)]
		public int NoteLength
		{
			get
			{
				return noteLength;
			}
			set
			{
				noteLength = value;
			}
		}

		private bool pauseBetweenEntries;
		private int program;
		private int noteLength;
	}
	#endregion

	/// <summary>
	/// Writes a journal to a stream in MIDI.
	/// </summary>
	public class MIDIJournalWriter : IJournalWriter
	{
		public MIDIJournalWriter()
		{
			settings = new MIDIJournalWriterSettings(true, 12, 50);
			r = new Regex("<.*?>");
		}

		#region IJournalWriter Members
		public void WriteJournal(System.IO.Stream s, Journal j, int[] eventIDs, int[] commentIDs, bool header, bool footer)
		{
			MIDIFile m = new MIDIFile();
			Track t = new Track();
			t.Events.Add(new TimeSignature(0, 4, 4, 24, 8));
			t.Events.Add(new ProgramChange(0, 1, (byte) settings.Program));
			foreach (Journal.EventsRow er in j.Events)
			{
				if (eventIDs != null && !((IList) eventIDs).Contains(er.ID))
					continue;
				string text = string.Empty;
				char lastChar = (char) 32;
				if (!er.IsBodyNull())
					text += r.Replace(er.Body, string.Empty);
				if (!er.IsSubjectNull())
					text += " " + r.Replace(er.Subject, string.Empty);
				foreach (Journal.CommentsRow cr in j.Comments.Select("JItemID = " + er.ID.ToString()))
				{
					if (commentIDs != null && !((IList) commentIDs).Contains(cr.ID))
						continue;
					if (!cr.IsBodyNull())
						text += r.Replace(cr.Body, string.Empty);
					if (!cr.IsSubjectNull())
						text += " " + r.Replace(cr.Subject, string.Empty);
				}
				foreach (char c in text.ToCharArray())
				{
					if (c > 32 && c < 160)
					{
						t.Events.Add(new NoteOn(settings.NoteLength, 1, (byte) (c - 32), 127));
						t.Events.Add(new NoteOff(settings.NoteLength, 1, (byte) (c - 32), 127));
						lastChar = c;
					}
				}
				t.Events.Add(new NoteOn(settings.NoteLength, 1, (byte) (lastChar - 32), 0));
				if (settings.PauseBetweenEntries)
					t.Events.Add(new AllNotesOff(settings.NoteLength * 100 > 1000 ? 1000 : settings.NoteLength * 100, 1));
			}
			t.Events.Add(new EndofTrack(settings.NoteLength));
			m.Chunks.Add(t);
			m.WriteMIDI(s);
		}

		public string Filter
		{
			get { return "MIDI files (*.mid)|*.mid|All files (*.*)|*.*"; }
		}

		public object Settings
		{
			get { return settings; }
			set
			{
				if (value is MIDIJournalWriterSettings)
					settings = (MIDIJournalWriterSettings) value;
				else
					throw new ArgumentException("Settings must be of type MIDIJournalWriterSettings", "Settings");

			}
		}

		public string Description
		{
			get
			{
				return "Exports to MIDI, by mapping characters to notes.  Useful for very "
					+ "little besides answering the question 'What would my journal sound like as a MIDI?'.";
			}
		}

		public string Author
		{
			get { return "Erik Frey"; }
		}

		public string Name
		{
			get { return "MIDI Writer"; }
		}

		public string URL
		{
			get { return "http://ljarchive.sourceforge.net/"; }
		}

		public Version Version
		{
			get { return new Version(0, 9, 3, 1); }
		}
		#endregion

		MIDIJournalWriterSettings settings;
		Regex r;
	}
}
