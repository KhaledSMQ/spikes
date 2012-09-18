using System;
using System.Collections.Generic;
using System.Windows;

namespace AttachedPropertyExperiments
{
	public static class Utilities
	{
		private static IList<string> Messages { get; set; }
 
		static Utilities()
		{
			Messages = new List<string>();
		}

		public static void Write(string text)
		{
			var output = ((MainWindow) Application.Current.MainWindow).Output;
			if (output != null)
			{
				Messages.Add(text);
				foreach (var message in Messages)
					output.Text += message + Environment.NewLine;
				Messages.Clear();
			}
			else
			{
				Messages.Add("Added before output initialization: " + text);
			}
		}

		public static void Write(string objectName, QuoteData quote)
		{
			Write(string.Format("object: {0}, quote: {1}", objectName, quote));
		}
	}
}
