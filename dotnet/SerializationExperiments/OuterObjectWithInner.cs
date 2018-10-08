namespace SerializationExperiments
{
	public class OuterObjectWithInner
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public object ContainedObject { get; set; }
		public InnerObject InnerObject { get; set; }
	}
}