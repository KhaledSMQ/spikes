using System;
using System.Windows;

namespace AttachedPropertyExperiments
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			PlainQuoteButton.Click += QuoteButtonOnClick;
			QuoteButton.Click += QuoteButtonOnClick;
		}

		private void QuoteButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var button = (QuoteButton) sender;
			var name = button.Name;
			var quote = button.Quote;
			Utilities.Write(name, quote);

			var uiElement = button.Content as UIElement;
			if (uiElement != null)
			{
				var innerQuote = (QuoteData) uiElement.GetValue(QuoteButton.QuoteProperty);
				Utilities.Write(uiElement.GetType().Name, innerQuote);
			}
		}
	}
}
