using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncWrappers
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        private void Run()
        {
            Write("Starting execution...");
            ExecuteSync();
            ExecuteAsync();
            ExecuteWrapper();
            Write("Execution finished.");
            Console.ReadLine();
        }

        private static void ExecuteSync()
        {
            Write("Sync");
            var d = new DummyServer();
            var r = d.Execute(10);
            Write(r.ToString());
        }

        private delegate int ExecuteDelegate(int v);
        private delegate int ExecuteDelegate2(int v, int w);

        private static void ExecuteAsync()
        {
            var d = new DummyServer();
            var ed = new ExecuteDelegate(d.Execute);
            ed.BeginInvoke(11, ResultReady, ed);

            var ed2 = new ExecuteDelegate2(d.Execute);
            ed2.BeginInvoke(11, 22, ResultReady2, ed2);
        }

        private static void ExecuteWrapper()
        {
            var d = new DummyServer();
            var ed = new ExecuteDelegate(d.Execute);
            ed.BeginInvoke(271, AsyncHelper.DispatchExceptions(ResultReady), ed);
        }

        private static void ResultReady(IAsyncResult ar)
        {
            Write("Async");
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
            Write(r.ToString());
        }

        private static void ResultReady2(IAsyncResult ar)
        {
            Write("Async2");
            var asyncState = ar.AsyncState;
            var ed = asyncState as ExecuteDelegate2;
            var r = ed.EndInvoke(ar);
            Write(r.ToString());
        }

        private static void Write(string text)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:MM:ss.fff") + " - " + text);
        }
    }
}
