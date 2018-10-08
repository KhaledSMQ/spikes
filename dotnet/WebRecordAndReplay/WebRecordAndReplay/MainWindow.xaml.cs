using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WebRecorder;

namespace WebRecordAndReplay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private Browser Browser { get; set; }
        private bool IsRecording { get; set; }
        private bool IsPlaying { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			Initialize();
			SubscribeToEvents();
		}

		private void Initialize()
		{
			Browser = new Browser();
			MainDockPanel.Children.Add(Browser.WebView);
			InitializeCommandsGrid();
		}

		private void InitializeCommandsGrid()
		{
			var properties = new[] {"Type", "Href", "Id", "Path", "Selector", "Value"};
			foreach (var property in properties)
			{
				var column = new DataGridTextColumn
					             {
									 Header = property,
						             Binding = new Binding(property),
									 MaxWidth = 120
					             };
				CommandsGrid.Columns.Add(column);
			}
		}

		private void Shutdown()
		{
			Browser.Shutdown();
		}

		private void SubscribeToEvents()
		{
			Closing += MainWindowClosing;
            Browser.LogMessageReceived += BrowserLogMessageReceived;
            Browser.ConsoleMessageReceived += BrowserConsoleMessageReceived;
            Browser.FinishedPlaying += BrowserFinishedPlaying;
			Browser.UserActionPlaying += BrowserUserActionPlaying;
			Browser.UserActionPlayed += BrowserUserActionPlayed;
			Browser.UserActionRecording += BrowserUserActionRecording;
			Browser.UserActionRecorded += BrowserUserActionRecorded;
		}

		private void Navigate(string address)
		{
			Browser.Navigate(address);
		}

		private void StartRecording()
		{
			if (!IsRecording)
			{
				IsRecording = true;
				RecordButton.Content = "Recording";
				Browser.StartRecording();
			}
		}

		private void StopRecording()
		{
			if (IsRecording)
			{
				RecordButton.Content = "Record";
				IsRecording = false;
                Browser.StopRecording();
			}
		}

		private void StartPlaying()
		{
            if (!IsPlaying)
            {
                IsPlaying = true;
                PlayButton.Content = "Playing";
                Browser.StartPlaying();
            }
		}

        private void StopPlaying()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                PlayButton.Content = "Play";
                Browser.StopPlaying();
            }
        }

		private void AddUserActionToGrid(UserAction userAction)
		{
			Dispatcher.BeginInvoke((Action)(() => CommandsGrid.Items.Add(userAction)));
		}

        private void BrowserConsoleMessageReceived(object sender, ConsoleMessageEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() => { LogOutput.Text += "console: " + e.Message + Environment.NewLine; }));
        }

        private void BrowserLogMessageReceived(object sender, LogMessageEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() => { LogOutput.Text += e.Message + Environment.NewLine; }));
        }

        private void BrowserFinishedPlaying(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(StopPlaying));
        }

		private void BrowserUserActionPlaying(object sender, UserActionEventArgs e)
		{
		}

		private void BrowserUserActionPlayed(object sender, UserActionEventArgs e)
		{
		}

		private void BrowserUserActionRecording(object sender, UserActionEventArgs e)
		{
		}

		private void BrowserUserActionRecorded(object sender, UserActionEventArgs e)
		{
			var userAction = e.UserAction;
			AddUserActionToGrid(userAction);
		}

		private void AddressPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				var address = AddressTextBox.Text;
				if (!string.IsNullOrWhiteSpace(address))
				{
					Navigate(address);
				}
			}
		}

		private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Shutdown();
		}

		private void RecordButtonOnClick(object sender, RoutedEventArgs e)
		{
			if (IsRecording)
				StopRecording();
			else
				StartRecording();

            if (IsPlaying)
                StopPlaying();
		}

		private void PlayButtonOnClick(object sender, RoutedEventArgs e)
		{
			if (IsRecording)
				StopRecording();

			StartPlaying();
		}

	    private void ClearLogButton_OnClick(object sender, RoutedEventArgs e)
	    {
	        LogOutput.Clear();
	    }
	}
}
