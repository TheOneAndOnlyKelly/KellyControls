﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KellyControls.CommonClasses;
using KellyControls.PlugInToolBtn.Interfaces;
using CanvasPoint = System.Drawing.Point;
using KRes = KellyControls.Properties.Resources;
using LatticePoint = System.Drawing.Point;

namespace KellyControls.PlugInToolBtn.Tools
{
	/// <summary>
	/// Contains a lot of the basic stuff shared between Tools
	/// </summary>
	public class BaseTool : Disposable, IPlugInTool
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		protected static extern int GetDoubleClickTime();

		#region [ Private Variables ]

		protected bool _isSelected = false;
		protected string _undoText = string.Empty;
		protected string _toolTipText = string.Empty;
		protected IntPtr _cursorHandle = IntPtr.Zero;
		protected bool _shutDown = false;
		protected Bitmap _selectedToolImage;
		protected ToolStrip _toolStrip;

		#endregion [ Private Variables ]

		#region [ Protected Variables ]

		protected bool _cancel = false;
		//protected Workshop _workshop = Workshop.Instance;
		//protected Settings _settings = Settings.Instance;
		//protected ConstrainDirection _constrainingDirection = ConstrainDirection.NotSet;
		protected bool _eventsAttached = false;
		//protected ToolStripForm _toolStrip_Form = null;

		/// <summary>
		/// Cell where the mouse button was pressed down
		/// </summary>
		protected Point _mouseDownLatticePoint = Point.Empty;

		/// <summary>
		/// Cell Pixel where the mouse button was pressed down
		/// </summary>
		protected Point _mouseDownCanvasPoint = Point.Empty;

		/// <summary>
		/// Cell where the mouse is currently
		/// </summary>
		protected Point _currentLatticePoint = Point.Empty;

		/// <summary>
		/// Cell Pixel where the mouse is currently
		/// </summary>
		protected Point _currentMouseCanvasPoint = Point.Empty;

		/// <summary>
		/// Constrained version of the current mouse cell pixel position. If control button is not pressed, then is is the same value as _currentMouseCellPixel
		/// </summary>
		protected Point _constrainedCanvasPoint = Point.Empty;

		/// <summary>
		/// Is the mouse button currently pressed?
		/// </summary>
		protected bool _isMouseDown = false;

		/// <summary>
		/// Did a mouse Click event just fire?
		/// </summary>
		protected bool _click = false;

		/// <summary>
		/// Did a mouse DoubleClick event just fire?
		/// </summary>
		protected bool _doubleClick = false;

		/// <summary>
		/// Graphics object for the CanvasPane PictureBox
		/// </summary>
		protected Graphics _canvasControlGraphics = null;

		/// <summary>
		/// Graphics object used to render the LatticeBuffer
		/// </summary>
		protected Graphics _latticeBufferGraphics = null;

		/// <summary>
		/// Bitmap representing the actual cell data
		/// </summary>
		protected Bitmap _latticeBuffer = null;

		/// <summary>
		/// Bitmap of the CanvasPane PictureBox before we do anything to it
		/// </summary>
		protected Bitmap _capturedCanvas = null;

		/// <summary>
		/// Path in the settings file to find data for this tool
		/// </summary>
		protected string _savePath = string.Empty;

		///// <summary>
		///// The CanvasPane PictureBox on the form CanvasWindow
		///// </summary>
		//protected PictureBox _canvasControl = null;	

		#endregion [ Protected Variables ]

		#region [ Properties ]

		/// <summary>
		/// True if this tool typically affects more than just the Active Channel
		/// </summary>
		public virtual bool AffectMultipleChannels { get; set; }

		/// <summary>
		/// True if the tool generates output in the standard, bitmapped manner. Support for vector based drawing is still in the works.
		/// </summary>
		public virtual bool BitmapMode { get; set; }

		/// <summary>
		/// Cursor to use on the Canvas window when the mouse is within its bounds. A safe cursor to use might be: Cursors.Cross
		/// </summary>
		public virtual Cursor Cursor { get; set; }

		/// <summary>
		/// True if this tool will do a marquee selection on the Canvas window. This flag is needed to display information in the Editor.
		/// </summary>
		public virtual bool DoesSelection { get; set; }

		/// <summary>
		/// Numeric identifier for the tool.
		/// </summary>
		public virtual int ID { get; set; }

		/// <summary>
		/// Name of the Tool
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Indicates whether this Tool is selected or not.
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (_isSelected != value)
				{
					_isSelected = value;
					if (_isSelected)
						OnSelected();
					else
						OnUnselected();
				}
			}
		}

		/// <summary>
		/// ToolStrip to display under the menu when this tool is selected. If no settings are available, this can safely return null.
		/// </summary>
		public virtual ToolStrip SettingsToolStrip
		{
			get
			{
				if (_toolStrip == null)
					CreateToolStrip();
				return _toolStrip;
			}
			set { _toolStrip = value; }
		}

		/// <summary>
		/// Image to display in the ToolBox. 16x16 transparent PNG are best
		/// </summary>
		public virtual Bitmap ToolBoxImage { get; set; }

		/// <summary>
		/// 16x16 transparent bitmap to display in the toolbox when the tool is selected. 16x16 transparent PNG are best
		/// </summary>
		public virtual Bitmap ToolBoxImageSelected
		{
			get
			{
				if (_selectedToolImage == null)
					return this.ToolBoxImage;
				else
					return _selectedToolImage;
			}
			set { _selectedToolImage = value; }
		}

		/// <summary>
		/// Returns the name of the tool that is a Tool Group, in which this tool should be a child tool of
		/// </summary>
		public virtual string ToolGroupName { get; set; }

		/// <summary>
		/// ToolTip to display when the mouse hovers over the button in the ToolBox
		/// </summary>
		public virtual string ToolTipText
		{
			get
			{
				if ((_toolTipText ?? string.Empty).Length > 0)
					return _toolTipText;
				else
					return this.Name + " Tool";
			}
			set { _toolTipText = value; }
		}

		/// <summary>
		/// String to use when creating the Undo menu item
		/// </summary>
		public virtual string UndoText
		{
			get
			{
				if ((_undoText ?? string.Empty).Length > 0)
					return _undoText;
				else
					return this.Name;
			}
			set { _undoText = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public BaseTool()
		{
			this.ID = -1;
			this.ToolGroupName = string.Empty;
			this.Name = string.Empty;
			this.ToolBoxImage = KRes.undefined;
			this.ToolBoxImageSelected = KRes.undefined;
		}

		#endregion [ Constructors ]

		#region [ Private Methods ]

		protected virtual void CreateToolStrip()
		{
			var GenericToolName = new ToolStripLabel();

			_toolStrip = new ToolStrip();
			_toolStrip.SuspendLayout();

			GenericToolName.BackColor = SystemColors.ControlDark;
			GenericToolName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			GenericToolName.Image = this.ToolBoxImage;
			GenericToolName.Name = "GenericToolName";
			GenericToolName.Size = new Size(93, 22);
			GenericToolName.Text = this.Name;

			_toolStrip.GripStyle = ToolStripGripStyle.Hidden;
			_toolStrip.Items.AddRange(new ToolStripItem[] 
			{
				GenericToolName
			});
			_toolStrip.Name = "Generic_ToolStrip";

			_toolStrip.ResumeLayout(false);
			_toolStrip.PerformLayout();
		}

		#endregion [ Private Methods ]

		#region [ Protected Methods ]

		/// <summary>
		/// Concatenates the path with a name, delimited by the pre-defined path delimiter character.
		/// </summary>
		/// <param name="path">Pre-built path</param>
		/// <param name="newNode">Name to be appended</param>
		protected virtual string AppendPath(string path, string newNode)
		{
			return String.Empty;
			//return _settings.AppendPath(path, newNode);
		}

		/// <summary>
		/// Attaches or detaches events to objects, such as Click events to buttons.
		/// </summary>
		/// <param name="attach">Indicates that the events should be attached. If false, then detaches the events</param>
		protected virtual void AttachEvents(bool attach)
		{
			// if we are attaching and already attached, or if we are detaching and already detached, then leave.
			if (attach & _eventsAttached)
				return;

			if (attach)
			{
				if (_eventsAttached)
					return;

				//if (_workshop.ProfileController != null)
				//	_workshop.ProfileController.Switched += new EventHandlers.ProfileEventHandler(this.Profiles_Switched);

				//if (Profile != null)
				//	Profile.PropertyChanged += new PropertyChangedEventHandler(this.Profile_PropertyChanged);
			}
			else
			{
				if (!_eventsAttached)
					return;

				//if (_workshop.ProfileController != null)
				//	_workshop.ProfileController.Switched -= this.Profiles_Switched;

				//if (Profile != null)
				//	Profile.PropertyChanged -= this.Profile_PropertyChanged;
			}
			_eventsAttached = attach;
		}

		/// <summary>
		/// Pass through to the CaptureCanvas method on the Workshop object
		/// </summary>
		/// <param name="_canvas"></param>
		protected virtual void CaptureCanvas()
		{
			//_capturedCanvas = _workshop.CaptureCanvas();
		}

		/// <summary>
		/// Fire the OperationCompleted event
		/// </summary>
		protected void EndOperation()
		{
			if (_cancel)
				return;
			OperationCompleted?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Returns TRUE if the user is current pressing the Control key
		/// </summary>
		protected bool ControlKeyPressed()
		{
			return (Control.ModifierKeys == Keys.Control);
		}

		/// <summary>
		/// Creates a custom cursor for this tool
		/// </summary>
		/// <param name="baseImage">Basic image to create the cursor</param>
		/// <param name="hotspot">Position of the cursor's hotspot</param>
		/// <returns>A cursor created from the composite of the images.</returns>
		protected Cursor CreateCursor(Bitmap baseImage, Point hotspot)
		{
			return this.CreateCursor(baseImage, null, hotspot);
		}

		/// <summary>
		/// Creates a custom cursor for this tool, based on a base image, with another image in the upper right corner
		/// </summary>
		/// <param name="baseImage">Basic image to create the cursor</param>
		/// <param name="modifierImage">If present, an additional image place in the upper right hand corner</param>
		/// <param name="hotspot">Position of the cursor's hotspot</param>
		/// <returns>A cursor created from the composite of the images.</returns>
		protected virtual Cursor CreateCursor(Bitmap baseImage, Bitmap modifierImage, Point hotspot)
		{
			using (Bitmap Base = new Bitmap(baseImage))
			{
				using (Graphics g = Graphics.FromImage(Base))
				{
					if (modifierImage != null)
						g.DrawImage(modifierImage, Base.Width - modifierImage.Width, 0, modifierImage.Width, modifierImage.Height);
				}
				var NewCursor = CustomCursors.CreateCursor(Base, hotspot.X, hotspot.Y);
				_cursorHandle = NewCursor.Handle;
				return NewCursor;
			}
		}

		/// <summary>
		/// Render the captured canvas bitmap onto the CanvasPane PictureBox
		/// </summary>
		protected void DisplayCapturedCanvas()
		{
			if ((_canvasControlGraphics != null) && (_capturedCanvas != null))
				_canvasControlGraphics.DrawImageUnscaled(_capturedCanvas, new Point(0, 0));
		}

		/// <summary>
		/// Clean up all child objects here, unlink all events and dispose
		/// </summary>
		protected override void DisposeChildObjects()
		{
			base.DisposeChildObjects();

			if (!_shutDown)
				ShutDown();
		}

		/// <summary>
		/// Sets the DashStyle dropdown menu from the settings bar to the value. If it cannot find that value, returns the first item
		/// </summary>
		protected ToolStripMenuItem FindDropMenuItemFromValue(ToolStripDropDownButton dropDownButton, int value)
		{
			foreach (ToolStripMenuItem Item in dropDownButton.DropDownItems)
			{
				if (Int32.Parse(Item.Tag.ToString()) == value)
					return Item;
			}
			return (ToolStripMenuItem)dropDownButton.DropDownItems[0];
		}

		/// <summary>
		/// Returns an item from the toolstrip of a given index
		/// </summary>
		/// <typeparam name="T">Type of the object to return</typeparam>
		/// <param name="index">Index of the object to return, Example, if the 3rd button is desired, it will return the 3rd object of that type that is found, regardless of the number of other controls that preceeded it.</param>
		protected ToolStripItem GetItem<T>(int index)
		{
			if (_toolStrip == null)
				return null;

			int Counter = 0;

			for (int i = 0; i < _toolStrip.Items.Count; i++)
			{
				if (_toolStrip.Items[i] is T)
				{
					Counter++;
					if (Counter == index)
						return _toolStrip.Items[i];
				}
			}

			return null;
		}
		

		/// <summary>
		/// Returns an item from a toolstrip of a given type and name
		/// </summary>
		/// <typeparam name="T">Type of the object to return</typeparam>
		/// <param name="toolStrip">ToolStrip object to find the child control on.</param>
		/// <param name="name">Name of the control</param>
		protected ToolStripItem GetItem<T>(string name)
		{
			if (_toolStrip == null)
				return null;

			for (int i = 0; i < _toolStrip.Items.Count; i++)
			{
				if (_toolStrip.Items[i] is T)
				{
					if (string.Compare(_toolStrip.Items[i].Name, name, true) == 0)
						return _toolStrip.Items[i];
				}
			}

			return null;
		}

		public Control GetControl(string name)
		{
			Control[] Controls = _toolStrip.Parent.Controls.Find(name, true);
			if (Controls.Length > 0)
				return Controls[0];
			else
				return null;
		}

		/// <summary>
		/// Clean up our objects after a draw event is completed. This should be called after the work is done in MouseUp.
		/// </summary>
		protected virtual void PostDrawCleanUp()
		{
			_canvasControlGraphics?.Dispose();
			_latticeBufferGraphics?.Dispose();
			_capturedCanvas?.Dispose();
			_latticeBuffer?.Dispose();

			_cancel = false;
			_mouseDownLatticePoint = Point.Empty;
			_mouseDownCanvasPoint = Point.Empty;
			_currentLatticePoint = Point.Empty;
			_currentMouseCanvasPoint = Point.Empty;
			_constrainedCanvasPoint = Point.Empty;

			// Fire the event to indicate that this tool has finished working.
			EndOperation();
		}

		/// <summary>
		/// Release the capture of the mouse cursor
		/// </summary>
		protected virtual void ReleaseMouse()
		{
			//Profile.ReleaseMouse();
		}

		/// <summary>
		/// Creates the Pen used to render the shape onto the Paint Pane
		/// </summary>
		protected virtual Pen RenderPen()
		{
			Pen RenderPen = new Pen(Color.White, 1);
			return RenderPen;
		}

		///// <summary>
		///// If a mask is defined, then sets the clip on the Canvas
		///// </summary>
		//protected virtual void SetMaskClip()
		//{
		//	//if (Profile == null)
		//	//	return;

			//	//if (!Profile.HasMask)
			//	//	return;

			//	//using (Region Region = new System.Drawing.Region(Profile.GetMaskOutline(UnitScale.Canvas)))
			//	//	_canvasControlGraphics.SetClip(Region, CombineMode.Replace);

			//	//using (Region Region = new System.Drawing.Region(Profile.GetMaskOutline(UnitScale.Lattice)))
			//	//	_latticeBufferGraphics.SetClip(Region, CombineMode.Replace);
			//}

			///// <summary>
			///// Sets the tags for all the DashStyle menu items in the DashStyle drop down button on the settings toolbar.
			///// </summary>
			///// <param name="dashStyleDD">ToolStripDropDownButton control that has the menu items</param>
			//protected virtual void SetDashStyleDropDownButton(ToolStripDropDownButton dashStyleDD)
			//{
			//	dashStyleDD.DropDownItems[0].Tag = (int)DashStyle.Solid;
			//	dashStyleDD.DropDownItems[1].Tag = (int)DashStyle.Dot;
			//	dashStyleDD.DropDownItems[2].Tag = (int)DashStyle.Dash;
			//	dashStyleDD.DropDownItems[3].Tag = (int)DashStyle.DashDot;
			//	dashStyleDD.DropDownItems[4].Tag = (int)DashStyle.DashDotDot;
			//}

			/// <summary>
			/// Trap the mouse to only live inside of the canvas, so we don't get weird effects, like drawings starting outside, or ending outside the pictureBox.
			/// Call ReleaseMouse() on the MouseUp event to allow the cursor to act normal.
			/// </summary>
		protected virtual void TrapMouse()
		{
			//Profile.TrapMouse();
		}

		/// <summary>
		/// Validate that the text entered in the textbox is a proper number. If so, return that value.
		/// If not, reset the text in the text box with the original value of our variable and return that original value
		/// </summary>
		protected virtual int ValidateInteger(ToolStripTextBox textBox, int originalValue)
		{
			int Value = 0;
			if (Int32.TryParse(textBox.Text, out Value))
				return Value;
			else
			{
				textBox.Text = originalValue.ToString();
				return originalValue;
			}
		}

		#region [ LoadValue ]

		/// <summary>
		/// Returns the string value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		/// <returns>Setting value as a string</returns>
		protected virtual string LoadValue(string pathName, string defaultValue, bool appendPath)
		{
			return String.Empty;
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//return _settings.GetValue(Path, defaultValue);
		}

		/// <summary>
		/// Returns the string value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <returns>Setting value as a string</returns>
		protected virtual string LoadValue(string pathName, string defaultValue)
		{
			return String.Empty;
			//return _settings.GetValue(AppendPath(_savePath, pathName), defaultValue);
		}

		/// <summary>
		/// Returns the integer value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		/// <returns>Setting value as an integer</returns>
		protected virtual int LoadValue(string pathName, int defaultValue, bool appendPath)
		{
			return 0;
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//return _settings.GetValue(Path, defaultValue);
		}

		/// <summary>
		/// Returns the integer value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <returns>Setting value as an integer</returns>
		protected virtual int LoadValue(string pathName, int defaultValue)
		{
			return 0;
			//return _settings.GetValue(AppendPath(_savePath, pathName), defaultValue);
		}

		/// <summary>
		/// Returns the floating point value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		/// <returns>Setting value as a float</returns>
		protected virtual float LoadValue(string pathName, float defaultValue, bool appendPath)
		{
			return 0;
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//return _settings.GetValue(Path, defaultValue);
		}

		/// <summary>
		/// Returns the floating point value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <returns>Setting value as a float</returns>
		protected virtual float LoadValue(string pathName, float defaultValue)
		{
			return 0;
			//return _settings.GetValue(AppendPath(_savePath, pathName), defaultValue);
		}

		/// <summary>
		/// Returns the boolean value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		/// <returns>Setting value as a boolean</returns>
		protected virtual bool LoadValue(string pathName, bool defaultValue, bool appendPath)
		{
			return true;
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//return _settings.GetValue(Path, defaultValue);
		}

		/// <summary>
		/// Returns the boolean value stored in Settings
		/// </summary>
		/// <param name="pathName">Path to find the setting value</param>
		/// <param name="defaultValue">Value to return if the setting value was not present</param>
		/// <returns>Setting value as a boolean</returns>
		protected virtual bool LoadValue(string pathName, bool defaultValue)
		{
			return true;
			//return _settings.GetValue(AppendPath(_savePath, pathName), defaultValue);
		}

		#endregion [ LoadValue ]

		#region [ SaveValue ]

		/// <summary>
		/// Saves the value to the settings object as a string.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		protected virtual void SaveValue(string pathName, string value, bool appendPath)
		{
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//_settings.SetValue(Path, value);
		}

		/// <summary>
		/// Saves the value to the settings object as a string.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		protected virtual void SaveValue(string pathName, string value)
		{
			//SaveValue(pathName, value, true);
		}

		/// <summary>
		/// Saves the value to the settings object as an integer.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		protected virtual void SaveValue(string pathName, int value, bool appendPath)
		{
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//_settings.SetValue(Path, value);
		}

		/// <summary>
		/// Saves the value to the settings object as an integer.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		protected virtual void SaveValue(string pathName, int value)
		{
			//SaveValue(pathName, value, true);
		}

		/// <summary>
		/// Saves the value to the settings object as a float.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		protected virtual void SaveValue(string pathName, float value, bool appendPath)
		{
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//_settings.SetValue(Path, value);
		}

		/// <summary>
		/// Saves the value to the settings object as an float.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		protected virtual void SaveValue(string pathName, float value)
		{
			//SaveValue(pathName, value, true);
		}

		/// <summary>
		/// Saves the value to the settings object as a boolean.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		/// <param name="appendPath">Indicates that we should append the path passed in with the _savePath property of this Tool</param>
		protected virtual void SaveValue(string pathName, bool value, bool appendPath)
		{
			//string Path = pathName;
			//if (appendPath)
			//	Path = AppendPath(_savePath, pathName);
			//_settings.SetValue(Path, value);
		}

		/// <summary>
		/// Saves the value to the settings object as a boolean.
		/// </summary>
		/// <param name="pathName">Path to the place to store the setting value</param>
		/// <param name="value">Value to store</param>
		protected virtual void SaveValue(string pathName, bool value)
		{
			//SaveValue(pathName, value, true);
		}

		#endregion [ SaveValue ]

		#endregion [ Protected Methods ]

		#region [ Public Methods ]

		/// <summary>
		/// Informs the tool that the caller wants it to cease its current operation
		/// </summary>
		public virtual void Cancel()
		{
			_cancel = true;
		}

		/// <summary>
		/// Clears out the clipping that has been set in the Canvas and Paint graphics objects.
		/// </summary>
		//public void ClearMaskClip()
		//{
		//	try
		//	{
		//		_latticeBufferGraphics?.ResetClip();
		//		_canvasControlGraphics?.ResetClip();
		//	}
		//	catch (Exception Exception)
		//	{
		//		Debug.WriteLine(Exception.ToString());
		//		//_workshop.WriteTraceMessage(ex.ToString(), TraceLevel.Error);
		//	}
		//}

		/// <summary>
		/// Setup the tool
		/// </summary>
		public virtual void Initialize()
		{
			//_toolStrip_Form = new Tools_ToolStripContainer();
			//_savePath = AppendPath(Constants.TOOLSETTINGS, this.SafeName());
		}

		/// <summary>
		/// Handles keystrokes for the tool. Returns true if the keystroke was handled within the tool
		/// </summary>
		/// <param name="e"></param>
		public virtual bool KeyDown(KeyEventArgs e)
		{
			return false;
		}

		/// <summary>
		/// Canvas Click event was fired
		/// </summary>
		/// <param name="buttons">From the MouseEventArgs, indicates which mouse button was clicked</param>
		/// <param name="latticePoint">Point on the picture box (in Cells) where the mouse event happened</param>
		/// <param name="actualCanvasPoint">Point on the picture box (in Pixel) where the mouse event happened</param>
		public virtual void MouseClick(MouseButtons buttons, LatticePoint latticePoint, CanvasPoint actualCanvasPoint)
		{ }

		/// <summary>
		/// Canvas DoubleClick event was fired
		/// </summary>
		/// <param name="buttons">From the MouseEventArgs, indicates which mouse button was clicked</param>
		/// <param name="latticePoint">Point on the picture box (in Cells) where the mouse event happened</param>
		/// <param name="actualCanvasPoint">Point on the picture box (in Pixel) where the mouse event happened</param>
		public virtual void MouseDoubleClick(MouseButtons buttons, LatticePoint latticePoint, CanvasPoint actualCanvasPoint)
		{ }

		/// <summary>
		/// Canvas MouseDown event was fired
		/// </summary>
		/// <param name="buttons">From the MouseEventArgs, indicates which mouse button was clicked</param>
		/// <param name="latticePoint">Point on the picture box (in Cells) where the mouse event happened</param>
		/// <param name="actualCanvasPoint">Point on the picture box (in Pixel) where the mouse event happened</param>
		public virtual void MouseDown(MouseButtons buttons, LatticePoint latticePoint, CanvasPoint actualCanvasPoint)
		{
//			_isMouseDown = true;
//			_mouseDownLatticePoint = latticePoint;
//			_mouseDownCanvasPoint = _workshop.CalcCanvasPoint(latticePoint);

//			_currentLatticePoint = latticePoint;
//			_currentMouseCanvasPoint = _workshop.CalcCanvasPoint(latticePoint);

//			_canvasControlGraphics = Profile.GetCanvasGraphics();

//			// Trap the mouse into the Canvas while we are working
//			//TrapMouse();

//			_latticeBuffer = Profile.Channels.Active.LatticeBuffer;
//			_latticeBufferGraphics = Graphics.FromImage(_latticeBuffer);

//#if DEBUG
//			_workshop.ExposePane(_latticeBuffer, Panes.LatticeBuffer);
//#endif

//			CaptureCanvas();
		}

		/// <summary>
		/// Canvas MouseMove event was fired
		/// </summary>
		/// <param name="buttons">From the MouseEventArgs, indicates which mouse button was clicked</param>
		/// <param name="latticePoint">Point on the picture box (in Cells) where the mouse event happened</param>
		/// <param name="actualCanvasPoint">Point on the picture box (in Pixel) where the mouse event happened</param>
		/// <returns>Return true if the MouseDown flag was set.</returns>
		public virtual bool MouseMove(MouseButtons buttons, LatticePoint latticePoint, CanvasPoint actualCanvasPoint)
		{
			if (!_isMouseDown)
				return false;

			//// Limit the mouse position to be within the bounds of the canvas
			//if (latticePoint.X >= _workshop.UI.LatticeSize.Width)
			//    latticePoint.X = _workshop.UI.LatticeSize.Width-1;
			//else if (latticePoint.X < 0)
			//    latticePoint.X = 0;

			//if (latticePoint.Y >= _workshop.UI.LatticeSize.Height)
			//    latticePoint.Y = _workshop.UI.LatticeSize.Height-1;
			//else if (latticePoint.Y < 0)
			//    latticePoint.Y = 0;

			_currentLatticePoint = latticePoint;
			//_currentMouseCanvasPoint = _workshop.CalcCanvasPoint(latticePoint);
			//_constrainedCanvasPoint = _workshop.ConstrainLine(_currentMouseCanvasPoint, _mouseDownCanvasPoint);

			// Draw the captured bitmap onto the CanvasPane PictureBox
			DisplayCapturedCanvas();

			return true;
		}

		/// <summary>
		/// Canvas MouseUp event was fired
		/// </summary>
		/// <param name="buttons">From the MouseEventArgs, indicates which mouse button was clicked</param>
		/// <param name="latticePoint">Point on the picture box (in Cells) where the mouse event happened</param>
		/// <param name="actualCanvasPoint">Point on the picture box (in Pixel) where the mouse event happened</param>
		/// <returns>Return true if the MouseDown flag was set.</returns>
		public virtual bool MouseUp(MouseButtons buttons, LatticePoint latticePoint, CanvasPoint actualCanvasPoint)
		{
			if (!_isMouseDown)
				return false;

			_isMouseDown = false;
			_currentLatticePoint = latticePoint;
			//_currentMouseCanvasPoint = _workshop.CalcCanvasPoint(latticePoint);

			// Release the capture of the mouse cursor
			//ReleaseMouse();

			//if (!_createUndoOnMouseDown)
			//    // Create Undo data to be able to reverse out our changes
			//    _workshop.CreateUndo_Channel(this.UndoText);

			// Fire the event to indicate that this tool has finished working.
			//EndOperation();

			return true;
		}

		/// <summary>
		/// Returns the tool's name, modified to be used for variable names or Xml node names
		/// </summary>
		/// <returns></returns>
		public string SafeName()
		{
			return Regex.Replace(this.Name, @"[^\w]", "", RegexOptions.None);
		}

		/// <summary>
		/// Save this toolstrip settings back to the Settings object.
		/// </summary>
		public virtual void SaveSettings()
		{
			// Nothing to Save
		}

		/// <summary>
		/// Method fires when we are closing out of the editor, want to clean up all our objects.
		/// </summary>
		public virtual void ShutDown()
		{
			// Remove all event delegates from their associated events.
			AttachEvents(false);

			if (this.Cursor != null)
			{
				CustomCursors.DestroyCreatedCursor(_cursorHandle);
				this.Cursor?.Dispose();
			}
			_canvasControlGraphics?.Dispose();
			_latticeBufferGraphics?.Dispose();
			_latticeBuffer?.Dispose();
			_capturedCanvas?.Dispose();
			_shutDown = true;
		}

		#endregion [ Public Methods ]

		#region [ Events ]

		#region [ Event Handlers ]

		public event EventHandler OperationCompleted;

		public event EventHandler Selected;

		public event EventHandler Unselected;

		#endregion [ Event Handlers ]

		#region [ Event Triggers ]

		/// <summary>
		/// Occurs when this Tool has been selected from the main Toolbar
		/// </summary>
		public virtual void OnSelected()
		{
			AttachEvents(true);

			// Set the cursor for the Canvas. This is important because there could have been UI changes since this Select method
			// was called initially on Tool_Click, and some tool's cursors are sensitive to UI settings (ie Paint, Erase, etc)
			//if (Profile != null)
			//	Profile.Cursor = this.Cursor;

			this.Selected?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Method fires when a different tool is selected, gives the tool the chance to clean up a bit, but not as much as a ShutDown.
		/// </summary>
		public virtual void OnUnselected()
		{
			// Detach from the Profile events, we don't want to to fire if this is not the selected Tool
			AttachEvents(false);

			this.Unselected?.Invoke(this, new EventArgs());
		}

		#endregion [ Event Triggers ]

		#region [ Event Delegates ]

		///// <summary>
		///// Occurs when the first Document is loaded, a Document closes, or one Profile becomes Active, replacing another.
		///// </summary>
		//protected virtual void Documents_Switched(object sender, DocumentEventArgs e)
		//{
		//	if (e.OldDocument != null)
		//	{
		//		e.OldDocument.PropertyChanged -= Document_PropertyChanged;
		//	}
		//	if (e.Document != null)
		//	{
		//		e.Document.PropertyChanged += new PropertyChangedEventHandler(this.Document_PropertyChanged);
		//	}
		//}

		//protected virtual void Document_PropertyChanged(object sender, PropertyChangedEventArgs e)
		//{ }

		#endregion [ Event Delegates ]

		#endregion [ Events ]
	}
}
