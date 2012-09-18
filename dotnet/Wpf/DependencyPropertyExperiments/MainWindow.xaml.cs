using System;
using System.Windows;

namespace DependencyPropertyExperiments
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
			QuoteButtonWithChange.Click += QuoteButtonWithChangeOnClick;
			QuoteButtonWithCoercion.Click += QuoteButtonWithCoercionOnClick;
			ChangeQuoteButtonWithChange.Click += ChangeQuoteButtonWithChangeOnClick;
			ChangeQuoteButtonWithCoercion.Click += ChangeQuoteButtonWithCoercionOnClick;
		}

		private void ChangeQuoteButtonWithChangeOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var newQuote = new QuoteData {Price = 100.0, Symbol = "AAPL"};
			QuoteButtonWithChange.Quote = newQuote;
		}

		private void ChangeQuoteButtonWithCoercionOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var newQuote = new QuoteData { Price = 100.0, Symbol = "AAPL" };
			QuoteButtonWithCoercion.Quote = newQuote;
		}

		private void QuoteButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var button = (QuoteButton) sender;
			var name = button.Name;
			var quote = button.Quote;
			Utilities.Write(name, quote);
		}

		private void QuoteButtonWithChangeOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var button = (QuoteButtonWithChange)sender;
			var name = button.Name;
			var quote = button.Quote;
			Utilities.Write(name, quote);
		}

		private void QuoteButtonWithCoercionOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var button = (QuoteButtonWithCoercion)sender;
			var name = button.Name;
			var quote = button.Quote;
			Utilities.Write(name, quote);
		}
	}
}
