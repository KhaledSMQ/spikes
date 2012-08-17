using System;
using System.Windows;
using System.Windows.Threading;

namespace AsyncWrappers
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private delegate int ExecuteDelegate(int v);
        private delegate int ExecuteDelegate2(int v, int w);

        public Window1()
        {
            InitializeComponent();
            Write("Started.");
        }

        private void ResultReady(IAsyncResult ar)
        {
            Write("In callback...");
            var asyncState = ar.AsyncState;
            var ed = asyncState as ExecuteDelegate;
            var r = 0;
            try
            {
                r = ed.EndInvoke(ar);
            }
            catch (SpecificException se)
            {
                Write("Callback caught exception.");
                Write(se.ToString());
            }
            Write("Result = " + r);
        }

        private void ResultReady2(IAsyncResult ar)
        {
            Write("In callback 2...");
            var asyncState = ar.AsyncState;
            var ed = asyncState as ExecuteDelegate2;
            var r = 0;
            try
            {
                r = ed.EndInvoke(ar);
            }
            catch (SpecificException se)
            {
                Write("Callback caught exception.");
                Write(se.ToString());
            }
            Write("Result = " + r);
        }

        public void Write(string text)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(
                        DispatcherPriority.Normal,
                        new Action<string>(Write), text);
                return;
            }

            OutputText.Text += DateTime.Now.ToString("hh:MM:ss.fff") + " - " + text + Environment.NewLine;
        }

        private void RaiseSpecificButton_Click(object sender, RoutedEventArgs e)
        {
            var d = new DummyServer();
            var ed = new ExecuteDelegate(d.Execute);
            Write("Raising specific exception...");
            ed.BeginInvoke(314, AsyncHelper.DispatchExceptions(ResultReady), ed);
        }

        private void RaiseGeneralButton_Click(object sender, RoutedEventArgs e)
        {
            var d = new DummyServer();
            var ed = new ExecuteDelegate(d.Execute);
            Write("Raising general exception...");
            ed.BeginInvoke(271, AsyncHelper.DispatchExceptions(ResultReady), ed);
        }

        private void RaiseSpecific2Button_Click(object sender, RoutedEventArgs e)
        {
            var d = new DummyServer();
            var ed2 = new ExecuteDelegate2(d.Execute);
            Write("Raising specific exception with 2 parameters...");
            ed2.BeginInvoke(314, 271, AsyncHelper.DispatchExceptions(ResultReady2), ed2);
        }

        private void RaiseGeneral2Button_Click(object sender, RoutedEventArgs e)
        {
            var d = new DummyServer();
            var ed2 = new ExecuteDelegate2(d.Execute);
            Write("Raising general exception with 2 parameters...");
            ed2.BeginInvoke(271, 314, AsyncHelper.DispatchExceptions(ResultReady2), ed2);
        }
    }
}
