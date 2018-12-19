using KellyControls.CommonClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace KellyControls.ColorList
{
	[DisplayName("ColorList")]
	[Description("Display a list of colors.")]
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\ColorList\ColorList.bmp")]
	[DesignTimeVisible(true)]
	public class ColorList : UserControl
	{
		#region [ Enums ]

		public enum ListStyleEnum : int
		{
			Web,
			System
		}

		#endregion [ Enums ]

		#region [ Private Variables ]

		private Color _color;
		private Padding _padding = new Padding(4);
		private Size _gridSize = new Size(40, 20);
		private ListStyleEnum _listStyle = ListStyleEnum.Web;
		private IContainer components;
		private TextBox txtSearch;
		private ListBox lstColors;
		private ToolTip toolTip1;
		private bool _suppressEvents = false;

		#endregion [ Private Variables ]

		#region [ Properties ]

		/// <summary>
		/// Gets or Sets the color that is the currently selected one.
		/// </summary>
		[DefaultValue(typeof(Color), "Red")]
		public Color Color
		{
			get { return _color; }
			set
			{
				_color = value;
				if (value == Color.Empty)
					this.SelectedIndex = -1;
				else
					this.SelectedIndex = Find(lstColors, value);
			}
		}

		[Description("Spacing between the colors.")]
		public Size GridSize
		{
			get { return _gridSize; }
			set
			{
				_gridSize = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Amount of space between entries.
		/// </summary>
		public Padding GridPadding
		{
			get { return _padding; }
			set
			{
				_padding = value;
				this.Invalidate();
			}
		}

		public List<NamedColor> Items;

		/// <summary>
		/// Gets or Sets which list of colors to display.
		/// </summary>
		[Description("Gets or Sets which list of colors to display.")]
		public ListStyleEnum ListStyle
		{
			get { return _listStyle; }
			set
			{
				_listStyle = value;
				if (_listStyle == ListStyleEnum.System)
					LoadSystemColors();
				else if (_listStyle == ListStyleEnum.Web)
					LoadWebColors();
				
				//if (_selectedIndex > Items.Count - 1)
				//{
				//    _selectedIndex = Items.Count - 1;
				//    OnSelectedIndexChange();
				//}
				// Check to see if we can match up (probably not).
				this.Color = _color;
					
				this.Invalidate();
			}
		}

		public int SelectedIndex
		{
			get { return lstColors.SelectedIndex; }
			set { lstColors.SelectedIndex = value; }
			//{
			//    if (_selectedIndex != value)
			//    {
			//        _selectedIndex = value;
			//        if (_selectedIndex != -1)
			//            _color = Items[_selectedIndex].Color;
			//        this.Invalidate();
			//        OnSelectedIndexChange();
			//    }
			//}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public ColorList()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
			this.Items = new List<NamedColor>();
			txtSearch.Text = string.Empty;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		//private void DrawTransparencyGrid(Graphics g, Rectangle area)
		//{
		//    bool b = false;
		//    Rectangle r = new Rectangle(0, 0, 8, 8);
		//    g.FillRectangle(Brushes.White, area);
		//    for (r.Y = area.Y; r.Y < area.Bottom; r.Y += 8)
		//        for (r.X = ((b = !b) ? area.X : 8); r.X < area.Right; r.X += 16)
		//            g.FillRectangle(Brushes.LightGray, r);
		//}

		//public int IndexOf(Point point)
		//{
		//    point.Offset(_xOffset, _yOffset);
		//    //int colorsPerLine = 1;//			(int)(this.Width / (_gridSize.Width + _gridPadding));
		//    //int column = (int)Math.Round((float)(point.X / (_gridSize.Width + _gridPadding)));
			
		//    int line = (int)Math.Round((float)(point.Y / (_gridSize.Height + _padding.Vertical))) + 1;
		//    int index = line - 1;
		//    return index < Items.Count ? index : -1;
		//}

		//public int IndexOf(Color color, bool ignoreAlpha)
		//{
		//    int intCount = Items.Count;
		//    for (int i = 0; i < intCount; i++)
		//    {
		//        if ((ignoreAlpha ? true : Items[i].Color.A == color.A) &&
		//            Items[i].Color.R == color.R &&
		//            Items[i].Color.G == color.G &&
		//            Items[i].Color.B == color.B)
		//            return i;
		//    }
		//    return -1;
		//}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.lstColors = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 5;
			this.toolTip1.ReshowDelay = 5;
			// 
			// txtSearch
			// 
			this.txtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(0, 132);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(150, 18);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "Color Name";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// lstColors
			// 
			this.lstColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstColors.FormattingEnabled = true;
			this.lstColors.Location = new System.Drawing.Point(0, 0);
			this.lstColors.Name = "lstColors";
			this.lstColors.Size = new System.Drawing.Size(150, 132);
			this.lstColors.TabIndex = 1;
			this.lstColors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstColors_DrawItem);
			this.lstColors.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstColors_MeasureItem);
			this.lstColors.SelectedIndexChanged += new System.EventHandler(this.lstColors_SelectedIndexChanged);
			// 
			// ColorList
			// 
			this.Controls.Add(this.lstColors);
			this.Controls.Add(this.txtSearch);
			this.Name = "ColorList";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		/// <summary>
		/// Generates a list of all the names of the System.Drawing.SystemColor members.
		/// </summary>
		/// <returns>List of SystemColor color names.</returns>
		private List<string> GetSystemColorNames()
		{
			List<string> SystemEnvironmentColors = new List<string>();
			foreach (MemberInfo member in (typeof(System.Drawing.SystemColors)).GetProperties())
			{
				SystemEnvironmentColors.Add(member.Name);
			}
			return SystemEnvironmentColors;
		}

		/// <summary>
		/// Load the list of all the colors in the enum System.Drawing.KnownColor that are not also SystemColors.
		/// </summary>
		private void LoadWebColors()
		{
			Items.Clear();

			// Get the list of all the colors in the System.Drawing.KnowColor enum, including the system colors.
			var AllColors = new List<string>();
			AllColors.AddRange(Enum.GetNames(typeof(System.Drawing.KnownColor)));

			var SystemEnvironmentColors = GetSystemColorNames();

			foreach (var ColorName in AllColors)
			{
				if (!SystemEnvironmentColors.Contains(ColorName))
				{
					var nColor = new NamedColor(ColorName, Color.FromName(ColorName));
					Items.Add(nColor);
					lstColors.Items.Add(new ColorItem(ColorName, ColorName, nColor));
				}
			}
		}

		/// <summary>
		/// Load the list with all the System Colors.
		/// </summary>
		private void LoadSystemColors()
		{
			Items.Clear();
			lstColors.Items.Clear();
			var SystemEnvironmentColors = GetSystemColorNames();

			try
			{
				foreach (var ColorName in SystemEnvironmentColors)
				{
					var nColor = new NamedColor(ColorName, Color.FromName(ColorName));
					Items.Add(nColor);
					lstColors.Items.Add(new ColorItem(ColorName, ColorName, nColor));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		#region [ ListBox Methods ]

		[DebuggerHidden()]
		public int Find(ListBox listBox, string value)
		{
			if (value.Length == 0)
				return -1;
			else
				return listBox.FindString(value);
		}

		[DebuggerHidden()]
		public int Find(ListBox listBox, Color value)
		{
			NamedColor nColor;
			try
			{
				for (int i = 0; i <= listBox.Items.Count - 1; i++)
				{
					nColor = ((ColorItem)listBox.Items[i]).NamedColor;
					if (nColor.Color.Equals(value))
						return i;
				}
				return -1;
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}


		[DebuggerHidden()]
		private bool Set(ListBox listBox, string value)
		{
			listBox.SelectedIndex = Find(listBox, value);
			return (listBox.SelectedIndex > -1);
		}

		#endregion [ ListBox Methods ]

		#endregion [ Methods ]

		#region [ Events ]

		#region [ Event Handlers ]

		public event EventHandler SelectedIndexChange;

		#endregion [ Event Handlers ]

		#region [ Event Triggers ]

		//protected override void OnMouseClick(MouseEventArgs e)
		//{
		//    base.OnMouseClick(e);
		//    int index = IndexOf(e.Location);
		//    if (index != -1)
		//    {
		//        SelectedIndex = index;
		//    }
		//}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.toolTip1.Active = false;
		}

		//protected override void OnMouseMove(MouseEventArgs e)
		//{
		//    base.OnMouseMove(e);
		//    Point pos = this.PointToClient(Control.MousePosition);
		//    int index = IndexOf(pos);
		//    if (index != -1 && index != _hoverIndex)
		//    {
		//        _hoverIndex = index;
		//        toolTip1.SetToolTip(this, Items[index].ToString());
		//    }
		//}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//    Size MinArea = this.Size;

		//    e.Graphics.TranslateTransform(-_xOffset, -_yOffset);

		//    //int ColorsPerLine = 1; // (int)(MinArea.Width / (_gridSize.Width + _gridPadding));
		//    //int StartingLine = _yOffset == 0 ? 0 : (int)((_gridSize.Height + _gridPadding) / _yOffset);
		//    //int Index = StartingLine * ColorsPerLine;

		//    // Setup the StringFormat object
		//    StringFormat sf = new StringFormat();

		//    Rectangle gridBounds = new Rectangle(_padding.Left, _padding.Top, _gridSize.Width, _gridSize.Height);
			
		//    int TextX = _padding.Horizontal * 2 + _gridSize.Width;
		//    Rectangle TextBounds = new Rectangle(TextX, _padding.Top, this.Width - TextX - _padding.Right, _gridSize.Height);
			
		//    // Create a rectangle that represents the entire item.
		//    Rectangle EntryBounds = new Rectangle(TextX, _padding.Top, this.Width - _padding.Right, _gridSize.Height + _padding.Vertical);

		//    int DeltaY = _gridSize.Height + _padding.Vertical;

		//    using (SolidBrush brush = new SolidBrush(Color.Black))
		//    {
		//        for (int i = 0; i < Items.Count; i++)
		//        {
		//            // If this color has any alpha set, then draw a background grid to show this.
		//            if (Items[i].Color.A != 255)
		//                DrawTransparencyGrid(e.Graphics, gridBounds);
					
		//            brush.Color = Items[i].Color;

		//            // Paint the color
		//            e.Graphics.FillRectangle(brush, gridBounds);

		//            // Draw the boundary around the color section
		//            e.Graphics.DrawRectangle(SystemPens.ControlDark, gridBounds);

		//            // If selected, draw a highlight color rectangle around the entire entry.
		//            if (i == _selectedIndex)
		//            {
		//                Rectangle r = gridBounds;
		//                r.Inflate(2, 2);
		//                e.Graphics.DrawRectangle(SystemPens.Highlight, r);
		//            }

		//            // Move the gridBounds down one
		//            //gridBounds.X += (_gridSize.Width + _gridPadding);
		//            //if (gridBounds.X + _gridSize.Width > MinArea.Width)
		//            //    gridBounds.X = _xOffset == 0 ? _gridPadding : (int)((_gridSize.Width + _gridPadding) / _xOffset);

		//            //if (gridBounds.X == _gridPadding)
		//            gridBounds.Y += DeltaY;
		//            TextBounds.Y += DeltaY;
		//            EntryBounds.Y += DeltaY;
		//        }
		//    }
		//}

		//protected void OnSelectedIndexChange()
		//{
		//    if (SelectedIndexChange != null)
		//        SelectedIndexChange(null, null);
		//}
				
		#endregion [ Event Triggers ]

		#region [ Event Delegates ]

		private void lstColors_SelectedIndexChanged(object sender, EventArgs e)
		{
			_suppressEvents = true;
			if (lstColors.SelectedIndex >= 0)
				txtSearch.Text = ((ColorItem)lstColors.SelectedItem).Text;
			_suppressEvents = false;

			SelectedIndexChange?.Invoke(null, null);
		}

		private void lstColors_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();

			if (e.Index == -1)
				return;

			var ListBox = (ListBox)sender;

			// Get the Bounding rectangle
			var rc = new Rectangle(e.Bounds.X + 16, e.Bounds.Y, e.Bounds.Width - 16, e.Bounds.Height);

			// Setup the StringFormat object
			var StringFormat = new StringFormat();

			var Text = ListBox.Items[e.Index].ToString();

			var r1 = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

			var ColorBitmap = new Bitmap(_gridSize.Width, _gridSize.Height);
			using (Graphics g = Graphics.FromImage(ColorBitmap))
			{
				var nColor = ((ColorItem)lstColors.Items[e.Index]).NamedColor;
				var gridBounds = new Rectangle(e.Bounds.X + _padding.Left, e.Bounds.Y + _padding.Right, _gridSize.Width, _gridSize.Height);

				using (var GridBrush = new SolidBrush(nColor.Color))
				{
					// If there is any transparency, draw a gray and white checkerboard in the box first. This should help the user get an indication of the amount of alpha that is set within the color.
					if (nColor.Color.A != 255)
					{
						using (var HatchBrush = new HatchBrush(HatchStyle.SmallCheckerBoard, Color.White, Color.Silver))
							g.FillRectangle(HatchBrush, gridBounds);
					}

					// Fill in the box.
					g.FillRectangle(GridBrush, gridBounds);

					// Draw the border around the box.
					g.DrawRectangle(SystemPens.ControlDark, gridBounds);
				}
			}

			// Check if the item is selected
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				// Paint the item accordingly if it is selected
				if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
					e.DrawFocusRectangle();
				e.Graphics.FillRectangle(SystemBrushes.Highlight, r1);
				e.Graphics.DrawImage(ColorBitmap, e.Bounds.X, e.Bounds.Y, 16, 16);
				e.Graphics.DrawString(Text, ListBox.Font, SystemBrushes.HighlightText, rc, StringFormat);
			}
			else
			{
				// Paint the item that if not selected
				e.Graphics.FillRectangle(SystemBrushes.Window, r1);
				e.Graphics.DrawImage(ColorBitmap, e.Bounds.X, e.Bounds.Y, 16, 16);
				e.Graphics.DrawString(Text, ListBox.Font, SystemBrushes.WindowText, rc, StringFormat);
				e.DrawFocusRectangle();
			}
		}

		private void lstColors_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			var StringSize = e.Graphics.MeasureString("Xfg", lstColors.Font);
			e.ItemHeight = (int)StringSize.Height + _padding.Vertical;
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			if (_suppressEvents)
				return;

			var Index = Find(lstColors, txtSearch.Text);
			if (Index >= 0)
				lstColors.SelectedIndex = Index;
		}

		#endregion [ Event Delegates ]

		#endregion [ Events ]

	}	
}
