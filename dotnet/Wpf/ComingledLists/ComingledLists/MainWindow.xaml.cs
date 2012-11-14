using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infrastructure;

namespace ComingledLists
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<object> Items { get; set; } 

		public MainWindow()
		{
			InitializeComponent();
			Items = new ObservableCollection<object>();
			MainListBox.ItemsSource = Items;
		}

		public void AddItem(object item)
		{
			Items.Add(item);
		}

		public void OutputMessage(string message)
		{
			OutputTextBlock.Text = message;
		}

		private void AddFromOneButton_Click(object sender, RoutedEventArgs e)
		{
			MainEvents.RaiseAddFromOne();
		}

		private void AddFromTwoButton_Click(object sender, RoutedEventArgs e)
		{
			MainEvents.RaiseAddFromTwo();
		}
	}
}
