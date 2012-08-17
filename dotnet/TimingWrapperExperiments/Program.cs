using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace TimingWrapperExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var p = new Program();
            p.Run();
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private void Run()
        {
            TimingWrapper.TimingDone += TimingWrapper_TimingDone;
            TimingWrapper.TimeIt(Wait500ms, "500 ms wait");
            TimingWrapper.TimeIt(Wait1s);
            TimingWrapper.TimeIt(() => Wait(750));
            var ret = TimingWrapper.TimeIt(() => WaitAndReturn(750, 100));
        }

        private void TimingWrapper_TimingDone(object sender, Program.TimingEventArgs e)
        {
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Action {0} with message '{1}' completed in {2} ms.",
                    e.MethodName,
                    e.Message,
                    e.Duration
                    )
                );
                
        }

        private void Wait1s()
        {
            Thread.Sleep(1000);
        }

        private void Wait500ms()
        {
            Thread.Sleep(500);
        }

        private void Wait(int ms)
        {
            Thread.Sleep(ms);
        }

        private int WaitAndReturn(int ms, int ret)
        {
            Thread.Sleep(ms);
            return ret;
        }

        public class TimingWrapper
        {
            public static event EventHandler<TimingEventArgs> TimingDone;
            private static readonly double TickToMsFactor = Stopwatch.Frequency / 1000;

            public static T TimeIt<T>(Func<T> action)
            {
                return TimeIt(action, null);
            }

            public static void TimeIt(Action action)
            {
                TimeIt(action, null);
            }

            public static T TimeIt<T>(Func<T> action, string message)
            {
                var sw = new Stopwatch();
                sw.Start();
                var result = action.Invoke();
                sw.Stop();
                OnTimingDone(message, action.Method.Name, sw.ElapsedTicks / TickToMsFactor, null);
                return result;
            }

            public static void TimeIt(Action action, string message)
            {
                var sw = new Stopwatch();
                sw.Start();
                action.Invoke();
                sw.Stop();
                OnTimingDone(message, action.Method.Name, sw.ElapsedTicks / TickToMsFactor, null);
            }

            private static void OnTimingDone(string message, string methodName, double duration, object tag)
            {
                var handler = TimingDone;
                if (handler != null)
                    handler(null, new TimingEventArgs(message, methodName, duration, tag));
            }
        }

        public class TimingEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public string MethodName { get; private set; }
            public double Duration { get; private set; }
            public object Tag { get; private set; }

            public TimingEventArgs(string message, string methodName, double duration, object tag)
            {
                Message = message;
                MethodName = methodName;
                Duration = duration;
                Tag = tag;
            }
        }
    }
}
