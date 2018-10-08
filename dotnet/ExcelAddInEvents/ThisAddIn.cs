using System;
using ExcelAddInEvents.WorkbookClosedMonitor;
using ExcelAddInEvents.WorksheetClosedMonitor;
using Excel = Microsoft.Office.Interop.Excel;

// This project explores the events listed on http://support.microsoft.com/kb/291294

namespace ExcelAddInEvents
{
    public partial class ThisAddIn
    {
		private LogWindow Log { get; set; }
		
		private Excel.AppEvents_Event AppEvents
		{
			get { return Application; }
		}

	    private Excel.Application App
	    {
			get { return Application; }
	    }

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
	        Log = new LogWindow();
			Log.Show(new HelperWindow(new IntPtr(App.Hwnd)));
			Log.WriteLog("Add-in started");

			AppEvents.NewWorkbook += ThisAddIn_NewWorkbook;
			AppEvents.WorkbookActivate += App_WorkbookActivate;
			AppEvents.WorkbookDeactivate += App_WorkbookDeactivate;
			AppEvents.WorkbookOpen += App_WorkbookOpen;
			AppEvents.WorkbookNewSheet += App_WorkbookNewSheet;
			AppEvents.WorkbookBeforeClose += AppEvents_WorkbookBeforeClose;
		
			AppEvents.SheetActivate += AppEvents_SheetActivate;
			AppEvents.SheetDeactivate += AppEvents_SheetDeactivate;

			AppEvents.WindowActivate += AppEvents_WindowActivate;
			AppEvents.WindowDeactivate += AppEvents_WindowDeactivate;

	        var workbookMonitor = new WorkbookClosedMonitor.Monitor(App);
			workbookMonitor.WorkbookBeforeClose += Monitor_WorkbookBeforeClose;
			workbookMonitor.WorkbookClosed += Monitor_WorkbookClosed;

	        var worksheetMonitor = new WorksheetClosedMonitor.Monitor(App);
			worksheetMonitor.WorksheetBeforeDelete += Monitor_WorksheetBeforeDelete;
			worksheetMonitor.WorksheetDeleted += MonitorWorksheetDeleted;
        }

		private void ThisAddIn_Shutdown(object sender, EventArgs e)
		{
			Log.WriteLog("Add-in shut down");
		}

		void App_WorkbookNewSheet(Excel.Workbook wb, object sh)
		{
			var ws = sh as Excel.Worksheet;
			Log.WriteLog(string.Format("New sheet {0} added to workbook {1}", ws.Name, wb.Name));
		}

		void App_WorkbookOpen(Excel.Workbook wb)
		{
			Log.WriteLog(string.Format("Workbook {0} opened", wb.Name));			
		}

		void App_WorkbookDeactivate(Excel.Workbook wb)
		{
			Log.WriteLog(string.Format("Workbook {0} deactivated", wb.Name));
		}

		void App_WorkbookActivate(Excel.Workbook wb)
		{
			Log.WriteLog(string.Format("Workbook {0} activated", wb.Name));
		}

		void AppEvents_WorkbookBeforeClose(Excel.Workbook wb, ref bool cancel)
		{
			Log.WriteLog(string.Format("Workbook {0} about to close, cancel is {1}", wb.Name, cancel));
		}

		void AppEvents_SheetActivate(object sh)
		{
			var ws = sh as Excel.Worksheet;
			var wb = ws.Parent as Excel.Workbook;
			if (wb != null)
				Log.WriteLog(string.Format("Sheet {0} activated in workbook {1}", ws.Name, wb.Name));
			else
				Log.WriteLog(string.Format("Sheet {0} activated", ws.Name));
		}

		void AppEvents_SheetDeactivate(object sh)
		{
			var ws = sh as Excel.Worksheet;
			var wb = ws.Parent as Excel.Workbook;
			if (wb != null)
				Log.WriteLog(string.Format("Sheet {0} deactivated in workbook {1}", ws.Name, wb.Name));
			else
				Log.WriteLog(string.Format("Sheet {0} deactivated", ws.Name));
		}

		void ThisAddIn_NewWorkbook(Excel.Workbook wb)
		{
			Log.WriteLog("New workbook: " + wb.Name);
		}

		void AppEvents_WindowDeactivate(Excel.Workbook wb, Excel.Window wn)
		{
			Log.WriteLog(string.Format("Window {0} deactivated with workbook {1}", wn.Caption, wb.Name));
		}

		void AppEvents_WindowActivate(Excel.Workbook wb, Excel.Window wn)
		{
			Log.WriteLog(string.Format("Window {0} activated with workbook {1}", wn.Caption, wb.Name));
		}

		void Monitor_WorkbookClosed(object sender, WorkbookClosedEventArgs e)
		{
			Log.WriteLog(string.Format("Monitor detected workbook {0} was closed", e.Name));
		}

		void Monitor_WorkbookBeforeClose(object sender, WorkbookBeforeCloseEventArgs e)
		{
			Log.WriteLog(string.Format("Monitor detected workbook {0} is about to close", e.Name));
		}

		void MonitorWorksheetDeleted(object sender, WorksheetDeletedEventArgs e)
		{
			Log.WriteLog(string.Format("Monitor detected worksheet {0} in workbook {1} was deleted", e.WorksheetName, e.WorkbookName));
		}

		void Monitor_WorksheetBeforeDelete(object sender, WorksheetBeforeDeleteEventArgs e)
		{
			Log.WriteLog(string.Format("Monitor detected worksheet {0} in workbook {1} is about to be deleted", e.WorksheetName, e.WorkbookName));
		}

	    #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
