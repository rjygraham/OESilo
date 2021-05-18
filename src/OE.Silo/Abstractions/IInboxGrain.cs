using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OE.Silo.Abstractions
{
	public interface IInboxGrain : IGrainWithGuidKey
	{
		//[OneWay]
		Task AcceptInboxItemAsync(Guid messageId);

		Task<IEnumerable<Guid>> GetInboxAsync();
	}
}
