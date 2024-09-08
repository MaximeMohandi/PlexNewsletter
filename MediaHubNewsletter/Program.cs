using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter.Discord;
using PlexNewsletter;
using PlexNewsletter.Newsletter;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<ICatalog, PlexCatalog>();
        services.AddTransient<IMediaLibrary, MediaLibrary>();
        services.AddTransient<IDistributionCanal, DiscordDistribution>();
    })

    .Build();

host.Run();
