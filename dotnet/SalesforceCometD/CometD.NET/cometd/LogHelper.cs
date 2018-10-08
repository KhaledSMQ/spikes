using System.Diagnostics;
using System.Threading;

namespace Cometd
{
    public static class LogHelper
    {
        private static int Idx = 0;

        public static void Log(string message)
        {
            Trace.TraceInformation($"[{Interlocked.Increment(ref Idx),5}] {message}");
        }
    }
}