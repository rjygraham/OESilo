using Microsoft.AspNetCore.Mvc;
using OE.Silo.Abstractions;
using OE.Silo.Models;
using Orleans;
using System;
using System.Threading.Tasks;

namespace OE.Silo.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class EverythingController : ControllerBase
    {
        private readonly IGrainFactory grainFactory;

        public EverythingController(IGrainFactory grainFactory)
        {
            this.grainFactory = grainFactory;
        }

        [HttpPost("{followerId}/follow/{followeeId}")]
        public async Task<IActionResult> FollowAsync(Guid followerId, Guid followeeId)
        {
            var graph = grainFactory.GetGrain<IUserGraphGrain>(followerId);
            await graph.AddFolloweeAsync(followeeId);
            return Ok();
        }

        [HttpPost("{userId}/chirp")]
        public async Task<IActionResult> ChirpAsync(Guid userId, [FromBody] Note model)
        {
            var outbox = grainFactory.GetGrain<IOutboxGrain>(userId);
            await outbox.SendOutboxItem(Guid.NewGuid());
            return Ok();
        }

        [HttpGet("{userId}/inbox")]
        public async Task<IActionResult> GetInbox(Guid userId)
        {
            var inbox = grainFactory.GetGrain<IInboxGrain>(userId);
            var inboxItems = await inbox.GetInboxAsync();
            return new ObjectResult(inboxItems);
        }
    }
}
