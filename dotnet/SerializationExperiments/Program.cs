using System;
using SerializationUtilities;

namespace SerializationExperiments
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

        public void Run()
        {
            var serializer = new Serializer();

            var basePackage = new BaseEntities.Package(5);
            basePackage.Name = "BasePackage";

            var package1 = new Entities1.Package(10);
            package1.Name = "Package1";
            package1.Color = Entities1.Colors.Green;
            //package1.Value = "strvalue";
            package1.DoubleValue = 3.14;

            var package2 = new Entities2.Package(20);
            package2.Name = "Package2";
            package2.Color = Entities2.Colors.Yellow;
            package2.DoubleValue = 2.71;
            package2.DateValue = DateTime.UtcNow;

            var serializedBase = serializer.Serialize(basePackage);
			Print(serializedBase);
            var serializedEntity1 = serializer.Serialize(package1);
			Print(serializedEntity1);
			var serializedEntity2 = serializer.Serialize(package2);
			Print(serializedEntity2);

            var deserializedBase = serializer.Deserialize<BaseEntities.Package>(serializedBase);
            Print(deserializedBase);
            var deserializedEntity1 = serializer.Deserialize<Entities1.Package>(serializedEntity1);
            Print(deserializedEntity1);
            var deserializedEntity2 = serializer.Deserialize<Entities2.Package>(serializedEntity2);
            Print(deserializedEntity2);

            var deserializedEntity1AsBase = serializer.Deserialize<BaseEntities.Package>(serializedEntity1);
            Print(deserializedEntity1AsBase);
            var deserializedEntity2AsBase = serializer.Deserialize<BaseEntities.Package>(serializedEntity2);
            Print(deserializedEntity2AsBase);

            var deserializedEntity2AsEntity1 = serializer.Deserialize<Entities1.Package>(serializedEntity2);            
            Print(deserializedEntity2AsEntity1);

            RegisterConverters(serializer);

            var deserializedEntity1AsEntity2 = serializer.Deserialize<Entities2.Package>(serializedEntity1);
            Print(deserializedEntity1AsEntity2);
        }

        private static void RegisterConverters(Serializer serializer)
        {
            serializer.Converters.Register<Entities2.Package, Entities1.Package>(new Package1To2Converter());
        }

        private static void Print(object obj)
        {
            Console.WriteLine(obj);
            Console.WriteLine();
        }

		private static void Print(byte[] bytes)
		{
			var stringRepresentation = System.Text.Encoding.ASCII.GetString(bytes);
			Console.WriteLine(string.Format("Bytes {0} length: {1}", bytes.Length, stringRepresentation));
			Console.WriteLine();
		}

        public class Package1To2Converter : SerializationConverterBase<Entities2.Package, Entities1.Package>
        {
            public override Entities2.Package Convert(Entities1.Package original)
            {
                var result = new Entities2.Package(original.Value);
                result.Color = Entities2.Colors.Red;
                result.DoubleValue = original.DoubleValue;
                result.Name = original.Name;
                return result;
            }
        }
    }
}
