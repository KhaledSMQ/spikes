using System.Windows;
using Infrastructure;

namespace ComingledLists
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private Window Window { get; set; }

		public App()
		{
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			Startup += App_Startup;
		}

		private void SubscribeToWindowEvents()
		{
			Window.Loaded += Window_Loaded;
		}

		void App_Startup(object sender, StartupEventArgs e)
		{
			Window = new MainWindow();
			SubscribeToWindowEvents();
			MainWindow = Window;
			Window.Show();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitializeApplication();
		}

		private static void InitializeApplication()
		{
			var main = MainApplication.Instance;
			main.Workspace = new Workspace();
			main.Initialize();
		}
	}
}
