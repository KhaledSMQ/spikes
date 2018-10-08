namespace SerializationExperiments
{
	public class OuterObjectWithInner2
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public InnerObject ContainedObject { get; set; }
		public InnerObject InnerObject { get; set; }
	}
}