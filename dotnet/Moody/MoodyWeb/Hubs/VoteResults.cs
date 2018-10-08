using System;
using Microsoft.AspNet.SignalR;
using MoodyWeb.Models;

namespace MoodyWeb.Hubs
{
	public class VoteResultsHub : Hub
	{
		public void ReceiveVote(int vote, string message)
		{
			var timestamp = DateTime.UtcNow;
			Clients.All.ReceiveVote(vote, message, timestamp);
			UpdateDatabase(Context.ConnectionId, vote, message, timestamp);
		}

		public void Reset()
		{
			ResetDatabase();
		}

		private void UpdateDatabase(string participantId, int vote, string message, DateTime timestamp)
		{
			var entities = new moodywebEntities();

			if (vote == 0)
				participantId = "0";

			var mood = new tbl_moody
			{
				participant_id = participantId,
				event_id = 0,
				comment = message,
				vote = vote,
				time = timestamp
			};
			entities.tbl_moody.Add(mood);
			entities.SaveChanges();
		}

		private void ResetDatabase()
		{
			var entities = new moodywebEntities();
			var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)entities).ObjectContext;
			objCtx.ExecuteStoreCommand("TRUNCATE TABLE tbl_moody");
		}
	}
}