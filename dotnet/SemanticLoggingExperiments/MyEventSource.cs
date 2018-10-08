using System.Diagnostics.Tracing;

namespace SemanticLoggingExperiments
{
    public class MyEventSource : EventSource
    {
        public static readonly MyEventSource Log = new MyEventSource();

        protected MyEventSource()
            : base(true)
        { }

        //[Event(1, Message = "This is MethodOne with params '{0}', '{1}', '{2}'", Level = EventLevel.Informational)]
        public void MethodOne(string text, string text2, string text3)
        {
            if (IsEnabled())
                WriteEvent(1, text, text2, text3);
        }

        /*public void MethodOneDotOne(string text, object obj)
        {
            if (IsEnabled())
                WriteEvent(1, obj);
        }*/

        //[Event(2, Message = "This is MethodTwo with param '{0}'", Level = EventLevel.Informational)]
        public void MethodTwo(double value)
        {
            if (IsEnabled())
                WriteEvent(2, value, "three", 4);
        }

        public void MethodThree(string text)
        {
            if (IsEnabled())
                WriteEvent(3, text);
        }
    }
}
