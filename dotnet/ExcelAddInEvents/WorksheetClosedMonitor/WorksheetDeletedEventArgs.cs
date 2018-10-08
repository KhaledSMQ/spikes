using System;

namespace ExcelAddInEvents.WorksheetClosedMonitor
{
	public sealed class WorksheetDeletedEventArgs : EventArgs
	{
		public string WorkbookName { get; private set; }
		public string WorksheetName { get; private set; }

		public WorksheetDeletedEventArgs(string worksheetName, string workbookName)
		{
			WorksheetName = worksheetName;
			WorkbookName = workbookName;
		}
	}
}