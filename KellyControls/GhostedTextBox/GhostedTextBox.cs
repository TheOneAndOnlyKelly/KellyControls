using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// https://social.msdn.microsoft.com/Forums/vstudio/en-US/a07c453a-c5dd-40ed-8895-6615cc808d91/search-box?forum=csharpgeneral
/// </summary>
namespace KellyControls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\GhostedTextBox\GhostedTextBox.bmp")]
	public class GhostedTextBox : TextBox
	{
		#region [ Declares ]

		// P/Invoke
		[DllImport("user32.dll", EntryPoint = "SendMessageW")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		#endregion [ Declares ]

		#region [ Constants ]

		private const int EM_SETCUEBANNER = 0x1501;

		#endregion [ Constants ]

		#region [ Private Variables ]

		private string _ghostedValue;

		#endregion [ Private Variables ]

		#region [ Properties ]

		public string GhostedText
		{
			get { return _ghostedValue; }
			set
			{
				_ghostedValue = value;
				UpdateCue();
			}
		}

		public override string Text
		{
			get
			{
				// In Design time, only ever return the set value of the text
				if (this.DesignMode)
					return base.Text;
				else 
				{
					// In Run time, show return the ghosted value if the text property is blank
					if (!string.IsNullOrEmpty(base.Text))
						return _ghostedValue;
					else
						return base.Text;
				}
			}
			set => base.Text = value;
		}

		#endregion [ Properties ]

		#region [ Methods ]

		private void UpdateCue()
		{
			if (!this.IsHandleCreated || string.IsNullOrEmpty(_ghostedValue))
				return;
			IntPtr mem = Marshal.StringToHGlobalUni(_ghostedValue);
			SendMessage(this.Handle, EM_SETCUEBANNER, (IntPtr)1, mem);
			Marshal.FreeHGlobal(mem);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			UpdateCue();
		}

		#endregion [ Methods ]
	}
}
