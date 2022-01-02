using System;
using System.Collections;

namespace EF.ljArchive.Engine.HTML
{
	#region InfixEnumerator
	/// <summary>
	/// Enumerates through a <see cref="CommentNode"/> tree in a fast infix fashion.
	/// </summary>
	/// <remarks>Infix means return my node first, then any children nodes.</remarks>
	internal class InfixEnumerator : IEnumerator
	{
		public InfixEnumerator(CommentNode root)
		{
			this.root = root;
			current = null;
			index = -1;
			ife = null;
		}

		public void Reset()
		{
			current = null;
			index = -1;
			ife = null;
		}

		public object Current
		{
			get
			{
				return current;
			}
		}

		public bool MoveNext()
		{
			if (index == -1 && root.Comment != null)
			{
				index = 0;
				current = root;
				return true;
			}
			if (index == -1)
				index = 0;
			if (root.Children != null && root.Children.Count > 0)
			{
				while (index < root.Children.Count + 1)
				{
					if (ife != null && ife.MoveNext())
					{
						current = ife.current;
						return true;
					}
					if (index < root.Children.Count)
						ife = (InfixEnumerator)
							((CommentNode) root.Children[index]).GetInfixEnumerator().GetEnumerator();
					++index;
				}
			}
			return false;
		}
		
		public CommentNode Root
		{
			get
			{
				return root;
			}
			set
			{
				root = value;
			}
		}

		private InfixEnumerator ife;
		int index;
		private CommentNode root;
		private CommentNode current;
	}
	#endregion

	#region InfixEnumerable
	/// <summary>
	/// Provides an <see cref="InfixEnumerator"/>.
	/// </summary>
	internal class InfixEnumerable : IEnumerable
	{
		public InfixEnumerable(CommentNode cn)
		{
			this.cn = cn;
		}

		public IEnumerator GetEnumerator()
		{
			return new InfixEnumerator(cn);
		}

		private CommentNode cn;
	}
	#endregion

	#region CommentNode
	/// <summary>
	/// Provides a method for building a tree of <see cref="EF.ljArchive.Common.Journal.CommentsRow"/> objects,
	/// based on their ID's.
	/// </summary>
	internal class CommentNode
	{
		public CommentNode(Journal.CommentsRow comment, ArrayList children)
		{
			this.depth = 0;
			this.comment = comment;
			this.children = children;
		}

		public void AddChildComment(Journal.CommentsRow comment)
		{
			CommentNode tc = new CommentNode(comment, null);
			tc.depth = this.depth + 1;
			if (children == null)
				children = new ArrayList();
			children.Add(tc);
		}

		public CommentNode Find(int ID)
		{
			if (comment != null && comment.ID == ID)
				return this;
			if (children != null)
			{
				foreach (CommentNode tc in children)
				{
					CommentNode ret = tc.Find(ID);
					if (ret != null)
						return ret;
				}
			}
			return null;
		}

		public IEnumerable GetInfixEnumerator()
		{
			return new InfixEnumerable(this);
		}

		public int Depth
		{
			get { return depth; }
		}

		public Journal.CommentsRow Comment
		{
			get { return comment; }
			set { comment = value; }
		}

		public ArrayList Children
		{
			get { return children; }
			set { children = value; }
		}

		private int depth;
		private Journal.CommentsRow comment;
		private ArrayList children;
	}
	#endregion
}
