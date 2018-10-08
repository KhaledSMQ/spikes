using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace PushFrameExperiments
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private DispatcherTimer NormalDispatcherTimer { get; set; }
        private Timer NormalTimer { get; set; }
        private DispatcherTimer DispatcherTimerWithPushFrame { get; set; }

		public MainWindow()
		{
			InitializeComponent();
		}

		private void PushFrame()
		{
			var frame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(RunInFrame), frame);
			Dispatcher.PushFrame(frame);
		}

		private object RunInFrame(object obj)
		{
			for (var i = 0; i < 10; i++)
			{
				OutputText("Frame output");
				Thread.Sleep(2000);
			}
			var frame = (DispatcherFrame)obj;
			frame.Continue = false;
			return null;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NormalDispatcherTimer = new DispatcherTimer();
            NormalDispatcherTimer.Tick += (ts, te) => OutputText("DispatcherTimer output");
            NormalDispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            NormalDispatcherTimer.Start();

			NormalTimer = new Timer(
				ts => OutputText("Timer output"),
				null, 2000, 2000);

		    var delayInTicks = 3;
            DispatcherTimerWithPushFrame = new DispatcherTimer();
            DispatcherTimerWithPushFrame.Tick += (ts, te) =>
			    {
			        delayInTicks--;
                    if (delayInTicks == 0)
			            PushFrame();
			    };
            DispatcherTimerWithPushFrame.Interval = new TimeSpan(0, 0, 2);
            DispatcherTimerWithPushFrame.Start();
		}

		private void OutputText(string text)
		{
			Dispatcher.Invoke(() => Output.Text += text + " " + DateTime.Now + Environment.NewLine, DispatcherPriority.Normal);
		}
	}
}
