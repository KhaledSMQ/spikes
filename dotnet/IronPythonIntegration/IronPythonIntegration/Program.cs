using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Bridge;
using Entities;

namespace IronPythonIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            //p.ScriptInFile();
            //p.ScriptInString();
            //p.SyntaxError();
            //p.RuntimeError();
			p.CompilationPerfSmallMethod();
			p.CompilationPerfLargeMethod();
			p.ExecutionPerf();

            Console.Read();
        }

        private void ScriptInFile()
        {
            Console.WriteLine("Running script from file...");

            var script = new Script();
            script.InitializeFromFile(@"..\..\..\Scripts\script1.py");

            // no params call
            Console.WriteLine(script.Execute<string>("script1class", "Hello"));

            // single param call
            Console.WriteLine(script.Execute<string>("script1class", "HelloWithName", "Paulo"));

            // pass instance of complex type
            var person = new Person { Name = "Paulo Mouat", Address = "Boston" };
            Console.WriteLine(script.Execute<string>("script1class", "HelloWithPerson", person));

            // duck typing
            var hasNameLikePerson = new HasNameLikePerson { Name = "HAL", Model = 9000 };
            Console.WriteLine(script.Execute<string>("script1class", "HelloWithPerson", hasNameLikePerson));

            // change field in passed type
            var changeState = new Person { Name = "Paulo Mouat", Address = "Boston" };
            Console.WriteLine("Original state: " + changeState);
            script.Execute<string>("script1class", "ChangeAddress", changeState);
            Console.WriteLine("New state: " + changeState);
            Console.WriteLine();
        }

        private void ScriptInString()
        {
            Console.WriteLine("Running script from string...");

            var code = @"
class script2class:
	def Goodbye(self):
		message = 'Goodbye via the script!'
		return message
	def GoodbyeWithName(self, name):
		message = 'Goodbye ' + name
		return message
	def GoodbyeWithPerson(self, person):
		message = 'Goodbye ' + person.Name
		return message";

            var compiled = new Script();
            compiled.InitializeFromString(code);
            Console.WriteLine(compiled.Execute<string>("script2class", "Goodbye"));

            // single param call
            Console.WriteLine(compiled.Execute<string>("script2class", "GoodbyeWithName", "Paulo"));

            // pass instance of complex type
            var person = new Person { Name = "Paulo Mouat", Address = "Boston" };
            Console.WriteLine(compiled.Execute<string>("script2class", "GoodbyeWithPerson", person));

            // duck typing
            var hasNameLikePerson = new HasNameLikePerson { Name = "HAL", Model = 9000 };
            Console.WriteLine(compiled.Execute<string>("script2class", "GoodbyeWithPerson", hasNameLikePerson));
            Console.WriteLine();
        }

        private void SyntaxError()
        {
            Console.WriteLine("Running script with syntax error...");

            var codeWithSyntaxError = @"
class script3class:
	def Message(self):
		msg =! 'This has a syntax error!'
		return message";

            var compiled = new Script();
            try
            {
                compiled.InitializeFromString(codeWithSyntaxError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() + " - " + e.Message);
                Console.WriteLine("The formatted error message is:" + Environment.NewLine + compiled.LastError);
            }
            Console.WriteLine();
        }

        private void RuntimeError()
        {
            Console.WriteLine("Running script with runtime error...");

            var codeWithRuntimeError = @"
class script4class:
	def Message(self):
		msg = 1/0
		return msg";

            var compiled = new Script();
            try
            {
                compiled.InitializeFromString(codeWithRuntimeError);
                compiled.Execute<string>("script4class", "Message");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() + " - " + e.Message);
            }
            Console.WriteLine();
        }

		private void CompilationPerfSmallMethod()
		{
			Console.WriteLine("Measuring compilation performance for a small method...");

			const int totalCount = 100;
			var sw = new Stopwatch();

			// compile a dummy script to force the initialization of the CLR and DLR
			var dummyCode = GetSmallMethod(99);
			var dummyCompiled = new Script();
			dummyCompiled.InitializeFromString(dummyCode);

			for (var i = 0; i < totalCount; ++i)
			{
				var code = GetSmallMethod(100 + i);
				var compiled = new Script();
				sw.Start();
				compiled.InitializeFromString(code);
				sw.Stop();
			}

			Console.WriteLine(
				string.Format("Compiled a total of {0} scripts, in {1} ms, averaging {2} ms per script.",
				totalCount,
				sw.ElapsedMilliseconds,
				1.0 * sw.ElapsedMilliseconds / totalCount
				));
		}

		private void CompilationPerfLargeMethod()
		{
			Console.WriteLine("Measuring compilation performance for a large method...");

			const int totalCount = 100;
			var sw = new Stopwatch();

			// build the large methods ahead of time to not affect perf measurement
			var methods = new List<string>();
			for (var i = 0; i < totalCount; ++i)
			{
				var code = GetLargeMethod(200 + i);
				methods.Add(code);
			}

			// compile a dummy script to force the initialization of the CLR and DLR
			var dummyCode = GetSmallMethod(99);
			var dummyCompiled = new Script();
			dummyCompiled.InitializeFromString(dummyCode);

			foreach (var code in methods)
			{
				var compiled = new Script();
				sw.Start();
				compiled.InitializeFromString(code);
				sw.Stop();
			}

			Console.WriteLine(
				string.Format("Compiled a total of {0} scripts, in {1} ms, averaging {2} ms per script.",
				totalCount,
				sw.ElapsedMilliseconds,
				1.0 * sw.ElapsedMilliseconds / totalCount
				));
		}

		private void ExecutionPerf()
		{
			Console.WriteLine("Measuring execution performance for a large method...");

			const int totalCount = 10000;
			var sw = new Stopwatch();

			const int id = 300;
			var code = GetLargeMethod(id);
			var compiled = new Script();
			compiled.InitializeFromString(code);

			for (var i = 0; i < totalCount; ++i)
			{
				sw.Start();
				compiled.Execute<string>("script300class", "ManyIfStatements", i);
				sw.Stop();
			}

			Console.WriteLine(
				string.Format("Executed script a total of {0} times, in {1} ms, averaging {2} ms per run.",
				totalCount,
				sw.ElapsedMilliseconds,
				1.0 * sw.ElapsedMilliseconds / totalCount
				));
		}

		private static string GetSmallMethod(int i)
		{
			const string format = @"
class script{0}class:
	def Goodbye(self):
		message = 'Goodbye via the script!'
		return message
	def GoodbyeWithName(self, name):
		message = 'Goodbye ' + name
		return message
	def GoodbyeWithPerson(self, person):
		message = 'Goodbye ' + person.Name
		return message";
			var code = string.Format(format, i);
			return code;
		}

		private static string GetLargeMethod(int i)
		{
			const string format = @"
class script{0}class:
	def ManyIfStatements(self, var):
		message = 'default message'
		{1}
		return message
";
			const string ifFormat = @"
		if var == {0}:
			message = 'var is ' + `{0}`
";
			const string elifFormat = @"
		elif var == {0}:
			message = 'var is ' + `{0}`
";
			const string elseFormat = @"
		else:
			message = 'var is undetermined'
";
			var codeBuilder = new StringBuilder();
			codeBuilder.AppendFormat(ifFormat, -1);

			const int totalIfs = 50;
			for(var j = 0; j < totalIfs; j++)
			{
				codeBuilder.AppendFormat(elifFormat, j);
			}

			codeBuilder.Append(elseFormat);

			var code = string.Format(format, i, codeBuilder);
			return code;
		}
	}
}
