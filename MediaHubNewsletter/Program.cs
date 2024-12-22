using MediahubNewsletter;
using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter;
using MediahubNewsletter.Newsletter.Discord;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient();
        services.AddTransient<ICatalog, PlexCatalog>();
        services.AddTransient<IMediaLibrary, MediaLibrary>();
        services.AddTransient<IDistributionCanal, DiscordDistribution>();
    })
    .Build();

host.Run();
