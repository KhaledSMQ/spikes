using CefSharp.Wpf;

namespace WebRecorder
{
	public class SubmitAction : UserAction
	{
        private const string ClickById = "(function() {{ " +
            "var button = document.getElementById('{0}'); " +
            "button.click(); " +
            "}})();";
        
        public SubmitAction()
            : base(UserActionTypes.Submit)
		{ }

        public string Value
        {
            get { return GetParameter(UserActionParameters.Value); }
            set { AddParameter(UserActionParameters.Value, value); }
        }

        public override void Execute(WebView browser)
        {
            browser.ExecuteScript(string.Format(ClickById, Id));
            base.Execute(browser);
        }

        public override string ToString()
        {
            return string.Format("value='{0}'", GetParameter(UserActionParameters.Value));
        }
    }
}
