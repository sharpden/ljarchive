// ------------------------------------------------------------
// Writer, WYSIWYG editor for HTML
// Copyright (c) 2002-2003 Lutz Roeder. All rights reserved.
// http://www.aisto.com/roeder
// ------------------------------------------------------------
// Based on HTML editor control code
// Copyright (c) 2002-2003 Nikhil Kothari. All rights reserved.
// http://www.nikhilk.net
// ------------------------------------------------------------
namespace Writer.Html
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Collections;
    using System.Reflection;
    using Writer.Html;

    [DesignOnly(true)]
    public class HtmlElement 
    {
		private NativeMethods.IHTMLElement peer;
		private HtmlControl owner;

		internal HtmlElement(NativeMethods.IHTMLElement peer, HtmlControl owner)
		{
			Debug.Assert(peer != null);
			this.peer = peer;
			this.owner = owner;

		
		}

        [Browsable(false)]
        public string InnerHtml 
        {
            get 
            {
                try 
                {
                    return this.peer.GetInnerHTML();
                }
                catch (Exception e) 
                {
                    Debug.Fail(e.ToString(), "Could not get Element InnerHTML");

                    return string.Empty;
                }
            }

			set 
            {
                try 
                {
                    this.peer.SetInnerHTML(value);
                }
                catch (Exception e) 
                {
                    Debug.Fail(e.ToString(), "Could not set Element InnerHTML");
                }
            }
        }

        [Browsable(false)]
        public string OuterHtml 
        {
            get 
            {
                try 
                {
                    return this.peer.GetOuterHTML();
                }
                catch (Exception e) 
                {
                    Debug.Fail(e.ToString(), "Could not get Element OuterHTML");

                    return String.Empty;
                }
            }
            set 
            {
                try 
                {
                    this.peer.SetOuterHTML(value);
                }
                catch (Exception exception) 
                {
                    Debug.Fail(exception.ToString(), "Could not set Element OuterHTML");
                }
            }
        }

        [Browsable(false)]
        public string Name 
        {
            get 
            {
                try 
                {
                    return this.peer.GetTagName();
                }
                catch (Exception exception) 
                {
                    Debug.Fail(exception.ToString(), "Could not get Element TagName" + exception.ToString());

                    return string.Empty;
                }
            }
        }

        internal NativeMethods.IHTMLElement Peer 
        {
            get { return this.peer; }
        }

        public object GetAttribute(string attribute) 
        {
            try 
            {
                object[] obj = new object[1];

                this.peer.GetAttribute(attribute, 0, obj);

                object o = obj[0];
                if (o is DBNull) 
                {
                    o = null;
                }
                return o;
            }
            catch (Exception e) 
            {
                Debug.Fail(e.ToString(), "Call to IHTMLElement::GetAttribute failed in Element");

                return null;
            }
        }

		/*
        protected internal bool GetBooleanAttribute(string attribute) 
        {
            object o = GetAttribute(attribute);

            if (o == null) 
            {
                return false;
            }

            Debug.Assert(o is bool, "Attribute " + attribute + " is not of type Boolean");
            if (o is bool) 
            {
                return (bool)o;
            }

            return false;
        }

        protected internal Color GetColorAttribute(string attribute) 
        {
            string color = GetStringAttribute(attribute);
            
            if (color.Length == 0) 
            {
                return Color.Empty;
            }
            else 
            {
                return ColorTranslator.FromHtml(color);
            }
        }

        protected internal Enum GetEnumAttribute(string attribute, Enum defaultValue) 
        {
            Type enumType = defaultValue.GetType();

            object o = GetAttribute(attribute);
            if (o == null) 
            {
                return defaultValue;
            }

            Debug.Assert(o is string, "Attribute " + attribute + " is not of type String");
            string s = o as string;
            if ((s == null) || (s.Length == 0)) 
            {
                return defaultValue;
            }

            try 
            {
                return (Enum)Enum.Parse(enumType, s, true);
            }
            catch 
            {
                return defaultValue;
            }
        }

        protected internal int GetIntegerAttribute(string attribute, int defaultValue) 
        {
            object o = GetAttribute(attribute);

            if (o == null) 
            {
                return defaultValue;
            }
            if (o is int) 
            {
                return (int)o;
            }
            if (o is short) 
            {
                return (short)o;
            }
            if (o is string) 
            {
                string s = (string)o;
                if ((s.Length != 0) && (Char.IsDigit(s[0]))) 
                {
                    try 
                    {
                        return Int32.Parse((string)o);
                    }
                    catch 
                    {
                    }
                }
            }

            Debug.Fail("Attribute " + attribute + " is not an integer");
            return defaultValue;
        }
		*/

        public HtmlElement GetChild(int index) 
        {
            NativeMethods.IHTMLElementCollection children = (NativeMethods.IHTMLElementCollection)this.peer.GetChildren();
            NativeMethods.IHTMLElement child = (NativeMethods.IHTMLElement)children.Item(null, index);

            return new HtmlElement(child, this.owner);
        }

        public HtmlElement GetChild(string name) 
        {
            NativeMethods.IHTMLElementCollection children = (NativeMethods.IHTMLElementCollection)this.peer.GetChildren();
            NativeMethods.IHTMLElement child = (NativeMethods.IHTMLElement)children.Item(name, null);

            return new HtmlElement(child, this.owner);
        }

		public HtmlElement Parent
		{
			get
			{
				NativeMethods.IHTMLElement parent = (NativeMethods.IHTMLElement)this.peer.GetParentElement();
				return new HtmlElement(parent, this.owner);
			}
		}

        protected string GetRelativeUrl(string absoluteUrl) 
        {
            if ((absoluteUrl == null) || (absoluteUrl.Length == 0)) 
            {
                return String.Empty;
            }
            
            string s = absoluteUrl;
            if (this.owner != null) 
            {
                string ownerUrl = this.owner.Url;

                if (ownerUrl.Length != 0) 
                {
                    try 
                    {
                        Uri ownerUri = new Uri(ownerUrl);
                        Uri imageUri = new Uri(s);

                        s = ownerUri.MakeRelative(imageUri);
                    }
                    catch 
                    {
                    }
                }
            }

            return s;
        }

        protected internal string GetStringAttribute(string attribute) 
        {
            return GetStringAttribute(attribute, String.Empty);
        }

        protected internal string GetStringAttribute(string attribute, string defaultValue) 
        {
            object o = GetAttribute(attribute);

            if (o == null) 
            {
                return defaultValue;
            }
            if (o is string) 
            {
                return (string)o;
            }

            return defaultValue;
        }

        public void RemoveAttribute(string attribute) 
        {
            try 
            {
                this.peer.RemoveAttribute(attribute, 0);
            }
            catch (Exception e) 
            {
                Debug.Fail(e.ToString(), "Call to IHTMLElement::RemoveAttribute failed in Element");
            }
        }

        public void SetAttribute(string attribute, object value) 
        {
            try 
            {
                this.peer.SetAttribute(attribute, value, 0);
            }
            catch (Exception e) 
            {
                Debug.Fail(e.ToString(), "Call to IHTMLElement::SetAttribute failed in Element");
            }
        }

        protected internal void SetBooleanAttribute(string attribute, bool value) 
        {
            if (value) 
            {
                SetAttribute(attribute, true);
            }
            else 
            {
                RemoveAttribute(attribute);
            }
        }

        protected internal void SetColorAttribute(string attribute, Color value) 
        {
            if (value.IsEmpty) 
            {
                RemoveAttribute(attribute);
            }
            else 
            {
                SetAttribute(attribute, ColorTranslator.ToHtml(value));
            }
        }

        protected internal void SetEnumAttribute(string attribute, Enum value, Enum defaultValue) 
        {
            Debug.Assert(value.GetType().Equals(defaultValue.GetType()));

            if (value.Equals(defaultValue)) 
            {
                RemoveAttribute(attribute);
            }
            else 
            {
                SetAttribute(attribute, value.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected internal void SetIntegerAttribute(string attribute, int value, int defaultValue) 
        {
            if (value == defaultValue) 
            {
                RemoveAttribute(attribute);
            }
            else 
            {
                SetAttribute(attribute, value);
            }
        }

		protected internal void SetStringAttribute(string attribute, string value) 
        {
            this.SetStringAttribute(attribute, value, String.Empty);
        }

        protected internal void SetStringAttribute(string attribute, string value, string defaultValue) 
        {
            if ((value == null) || value.Equals(defaultValue)) 
            {
                this.RemoveAttribute(attribute);
            }
            else 
            {
                this.SetAttribute(attribute, value);
            }
        }

        public override string ToString() 
        {
            if (this.peer != null) 
            {
                try 
                {
                    return "<" + this.peer.GetTagName() + ">";
                }
                catch 
                {
                }
            }
            return string.Empty;
        }
    }
}
