using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter.Discord;
using PlexNewsletter.Newsletter;

namespace PlexNewsletter;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDistributionCanal _distribution;

    public Worker(ILogger<Worker> logger, IDistributionCanal distribution)
    {
        _logger = logger;
        _distribution = distribution;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await _distribution.Send();
            await Task.Delay(100000, stoppingToken);
        }
    }
}
