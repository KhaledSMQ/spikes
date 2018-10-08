using System;

namespace ExcelAddInEvents.WorkbookClosedMonitor
{
	public sealed class WorkbookBeforeCloseEventArgs : EventArgs
	{
		public string Name { get; private set; }
		public Microsoft.Office.Interop.Excel.Workbook Workbook { get; private set; }

		public WorkbookBeforeCloseEventArgs(string name, Microsoft.Office.Interop.Excel.Workbook workbook)
		{
			Name = name;
			Workbook = workbook;
		}
	}
}