using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Inspector
{
	public partial class FormInspector : Form
	{
		public FormInspector()
		{
			InitializeComponent();
		}

		private void ButtonInspect_Click(object sender, EventArgs e)
		{
			Inspect();
		}

		private async void Inspect()
		{
			Log("Inspecting...");
			var cs = TextConnectionString.Text;
			var manager = new Manager(cs);
			Log("Connected to address " + manager.Address);
			Log("Getting queue information...");
			var queues = await manager.GetQueues();
			PrintQueues(queues);
			Log("Getting topic information...");
			var topics = await manager.GetTopics();
			PrintTopics(topics);
			Log("Completed.");
		}

		private void PrintTopics(IEnumerable<TopicInformation> obj)
		{
			Log("Topic information received.");
			Log("This namespace has the following topics and subscriptions:");
			foreach (var topicInfo in obj)
			{
				Log("  " + topicInfo.Path + " has " + topicInfo.Subscriptions.Count() + " subscriptions:");
				foreach(var subscriptionInfo in topicInfo.Subscriptions)
					Log("    " + subscriptionInfo.Name + ", " + subscriptionInfo.MessageCount);
			}
		}

		private void PrintQueues(IEnumerable<QueueInformation> obj)
		{
			Log("Queue information received.");
			Log("This namespace has the following queues and pending messages:");
			foreach(var queueInfo in obj)
				Log("  " + queueInfo.Path + ", " + queueInfo.MessageCount);
		}

		private void Log(string message)
		{
			if (TextLog.InvokeRequired)
			{
				TextLog.Invoke(new Action(() => Log(message)));
				return;
			}

			TextLog.AppendText(message + Environment.NewLine);
		}
	}
}
