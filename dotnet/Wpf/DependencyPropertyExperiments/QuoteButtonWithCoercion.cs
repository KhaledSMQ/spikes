using System.Windows;
using System.Windows.Controls;

namespace DependencyPropertyExperiments
{
	public class QuoteButtonWithCoercion : Button
	{
	    public static readonly DependencyProperty QuoteProperty =
			DependencyProperty.Register("Quote", typeof(QuoteData), typeof(QuoteButtonWithCoercion), new PropertyMetadata(default(QuoteData), QuoteChangedCallback, CoerceQuoteCallback));

		public QuoteData Quote
		{
			get { return (QuoteData) GetValue(QuoteProperty); }
			set { SetValue(QuoteProperty, value); }
		}

		private static void QuoteChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var button = (QuoteButtonWithCoercion)o;
			var oldValue = (QuoteData)e.OldValue;
			var newValue = (QuoteData)e.NewValue;
			var property = e.Property;
			Utilities.Write(string.Format("QuoteChangedCallback called on {0}. Property: {1}; old value: {2}; new value {3}", button.Name, property.Name, oldValue, newValue));
		}

		private static object CoerceQuoteCallback(DependencyObject o, object b)
		{
			var button = (QuoteButtonWithCoercion)o;
			Utilities.Write(string.Format("CoerceQuoteCallback called on {0}. Base value: {1}", button.Name, b));
			return b;
		}
	}
}
