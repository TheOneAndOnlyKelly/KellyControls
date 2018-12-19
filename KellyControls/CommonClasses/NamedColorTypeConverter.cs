using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace KellyControls.CommonClasses
{
	public class NamedColorTypeConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
				return new NamedColor();

			if (value is string)
			{
				var s = (string)value;
				if (s.Length == 0)
					return new NamedColor();

				var parts = s.Split(';');

				// Determine if name is stored as first and 
				// last; first, middle, and last;
				// or is in error.
				if (parts.Length != 3)
				{
					throw new ArgumentException("NamedColor must have 2 or 3 parts.", "value");
				}

				var ColorValue = parts[0];
				var Name = parts[1];
				var Loc = parts[2];

				Color Color = Color.FromArgb(Int32.Parse(ColorValue, System.Globalization.NumberStyles.HexNumber));
				var Location = new Point(Int32.Parse(Loc.Split(',')[0]), Int32.Parse(Loc.Split(',')[1]));
				return new NamedColor(Color, Name, Location);
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value != null)
			{
				if (!(value is NamedColor))
				{
					throw new ArgumentException("Invalid NamedColor", "value");
				}
			}

			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return String.Empty;
				}

				var nColor = (NamedColor)value;
				var ColorValue = nColor.Color.ToArgb();
				var ConvColor = string.Format("#{0:X8}", ColorValue);
				return ConvColor + ";" + nColor.Name + ";" + nColor.Location.X + "," + nColor.Location.Y;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

}
