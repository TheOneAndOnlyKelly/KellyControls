using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KellyControls
{
	[DisplayName("ColorSelector")]
	[Description("Dropdown list of colors")]
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\ColorSelector\ColorSelector.bmp")]
	[DesignTimeVisible(true)]
	public partial class ColorSelector : DropDownControl
	{

		#region [ Private Variables ]

		private Color _color = Color.White;
		private bool _isUpdating = false;
		private bool _showNoColor = false;

		#endregion [ Private Variables ]

		#region [ Properties ]

		/// <summary>
		/// Color selected.
		/// </summary>
		public Color Color
		{
			get { return _color; }
			set
			{
				_isUpdating = true;
				_color = value;
				if (value == Color.Empty)
				{
					cmdNoColor.Focus();
					pctNoColor.Visible = true;
				}
				pnlSelectedColor.Color = _color;
				ColorGrid.Color = value;
				OnColorChanged();
				_isUpdating = false;
			}
		}

		/// <summary>
		/// Indicates whether the No Color button should be displayed.
		/// </summary>
		public bool ShowNoColor
		{
			get { return _showNoColor; }
			set
			{
				_isUpdating = true;
				_showNoColor = value;
				if (!_showNoColor)
				{
					if (cmdNoColor.Visible)
					{
						cmdNoColor.Visible = false;
						pnlControls.Size = new Size(pnlControls.Width, pnlControls.Height - cmdNoColor.Size.Height);
					}
				}
				else
				{
					if (!cmdNoColor.Visible)
					{
						cmdNoColor.Visible = true;
						pnlControls.Size = new Size(pnlControls.Width, pnlControls.Height + cmdNoColor.Size.Height);
					}
				}
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public ColorSelector()
		{
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			_isUpdating = true;
			InitializeComponent();
			InitializeDropDown(pnlControls);
			this.cmdNoColor.Focus();
			_isUpdating = false;
		}

		#endregion [ Constructors ]

		#region [ Events ]

		#region [ Event Handlers ]

		public event EventHandler ColorChanged;

		#endregion [ Event Handlers ]

		#region [ Event Triggers ]

		protected void OnColorChanged()
		{
			ColorChanged?.Invoke(null, null);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			pnlSelectedColor.Bounds = AnchorClientBounds;
			//lblNoColor.Bounds = AnchorClientBounds;
		}

		#endregion [ Event Triggers ]

		#region [ Event Delegates ]

		private void Button_MouseEnter(object sender, EventArgs e)
		{
			((Control)sender).BackColor = Color.FromArgb(193, 210, 238);
		}

		private void Button_MouseLeave(object sender, EventArgs e)
		{
			((Control)sender).BackColor = pnlControls.BackColor;
		}

		private void cmdMoreSolidColors_Click(object sender, EventArgs e)
		{
			if (_isUpdating) 
				return;
			this.FreezeDropDown(false);
			colorDialog1.Color = _color;
			
			if (colorDialog1.ShowDialog() != DialogResult.Cancel)
			{
				this.Color = colorDialog1.Color;
				pnlSelectedColor.Visible = true;
				pctNoColor.Visible = true;
			    this.CloseDropDown();
			    OnColorChanged();
			}
			else
			    this.UnFreezeDropDown();
		}

		private void cmdNoColor_Click(object sender, EventArgs e)
		{
			pnlSelectedColor.Color = Color.Empty;
			pnlSelectedColor.Visible = false;
			pctNoColor.Visible = true;
			this.CloseDropDown();
			OnColorChanged();
		}

		private void colorPanel1_Click(object sender, EventArgs e)
		{
			OpenDropDown();
		}

		private void colorGrid1_SelectedIndexChange(object sender, EventArgs e)
		{
			if (_isUpdating)
				return;
			this.Color = ColorGrid.Color;
			pnlSelectedColor.Visible = true;
			this.CloseDropDown();
			OnColorChanged();
		}

		#endregion [ Event Delegates ]

		#endregion [ Events ]
	}
}
