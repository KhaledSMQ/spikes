using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;

namespace Bridge
{
    public class Script
    {
        private ScriptEngine Engine { get; set; }
        private ScriptSource Source { get; set; }
        private ScriptScope Scope { get; set; }
        private CompiledCode Compiled { get; set; }
        private ObjectOperations Operations { get; set; }

        public string LastError { get; private set; }

        public void InitializeFromFile(string file)
        {
            Engine = Python.CreateEngine();
            Source = Engine.CreateScriptSourceFromFile(file);
            Scope = Engine.CreateScope();
            Operations = Engine.Operations;
        }

        public void InitializeFromString(string script)
        {
            Engine = Python.CreateEngine();
            Source = Engine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
            try
            {
                Compiled = Source.Compile();
            }
            catch(SyntaxErrorException see)
            {
                var eo = Engine.GetService<ExceptionOperations>();
                LastError = eo.FormatException(see);
                throw;
            }
            Scope = Engine.CreateScope();
            Operations = Engine.Operations;
        }

        public TResults Execute<TResults>(string className, string methodName)
        {
            if (Compiled != null)
                Compiled.Execute(Scope);
            else
                Source.Execute(Scope);
            var classObj = Scope.GetVariable(className);
            var classInstance = Operations.Call(classObj);
            var classMethod = Operations.GetMember(classInstance, methodName);
            var results = (TResults) Operations.Call(classMethod);
            return results;
        }

        public TResults Execute<TResults>(string className, string methodName, params object[] parameters)
        {
            if (Compiled != null)
                Compiled.Execute(Scope);
            else
                Source.Execute(Scope);
            var classObj = Scope.GetVariable(className);
            var classInstance = Operations.Call(classObj);
            var classMethod = Operations.GetMember(classInstance, methodName);
            var results = (TResults)Operations.Call(classMethod, parameters);
            return results;
        }
    }
}
