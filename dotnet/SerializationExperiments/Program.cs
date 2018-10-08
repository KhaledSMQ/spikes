using System;

namespace SerializationExperiments
{
    class Program
    {	
		private static ObjectsForTesting Objects = new ObjectsForTesting();

		static void Main(string[] args)
		{
			new PerformanceMeasurements().Run();
			Console.WriteLine("Completed.");
	        Console.ReadLine();
        }

		private static void RunNormalSerialization()
		{
			var o = new OuterObject { Id = 10, ContainedObject = "Some content", Name = "Hello" };
			var n = new NormalSerialization();
			n.Serialize(o, typeof(OuterObject));
			Print(n.Serialized);
		}

		private static void RunNormalSerializationWithComplexObject()
		{
			var n = new NormalSerialization();
			n.Serialize(Objects.Root2, typeof(SerializedRoot2));
			Print(n.Serialized);
		}

		private static void RunSerializationWithResolver()
		{
			var m = new SerializationWithResolverAndNoKnownTypes();
			m.Serialize(Objects.Outer, typeof(SerializedRoot), Objects.Root, new CustomResolverWithNoDeclaredTypes());
			Print(m.Serialized);
		}

		private static void Print(string message)
		{
			Console.WriteLine(message);
		}
    }
}
