using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfControls
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IList<Type> _controls = new List<Type>();

		private const int MinRows = 3;
		private const int ColumnCount = 4;

		public MainWindow()
		{
			InitializeComponent();
			PrepareGrid();
		}

		private void PrepareGrid()
		{
			PrepareControls();
			AddRowsAndColumns();
			PrepareButtons();
		}

		private void AddControl(Type controlType)
		{
			_controls.Add(controlType);
		}

		private void PrepareControls()
		{
			AddControl(typeof(GridWindow));
		}

		private void AddRowsAndColumns()
		{
			AddColumns();

			var rowCount = _controls.Count / ColumnCount;
			if (rowCount < MinRows)
				rowCount = MinRows;

			for (var i = 0; i < rowCount; ++i)
				AddRow();
		}

		private void AddColumns()
		{
			for (var i = 0; i < ColumnCount; ++i)
				MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
		}

		private void AddRow()
		{
			MainGrid.RowDefinitions.Add(new RowDefinition());
		}

		private void PrepareButtons()
		{
			var i = 0;
			foreach(var control in _controls)
			{
				var row = i / ColumnCount;
				var column = i % ColumnCount;
				CreateAndAddButton(row, column, control);
				++i;
			}
		}

		private void CreateAndAddButton(int row, int column, Type controlType)
		{
			var button = new Button();
			button.SetValue(Grid.RowProperty, row);
			button.SetValue(Grid.ColumnProperty, column);
			button.Content = controlType.Name;
			MainGrid.Children.Add(button);

			WireEvents(button, controlType);
		}

		private void WireEvents(Button button, Type controlType)
		{
			button.Click +=
				delegate
					{
						if (controlType.IsSubclassOf(typeof(Window)))
						{
							var window = (Window) Activator.CreateInstance(controlType);
							window.Show();
						}
					};
		}
	}
}
