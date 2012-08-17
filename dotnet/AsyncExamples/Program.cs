using System;
using System.ComponentModel;
using System.Threading;

namespace AsyncExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var program = new Program();
            program.Run();
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private void Run()
        {
            CallSynchronously();
            CallWithApm();
            CallWithBackgroundWorker();
            CallWithQueueUserWorkItem();
            CallWithThreadStart();
        }

        private void SomeMethod()
        {
            // simulate long operation
            Thread.Sleep(1000);
        }

        private string SomeOtherMethod(int i)
        {
            // simulate long operation
            Thread.Sleep(1000);
            return i.ToString();
        }

        #region Synchronous

        private void CallSynchronously()
        {
            Print("CallSynchronously() calling SomeMethod()...");
            SomeMethod();
            Print("CallSynchronously() called SomeMethod().");

            var param = 100;
            Print("CallSynchronously() calling SomeOtherMethod()...");
            var s = SomeOtherMethod(param);
            Print("CallSynchronously() called SomeOtherMethod() with result = " + s);
        }

        #endregion

        #region APM

        private void CallWithApm()
        {
            Print("CallWithApm() calling void SomeMethod()...");
            Action action = SomeMethod;
            action.BeginInvoke(ActionCompleted, action);
            Print("CallWithApm() called void SomeMethod().");

            var param = 200;
            Print("CallWithApm() calling SomeOtherMethod()...");
            Func<int, string> func = SomeOtherMethod;
            func.BeginInvoke(param, FuncCompleted, func);
            Print("CallWithApm() called SomeOtherMethod().");
        }

        private void ActionCompleted(IAsyncResult ar)
        {
            var action = ar.AsyncState as Action;
            try
            {
                action.EndInvoke(ar);
            }
            catch(Exception e)
            {
                Print("Exception occurred! " + e);
            }
            Print("CallWithApm() SomeMethod is done.");
        }

        private void FuncCompleted(IAsyncResult ar)
        {
            var func = ar.AsyncState as Func<int, string>;
            var s = string.Empty;
            try
            {
                s = func.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Print("Exception occurred! " + e);
            }
            Print("CallWithApm() SomeOtherMethod is done with result = " + s);
        }

        #endregion

        #region BackgroundWorker

        private void CallWithBackgroundWorker()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += DoWorkSomeMethod;
            worker.RunWorkerCompleted += RunWorkerCompletedSomeMethod;
            Print("CallWithBackgroundWorker() calling SomeMethod()...");
            worker.RunWorkerAsync();
            Print("CallWithBackgroundWorker() called SomeMethod().");

            var otherWorker = new BackgroundWorker();
            otherWorker.DoWork += DoWorkSomeOtherMethod;
            otherWorker.RunWorkerCompleted += RunWorkerCompletedSomeOtherMethod;
            Print("CallWithBackgroundWorker() calling SomeOtherMethod()...");
            var param = 300;
            otherWorker.RunWorkerAsync(param);
            Print("CallWithBackgroundWorker() called SomeOtherMethod().");
        }

        private void DoWorkSomeMethod(object sender, DoWorkEventArgs e)
        {
            Print("CallWithBackgroundWorker() DoWorkSomeMethod is executing...");
            SomeMethod();
        }

        private void RunWorkerCompletedSomeMethod(object sender, RunWorkerCompletedEventArgs e)
        {
            Print("CallWithBackgroundWorker() RunWorkerCompletedSomeMethod is done.");
        }

        private void DoWorkSomeOtherMethod(object sender, DoWorkEventArgs e)
        {
            Print("CallWithBackgroundWorker() DoWorkSomeOtherMethod is executing...");
            var param = (int)e.Argument;
            var s = SomeOtherMethod(param);
            e.Result = s;
        }

        private void RunWorkerCompletedSomeOtherMethod(object sender, RunWorkerCompletedEventArgs e)
        {
            var s = (string) e.Result;
            Print("CallWithBackgroundWorker() RunWorkerCompletedSomeOtherMethod is done with result = " + s);
        }

        #endregion

        #region QueueUserWorkItem

        private void CallWithQueueUserWorkItem()
        {
            Print("CallWithQueueUserWorkItem() calling SomeMethod()...");
            ThreadPool.QueueUserWorkItem(SomeMethodWaitCallback);
            Print("CallWithQueueUserWorkItem() called SomeMethod().");

            Print("CallWithQueueUserWorkItem() calling SomeOtherMethod()...");
            var param = 400;
            ThreadPool.QueueUserWorkItem(SomeOtherMethodWaitCallback, param);
            Print("CallWithQueueUserWorkItem() called SomeOtherMethod().");
        }

        private void SomeMethodWaitCallback(object state)
        {
            Print("CallWithQueueUserWorkItem() SomeMethodWaitCallback is executing...");
            SomeMethod();
            Print("CallWithQueueUserWorkItem() SomeMethodWaitCallback is done.");
        }

        private void SomeOtherMethodWaitCallback(object state)
        {
            Print("CallWithQueueUserWorkItem() SomeOtherMethodWaitCallback is executing...");
            var param = (int) state;
            var s = SomeOtherMethod(param);
            Print("CallWithQueueUserWorkItem() SomeOtherMethodWaitCallback is done with result = " + s);
        }

        #endregion

        #region Thread.Start

        private void CallWithThreadStart()
        {
            Print("CallWithThreadStart() calling SomeMethod()...");
            var thread = new Thread(SomeMethodThreadStartDelegate);
            thread.Start();
            Print("CallWithThreadStart() called SomeMethod().");

            Print("CallWithThreadStart() calling SomeOtherMethod()...");
            var param = 500;
            var otherThread = new Thread(SomeOtherMethodThreadStartDelegate);
            otherThread.Start(param);
            Print("CallWithThreadStart() called SomeOtherMethod().");
        }

        private void SomeMethodThreadStartDelegate()
        {
            Print("CallWithThreadStart() SomeMethodThreadStartDelegate is executing...");
            SomeMethod();
            Print("CallWithThreadStart() SomeMethodThreadStartDelegate is done.");
        }

        private void SomeOtherMethodThreadStartDelegate(object state)
        {
            Print("CallWithThreadStart() SomeOtherMethodThreadStartDelegate is executing...");
            var param = (int) state;
            var s = SomeOtherMethod(param);
            Print("CallWithThreadStart() SomeOtherMethodThreadStartDelegate is done with result = " + s);
        }

        #endregion

        private static void Print(string message)
        {
            Console.WriteLine(
                string.Format(
                    "{0} - Thread {1} - {2}", DateTime.Now.ToString("HH:mm:ss.fff"),
                    Thread.CurrentThread.ManagedThreadId,
                    message)
                );
        }
    }
}
