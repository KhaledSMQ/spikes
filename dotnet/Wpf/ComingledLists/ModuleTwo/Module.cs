using System;
using System.Windows;
using System.Windows.Controls;
using Infrastructure;

namespace ModuleTwo
{
	public class Module
	{
		private const string ItemTwoTemplate = "ItemTwoTemplate";
		private ItemTwoFactory Factory { get; set; }
		private DataTemplates DataTemplates { get; set; }

		public Module()
		{
			InitializeResources();
			Factory = new ItemTwoFactory();
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			MainEvents.AddFromTwo += MainEvents_AddFromTwo;
		}

		private void InitializeResources()
		{
			DataTemplates = new DataTemplates();
			//var r = DataTemplates[ItemTwoTemplate];
			//MainApplication.Instance.Workspace.AddResource(ItemTwoTemplate, r);
		}

		private void MainEvents_AddFromTwo(object sender, EventArgs e)
		{
			AddItem();
		}

		private void AddItem()
		{
			var item = BuildEntity();
			var listBoxItem = new ListBoxItem();
			listBoxItem.Content = item;
			listBoxItem.ContentTemplate = (DataTemplate)DataTemplates[ItemTwoTemplate];
			listBoxItem.MouseUp += listBoxItem_MouseUp;
			MainApplication.Instance.Workspace.AddItem(listBoxItem);
		}

		private static void listBoxItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var listBoxItem = (ListBoxItem)sender;
			var item = (ItemTwo)listBoxItem.Content;
			MainApplication.Instance.Workspace.OutputMessage("ModuleTwo is responding to a click. Clicked on item with description = " + item.Description);
		}

		private ItemTwo BuildEntity()
		{
			var item = Factory.CreateItem();
			return item;
		}
	}
}
