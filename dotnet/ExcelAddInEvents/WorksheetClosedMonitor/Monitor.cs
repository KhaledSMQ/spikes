using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelAddInEvents.WorksheetClosedMonitor
{
	public sealed class Monitor
	{
		public event EventHandler<WorksheetDeletedEventArgs> WorksheetDeleted;
		public event EventHandler<WorksheetBeforeDeleteEventArgs> WorksheetBeforeDelete;

		public Excel.Application Application { get; private set; }

		private CloseRequestInfo PendingRequest { get; set; }

		public Monitor(Excel.Application application)
		{
			Application = application;
			Application.SheetActivate += Application_SheetActivate;
			Application.SheetDeactivate += Application_SheetDeactivate;
			Application.SheetBeforeDelete += Application_SheetBeforeDelete;
		}

		void Application_SheetActivate(object sh)
		{
			var ws = sh as Excel.Worksheet;
			var wb = ws.Parent as Excel.Workbook;

			// A worksheet was closed if a request is pending and the worksheet count decreased
			var wasClosed = PendingRequest != null && wb.Worksheets.Count < PendingRequest.WorksheetCount;

			if (wasClosed)
			{
				var args = new WorksheetDeletedEventArgs(PendingRequest.WorksheetName, PendingRequest.WorkbookName);
				PendingRequest = null;
				OnWorksheetDeleted(args);
			}
			else
			{
				PendingRequest = null;
			}
		}

		void Application_SheetDeactivate(object sh)
		{
			var ws = sh as Excel.Worksheet;
			var wb = ws.Parent as Excel.Workbook;

			if (wb.Worksheets.Count == 1)
			{
				// With only one workbook available deactivating means it will be closed
				PendingRequest = null;
				OnWorksheetDeleted(new WorksheetDeletedEventArgs(ws.Name, wb.Name));
			}
		}

		void Application_SheetBeforeDelete(object sh)
		{
			var ws = sh as Excel.Worksheet;
			var wb = ws.Parent as Excel.Workbook;

			OnWorksheetBeforeDelete(new WorksheetBeforeDeleteEventArgs(ws.Name, wb.Name, ws));
			PendingRequest = new CloseRequestInfo(ws.Name, wb.Name, wb.Worksheets.Count);
		}

		private void OnWorksheetDeleted(WorksheetDeletedEventArgs e)
		{
			var handler = WorksheetDeleted;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void OnWorksheetBeforeDelete(WorksheetBeforeDeleteEventArgs e)
		{
			var handler = WorksheetBeforeDelete;

			if (handler != null)
			{
				handler(this, e);
			}
		}
	}
}
