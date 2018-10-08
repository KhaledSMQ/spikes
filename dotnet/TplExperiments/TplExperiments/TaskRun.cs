using System;
using System.Threading.Tasks;

namespace TplExperiments
{
    public class TaskRun
    {
        public Task<string> ThrowInRun()
        {
            var task = Task.Run(() =>
            {
                if (DateTime.Now.Year < 3000)
                    throw new InvalidOperationException("Raising exception in task.");

                return "OK";
            });
            return task;
        }

        public Task<string> ThrowInRunAndWait()
        {
            var task = Task.Run(() =>
            {
                if (DateTime.Now.Year < 3000)
                    throw new InvalidOperationException("Raising exception in task.");

                return "OK";
            });
            task.Wait();
            return task;
        }

        public async Task<string> ThrowInRunWithAwait()
        {
            var result = await Task.Run(() =>
            {
                if (DateTime.Now.Year < 3000)
                    throw new InvalidOperationException("Raising exception in task.");

                return "OK";
            });
            return result;
        }
    }
}
