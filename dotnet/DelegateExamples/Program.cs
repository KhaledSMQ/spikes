using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateExamples
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
            ExecuteDirectly();
            ExecuteThroughDeclaredDelegates();
			ExecuteThroughDelegateWithGenerics();
			ExecuteThroughActionAndFunc();
            ExecuteThroughAnonymousDelegates();
            ExecuteThroughLambdas();
        }

        private void SomeMethod()
        {
            Print("in SomeMethod().");
        }

        private string SomeOtherMethod(int i)
        {
            Print("in SomeOtherMethod(" + i + ").");
            return i.ToString();
        }

        private int YetAnotherMethod(int i, int j)
        {
            Print("in YetAnotherMethod(" + i + ", " + j + ").");
            return i + j;
        }

		private TResult GenericMethod<TParameter, TResult>(TParameter parameter)
		{
			throw new NotImplementedException();
		}

        #region Direct

        private void ExecuteDirectly()
        {
            Print("Executing directly...");
            
            SomeMethod();
            var s = SomeOtherMethod(100);
            Print("ExecuteDirectly() SomeOtherMethod() returned " + s);
        }

        #endregion

        #region Declared delegates

        private delegate void SomeMethodDelegate();
        private delegate string SomeOtherMethodDelegate(int i);
    	private delegate TResult DelegateWithGenerics<TParameter, TResult>(TParameter parameter);

        private void ExecuteThroughDeclaredDelegates()
        {
            Print("Executing through declared delegates...");

            var someMethod = new SomeMethodDelegate(SomeMethod);
            someMethod.Invoke();

            var someOtherMethod = new SomeOtherMethodDelegate(SomeOtherMethod);
            var s = someOtherMethod.Invoke(200);
            Print("ExecuteThroughDeclaredDelegates() SomeOtherMethod() returned " + s);
        }

		private void ExecuteThroughDelegateWithGenerics()
		{
			Print("Executing through delegate with generics...");

			var someOtherMethod = new DelegateWithGenerics<int, string>(SomeOtherMethod);
			DelegateWithGenerics<int, string> m = SomeOtherMethod;
			var s = m.Invoke(10);

			Print("ExecuteThroughDelegateWithGenerics() SomeOtherMethod() returned " + s);
		}

        #endregion

        #region Action and Func

        private void ExecuteThroughActionAndFunc()
        {
            Print("Executing through action and func...");
            
            Action action = SomeMethod;
            action.Invoke();

            Func<int, string> func = SomeOtherMethod;
            var s = func.Invoke(300);
            Print("ExecuteThroughActionAndFunc() SomeOtherMethod() returned " + s);
        }

        #endregion

        #region Anonymous delegates

        private void ExecuteThroughAnonymousDelegates()
        {
            Print("Executing through anonymous delegates...");

            Action action = delegate
                                {
                                    Print("in parameterless anonymous delegate");
                                };
            action.Invoke();

            Func<int, string> func = delegate(int i)
                                         {
                                             Print("in anonymous delegate that accepts an int and returns a string");
                                             return i.ToString();
                                         };
            var s = func.Invoke(400);
            Print("ExecuteThroughAnonymousDelegates() SomeOtherMethod() returned " + s);
        }

        #endregion

        #region Lambdas

        private void ExecuteThroughLambdas()
        {
            Print("Executing through lambdas...");

            // straight parameter-less lambda
            Action action = () => Print("in parameterless lambda");
            action.Invoke();

            // lambda accepting an int, no return value
            Action<int> actionWithParam = i =>
                                    {
                                        Print("in lambda that accepts an int");
                                        var s = SomeOtherMethod(i);
                                        Print("ExecuteThroughLambdas() SomeOtherMethod() returned " + s);
                                    };
            actionWithParam.Invoke(500);

            // lambda accepting two ints, no return value
            Action<int, int> actionWithTwoParams = (i, j) =>
                                    {
                                        Print("in lambda that accepts two ints");
                                        var result = YetAnotherMethod(i, j);
                                        Print("ExecuteThroughLambdas() YetAnotherMethod() returned " + result);
                                    };
            actionWithTwoParams.Invoke(100, 500);

            // lambda capturing an external value, no params and no return value
            var param = 700;
            Action actionWithExternalParam = () =>
                                    {
                                        Print("in lambda that captures an external value");
                                        var s = SomeOtherMethod(param);
                                        Print("ExecuteThroughLambdas() SomeOtherMethod() returned " + s);
                                    };
            actionWithExternalParam.Invoke();

            // lambda accepting an int and returning a string
            Func<int, string> func = i =>
                                         {
                                             Print("in lambda that accepts an int and returns a string");
                                             return SomeOtherMethod(i);
                                         };
            var r = func.Invoke(800);
            Print("ExecuteThroughLambdas() SomeOtherMethod() returned " + r);
        }

        #endregion

        private static void Print(string message)
        {
            Console.WriteLine(
                string.Format(
                    "{0} - {1}", DateTime.Now.ToString("HH:mm:ss.fff"),
                    message)
                );
        }
    }
}
