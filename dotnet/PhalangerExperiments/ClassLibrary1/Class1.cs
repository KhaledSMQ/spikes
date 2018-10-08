using System;
using System.Collections;

namespace ClassLibrary1
{
    public class Class1
    {
	    public void ExecuteStaticMethodInPhp()
	    {
		    //dynamic globals = ScriptContext.CurrentContext.Globals;
		    //globals.@namespace.Phalanger.@class.Generics.Run();
		    var gm = Phalanger.Generics.GenericMethod("v1");
		    var dict = (IDictionary) gm;
			Console.WriteLine("Executed static method in PHP. Value is " + dict["test"]);
		    
			try
		    {
				var mse = Phalanger.Generics.SomeMethodWithSyntaxError("v1");
				dict = (IDictionary)mse;
				Console.WriteLine("Executed SomeMethodWithSyntaxError. Value is " + dict["test"]);
			}
		    catch (Exception e)
		    {
				Console.WriteLine("SomeMethodWithSyntaxError raised exception: " + e);
		    }

		    try
		    {
				var mle = Phalanger.Generics.SomeMethodWithLogicalError("v1");
				dict = (IDictionary)mle;
				Console.WriteLine("Executed SomeMethodWithLogicalError. Value is " + dict["test"]);
			}
		    catch (Exception e)
		    {
				Console.WriteLine("SomeMethodWithLogicalError raised exception: " + e);
			}

			try
			{
				var mte = Phalanger.Generics.SomeMethodWithTriggerError("v1");
				dict = (IDictionary)mte;
				Console.WriteLine("Executed SomeMethodWithTriggerError. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithTriggerError raised exception: " + e);
			}

			try
			{
				var mde = Phalanger.Generics.SomeMethodWithDie("v1");
				dict = (IDictionary)mde;
				Console.WriteLine("Executed SomeMethodWithDie. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithDie raised exception: " + e);
			}

		    try
		    {
				var me = Phalanger.Generics.SomeMethodWithException("v1");
				dict = (IDictionary)me;
				Console.WriteLine("Executed SomeMethodWithException. Value is " + dict["test"]);
			}
		    catch (Exception e)
		    {
				Console.WriteLine("SomeMethodWithException raised exception: " + e);
			}

		    try
		    {
				var mre = Phalanger.Generics.SomeMethodWithRuntimeException(0);
			    //dict = (IDictionary) mre;
				//Console.WriteLine("Executed SomeMethodWithRuntimeException. Value is " + dict["test"]);
			}
		    catch (Exception e)
		    {
				Console.WriteLine("SomeMethodWithRuntimeException raised exception: " + e);
			}
		}
	
		public void TestErrorsInPhp()
		{
			IDictionary dict;

			try
			{
				var mse = Phalanger.TestErrors.SomeMethodWithSyntaxError("v1");
				dict = (IDictionary)mse;
				Console.WriteLine("Executed SomeMethodWithSyntaxError. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithSyntaxError raised exception: " + e);
			}

			try
			{
				var mle = Phalanger.TestErrors.SomeMethodWithLogicalError("v1");
				var i = (int)mle;
				Console.WriteLine("Executed SomeMethodWithLogicalError. Value is " + i);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithLogicalError raised exception: " + e);
			}

			try
			{
				var mte = Phalanger.TestErrors.SomeMethodWithTriggerError("v1");
				dict = (IDictionary)mte;
				Console.WriteLine("Executed SomeMethodWithTriggerError. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithTriggerError raised exception: " + e);
			}

			try
			{
				var mde = Phalanger.TestErrors.SomeMethodWithDie("v1");
				dict = (IDictionary)mde;
				Console.WriteLine("Executed SomeMethodWithDie. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithDie raised exception: " + e);
			}

			try
			{
				var me = Phalanger.TestErrors.SomeMethodWithException("v1");
				dict = (IDictionary)me;
				Console.WriteLine("Executed SomeMethodWithException. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithException raised exception: " + e);
			}

			try
			{
				var mre = Phalanger.TestErrors.SomeMethodWithRuntimeException(0);
				//dict = (IDictionary) mre;
				//Console.WriteLine("Executed SomeMethodWithRuntimeException. Value is " + dict["test"]);
			}
			catch (Exception e)
			{
				Console.WriteLine("SomeMethodWithRuntimeException raised exception: " + e);
			}
		}
	}
}
