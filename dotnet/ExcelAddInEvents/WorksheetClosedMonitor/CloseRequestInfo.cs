namespace ExcelAddInEvents.WorksheetClosedMonitor
{
	public class CloseRequestInfo
	{
		public string WorksheetName { get; set; }
		public string WorkbookName { get; set; }
		public int WorksheetCount { get; set; }

		public CloseRequestInfo(string worksheetName, string workbookName, int count)
		{
			WorksheetName = worksheetName;
			WorkbookName = workbookName;
			WorksheetCount = count;
		}
	}
}