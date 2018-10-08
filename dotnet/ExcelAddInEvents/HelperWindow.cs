using System;
using System.Windows.Forms;

namespace ExcelAddInEvents
{
	internal class HelperWindow : IWin32Window
	{
		public IntPtr Handle { get; private set; }

		public HelperWindow(IntPtr handle)
		{
			Handle = handle;
		}
	}
}