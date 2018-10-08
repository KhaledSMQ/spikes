using System;
using System.Text;
using Microsoft.AspNet.SignalR;

namespace SignalRExperiments.Hubs
{
	public class VolumeHub : Hub
	{
		private const int Size = 250;
		private string Payload { get; set; }

		public VolumeHub()
		{
			Payload = GetRandomPayload(Size);
		}

		public void StartSending(int batchCount)
		{
			if (batchCount == 0)
				batchCount = 10;

			var countInBatch = new[] { 1000, 5000, 10000, 20000, 50000 };
			var random = new Random();
			for (var i = 0; i < batchCount; ++i)
			{
				var countIdx = random.Next(countInBatch.Length);
				var count = countInBatch[countIdx];
				SendMultipleMessages(count);
			}

			Clients.Others.printStats();
		}

		public void StopSending()
		{
			Clients.Others.stopReceiving();
		}

		private void SendMultipleMessages(int count)
		{
			var start = DateTime.UtcNow;
			for (var i = 0; i < count; ++i)
			{
				SendMessage(i, count, start);
			}
		}

		private void SendMessage(int counter, int totalCount, DateTime start)
		{
			var now = DateTime.UtcNow;
			var ms = (now - start).TotalMilliseconds;
			var message = string.Format(@"{{""index"":""{0}"",""total"":""{1}"",""time"":""{2}"",""payload"":""{3}""}}", counter, totalCount, ms, Payload);
			if (counter == 0)
			{
				Clients.Others.addStartMessage(message);
			}
			else if (counter == totalCount - 1)
			{
				Clients.Others.addLastMessage(message);
			}
			else
				Clients.Others.addMessage(message);
		}

		private string GetRandomPayload(int size)
		{
			var builder = new StringBuilder();
			for (var i = 0; i < size; i++)
			{
				var c = 48 + i % 10;
				builder.Append((char)c);
			}
			return builder.ToString();
		}
	}
}