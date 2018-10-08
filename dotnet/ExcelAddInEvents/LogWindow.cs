using System;
using System.Windows.Forms;

namespace ExcelAddInEvents
{
	public partial class LogWindow : Form
	{
		public LogWindow()
		{
			InitializeComponent();
		}

		public void WriteLog(string message)
		{
			LogTextBox.Text += DateTime.Now.ToLocalTime() + ": " + message + Environment.NewLine;
		}
	}
}
