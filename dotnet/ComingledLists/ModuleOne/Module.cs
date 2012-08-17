using System;
using System.Windows;
using System.Windows.Controls;
using Infrastructure;

namespace ModuleOne
{
	public class Module
	{
		private const string ItemOneTemplate = "ItemOneTemplate";
		private ItemOneFactory Factory { get; set; }
		private DataTemplates DataTemplates { get; set; }

		public Module()
		{
			InitializeResources();
			Factory = new ItemOneFactory();
			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			MainEvents.AddFromOne += MainEvents_AddFromOne;
		}

		private void InitializeResources()
		{			
			DataTemplates = new DataTemplates();
			//var r = DataTemplates[ItemOneTemplate];
			//MainApplication.Instance.Workspace.AddResource(ItemOneTemplate, r);
		}

		private void MainEvents_AddFromOne(object sender, EventArgs e)
		{
			AddItem();
		}

		private void AddItem()
		{
			var item = BuildEntity();
			var listBoxItem = new ListBoxItem();
			listBoxItem.Content = item;
			listBoxItem.ContentTemplate = (DataTemplate)DataTemplates[ItemOneTemplate];
			listBoxItem.MouseUp += listBoxItem_MouseUp;
			MainApplication.Instance.Workspace.AddItem(listBoxItem);
		}

		private static void listBoxItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var listBoxItem = (ListBoxItem) sender;
			var item = (ItemOne) listBoxItem.Content;
			MainApplication.Instance.Workspace.OutputMessage("ModuleOne is responding to a click. Clicked on item with name = " + item.Name);
		}

		private ItemOne BuildEntity()
		{
			var item = Factory.CreateItem();
			return item;
		}
	}
}
