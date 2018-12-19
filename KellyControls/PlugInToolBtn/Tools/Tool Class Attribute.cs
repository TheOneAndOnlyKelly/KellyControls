using System;

namespace KellyControls.PlugInToolBtn.Tools
{
	[AttributeUsage(AttributeTargets.Class)]
	public class Tool : Attribute
	{
		#region [ Private Variables ]

		private string _name;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors ]

		public Tool(string name)
		{
			_name = name;
		}

		#endregion [ Constructors ]
	}
}
