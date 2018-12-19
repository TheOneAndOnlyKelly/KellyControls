using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using KellyControls.CommonClasses;

namespace KellyControls.SelectColorDlg
{
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\SelectColorDialog\SelectColorDialog.bmp")]
	public partial class SelectColorDialog : Component
	{
		#region [ Public Variables ]

		/// <summary>
		/// Gets or sets the color name selected by the user.
		/// </summary>
		[Description("Gets or sets the color name selected by the user.")]
		public string ColorName { get; set; }

		/// <summary>
		/// Gets or sets the color selected by the user.
		/// </summary>
		[Description("Gets or sets the color selected by the user.")]
		public Color Color { get; set; }

		/// <summary>
		/// List of customized colors.
		/// </summary>
		[Description("List of customized colors.")]
		public Palette CustomColors { get; set; }

		/// <summary>
		/// Custom image to use for the OK button on the Select Color dialog.
		/// </summary>
		[Description("Custom image to use for the OK button on the Select Color dialog.")]
		public Image OKButton_Image { get; set; }

		/// <summary>
		/// Custom image to use for the Cancel button on the Select Color dialog.
		/// </summary>
		[Description("Custom image to use for the Cancel button on the Select Color dialog.")]
		public Image CancelButton_Image { get; set; }

		/// <summary>
		/// Show the palette of colors next to the color picker.
		/// </summary>
		[Description("Show the palette of colors next to the color picker.")]
		public bool ShowPalette { get; set; }

		/// <summary>
		/// Title for the dialog.
		/// </summary>
		[Description("Title for the dialog.")]
		public string Title { get; set; }

		#endregion [ Public Variables ]

		#region [ Constructors ]

		public SelectColorDialog()
		{
			InitializeComponent();
		}

		public SelectColorDialog(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		/// <summary>
		/// Runs a common dialog box with a default owner.
		/// </summary>
		/// <returns>System.Windows.Forms.DialogResult.OK if the user clicks OK in the dialog box; otherwise, System.Windows.Forms.DialogResult.Cancel.</returns>
		public DialogResult ShowDialog()
		{
			var frmDialog = new SelectColorForm
			{
				OKButton_Image = this.OKButton_Image,
				CancelButton_Image = this.CancelButton_Image,
				ShowPalette = this.ShowPalette,
				CustomColors = this.CustomColors,
				Color = this.Color
			};
			if (!string.IsNullOrEmpty(Title))
				frmDialog.Text = Title;
			if (!string.IsNullOrEmpty(ColorName))
				frmDialog.ColorName = this.ColorName;

			var Result = frmDialog.ShowDialog();
			if (Result != DialogResult.Cancel)
			{
				this.Color = frmDialog.Color;
				this.ColorName = frmDialog.ColorName;
			}

			frmDialog?.Dispose();

			return Result;
		}

		/// <summary>
		/// Runs a common dialog box with the specified owner.
		/// </summary>
		/// <param name="owner">Any object that implements System.Windows.Forms.IWin32Window that represents the top-level window that will own the modal dialog box.</param>
		/// <returns>System.Windows.Forms.DialogResult.OK if the user clicks OK in the dialog box; otherwise, System.Windows.Forms.DialogResult.Cancel.</returns>
		public DialogResult ShowDialog(IWin32Window owner)
		{
			var frmDialog = new SelectColorForm
			{
				Color = this.Color,
				ColorName = this.ColorName,
				OKButton_Image = this.OKButton_Image,
				CancelButton_Image = this.CancelButton_Image
			};
			if (!string.IsNullOrEmpty(Title))
				frmDialog.Text = Title;

			var Result = frmDialog.ShowDialog(owner);
			if (Result != DialogResult.Cancel)
			{
				this.Color = frmDialog.Color;
				this.ColorName = frmDialog.ColorName;
			}

			frmDialog?.Dispose();

			return Result;
		}

		#endregion [ Methods ]

	}

}
