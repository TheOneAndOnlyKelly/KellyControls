using System.Drawing;

namespace KellyControls
{
	public class ImageListItem
	{
		public Bitmap Image = null;
		public string Text = string.Empty;
		public string Key = string.Empty;
		public object Tag = null;

		#region[ Constructors ]

		public ImageListItem(string text)
		{
			this.Text = text;
		}

		public ImageListItem(string text, string key)
			: this(text)
		{
			this.Key = key;
		}

		public ImageListItem(string text, Bitmap image)
			: this(text)
		{
			this.Image = image;
		}

		public ImageListItem(string text, string key, Bitmap image)
			: this(text, key)
		{
			this.Image = image;
		}

		public ImageListItem(string text, int key, Bitmap image)
			: this(text, key.ToString())
		{
			this.Image = image;
		}

		public ImageListItem(string text, string value, Bitmap image, object tag)
			: this(text, value, image)
		{
			this.Tag = tag;
		}

		#endregion[ Constructors ]

		public override string ToString()
		{
			return this.Text;
		}
	}

}
