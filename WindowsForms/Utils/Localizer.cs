using System;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;

namespace EF.ljArchive.WindowsForms
{
	/// <summary>
	/// Summary description for Localizer.
	/// </summary>
	public class Localizer
	{
		private Localizer(){}

		static Localizer()
		{
			rm = new ResourceManager("EF.ljArchive.WindowsForms.res.Strings", Assembly.GetExecutingAssembly());
		}

		static public string GetString(string name)
		{
			return rm.GetString(name);
		}

		static public string GetString(System.Type source, string name)
		{
			return rm.GetString(source.Name + "." + name);
		}

		static public void SetControlText(System.Type source, params Control[] controls)
		{
			foreach (Control c in controls)
			{
				if (c.GetType() != source)
					c.Text = GetString(source, c.Name + ".Text");
				else
					c.Text = GetString(source, "Text");
			}
		}

		static private ResourceManager rm;
	}
}
