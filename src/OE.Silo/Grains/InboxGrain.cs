using Microsoft.Extensions.Logging;
using OE.Silo.Abstractions;
using Orleans;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OE.Silo.Grains
{
	public class InboxGrain : Grain, IInboxGrain
	{
		private HashSet<Guid> messages = new HashSet<Guid>();

		public InboxGrain()
		{
			//
		}

		public async Task AcceptInboxItemAsync(Guid messageId)
		{
			messages.Add(messageId);
			//logger.LogInformation($"{GrainReference.GrainIdentity.PrimaryKey} accepted: {messageId}");
		}

		public async Task<IEnumerable<Guid>> GetInboxAsync()
		{
			return messages.AsEnumerable();
		}
	}
}
