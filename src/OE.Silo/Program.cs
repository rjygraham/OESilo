using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OE.Silo.Grains;
using Orleans;
using Orleans.Hosting;

await Host
	.CreateDefaultBuilder()
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .ConfigureApplicationParts(_ => _.AddApplicationPart(typeof(UserGraphGrain).Assembly).WithReferences())
            .AddMemoryGrainStorageAsDefault()
            .AddMemoryGrainStorage("PubSubStore");
    })
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureServices(services =>
        {
            services.AddControllers();
        });

        builder.Configure(builder =>
        {
            builder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        });
    })
    .RunConsoleAsync();