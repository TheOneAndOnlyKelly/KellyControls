using System;

namespace KellyControls.PlugInToolBtn.ToolGroups
{

	[AttributeUsage(AttributeTargets.Class)]
	public class ToolGroup : Attribute
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

		public ToolGroup(string name)
		{
			_name = name + "_ToolGroup";
		}

		#endregion [ Constructors ]
	}
}