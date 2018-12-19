using System.Drawing;

namespace KellyControls.CommonClasses
{
	public class HSL
	{
		#region [ Private Variables ]

		double _h;
		double _s;
		double _l;
		double _alpha;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public double Alpha
		{
			get { return _alpha; }
			set
			{
				_alpha = value;
				_alpha = _alpha > 1 ? 1 : _alpha < 0 ? 0 : _alpha;
			}
		}

		/// <summary>
		/// The "attribute of a visual sensation according to which an area appears to be similar to one of the perceived colors: red, yellow, green, and blue, or to a combination of two of them".
		/// </summary>
		public double Hue
		{
			get { return this.H; }
			set { this.H = value; }
		}

		/// <summary>
		/// Hue. The "attribute of a visual sensation according to which an area appears to be similar to one of the perceived colors: red, yellow, green, and blue, or to a combination of two of them".
		/// </summary>
		public double H
		{
			get { return _h; }
			set
			{
				_h = value;
				_h = _h > 1 ? 1 : _h < 0 ? 0 : _h;
			}
		}

		/// <summary>
		/// The "colorfulness of a stimulus relative to its own brightness".
		/// </summary>
		public double Saturation
		{
			get { return this.S; }
			set { this.S = value; }
		}

		/// <summary>
		/// Saturation. The "colorfulness of a stimulus relative to its own brightness".
		/// </summary>
		public double S
		{
			get { return _s; }
			set
			{
				_s = value;
				_s = _s > 1 ? 1 : _s < 0 ? 0 : _s;
			}
		}

		/// <summary>
		/// The radiance weighted by the effect of each wavelength on a typical human observer, measured in candela per square meter (cd/m2). 
		/// Often the term luminance is used for the relative luminance, Y/Yn, where Yn is the luminance of the reference white point.
		/// </summary>
		public double Luminance
		{
			get { return this.L; }
			set { this.L = value; }
		}

		/// <summary>
		/// Luminance. The radiance weighted by the effect of each wavelength on a typical human observer, measured in candela per square meter (cd/m2). 
		/// Often the term luminance is used for the relative luminance, Y/Yn, where Yn is the luminance of the reference white point.
		/// </summary>
		public double L
		{
			get { return _l; }
			set
			{
				_l = value;
				_l = _l > 1 ? 1 : _l < 0 ? 0 : _l;
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public HSL()
		{
			_h = 0;
			_s = 0;
			_l = 0;
			_alpha = 1;
		}

		/// <summary>
		/// Create a "pure" color version of this color.
		/// </summary>
		/// <param name="hue"></param>
		public HSL(double hue)
			: this()
		{
			_h = hue;
			_s = 1;
			_l = 1;
		}

		public HSL(double hue, double saturation, double luminosity)
			: this(hue)
		{
			_s = saturation;
			_l = luminosity;
		}

		public HSL(double hue, double saturation, double luminosity, double alpha)
			: this(hue, saturation, luminosity)
		{
			_alpha = alpha;
		}

		public HSL(HSL colorToCopy)
		{
			_alpha = colorToCopy.Alpha;
			_h = colorToCopy.H;
			_s = colorToCopy.S;
			_l = colorToCopy.L;
		}

		public HSL(Color rgb)
		{
			HSL colorToCopy = ColorManager.RGB_to_HSL(rgb);
			_alpha = colorToCopy.Alpha;
			_h = colorToCopy.H;
			_s = colorToCopy.S;
			_l = colorToCopy.L;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		public override string ToString()
		{
			return "A: " + (_alpha * 255) + " H: " + (_h * 360) + " S: " + (_s * 100) + " L: " + (_l * 100);
		}

		#endregion [ Methods ]

	}
}
