using System;

namespace TplExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            var tr = new TaskRun();
            try
            {
                Console.WriteLine("Throw in Task.Run()");
                tr.ThrowInRun();
            }
            catch (Exception e)
            {
                // we'll not get here because the exception is swallowed
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("Throw in Task.Run() and then Wait()");
                tr.ThrowInRunAndWait();
            }
            catch (Exception e)
            {
                // the task.Wait() call allows us to get here
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("Throw in Task.Run(), await, then Wait() for it");
                var t = tr.ThrowInRunWithAwait();
                t.Wait();
            }
            catch (Exception e)
            {
                // the task.Wait() call allows us to get here
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("Throw in Task.Run(), await, then extract Result");
                var t = tr.ThrowInRunWithAwait();
                Console.WriteLine(t.Result);
            }
            catch (Exception e)
            {
                // the task.Wait() call allows us to get here
                Console.WriteLine(e);
            }


            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
