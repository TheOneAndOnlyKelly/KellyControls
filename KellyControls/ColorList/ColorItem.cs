using KellyControls.CommonClasses;

namespace KellyControls.ColorList
{
	public class ColorItem
	{
		#region [ Private Variables ]

		private string _key;
		private string _text;
		private NamedColor _namedColor;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public NamedColor NamedColor
		{
			get { return _namedColor; }
			set { _namedColor = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public ColorItem()
		{
			Initialize(true);
		}

		public ColorItem(string text)
			: this()
		{
			_key = text;
			_text = text;
		}

		public ColorItem(string text, string key)
			: this()
		{
			_key = key;
			_text = text;
		}

		public ColorItem(string text, int key)
			: this()
		{
			_key = key.ToString();
			_text = text;
		}

		public ColorItem(string text, string key, NamedColor namedColor)
			: this(text, key)
		{
			_namedColor = namedColor;
		}

		protected void Initialize(bool setObjects)
		{
			_key = string.Empty;
			_text = string.Empty;
		}

		#endregion [ Constructors ]

		#region [ Methods ]

		public override string ToString()
		{
			return _text;
		}

		#endregion [ Methods ]
	}
}
