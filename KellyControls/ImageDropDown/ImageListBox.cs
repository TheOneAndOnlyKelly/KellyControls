using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KellyControls
{
	/// <summary>
	/// ListBox modified to display an image on the left side of the text.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\ImageDropDown\ImageListBox.bmp")]
	public class ImageListBox : ListBox
	{
		#region [ Constructors ]

		public ImageListBox()
		{
			this.DrawMode = DrawMode.OwnerDrawVariable;
		}

		#endregion [ Constructors ]

		#region [ Events ]

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			string Text = string.Empty;
			Bitmap Bitmap = null;

			// Get the Bounding rectangle
			var Rectangle = new Rectangle(e.Bounds.X + 16, e.Bounds.Y, e.Bounds.Width - 16, e.Bounds.Height);

			// Setup the StringFormat object
			var StringFormat = new StringFormat();

			// Draw the rectangle
			e.Graphics.FillRectangle(new SolidBrush(Color.White), Rectangle);

			var Rectangle1 = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
			int YOffset = 0;

			// Get the bitmap for this entry
			if ((e.Index < this.Items.Count) && (e.Index >= 0))
			{
				Text = this.Items[e.Index].ToString();
				if (this.Items[e.Index] is ImageListItem)
					Bitmap = ((ImageListItem)this.Items[e.Index]).Image;
				YOffset = (e.Bounds.Height - ((Bitmap != null) ? Bitmap.Height : 0)) / 2;
			}

			if (this.Enabled)
			{
				// Check if the item is selected
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					// Paint the item accordingly if it is selected
					if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
						e.DrawFocusRectangle();
					e.Graphics.FillRectangle(SystemBrushes.Highlight, Rectangle1);
					if (Bitmap != null)
						e.Graphics.DrawImage(Bitmap, e.Bounds.X, e.Bounds.Y + YOffset, Bitmap.Width, Bitmap.Height);
					e.Graphics.DrawString(Text, this.Font, SystemBrushes.HighlightText, Rectangle, StringFormat);
				}
				else
				{
					// Paint the item that if not selected
					e.Graphics.FillRectangle(SystemBrushes.Window, Rectangle1);
					if (Bitmap != null)
						e.Graphics.DrawImage(Bitmap, e.Bounds.X, e.Bounds.Y + YOffset, Bitmap.Width, Bitmap.Height);
					e.Graphics.DrawString(Text, this.Font, SystemBrushes.WindowText, Rectangle, StringFormat);
					e.DrawFocusRectangle();
				}
			}
			else
			{
				// Paint the item disabled
				e.Graphics.FillRectangle(SystemBrushes.Window, Rectangle1);
				if (Bitmap != null)
					e.Graphics.DrawImage(Bitmap, e.Bounds.X, e.Bounds.Y + YOffset, Bitmap.Width, Bitmap.Height);
				e.Graphics.DrawString(Text, this.Font, SystemBrushes.GrayText, Rectangle, StringFormat);
				e.DrawFocusRectangle();
			}
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			int Height = 0;

			if ((e.Index < this.Items.Count) && (e.Index >= 0))
			{
				if (this.Items[e.Index] is ImageListItem)
				{
					var bmp = ((ImageListItem)this.Items[e.Index]).Image;
					Height = bmp?.Height ?? 0;
				}
			}
			var TextSize = e.Graphics.MeasureString("Xfg", this.Font);

			e.ItemHeight = Math.Max(Height, (int)Math.Ceiling(TextSize.Height));
		}

		#endregion [ Events ]
	}

}
