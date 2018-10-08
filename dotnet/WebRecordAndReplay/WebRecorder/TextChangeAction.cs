using CefSharp.Wpf;

namespace WebRecorder
{
	public class TextChangeAction : UserAction
	{
        private const string ChangeValueById = "(function() {{ " +
            "var field = document.getElementById('{0}'); " +
            "field.value = '{1}'; " +
            "}})();";

		public TextChangeAction() : base(UserActionTypes.TextChange)
		{ }

        public string Value
        {
            get { return GetParameter(UserActionParameters.Value); }
            set { AddParameter(UserActionParameters.Value, value); }
        }

        public override void Execute(WebView browser)
        {
            browser.ExecuteScript(string.Format(ChangeValueById, Id, Value));
            base.Execute(browser);
        }

        public override string ToString()
        {
            return string.Format("value='{0}'", GetParameter(UserActionParameters.Value));
        }
    }
}
