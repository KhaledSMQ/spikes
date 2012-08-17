using System.Windows;
using Infrastructure;

namespace ComingledLists
{
	public class Workspace : IWorkspace
	{
		private static MainWindow MainWindow
		{
			get { return (MainWindow) Application.Current.MainWindow; }
		}

		public void AddItem(object item)
		{
			MainWindow.AddItem(item);
		}

		public void AddResource(string key, object resource)
		{
			MainWindow.Resources.Add(key, resource);
		}

		public void OutputMessage(string message)
		{
			MainWindow.OutputMessage(message);
		}
	}
}
