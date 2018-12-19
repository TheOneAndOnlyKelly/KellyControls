using System;
using System.ComponentModel;
using System.Drawing;

namespace KellyControls.CommonClasses
{
	[Serializable]
	public class NamedColor
	{
		#region [ Private Variables ]

		private Color _color;
		private string _name;
		private Point _location;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		[Browsable(false)]
		public Point Location
		{
			get { return _location; }
			set { _location = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public NamedColor()
		{
			_color = Color.White;
			_name = GetColorName(_color);
			_location = Point.Empty;
		}

		public NamedColor(Color color)
		{
			_color = color;
			_name = GetColorName(_color);
			_location = Point.Empty;
		}

		public NamedColor(Color color, string name)
			: this(color)
		{
			if (name.Length == 0)
				_name = GetColorName(color);
			else
				_name = name;
		}

		public NamedColor(string name, Color color)
			: this(color, name)
		{ }

		public NamedColor(Color color, string name, Point location)
			: this(color, name)
		{
			_location = location;
		}

		public NamedColor(string name, int red, int green, int blue)
			: this()
		{
			_name = name;
			_color = Color.FromArgb(red, green, blue);
		}

		public NamedColor(NamedColor nColor)
		{
			_color = nColor.Color;
			_name = nColor.Name;
			_location = new Point(nColor.Location.X, nColor.Location.Y);
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		public static string GetColorName(Color color)
		{
			if (color.A != 255)
				return GetFormattedColorString(color);

			if (color.IsNamedColor)
				return color.Name;

			return GetFormattedColorString(color);
		}

		public static string GetFormattedColorString(Color color)
		{
			return color.R + ", " + color.G + ", " + color.B;
		}

		public string GetRGBString()
		{
			return string.Format("R:{0}, G:{1}, B:{2}", _color.R, _color.G, _color.B);
		}

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(_name))
				return _name;
			else if (_color.IsKnownColor)
				return _color.ToKnownColor().ToString();
			else
				return GetRGBString();
		}

		#endregion [ Methods ]
	}
}
