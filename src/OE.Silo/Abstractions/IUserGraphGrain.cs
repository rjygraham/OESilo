using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OE.Silo.Abstractions
{
	public interface IUserGraphGrain : IGrainWithGuidKey
	{
		Task AddFollowerAsync(Guid followerId);
		Task RemoveFollowerAsync(Guid followerId);
		Task<IEnumerable<Guid>> GetFollowersAsync();

		Task AddFolloweeAsync(Guid followeeId);
		Task RemoveFolloweeAsync(Guid followeeId);
		Task<IEnumerable<Guid>> GetFolloweesAsync();

		Task BlockUserAsync(Guid userId);
		Task UnblockUserAsync(Guid userId);
	}
}
