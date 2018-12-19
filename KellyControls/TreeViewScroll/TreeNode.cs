using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KellyControls
{
	/// <summary>
	/// Extending the TreeNode class to add attributes
	/// </summary>
	public class TreeNode : System.Windows.Forms.TreeNode
	{
		#region [ Private Variables ]

		private Dictionary<string, string> _attributes = null;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public Dictionary<string, string> Attributes
		{
			get { return _attributes; }
		}

		public bool Enabled { get; set; }
		public bool Included { get; set; }

		public new TreeNode NextNode
		{
			get { return (TreeNode)base.NextNode; }
		}

		public new TreeNode Parent
		{
			get { return (TreeNode)base.Parent; }
		}		

		public new TreeNode PrevNode
		{
			get { return (TreeNode)base.PrevNode; }
		}

		public object StoredObject { get; set; }

		#endregion [ Properties ]

		#region [ Constructors ]

		public TreeNode()
			: base()
		{
			_attributes = new Dictionary<string, string>();
		}

		public TreeNode(string text)
			: base(text)
		{ }

		public TreeNode(string text, TreeNode[] children)
			: base(text, children)
		{
			_attributes = new Dictionary<string, string>();
		}

		public TreeNode(string text, int imageIndex, int selectedImageIndex)
			: base(text, imageIndex, selectedImageIndex)
		{
			_attributes = new Dictionary<string, string>();
		}

		public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
			: base(text, imageIndex, selectedImageIndex, children)
		{
			_attributes = new Dictionary<string, string>();
		}

		protected TreeNode(SerializationInfo serializationInfo, StreamingContext context)
			: base(serializationInfo, context)
		{
			_attributes = new Dictionary<string, string>();
		}

		#endregion [ Constructors ]

	}
}
