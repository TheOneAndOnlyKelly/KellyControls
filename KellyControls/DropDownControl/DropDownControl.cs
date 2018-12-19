using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace KellyControls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\DropDownControl\DropDownControl.bmp")]
	public partial class DropDownControl : UserControl
	{

		#region [ Enums ]

		public enum eDockSide
		{
			Left,
			Right
		}

		public enum eDropState
		{
			Closed,
			Closing,
			Dropping,
			Dropped
		}

		#endregion [ Enums ]

		#region [ Private Variables ]

		private Size _anchorSize = new Size(52, 21);
		private bool _closedWhileInControl;
		private bool _designView = true;
		private eDockSide _dockSide = eDockSide.Left;
		private DropDownContainer _dropContainer;
		protected Control _dropDownItem;
		private eDropState _dropState;
		protected bool _mousePressed;
		protected Size _storedSize;
		private string _text;
		private bool _useAnimation = false;
		private Size _dropDownSize = new Size(185, 300);
		private bool _showDropDownButton = true;
		private int _buttonWidth = 18;
		protected ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDown;

		#endregion [ Private Variables ]

		#region [ Properties ]

		[DefaultValue(typeof(Size), "52, 21")]
		public Size AnchorSize
		{
			get { return _anchorSize; }
			set
			{
				_anchorSize = value;
				this.Invalidate();
			}
		}

		[Browsable(false)]
		public Rectangle AnchorClientBounds
		{
			get
			{
				if (_showDropDownButton)
					return new Rectangle(0, 0, this.ClientRectangle.Width - _buttonWidth, ClientRectangle.Height);
				else
					return new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
			}
		}

		[DefaultValue(typeof(bool), "False")]
		public bool AnimateDropDownBox
		{
			get { return _useAnimation; }
			set { _useAnimation = value; }
		}

		protected virtual bool CanDrop
		{
			get
			{
				if (_dropContainer != null)
					return false;

				if (_dropContainer == null && _closedWhileInControl)
				{
					_closedWhileInControl = false;
					return false;
				}

				return !_closedWhileInControl;
			}
		}

		[DefaultValue(false)]
		protected bool DesignView
		{
			get { return _designView; }
			set
			{
				if (_designView == value)
					return;

				_designView = value;
				if (_designView)
				{
					this.Size = _storedSize;
				}
				else
				{
					_storedSize = this.Size;
					this.Size = _anchorSize;
				}
			}
		}

		[DefaultValue(typeof(eDockSide), "Left")]
		public eDockSide DockSide
		{
			get { return _dockSide; }
			set { _dockSide = value; }
		}

		[DefaultValue(typeof(Size), "185, 300"), Description("Size of the drop down area.")]
		public Size DropdownSize
		{
			get { return _dropDownSize; }
			set { _dropDownSize = value; }
		}

		protected eDropState DropState
		{
			get { return _dropState; }
		}

		[DefaultValue(typeof(ComboBoxStyle), "DropDown"), Description("Controls the appearance of the drop down area.")]
		public virtual ComboBoxStyle DropDownStyle
		{
			get { return _dropDownStyle; }
			set
			{
				_dropDownStyle = value;
				_showDropDownButton = (_dropDownStyle != ComboBoxStyle.Simple);
				this.Refresh();
			}
		}

		public override string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				this.Invalidate();
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public DropDownControl()
		{
			InitializeComponent();
			this._storedSize = this.Size;
			this.Text = this.Name;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		public void CloseDropDown()
		{
			if (_dropContainer != null)
			{
				_dropState = eDropState.Closing;

				if (_useAnimation)
				{
					_dropContainer.Freeze = true;
					tmrAnimate.Enabled = true;
				}
				else
				{
					_dropContainer.Freeze = false;
					_dropContainer.Close();
					_dropState = eDropState.Closed;
				}
			}
		}

		public void FreezeDropDown(bool remainVisible)
		{
			if (_dropContainer != null)
			{
				_dropContainer.Freeze = true;
				if (!remainVisible)
					_dropContainer.Visible = false;
			}
		}

		protected virtual Rectangle GetDropDownBounds()
		{
			Size inflatedDropSize = _dropDownSize;

			Rectangle screenBounds = (_dockSide == eDockSide.Left) ?
				  new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.X, this.Bounds.Bottom)), inflatedDropSize)
				: new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.Right - _dropDownItem.Width, this.Bounds.Bottom)), inflatedDropSize);

			Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

			// Make sure we're completely in the top-left working area
			if (screenBounds.X < workingArea.X)
				screenBounds.X = workingArea.X;
			if (screenBounds.Y < workingArea.Y)
				screenBounds.Y = workingArea.Y;

			// Make sure we're not extended past the working area's right /bottom edge
			if ((screenBounds.Right > workingArea.Right) && (workingArea.Width > screenBounds.Width))
				screenBounds.X = workingArea.Right - screenBounds.Width;
			if ((screenBounds.Bottom > workingArea.Bottom) && (workingArea.Height > screenBounds.Height))
				screenBounds.Y = workingArea.Bottom - screenBounds.Height;

			return screenBounds;
		}

		private ComboBoxState GetState()
		{
			if (_mousePressed || _dropContainer != null)
				return ComboBoxState.Pressed;
			else
				return ComboBoxState.Normal;
		}

		public void InitializeDropDown(Control dropDownItem)
		{
			if (_dropDownItem != null)
				throw new Exception("The drop down item has already been implemented!");
			_designView = false;
			_dropState = eDropState.Closed;
			this.Size = _anchorSize;

			// Removes the dropDown item from the controls list so it 
			// Won't be seen until the drop-down window is active
			if (this.Controls.Contains(dropDownItem))
				this.Controls.Remove(dropDownItem);

			_dropDownItem = dropDownItem;
		}

		protected virtual void OpenDropDown()
		{
			if (_dropDownItem == null)
				return;
				//throw new NotImplementedException("The drop down item has not been initialized!  Use the InitializeDropDown() method to do so.");

			if (!CanDrop)
				return;

			_dropContainer = new DropDownContainer(_dropDownItem, _dropDownSize)
			{
				Bounds = GetDropDownBounds()
			};
			_dropContainer.DropStateChange += new DropDownContainer.DropWindowArgs(DropContainer_DropStateChange);
			_dropContainer.FormClosed += new FormClosedEventHandler(DropContainer_Closed);
			this.ParentForm.Move += new EventHandler(ParentForm_Move);
			_dropState = eDropState.Dropping;

			if (_useAnimation)
			{
				tmrAnimate.Enabled = true;
				_dropContainer.Height = 0;
				_dropContainer.Show(this);
			}
			else
			{
				_dropContainer.Show(this);
				_dropState = eDropState.Dropped;
			}

			this.Invalidate();
		}

		public void UnFreezeDropDown()
		{
			if (_dropContainer != null)
			{
				_dropContainer.Freeze = false;
				if (!_dropContainer.Visible)
					_dropContainer.Visible = true;
			}
		}

		#endregion [ Methods ]

		#region [ Events ]

		#region [ Event Declarations ]

		public event EventHandler PropertyChanged;

		#endregion [ Event Declarations ]

		#region [ Event Triggers ]

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			_mousePressed = true;
			OpenDropDown();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (_dropState == eDropState.Closing)
				_dropState = eDropState.Dropping;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_mousePressed = false;
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (!_showDropDownButton)
				return;

			var Rectangle = new Rectangle(ClientRectangle.Width - _buttonWidth, 0, _buttonWidth, ClientRectangle.Height);

			if (ComboBoxRenderer.IsSupported)
				ComboBoxRenderer.DrawDropDownButton(e.Graphics, Rectangle, GetState());
			else
				ControlPaint.DrawComboButton(e.Graphics, Rectangle, (this.Enabled) ? ButtonState.Normal : ButtonState.Inactive);
		}

		protected void OnPropertyChanged()
		{
			PropertyChanged?.Invoke(null, null);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if (_designView)
				_storedSize = this.Size;

			_anchorSize.Width = this.Width;

			if (!_designView)
				_anchorSize.Height = this.Height;
		}

		#endregion [ Event Triggers ]

		#region [ Event Delegates ]

		void DropDownItem_MouseEnter(object sender, EventArgs e)
		{
			OnMouseEnter(e);
		}

		void DropContainer_DropStateChange(DropDownControl.eDropState state)
		{
			_dropState = state;
		}

		void DropContainer_Closed(object sender, FormClosedEventArgs e)
		{
			if (!_dropContainer.IsDisposed)
			{
				_dropContainer.DropStateChange -= DropContainer_DropStateChange;
				_dropContainer.FormClosed -= DropContainer_Closed;
				this.ParentForm.Move -= ParentForm_Move;
				_dropContainer.Dispose();
			}
			_dropContainer = null;
			_closedWhileInControl = (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position));
			_dropState = eDropState.Closed;
			tmrAnimate.Enabled = false;
			this.Invalidate();
		}

		void ParentForm_Move(object sender, EventArgs e)
		{
			_dropContainer.Bounds = GetDropDownBounds();
		}

		private void Animate_Tick(object sender, EventArgs e)
		{
			int Height = 0;
			switch (_dropState)
			{
				case eDropState.Closing:
					Height = _dropContainer.Height - _dropDownSize.Height / 10;
					if (Height < 0)
					{
						_dropContainer.Freeze = false;
						_dropContainer.Close();
						_dropState = eDropState.Closed;
						tmrAnimate.Enabled = false;
					}
					else
						_dropContainer.Height = Height;
					break;

				case eDropState.Dropping:
					Height = _dropContainer.Height + _dropDownSize.Height / 10;
					if (Height >= _dropDownSize.Height)
					{
						_dropContainer.Height = _dropDownSize.Height;
						_dropState = eDropState.Dropped;
						tmrAnimate.Enabled = false;
					}
					else
						_dropContainer.Height = Height;
					break;

				case eDropState.Closed:
				case eDropState.Dropped:
				default:
					tmrAnimate.Enabled = false;
					return;
			}
		}

		#endregion [ Event Delegates ]

		#endregion [ Events ]

		#region [ Class DropDownContainer ]

		internal sealed class DropDownContainer : Form, IMessageFilter
		{
			#region [ Private Variables ]

			public bool Freeze;

			#endregion [ Private Variables ]

			#region [ Constructors ]

			public DropDownContainer(Control dropDownItem)
			{
				this.FormBorderStyle = FormBorderStyle.None;
				dropDownItem.Location = new Point(1, 1);
				this.Controls.Add(dropDownItem);
				this.StartPosition = FormStartPosition.Manual;
				this.ShowInTaskbar = false;
				Application.AddMessageFilter(this);
			}

			public DropDownContainer(Control dropDownItem, Size dropDownSize)
				: this(dropDownItem)
			{
				this.Size = dropDownSize;
			}

			#endregion [ Constructors ]

			#region [ Methods ]

			public bool PreFilterMessage(ref Message m)
			{
				if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
				{
					OnDropStateChange(eDropState.Closing);
					this.Close();
				}
				return false;
			}

			#endregion [ Methods ]

			#region [ Events ]

			#region [ Event Declarations ]

			public delegate void DropWindowArgs(eDropState state);
			public event DropWindowArgs DropStateChange;

			#endregion [ Event Declarations ]

			#region [ Event Triggers ]

			protected override void OnActivated(EventArgs e)
			{
				base.OnActivated(e);
				this.Refresh();
			}

			private void OnDropStateChange(eDropState state)
			{
				DropStateChange?.Invoke(state);
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
			}

			protected override void OnClosing(CancelEventArgs e)
			{
				Application.RemoveMessageFilter(this);
				this.Controls.RemoveAt(0); // Prevent the control from being disposed
				base.OnClosing(e);
			}

			#endregion [ Event Triggers ]

			#endregion [ Events ]

		}

		#endregion [ Class DropDownContainer ]

	}
}
