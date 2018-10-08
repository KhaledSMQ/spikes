namespace SerializationExperiments
{
	public class ObjectsForTesting
	{
		public OuterObjectWithInner2 Outer2 { get; set; }
		public OuterObjectWithInner Outer { get; set; }
		public SerializedRoot2 Root2 { get; set; }
		public SerializedRoot Root { get; set; }

		public ObjectsForTesting()
		{
			SetUpObjects();
		}

		private void SetUpObjects()
		{
			Outer2 = new OuterObjectWithInner2
			{
				Id = 10,
				ContainedObject = new InnerObject
				{
					Value = 11,
					Content = "Some content"
				},
				Name = "Hello",
				InnerObject = new InnerObject
				{
					Value = 12,
					Content = "More content"
				}
			};

			Outer = new OuterObjectWithInner
			{
				Id = 10,
				ContainedObject = new InnerObject
				{
					Value = 11,
					Content = "Some content"
				},
				Name = "Hello",
				InnerObject = new InnerObject
				{
					Value = 12,
					Content = "More content"
				}
			};

			Root2 = new SerializedRoot2 { Root = Outer2 };
			Root = new SerializedRoot { Root = Outer };
		}
	}
}
