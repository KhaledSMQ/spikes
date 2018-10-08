using System;
using Microsoft.Office.Interop.Excel;

namespace ExcelAddInEvents.WorksheetClosedMonitor
{
	public sealed class WorksheetBeforeDeleteEventArgs : EventArgs
	{
		public string WorkbookName { get; private set; }
		public string WorksheetName { get; private set; }
		public Worksheet Worksheet { get; private set; }

		public WorksheetBeforeDeleteEventArgs(string worksheetName, string workbookName, Worksheet worksheet)
		{
			WorksheetName = worksheetName;
			WorkbookName = workbookName;
			Worksheet = worksheet;
		}
	}
}