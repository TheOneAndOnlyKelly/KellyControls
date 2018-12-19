using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace KellyControls.CommonClasses
{
	public class NamedColorListTypeConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var List = new List<NamedColor>();

			if (value == null)
				return List;

			if (value is string)
			{
				var s = (string)value;
				if (s.Length == 0)
					return List;

				var Elements = new List<string>();
				Elements.AddRange(s.Split('|'));
				var ncConv = new NamedColorTypeConverter();

				foreach (string Line in Elements)
				{
					List.Add((NamedColor)ncConv.ConvertFrom(Line));
				}
				ncConv = null;
				return List;
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value != null)
			{
				if (!(value is List<NamedColor>))
					throw new ArgumentException("Invalid List", "value");
			}

			if (destinationType == typeof(string))
			{
				if (value == null)
					return String.Empty;

				var List = (List<NamedColor>)value;

				var Converted = string.Empty;
				var ncConv = new NamedColorTypeConverter();
				foreach (var nColor in List)
				{
					Converted += (Converted.Length > 0 ? "|" : string.Empty) + ncConv.ConvertToString(nColor);
				}
				ncConv = null;
				return Converted;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

}
