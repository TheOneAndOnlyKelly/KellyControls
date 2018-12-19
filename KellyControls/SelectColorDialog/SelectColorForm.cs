using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KellyControls.CommonClasses;

namespace KellyControls.SelectColorDlg
{
	public partial class SelectColorForm : Form
	{
		private bool _showPalette = false;
		private bool _addButtonEventAttached = false;

		#region [ Properties ]

		public Color Color
		{
			get { return ColorPicker.Color; }
			set
			{
				//ColorPicker.Color = value;
				cgPalette.Color = value;
			}
		}

		public string ColorName
		{
			get { return ColorPicker.ColorName; }
			set { ColorPicker.ColorName = value; }
		}

		public Palette CustomColors
		{
			get { return cgPalette.CustomColors; }
			set { cgPalette.CustomColors = value; }
		}

		public Image OKButton_Image
		{
			get { return cmdOk.Image; }
			set
			{
				if (value != null)
					cmdOk.Image = value;
			}
		}

		public Image CancelButton_Image
		{
			get { return cmdCancel.Image; }
			set
			{
				if (value != null)
					cmdCancel.Image = value;
			}
		}

		public bool ShowPalette
		{
			get { return _showPalette; }
			set
			{
				_showPalette = value;
				this.SuspendLayout();
				if (_showPalette)
				{
					this.Size = new Size(420, 400);
					ColorPicker.Location = new Point(188, 5);
					cgPalette.Visible = true;
					cgPalette.SuspendNativeAddButtonClick = true;
					if (!_addButtonEventAttached)
					{
						_addButtonEventAttached = true;
						cgPalette.AddButtonClick += new System.EventHandler(this.cgPalette_AddButtonClick);
					}
				}
				else
				{
					cgPalette.Visible = false;
					cgPalette.SuspendNativeAddButtonClick = false;
					this.Size = new Size(237, 400);
					ColorPicker.Location = new Point(7, 5);
				}
				int ButtonWidth = cmdOk.Width + cmdCancel.Width + 8;
				cmdOk.Left = (this.ClientRectangle.Width - ButtonWidth) / 2;
				cmdCancel.Left = cmdOk.Left + cmdOk.Width + 8;
				this.ResumeLayout(false);
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public SelectColorForm()
		{
			InitializeComponent();
			this.ShowPalette = false;
		}

		public SelectColorForm(Color color)
			: this()
		{
			this.Color = color;
		}

		public SelectColorForm(NamedColor namedColor)
			: this(namedColor.Color)
		{
			this.ColorName = namedColor.Name;
		}

		#endregion [ Constructors ]

		#region [ Events ]

		private void cgPalette_SelectedIndexChange(object sender, System.EventArgs e)
		{
			if (cgPalette.NamedColor != null)
				this.ColorPicker.NamedColor = cgPalette.NamedColor;
			else
				this.ColorPicker.Color = cgPalette.Color;
		}

		private void cgPalette_AddButtonClick(object sender, System.EventArgs e)
		{
			cgPalette.AddColor(ColorPicker.Color, ColorPicker.ColorName);
		}

		#endregion [ Events ]
	}
}
