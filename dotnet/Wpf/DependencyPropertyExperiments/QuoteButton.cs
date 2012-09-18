using System;
using System.Windows;
using System.Windows.Controls;

namespace DependencyPropertyExperiments
{
	public class QuoteButton : Button
	{
	    public static readonly DependencyProperty QuoteProperty =
			DependencyProperty.Register("Quote", typeof (QuoteData), typeof (QuoteButton), new PropertyMetadata(default(QuoteData)));

		public QuoteData Quote
		{
			get { return (QuoteData) GetValue(QuoteProperty); }
			set { SetValue(QuoteProperty, value); }
		}
	}
}
