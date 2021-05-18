using OE.Silo.Abstractions;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OE.Silo.Grains
{
	public class UserGraphGrain : Grain, IUserGraphGrain
	{
		public HashSet<Guid> Followers { get; set; }
		public HashSet<Guid> Followees { get; set; }
		public HashSet<Guid> Blocked { get; set; }

		public UserGraphGrain()
		{
			Followers = new HashSet<Guid>();
			Followees = new HashSet<Guid>();
			Blocked = new HashSet<Guid>();
		}

		public async Task AddFolloweeAsync(Guid followeeId)
		{
			if (Followees.Contains(followeeId) || Blocked.Contains(followeeId))
			{
				return;
			}

			var followee = GrainFactory.GetGrain<IUserGraphGrain>(followeeId);
			await followee.AddFollowerAsync(GrainReference.GrainIdentity.PrimaryKey);

			Followees.Add(followeeId);
		}

		public async Task RemoveFolloweeAsync(Guid followeeId)
		{
			if (!Followees.Contains(followeeId))
			{
				return;
			}

			var followee = GrainFactory.GetGrain<IUserGraphGrain>(followeeId);
			await followee.RemoveFollowerAsync(GrainReference.GrainIdentity.PrimaryKey);

			Followees.Remove(followeeId);
		}

		public Task<IEnumerable<Guid>> GetFolloweesAsync()
		{
			return Task.FromResult(Followees.Except(Blocked));
		}

		public async Task AddFollowerAsync(Guid followerId)
		{
			if (Followers.Contains(followerId) || Blocked.Contains(followerId))
			{
				return;
			}

			Followers.Add(followerId);
		}

		public async Task RemoveFollowerAsync(Guid followerId)
		{
			if (!Followers.Contains(followerId))
			{
				return;
			}

			Followers.Remove(followerId);
		}

		public Task<IEnumerable<Guid>> GetFollowersAsync()
		{
			return Task.FromResult(Followers.Except(Blocked));
		}

		public async Task BlockUserAsync(Guid userId)
		{
			if (Blocked.Contains(userId))
			{
				return;
			}

			Blocked.Add(userId);
		}

		public async Task UnblockUserAsync(Guid userId)
		{
			if (!Blocked.Contains(userId))
			{
				return;
			}

			Blocked.Remove(userId);
		}

		
	}
}
