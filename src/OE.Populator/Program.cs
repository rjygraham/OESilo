using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OE.Populator
{
	class Program
	{
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press <enter> to continue");
            Console.ReadLine();

            var followeeId = Guid.NewGuid();
            var followerId = Guid.Empty;

            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

			for (int i = 1; i <= 1000; i++)
			{
                Console.WriteLine(i);

                var newFollowerId = Guid.NewGuid();

                if (followerId == Guid.Empty)
                {
                    followerId = newFollowerId;
                }

                await client.PostAsync($"everything/{newFollowerId}/follow/{followeeId}", null);
            }

            Console.WriteLine($"Followee ID: {followeeId}");
            Console.WriteLine($"Follower ID: {followerId}");
            Console.ReadLine();
        }
    }
}
