namespace KellyControls.CommonClasses
{
	public class CMYK
	{
		#region [ Private Variables ]

		double _c;
		double _m;
		double _y;
		double _k;
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

		public double C
		{
			get { return _c; }
			set
			{
				_c = value;
				_c = _c > 1 ? 1 : _c < 0 ? 0 : _c;
			}
		}

		public double M
		{
			get { return _m; }
			set
			{
				_m = value;
				_m = _m > 1 ? 1 : _m < 0 ? 0 : _m;
			}
		}

		public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
				_y = _y > 1 ? 1 : _y < 0 ? 0 : _y;
			}
		}

		public double K
		{
			get { return _k; }
			set
			{
				_k = value;
				_k = _k > 1 ? 1 : _k < 0 ? 0 : _k;
			}
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public CMYK()
		{
			_c = 0;
			_m = 0;
			_y = 0;
			_k = 0;
			_alpha = 1;
		}

		public CMYK(double cyan, double magenta, double yellow, double black)
			: this()
		{
			_c = cyan;
			_m = magenta;
			_y = yellow;
			_k = black;
		}

		public CMYK(double cyan, double magenta, double yellow, double black, double alpha)
			: this(cyan, magenta, yellow, black)
		{
			_alpha = alpha;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		public override string ToString()
		{
			return "A: " + (_alpha * 255) + " C: " + (_c * 255) + " M: " + (_m * 255) + " Y: " + (_y * 255) + " K: " + (_k * 255);
		}

		#endregion [ Methods ]
	}
}
