namespace SerializationExperiments
{
	public class OuterObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public object ContainedObject { get; set; }
	}
}