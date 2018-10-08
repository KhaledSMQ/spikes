using System.Runtime.Serialization;

namespace SerializationExperiments
{
	[KnownType(typeof(OuterObjectWithInner2))]
	[KnownType(typeof(InnerObject))]
	public class SerializedRoot2
	{
		public OuterObjectWithInner2 Root { get; set; }
	}
}