using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KellyControls;
using CRes = ControlsTesting.Properties.Resources;

/// <summary>
/// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-author-controls-for-windows-forms
/// </summary>
namespace ControlsTesting
{
	public partial class Testing : Form
	{
		public Testing()
		{
			InitializeComponent();

			imageDropDown1.Items.Add(new KellyControls.ImageListItem("cancel", CRes.cancel));
			imageDropDown1.Items.Add(new KellyControls.ImageListItem("color_box", CRes.color_box));
			imageDropDown1.Items.Add(new KellyControls.ImageListItem("color_dropdown", CRes.color_dropdown));
			imageDropDown1.Items.Add(new KellyControls.ImageListItem("color_grid", CRes.color_grid));
			imageDropDown1.Items.Add(new KellyControls.ImageListItem("color_panel", CRes.color_panel));

			imageListBox1.Items.Add(new KellyControls.ImageListItem("cancel", CRes.cancel));
			imageListBox1.Items.Add(new KellyControls.ImageListItem("color_box", CRes.color_box));
			imageListBox1.Items.Add(new KellyControls.ImageListItem("color_dropdown", CRes.color_dropdown));
			imageListBox1.Items.Add(new KellyControls.ImageListItem("color_grid", CRes.color_grid));
			imageListBox1.Items.Add(new KellyControls.ImageListItem("color_panel", CRes.color_panel));

		}
	}
}
