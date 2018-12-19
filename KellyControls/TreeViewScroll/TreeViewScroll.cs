using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KellyControls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(@"C:\Source\Repos\TheOneAndOnlyKelly\Utilities\KellyControls\TreeViewScroll\TreeViewScroll.bmp")]
	public class TreeViewScroll : TreeView
	{
		private const int SB_HORZ = 0;
		private const int SB_VERT = 1;
		private const int WM_VSCROLL = 0x0115;
		private const int WM_HSCROLL = 0x0114;
		private const int WM_MOUSEWHEEL = 0x020A;

		[DllImport("user32.dll")]
		private static extern int GetScrollPos(int hWnd, int nBar);

		public int HScrollPos => GetScrollPos((int)Handle, SB_VERT);

		public int VScrollPos => GetScrollPos((int)Handle, SB_HORZ);

		public event ScrollEventHandler Scroll;

		[DebuggerHidden]
		protected override void WndProc(ref Message m)
		{
			if (Scroll != null)
			{
				switch (m.Msg)
				{
					case WM_VSCROLL:
					case WM_HSCROLL:
					case WM_MOUSEWHEEL:
						{
							var ScrollEventType = (ScrollEventType)Enum.Parse(typeof(ScrollEventType), (m.WParam.ToInt32() & 65535).ToString());
							Scroll(m.HWnd, new ScrollEventArgs(ScrollEventType, ((int)(m.WParam.ToInt64() >> 16)) & 255));
						}
						break;
				}
			}
			base.WndProc(ref m);
		}

		public static TreeNode FindByPath(TreeView treeControl, string fullPath)
		{
			if (String.IsNullOrEmpty(fullPath))
				return null;

			var NameList = fullPath.Split('\\');

			// have to check all the top level nodes first
			var NodeName = NameList[0];

			var Match = treeControl.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Text == NodeName);
			if (Match == null)
				return null;

			for (var Index = 1; Index < NameList.Length; Index++)
			{
				NodeName = NameList[Index];

				foreach (TreeNode Node in Match.Nodes)
				{
					if (Node.Text != NodeName)
						continue;
					Match = Node;
					break;
				}
			}

			return Match.Text == NameList[NameList.Length - 1] ? Match : null;
		}

		public TreeNode FindByPath(string fullPath)
		{
			return FindByPath(this, fullPath);
		}
	}
}
