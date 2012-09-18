namespace AttachedPropertyExperiments
{
	public class QuoteData
	{
		public string Symbol { get; set; }
		public double Price { get; set; }

		public override string ToString()
		{
			return string.Format("{0}/{1}", Symbol, Price);
		}
	}
}
