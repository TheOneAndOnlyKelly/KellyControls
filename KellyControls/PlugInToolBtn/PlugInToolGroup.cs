using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KellyControls.PlugInToolBtn.Interfaces;

namespace KellyControls.PlugInToolBtn
{
	#region [ Class PlugInToolGroup ]

	// http://www.c-sharpcorner.com/UploadFile/rmcochran/plug_in_architecture09092007111353AM/plug_in_architecture.aspx
	/// <summary>
	/// This class wraps the PlugIn
	/// </summary>
	public class PlugInToolGroup : CommonClasses.Disposable, IItem
	{
		#region [ Private Variables ]

		private IPlugInToolGroup _toolGroup = null;
		private PlugInToolStripButton _button = null;
		private ToolStrip _toolBox = null;
		private ToolStrip _childToolBox = null;

		#endregion [ Private Variables ]

		#region [ Properties ]

		/// <summary>
		/// Indicates if this ToolGroup has been added to the main ToolBox.
		/// </summary>
		[DebuggerHidden()]
		public bool Added { get; set; }

		/// <summary>
		/// Button that represents the tool
		/// </summary>
		public PlugInToolStripButton Button
		{
			get
			{
				if (_button == null)
					_button = CreateButton();
				return _button;
			}
			set
			{
				_button = value;
				_button.PlugInToolGroup = this;
			}
		}

		/// <summary>
		/// Unique ID number
		/// </summary>
		[DebuggerHidden()]
		public int ID { get; set; }

		/// <summary>
		/// Index of this object's position within its parent ToolBox.
		/// </summary>
		[DebuggerHidden()]
		public int Index { get; set; }

		/// <summary>
		/// Name of the this ToolGroup
		/// </summary>
		[DebuggerHidden()]
		public string Name
		{
			get { return this.ToolGroup.Name; }
		}

		/// <summary>
		/// Returns the name of this ToolGroup with whitespace removed.
		/// </summary>
		[DebuggerHidden()]
		public string SafeName
		{
			get { return Regex.Replace(this.Name, @"[^\w]", "", RegexOptions.None); }
		}

		/// <summary>
		/// Links to the ToolGroup contained herein
		/// </summary>
		[DebuggerHidden()]
		public IPlugInToolGroup ToolGroup
		{
			get { return _toolGroup; }
		}

		/// <summary>
		/// ToolTipText of this ToolGroup
		/// </summary>
		[DebuggerHidden()]
		public string ToolTipText
		{
			get { return this.ToolGroup.ToolTipText; }
		}

		/// <summary>
		/// ToolStrip that this ToolGroup appears on
		/// </summary>
		[DebuggerHidden()]
		public ToolStrip ToolBox
		{
			get { return _toolBox; }
			set { _toolBox = value; }
		}

		/// <summary>
		/// Image for the button for this ToolGroup. Taken from the ToolGroup's active Tool
		/// </summary>
		[DebuggerHidden()]
		public Bitmap ToolBoxImage
		{
			get { return _toolGroup.ToolBoxImage; }
		}

		/// <summary>
		/// ToolStrip control that hold the child Tools.
		/// </summary>
		public ToolStrip ChildToolBox
		{
			set { _childToolBox = value; }
			get
			{
				if (_childToolBox == null)
				{
					// Create the child toolBox
					_childToolBox = new ToolStrip()
					{
						Dock = DockStyle.None,
						LayoutStyle = ToolStripLayoutStyle.Flow,
						Name = "SubMenu_" + this.SafeName,
						Padding = new Padding(3),
						Size = new Size(121, 29),
						Visible = true
					};
				}
				return _childToolBox;
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public PlugInToolGroup()
		{ }

		public PlugInToolGroup(IPlugInToolGroup toolGroup)
			: this()
		{
			_toolGroup = toolGroup;
			_toolGroup.SelectedIndexChanged += new EventHandler(this.ToolGroup_SelectedIndexChanged);
			this.ID = _toolGroup.ID;
		}

		public PlugInToolGroup(IPlugInToolGroup toolGroup, int id)
			: this(toolGroup)
		{
			this.ID = id;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		/// <summary>
		/// Close down the flyout menu.
		/// </summary>
		public void Close()
		{
			this.ChildToolBox.Visible = false;
		}

		private PlugInToolStripButton CreateButton()
		{
			_button = new PlugInToolStripButton()
			{
				CheckOnClick = true,
				DisplayStyle = ToolStripItemDisplayStyle.Image,
				Name = this.ToString() + "_Tool",
				Size = new Size(30, 20),
				Text = this.ToString(),
				PlugInToolGroup = this,
				Image = Flyout(this.ToolBoxImage)
			};
			return _button;
		}

		/// <summary>
		/// Clean up all child objects here, unlink all events and dispose
		/// </summary>
		protected override void DisposeChildObjects()
		{
			base.DisposeChildObjects();

			if (_toolGroup != null)
			{
				_toolGroup.Dispose();
				_toolGroup = null;
			}
			try
			{
				if (_childToolBox != null)
				{
					_childToolBox.Dispose();
					_childToolBox = null;
				}
			}
			catch
			{ }
			_button = null;
			_toolBox = null;
		}

		/// <summary>
		/// Combines a tool image with the flyout image.
		/// </summary>
		private Bitmap Flyout(Bitmap source)
		{
			if (source == null)
				return null;
			Bitmap FlyoutImage = Properties.Resources.annotation_flyout;
			if (FlyoutImage != null)
			{
				Graphics g = Graphics.FromImage(source);
				g.DrawImage(FlyoutImage, new Rectangle(0, 0, 16, 16));
				g.Dispose();
			}
			return source;
		}

		/// <summary>
		/// Determines the location of a ToolStripItem, since we cannot determine the location from a property on the control.
		/// </summary>
		/// <param name="button">ToolStripItem to examine</param>
		private Point GetLocationOfTool(ToolStripItem button, int offsetX, int offsetY)
		{
			ToolStrip Parent = _toolBox;
			Point Location = Parent.Location;
			int Padding = Parent.Padding.Top + Parent.Padding.Bottom;
			int Y = Location.Y + Parent.Padding.Top;
			foreach (ToolStripItem Item in Parent.Items)
			{
				if (Item == button)
					break;
				Y += Item.Size.Height + Item.Margin.Top + Item.Margin.Bottom;
			}

			return new Point(Location.X + Parent.Padding.Left + offsetX, Y + offsetY);
		}

		public void Initialize()
		{
			_toolGroup.Initialize();
		}

		/// <summary>
		/// Position the child toolbox to line up with the button on it's toolBox.
		/// </summary>
		public void LineUpToolBox()
		{
			_childToolBox.Location = GetLocationOfTool(_button, _toolBox.Width + 1, 0);
		}

		/// <summary>
		///  Closing out the program, disconnect objects and have child objects do the same.
		/// </summary>
		public void ShutDown()
		{
			if (_toolGroup != null)
			{
				_toolGroup.SelectedIndexChanged -= this.ToolGroup_SelectedIndexChanged;
				_toolGroup.ShutDown();
			}
		}

		/// <summary>
		/// Return the name.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>
		/// Combines an image with the flyout annotation image
		/// </summary>
		private Bitmap AddFlyoutAnnotation(Bitmap image)
		{
			if (image == null)
				throw new ArgumentNullException("image cannot be null.");

			Bitmap Copy = new Bitmap(image.Width, image.Height);
			Bitmap annotationImage = Properties.Resources.annotation_flyout;

			int Width = annotationImage.Width;
			int Height = annotationImage.Height;
			int X = Copy.Width - Width;
			int Y = Copy.Height - Height;

			using (Graphics g = Graphics.FromImage(Copy))
			{
				g.Clear(Color.Transparent);
				g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
				g.DrawImage(annotationImage, new Rectangle(X, Y, Width, Height));
			}
			return Copy;
		}

		#endregion [ Methods ]

		#region [ Events ]

		/// <summary>
		/// Fires when on the button on the child toolbox is clicked, sets the current tool to that indicated by the button and makes this the active tool for the application.
		/// </summary>
		private void ToolGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			PlugInTool pTool = _toolGroup[_toolGroup.SelectedIndex];
			this.Button.PlugInTool = pTool;
			this.Button.Image = AddFlyoutAnnotation(pTool.ToolBoxImage);
			this.Button.ToolTipText = pTool.ToolTipText;
			this.ChildToolBox.Visible = false;
			pTool.Tool.IsSelected = true;

			// Make sure only the selected button is clicked
			foreach (PlugInToolStripButton tButton in _childToolBox.Items)
			{
				tButton.Checked = (tButton == pTool.Button);
			}
		}

		#endregion [ Events ]
	}

	#endregion [ Class PlugInToolGroup ]

	#region [ Class PlugInToolGroupList ]

	public class PlugInToolGroupList : CollectionBase, IList<PlugInToolGroup>, ICollection<PlugInToolGroup>
	{
		public PlugInToolGroupList()
		{ }

		public void Add(PlugInToolGroup item)
		{
			List.Add(item);
		}

		public bool Contains(PlugInToolGroup item)
		{
			return List.Contains(item);
		}

		public void CopyTo(PlugInToolGroup[] array, int arrayIndex)
		{
			List.CopyTo(array, arrayIndex);
		}

		public int IndexOf(PlugInToolGroup item)
		{
			return List.IndexOf(item);
		}

		public void Insert(int index, PlugInToolGroup item)
		{
			List.Insert(index, item);
		}

		public bool IsReadOnly
		{
			get { return List.IsReadOnly; }
		}

		public PlugInToolGroup this[int index]
		{
			get { return (PlugInToolGroup)List[index]; }
			set { List[index] = value; }
		}

		public void Remove(PlugInToolGroup item)
		{
			List.Remove(item);
		}

		bool ICollection<PlugInToolGroup>.Remove(PlugInToolGroup item)
		{
			if (!List.Contains(item))
				return false;
			List.Remove(item);
			return true;
		}

		public PlugInToolGroup Where(int id)
		{
			foreach (PlugInToolGroup item in List)
				if (item.ID == id)
					return item;
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)GetEnumerator();
		}

		IEnumerator<PlugInToolGroup> IEnumerable<PlugInToolGroup>.GetEnumerator()
		{
			return new PlugInToolGroupListEnumerator(List.GetEnumerator());
		}

		#region [ Class PlugInToolGroupListEnumerator ]

		private class PlugInToolGroupListEnumerator : IEnumerator<PlugInToolGroup>
		{
			private IEnumerator _enumerator;

			public PlugInToolGroupListEnumerator(IEnumerator enumerator)
			{
				_enumerator = enumerator;
			}

			public PlugInToolGroup Current
			{
				get { return (PlugInToolGroup)_enumerator.Current; }
			}

			object IEnumerator.Current
			{
				get { return _enumerator.Current; }
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			public void Reset()
			{
				_enumerator.Reset();
			}

			public void Dispose()
			{ }
		}

		#endregion [ Class PlugInToolGroupListEnumerator ]
	}

	#endregion [ Class PlugInToolGroupList ]
}
