using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KellyControls.CommonClasses
{
	public class ImageController
	{
		#region [ Protected Variables ]

		protected List<JoinedImages> _joinedImages = new List<JoinedImages>();
		protected static readonly Lazy<ImageController> lazy = new Lazy<ImageController>(() => new ImageController());

		#endregion [ Protected Variables ]

		#region [ Properties ]

		public static ImageController Instance { get { return lazy.Value; } }

		#endregion [ Properties ]

		#region [ Constructors ]

		public ImageController()
		{ }

		static ImageController()
		{ }

		#endregion [ Constructors ]

		#region [ Methods ]

		#region [ Annotations ]

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <returns>The composited image</returns>
		public virtual Bitmap AddAnnotation(Bitmap image, int annotation)
		{
			return AddAnnotation(image, annotation, AnchorStyles.Top | AnchorStyles.Left, Point.Empty, true);
		}

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <param name="offset">Amount to offset the source image</param>
		/// <returns>The composited image</returns>
		public virtual Bitmap AddAnnotation(Bitmap image, int annotation, Point offset)
		{
			return AddAnnotation(image, annotation, AnchorStyles.Top | AnchorStyles.Left, offset, true);
		}

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <returns>The composited image</returns>
		public virtual Bitmap AddAnnotation(Bitmap image, int annotation, AnchorStyles anchor)
		{
			return AddAnnotation(image, annotation, anchor, Point.Empty, true);
		}

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <param name="offset">Amount to offset the source image</param>
		/// <returns>The composited image</returns>
		public virtual Bitmap AddAnnotation(Bitmap image, int annotation, AnchorStyles anchor, Point offset)
		{
			return AddAnnotation(image, annotation, anchor, offset, true);
		}

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <param name="overlap">Indicates whether the annotation image should be over the image, or if the image should be on top of the annotation image.</param>
		/// <returns>The composited image</returns>
		public Bitmap AddAnnotation(Bitmap image, int annotation, AnchorStyles anchor, bool overlap)
		{
			return AddAnnotation(image, annotation, anchor, Point.Empty, overlap);
		}

		/// <summary>
		/// Combines an image with a bitmap representing an Annotation
		/// </summary>
		/// <param name="image">Source image to modify</param>
		/// <param name="annotation">Annotation enum that indicates what Icon to insert</param>
		/// <param name="anchor">AnchorStyles enum that indicates how the annotation should be positioned relative to the image.</param>
		/// <param name="offset">Amount to offset the source image</param>
		/// <param name="overlap">Indicates whether the annotation image should be over the image, or if the image should be on top of the annotation image.</param>
		/// <returns>The composited image</returns>
		public Bitmap AddAnnotation(Bitmap image, int annotation, AnchorStyles anchor, Point offset, bool overlap)
		{
			if (image == null)
				throw new System.ArgumentNullException("image cannot be null.");

			var Copy = new Bitmap(image.Width, image.Height);

			var annotationImage = GetAnnotationImage(annotation);
			if (annotationImage == null)
				throw new Exception("annotation not found: " + annotation.ToString());

			int Width = annotationImage.Width;
			int Height = annotationImage.Height;
			int X = 0;
			int Y = 0;

			if ((anchor & AnchorStyles.Left) == AnchorStyles.Left)
				X = 0;
			else if ((anchor & AnchorStyles.Right) == AnchorStyles.Right)
				X = Copy.Width - Width;
			else
				X = (Copy.Width - Width) / 2;

			if ((anchor & AnchorStyles.Top) == AnchorStyles.Top)
				Y = 0;
			else if ((anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
				Y = Copy.Height - Height;
			else
				Y = (Copy.Height - Height) / 2;

			using (var g = Graphics.FromImage(Copy))
			{
				g.Clear(Color.Transparent);
				if (overlap)
					g.DrawImage(image, new Rectangle(offset.X, offset.Y, image.Width, image.Height));
				g.DrawImage(annotationImage, new Rectangle(X, Y, Width, Height));
				if (!overlap)
					g.DrawImage(image, new Rectangle(offset.X, offset.Y, image.Width, image.Height));
			}


			return Copy;
		}

		/// <summary>
		/// Returns the image from the Resources that correspond to the enum.
		/// </summary>
		protected virtual Bitmap GetAnnotationImage(int annotation)
		{
			return null;
		}

		#endregion [ Annotations ]

		#region [ Bitmaps ]

		/// <summary>
		/// Finds the bitmap in the program resources that corresponds to the enumeration passed in.
		/// These bitmaps are used to populate the tree and menu system with images.
		/// </summary>
		/// <param name="objectType">Enum indicating the bitmap desired</param>
		/// <returns>If the bitmap is defined for the enum, returns it, else returns null</returns>
		public virtual Bitmap GetBitmap(int objectType)
		{
			return null;
		}

		/// <summary>
		/// Create the requested image and composite it based on the annotation passed in.
		/// </summary>
		/// <param name="objectType">NodeType enum indicating the object</param>
		/// <param name="annotation">Annotation to add to the image</param>
		/// <returns>Composited Bitmap</returns>
		public virtual Bitmap GetBitmap(int objectType, int annotation)
		{
			if (_joinedImages == null)
				return AddAnnotation(GetBitmap(objectType), annotation);
			var Joined = _joinedImages.Where(j => j.ImageType == objectType && j.Annotation == annotation).ToList();
			return Joined.Count > 0 ? Joined[0].Bitmap : AddAnnotation(GetBitmap(objectType), annotation);
		}

		public virtual void BuildJoinedImagesList()
		{
			if (_joinedImages.Count > 0)
				return;			
		}


		#endregion [ Bitmaps ]

		#region [ Folders ]

		/// <summary>
		/// Determines which Icon is currently selected and returns the enum equivalent to the closed image.
		/// If it is not in the list, returns the original value.
		/// </summary>
		/// <param name="imageType"></param>
		/// <returns></returns>
		public virtual int GetClosedFolder(int imageType)
		{
			return 0;
		}

		/// <summary>
		/// Determines which Icon is currently selected and returns the enum equivalent to the open image.
		/// If it is not in the list, returns the original value.
		/// </summary>
		/// <param name="imageType"></param>
		/// <returns></returns>
		public virtual int GetOpenFolder(int imageType)
		{
			return 0;
		}

		#endregion [ Folders ]

		#region [ Icons ]

		/// <summary>
		/// Pulls out an icon from the Resources based the ImageType
		/// </summary>
		/// <param name="imageType">Enumeration indicating which icon to return</param>
		/// <param name="isNew">Indicates if the icon should have a "new" annotation. Relies on if that icon was precreated.</param>
		/// <returns>System.Drawing.Icon</returns>
		public virtual Icon GetIcon(int imageType, bool isNew = false)
		{
			return null;
		}

		#endregion [ Icons ]

		#region [ Misc ]

		/// <summary>
		/// Creates an image that contains the text specified.
		/// </summary>
		/// <param name="message">Message to print on bitmap.</param>
		/// <param name="background">Color to use for the background.</param>
		/// <param name="foreground">Color to use for the foreground.</param>
		/// <returns></returns>
		public virtual Bitmap CreateErrorMessageImage(string message, Color background, Color foreground)
		{
			Bitmap Output = null;
			message += "  ";
			int Width, Height;

			using (var Font = new Font("Arial", 12f))
			{
				using (var Temp = new Bitmap(16, 16))
				using (var g = Graphics.FromImage(Temp))
				{
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
					var Format = new StringFormat();
					Format.SetMeasurableCharacterRanges(new CharacterRange[] { new CharacterRange(0, message.Length) });
					var r = g.MeasureCharacterRanges(message, Font, new Rectangle(0, 0, 1000, 1000), Format);
					var rect = r[0].GetBounds(g);
					Width = (int)rect.Width;
					Height = (int)rect.Height;
					Format = null;
				}
				Output = new Bitmap(Width + 32, Height + 16);
				using (var g = Graphics.FromImage(Output))
				{
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
					g.Clear(background);
					using (var Pen = new Pen(foreground))
						g.DrawRectangle(Pen, new Rectangle(0, 0, Output.Width - 1, Output.Height - 1));
					using (var Brush = new SolidBrush(foreground))
						g.DrawString(message, Font, Brush, new Rectangle(16, 8, Width + 20, Height), new StringFormat());
				}
			}
			return Output;
		}

		/// <summary>
		/// Creates a fuzzy drop shadow on the graphics device, defined by the GraphicsPath passed in.
		/// http://www.codeproject.com/Articles/15847/Fuzzy-DropShadows-in-GDI
		/// </summary>
		/// <param name="g">Graphics object to use.</param>
		/// <param name="path">Path for the fuzzy drop shadow to follow.</param>
		/// <param name="shadowColor">Color to make the drop shadow. In the example on the website, Color.DimGray was used.</param>
		/// <param name="transparency">Transparency of the shadow. In the example on the website, a value of 180 was used.</param>
		public virtual void FuzzyDropShadow(Graphics g, GraphicsPath path, Color shadowColor, byte transparency)
		{
			using (var Brush = new PathGradientBrush(path))
			{
				// Set the wrapmode so that the colors will layer themselves from the outer edge in
				Brush.WrapMode = WrapMode.Clamp;

				// Create a color blend to manage our colors and positions and
				// since we need 3 colors set the default length to 3
				var ColorBlend = new ColorBlend(3);

				// Here is the important part of the shadow making process, remember the clamp mode on the colorblend object layers the colors from
				// the outside to the center so we want our transparent color first followed by the actual shadow color. Set the shadow color to a 
				// slightly transparent DimGray, I find that it works best.
				ColorBlend.Colors = new Color[] { Color.Transparent, Color.FromArgb(transparency, shadowColor), Color.FromArgb(transparency, shadowColor) };

				// Our color blend will control the distance of each color layer we want to set our transparent color to 0 indicating that the 
				// transparent color should be the outer most color drawn, then our Dimgray color at about 10% of the distance from the edgen
				//ColorBlend.Positions = new float[] { 0f, .1f, 1f };
				ColorBlend.Positions = new float[] { 0f, .4f, 1f };

				// Assign the color blend to the pathgradientbrush
				Brush.InterpolationColors = ColorBlend;

				// Fill the shadow with our pathgradientbrush
				g.FillPath(Brush, path);
			}
		}

		/// <summary>
		/// Gets the image format from the image.
		/// </summary>
		public virtual ImageFormat GetImageFormat(Image img)
		{
			if (img.RawFormat.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (img.RawFormat.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;
			if (img.RawFormat.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (img.RawFormat.Equals(ImageFormat.Emf))
				return ImageFormat.Emf;
			if (img.RawFormat.Equals(ImageFormat.Exif))
				return ImageFormat.Exif;
			if (img.RawFormat.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (img.RawFormat.Equals(ImageFormat.Icon))
				return ImageFormat.Icon;
			if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
				return ImageFormat.MemoryBmp;
			if (img.RawFormat.Equals(ImageFormat.Tiff))
				return ImageFormat.Tiff;
			else
				return ImageFormat.Wmf;
		}

		/// <summary>
		/// Based on the raw format of the bitmap object, return the extension that best fits.
		/// </summary>
		/// <param name="bitmap">Bitmap object to be examined.</param>
		/// <returns>Returns extention based on raw format.</returns>
		public virtual string GetFileExtension(Bitmap bitmap)
		{
			if (bitmap == null)
				throw new ArgumentNullException("bitmap cannot be null.");

			ImageFormat Format = GetImageFormat(bitmap);

			if (Format.Equals(ImageFormat.Jpeg))
				return "jpg";
			else if (Format.Equals(ImageFormat.Bmp))
				return "bmp";
			else if (Format.Equals(ImageFormat.Emf))
				return "emf";
			else if (Format.Equals(ImageFormat.Exif))
				return "exf";
			else if (Format.Equals(ImageFormat.Gif))
				return "gif";
			else if (Format.Equals(ImageFormat.MemoryBmp))
				return "mbmp";
			else if (Format.Equals(ImageFormat.Png))
				return "png";
			else if (Format.Equals(ImageFormat.Tiff))
				return "tif";
			else if (Format.Equals(ImageFormat.Wmf))
				return "wmf";
			else
				return string.Empty;
		}

		/// <summary>
		/// Loads a bitmap from file.
		/// </summary>
		/// <param name="filename">Name of the file to load.</param>
		/// <returns>Bitmap object</returns>
		public virtual Bitmap LoadBitmapFromFile(string filename)
		{
			try
			{
				return new Bitmap(filename);
			}
			catch (Exception Exception)
			{
				System.Diagnostics.Debug.WriteLine(Exception.ToString());
				return null;
			}
		}

		/// <summary>
		/// Loads a bitmap from an encoded string.
		/// </summary>
		/// <param name="encodedStream">Encoded byte string</param>
		/// <returns>Bitmap object</returns>
		public virtual Bitmap LoadBitmapFromEncoded(string encodedStream)
		{
			byte[] imageByteArray = Convert.FromBase64String(encodedStream);
			if (imageByteArray.Length == 0)
				return null;

			return new Bitmap(new MemoryStream(imageByteArray));
		}

		/// <summary>
		/// http://stackoverflow.com/questions/9356694/tint-property-when-drawing-image-with-vb-net
		/// Tints a bitmap using the specified color and intensity.
		/// </summary>
		/// <param name="b">Bitmap to be tinted</param>
		/// <param name="color">Color to use for tint</param>
		/// <returns>A bitmap with the requested Tint</returns>
		public virtual Bitmap TintBitmap(Bitmap b, Color color)
		{
			var b2 = new Bitmap(b.Width, b.Height);
			var ia = new ImageAttributes();

			var cMatrix = new ColorMatrix(new float[][] {
							new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
							new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
							new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
							new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
							new float[] { (float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f, 0.0f, 1.0f }
						});

			ia.SetColorMatrix(cMatrix);
			var g = Graphics.FromImage(b2);
			g.DrawImage(b, new Rectangle(0, 0, b.Width, b.Height), 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia);

			g.Dispose();

			return b2;
		}

		public virtual Color ColorMixer(Color c1, Color c2)
		{
			int Red = (c1.R + c2.R) / 2;
			int Green = (c1.G + c2.G) / 2;
			int Blue = (c1.B + c2.B) / 2;

			return Color.FromArgb((byte)Red, (byte)Green, (byte)Blue);
		}

		#endregion [ Misc ]

		#endregion [ Methods ]

		#region [ Structs ]

		public struct JoinedImages
		{
			public int ImageType { get; set; }
			public int Annotation { get; set; }
			public Bitmap Bitmap { get; set; }

			public JoinedImages(int imageType, int annotation, Bitmap bitmap)
			{
				ImageType = imageType;
				Annotation = annotation;
				Bitmap = bitmap;
			}
		}

		#endregion [ Structs ]
	}
}