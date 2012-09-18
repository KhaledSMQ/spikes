using System;
using System.Windows;
using System.Windows.Controls;

namespace AttachedPropertyExperiments
{
	public class QuoteButton : Button
	{
	    public static readonly DependencyProperty QuoteProperty =
			DependencyProperty.RegisterAttached("Quote", typeof (QuoteData), typeof (QuoteButton), new PropertyMetadata(default(QuoteData)));

		public static QuoteData GetQuote(UIElement element)
		{
			return (QuoteData) element.GetValue(QuoteProperty);
		}

		public static void SetQuote(UIElement element, QuoteData quote)
		{
			element.SetValue(QuoteProperty, quote);
		}

		public QuoteData Quote
		{
			get { return (QuoteData) GetValue(QuoteProperty); }
			set { SetValue(QuoteProperty, value); }
		}
	}
}
