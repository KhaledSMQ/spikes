using System;
using System.Windows.Forms;

namespace LogFileAnalyzer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			EnableDragAndDrop();
		}

		private void EnableDragAndDrop()
		{
			AllowDrop = true;
			DragEnter += Form1_DragEnter;
			DragDrop += Form1_DragDrop;
		}

		private void ProcessFile(string filename)
		{
			MessageBox.Show(filename);
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			var filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (filenames.Length >= 1)
			{
				var filename = filenames[0];
				ProcessFile(filename);
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}
	}
}
