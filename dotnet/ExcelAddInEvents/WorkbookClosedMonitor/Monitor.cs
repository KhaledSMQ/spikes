using System;

namespace ExcelAddInEvents.WorkbookClosedMonitor
{
	public sealed class Monitor
	{
		public event EventHandler<WorkbookClosedEventArgs> WorkbookClosed;
		public event EventHandler<WorkbookBeforeCloseEventArgs> WorkbookBeforeClose;

		public Microsoft.Office.Interop.Excel.Application Application { get; private set; }

		private CloseRequestInfo PendingRequest { get; set; }

		public Monitor(Microsoft.Office.Interop.Excel.Application application)
		{
			Application = application;
			Application.WorkbookActivate += Application_WorkbookActivate;
			Application.WorkbookBeforeClose += Application_WorkbookBeforeClose;
			Application.WorkbookDeactivate += Application_WorkbookDeactivate;
		}

		private void Application_WorkbookDeactivate(Microsoft.Office.Interop.Excel.Workbook wb)
		{
			if (Application.Workbooks.Count == 1)
			{
				// With only one workbook available deactivating means it will be closed
				PendingRequest = null;
				OnWorkbookClosed(new WorkbookClosedEventArgs(wb.Name));
			}
		}

		private void Application_WorkbookBeforeClose(Microsoft.Office.Interop.Excel.Workbook workbook, ref bool cancel)
		{
			if (!cancel)
			{
				OnWorkbookBeforeClose(new WorkbookBeforeCloseEventArgs(workbook.Name, workbook));
				PendingRequest = new CloseRequestInfo(workbook.Name, Application.Workbooks.Count);
			}
		}

		private void Application_WorkbookActivate(Microsoft.Office.Interop.Excel.Workbook wb)
		{
			// A workbook was closed if a request is pending and the workbook count decreased
			var wasWorkbookClosed = PendingRequest != null && Application.Workbooks.Count < PendingRequest.WorkbookCount;

			if (wasWorkbookClosed)
			{
				var args = new WorkbookClosedEventArgs(PendingRequest.WorkbookName);
				PendingRequest = null;
				OnWorkbookClosed(args);
			}
			else
			{
				PendingRequest = null;
			}
		}

		private void OnWorkbookClosed(WorkbookClosedEventArgs e)
		{
			var handler = WorkbookClosed;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void OnWorkbookBeforeClose(WorkbookBeforeCloseEventArgs e)
		{
			var handler = WorkbookBeforeClose;

			if (handler != null)
			{
				handler(this, e);
			}
		}
	}
}
