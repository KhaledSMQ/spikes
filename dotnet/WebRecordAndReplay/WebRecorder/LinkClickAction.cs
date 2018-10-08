using CefSharp.Wpf;

namespace WebRecorder
{
	public class LinkClickAction : UserAction
	{
		private const string ClickBySelector = "(function() {{ " +
			"$('{0}').click(); " +
			"}})();";

		public LinkClickAction() : base(UserActionTypes.LinkClick)
		{ }

        public string Href
        {
            get { return GetParameter(UserActionParameters.Href); }
            set { AddParameter(UserActionParameters.Href, value); }
        }

        public string Target
        {
            get { return GetParameter(UserActionParameters.Target); }
            set { AddParameter(UserActionParameters.Target, value); }
        }


		public override void Execute(WebView browser)
		{
			browser.ExecuteScript(string.Format(ClickBySelector, Selector));
			base.Execute(browser);
		}

        public override string ToString()
        {
            return string.Format("selector='{0}', href='{1}', target='{2}'", GetParameter(UserActionParameters.Selector), GetParameter(UserActionParameters.Href), GetParameter(UserActionParameters.Target));
        }
    }
}
