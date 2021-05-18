using OE.Silo.Abstractions;
using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OE.Silo.Grains
{
	public class OutboxGrain : Grain, IOutboxGrain
	{
		private HashSet<IInboxGrain> inboxes;

		public async Task SendOutboxItem(Guid id)
		{
			var graph = GrainFactory.GetGrain<IUserGraphGrain>(GrainReference.GrainIdentity.PrimaryKey);

			var tasks = new HashSet<Task>();

			if (inboxes == null)
			{
				inboxes = new HashSet<IInboxGrain>();

				foreach (var follower in await graph.GetFollowersAsync())
				{
					var inbox = GrainFactory.GetGrain<IInboxGrain>(follower);
					inboxes.Add(inbox);
					tasks.Add(GetInboxAndSendItem(inbox, id));
				}
			}
			else
			{
				foreach (var inbox in inboxes)
				{
					tasks.Add(GetInboxAndSendItem(inbox, id));
				}
			}
			

			await Task.WhenAll(tasks);
		}

		private async Task GetInboxAndSendItem(IInboxGrain inbox, Guid id)
		{
			await inbox.AcceptInboxItemAsync(id);
		}

	}
}
