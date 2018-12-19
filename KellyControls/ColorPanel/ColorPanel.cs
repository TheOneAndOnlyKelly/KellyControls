using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KellyControls
{
	[DefaultEvent("Click")]
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\ColorPanel\ColorPanel.bmp")]
	public partial class ColorPanel : Label
	{

		#region [ Private Variables ]

		private Color _color;
		private bool _paintColor = true;

		#endregion [ Private Variables ]

		#region [ Properties ]

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoSize => false;

		[Description("The color to be displayed on the form")]
		public Color Color
		{
			get { return _color; }
			set
			{
				_color = value;
				this.Invalidate();
			}
		}

		[Description("Indicates whether the color should be painted on the control")]
		public bool PaintColor
		{
			get { return _paintColor; }
			set
			{
				_paintColor = value;
				this.Invalidate();
			}
		}

		#region [ Hidden Properties ]

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft { get => base.RightToLeft; set => base.RightToLeft = value; }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text { get => base.Text; set => base.Text = value; }

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ContentAlignment TextAlign { get => base.TextAlign; set => base.TextAlign = value; }

		#endregion [ Hidden Properties ]

		#endregion [ Properties ]

		#region [ Constructors ]

		public ColorPanel()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		}

		#endregion [ Constructors ]

		#region [ Events ]

		#region [ Event Triggers ]

		protected override void OnPaint(PaintEventArgs e)
		{
			if (!_paintColor || _color.IsEmpty)
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.Clear(this.BackColor);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, this.ClientSize.Width, 0, 0, this.ClientSize.Height);
				return;
			}

			using (var HatchBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.Silver))
				e.Graphics.FillRectangle(HatchBrush, this.ClientRectangle);

			using (var FillBrush = new SolidBrush(_color))
				e.Graphics.FillRectangle(FillBrush, this.ClientRectangle);
		}

		#endregion [ Event Triggers ]

		#endregion [ Events ]
	}
}
