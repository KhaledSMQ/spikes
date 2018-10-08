using System;

namespace ExcelAddInEvents.WorkbookClosedMonitor
{
	public sealed class WorkbookClosedEventArgs : EventArgs
	{
		public string Name { get; private set; }

		public WorkbookClosedEventArgs(string name)
		{
			Name = name;
		}
	}
}