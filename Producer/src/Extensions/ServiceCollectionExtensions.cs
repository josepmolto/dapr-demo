

using Producer.Config;
using Producer.Sender;

namespace Producer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddSingleton<IQueueSender, DaprQueueSender>()
            .Configure<QueueConfiguration>(configuration.GetSection("QueueConfiguration"))
            .AddDaprClientService();

    private static IServiceCollection AddDaprClientService(this IServiceCollection services)
    {
        services.AddDaprClient(builder => builder
            .UseHttpEndpoint($"http://producer:60001")
            .UseGrpcEndpoint("http://producer:60000"));

        return services;
    }
}