using CLDV7112_PracticumGuide.Data;
using CLDV7112_PracticumGuide.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CLDV7112_PracticumGuide.Services
{
    public class DataSimulator : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<DataSimulator> _logger;
        private static readonly Random _rand = new();

        public DataSimulator(IServiceProvider services, ILogger<DataSimulator> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DataSimulator started.");

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    try
                    {
                        using var scope = _services.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var reading = new SensorReading
                        {
                            DeviceId = "Sim-" + _rand.Next(1, 5),
                            Temperature = Math.Round(_rand.NextDouble() * 10 + 20, 2),
                            RecordedAt = DateTime.UtcNow
                        };

                        db.SensorReadings.Add(reading);
                        await db.SaveChangesAsync(stoppingToken);

                        _logger.LogDebug("Inserted reading {DeviceId} {Temperature}", reading.DeviceId, reading.Temperature);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while creating sensor reading.");
                    }
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // expected during shutdown
            }
            finally
            {
                _logger.LogInformation("DataSimulator stopping.");
            }
        }
    }
}