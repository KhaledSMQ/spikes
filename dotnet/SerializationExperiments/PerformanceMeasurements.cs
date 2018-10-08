using System;
using System.Diagnostics;

namespace SerializationExperiments
{
	public class PerformanceMeasurements
	{
		private ObjectsForTesting Objects { get; set; }

		public void Run()
		{
			Objects = new ObjectsForTesting();
			RunMeasurements();			
		}

		private void RunMeasurements()
		{
			var n = new NormalSerialization();
			var m = new SerializationWithResolverAndNoKnownTypes();
			var p = new SerializationWithKnownTypesInCtor();

			var iterations = 10000;
			var cycles = 4;

			var sw = new Stopwatch();
			var ssw = new Stopwatch();
			var dsw = new Stopwatch();
			for (var c = 0; c < cycles; c++)
			{
				sw.Reset();
				ssw.Reset();
				dsw.Reset();
				iterations *= 2;
				sw.Start();
				for (var i = 0; i < iterations; ++i)
				{
					ssw.Start();
					n.Serialize(Objects.Root2, typeof(SerializedRoot2));
					ssw.Stop();
					dsw.Start();
					n.Deserialize(Objects.Root2, typeof(SerializedRoot2));
					dsw.Stop();
				}
				sw.Stop();
				var ms = sw.ElapsedMilliseconds;
				var sms = ssw.ElapsedMilliseconds;
				var dms = dsw.ElapsedMilliseconds;
				Console.WriteLine("No resolver, cycle {0}, total iterations {1}", c, iterations);
				Console.WriteLine("            total: {0} ms, {1}/s", ms, iterations * 1000.0 / ms);
				Console.WriteLine("    serialization: {0} ms, {1}/s", sms, iterations * 1000.0 / sms);
				Console.WriteLine("  deserialization: {0} ms, {1}/s", dms, iterations * 1000.0 / dms);

				sw.Reset();
				ssw.Reset();
				dsw.Reset();
				sw.Start();
				var cr = new CustomResolverWithNoDeclaredTypes();
				for (var i = 0; i < iterations; ++i)
				{
					ssw.Start();
					m.Serialize(Objects.Outer, typeof(SerializedRoot), Objects.Root, cr);
					ssw.Stop();
					dsw.Start();
					m.Deserialize(Objects.Outer, typeof(SerializedRoot), Objects.Root, cr);
					dsw.Stop();
				}
				sw.Stop();
				ms = sw.ElapsedMilliseconds;
				sms = ssw.ElapsedMilliseconds;
				dms = dsw.ElapsedMilliseconds;
				Console.WriteLine("With resolver, cycle {0}, total iterations {1}", c, iterations);
				Console.WriteLine("            total: {0} ms, {1}/s", ms, iterations * 1000.0 / ms);
				Console.WriteLine("    serialization: {0} ms, {1}/s", sms, iterations * 1000.0 / sms);
				Console.WriteLine("  deserialization: {0} ms, {1}/s", dms, iterations * 1000.0 / dms);

				sw.Reset();
				ssw.Reset();
				dsw.Reset();
				sw.Start();
				var knownTypes = new[] { typeof(OuterObjectWithInner), typeof(InnerObject) };
				for (var i = 0; i < iterations; ++i)
				{
					ssw.Start();
					p.Serialize(Objects.Root, typeof(SerializedRoot), knownTypes);
					ssw.Stop();
					dsw.Start();
					p.Deserialize(Objects.Root, typeof(SerializedRoot), knownTypes);
					dsw.Stop();
				}
				sw.Stop();
				ms = sw.ElapsedMilliseconds;
				sms = ssw.ElapsedMilliseconds;
				dms = dsw.ElapsedMilliseconds;
				Console.WriteLine("With inline known types, cycle {0}, total iterations {1}", c, iterations);
				Console.WriteLine("            total: {0} ms, {1}/s", ms, iterations * 1000.0 / ms);
				Console.WriteLine("    serialization: {0} ms, {1}/s", sms, iterations * 1000.0 / sms);
				Console.WriteLine("  deserialization: {0} ms, {1}/s", dms, iterations * 1000.0 / dms);

				Console.WriteLine();
			}
		}
	}
}
