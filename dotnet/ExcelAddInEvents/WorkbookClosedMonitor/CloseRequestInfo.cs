namespace ExcelAddInEvents.WorkbookClosedMonitor
{
	public class CloseRequestInfo
	{
		public string WorkbookName { get; set; }
		public int WorkbookCount { get; set; }

		public CloseRequestInfo(string name, int count)
		{
			WorkbookName = name;
			WorkbookCount = count;
		}
	}
}