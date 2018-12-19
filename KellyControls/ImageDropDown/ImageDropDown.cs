using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KellyControls
{
	/// <summary>
	/// ComboBox modified to display an image on the left side of the text.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\ImageDropDown\ImageDropDown.bmp")]
	public class ImageDropDown : ComboBox
	{
		#region [ Constructors ]

		public ImageDropDown()
		{
			this.DrawMode = DrawMode.OwnerDrawVariable;
		}

		#endregion [ Constructors ]

		#region [ Events ]

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground();

			if ((e.Index < this.Items.Count) && (this.Items.Count > 0) && (e.Index >= 0))
			{
				var Text = this.Items[e.Index].ToString();
				Image Bitmap = null;
				if (this.Items[e.Index] is ImageListItem)
					Bitmap = ((ImageListItem)this.Items[e.Index]).Image;
				int ItemHeight = 0;

				if (e.Index != -1)
				{
					var TextSize = e.Graphics.MeasureString(Text, this.Font);

					if (Bitmap != null)
						ItemHeight = (int)(Math.Max(TextSize.Height, Bitmap.Height));
					else
						ItemHeight = (int)TextSize.Height;

					// Draw the image in combo box using its bound and ItemHeight 
					if (Bitmap != null)
					{
						if (this.Enabled)
							e.Graphics.DrawImage(Bitmap, e.Bounds.X, e.Bounds.Y, Bitmap.Height, Bitmap.Height);
						else
							ControlPaint.DrawImageDisabled(e.Graphics, Bitmap, e.Bounds.X, e.Bounds.Y, this.BackColor);
					}

					var StringFormat = new StringFormat()
					{
						Trimming = StringTrimming.EllipsisCharacter
					};
					var DisabledColor = ControlPaint.Light(SystemColors.WindowText);
					
					// We need to draw the item as string because we made drawmode to ownervariable
					if (this.Enabled)
						e.Graphics.DrawString(Text, Font, Brushes.Black, new RectangleF(e.Bounds.X + ItemHeight, e.Bounds.Y, DropDownWidth, ItemHeight));
					else
						ControlPaint.DrawStringDisabled(e.Graphics, Text, Font, Color.Transparent, new RectangleF(e.Bounds.X + ItemHeight, e.Bounds.Y, DropDownWidth, ItemHeight), StringFormat);
				}
			}
			// draw rectangle over the item selected 
			e.DrawFocusRectangle();

		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			var Text = string.Empty;
			Image Bitmap = null;
			var TextSize = SizeF.Empty;
			float Width = 0;
			float MaxWidth = 0;

			using (var Graphics = CreateGraphics())
			{
				for (var i = 0; i < Items.Count; i++)
				{
					Text = this.Items[e.Index].ToString();
					if (this.Items[e.Index] is ImageListItem)
						Bitmap = ((ImageListItem)this.Items[e.Index]).Image;

					TextSize = e.Graphics.MeasureString(Text, this.Font);
					Width = TextSize.Width;
					if (Bitmap != null)
						Width += Bitmap.Width;
					MaxWidth = Math.Max(Width, MaxWidth);

					if (i == e.Index)
					{
						e.ItemHeight = (int)Math.Max(TextSize.Height, (Bitmap == null) ? 0 : Bitmap.Height);
						e.ItemWidth = (int)Math.Ceiling(Width);
					}
				}
				//DropDownWidth = (int)Math.Ceiling(MaxWidth) + 20;
			}
		}

		#endregion [ Events ]
	}

}
