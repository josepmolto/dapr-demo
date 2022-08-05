using Derbyzone.Config;
using Derbyzone.Generators;
using Derbyzone.Sender;

namespace Derbyzone.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IOffersOrchestrator, OffersOrchestrator>()
            .AddSingleton<IOfferGenerator, OfferGenerator>()
            .AddSingleton<IKeyGenerator, KeyGenerator>()
            .AddSingleton<IStorageSender, DaprStorageSender>()
            .Configure<OrchestratorConfig>(configuration.GetSection("OrchestratorConfig"))
            .Configure<RandomData>(configuration.GetSection("RandomData"))
            .Configure<SenderOptions>(configuration.GetSection("SenderOptions"))
            .AddDaprClientService()
            .AddHttpClientSender();
    }

    private static IServiceCollection AddDaprClientService(this IServiceCollection services)
    {
        services.AddDaprClient(builder => builder
            .UseHttpEndpoint($"http://localhost:50001")
            .UseGrpcEndpoint("http://localhost:50000"));

        return services;
    }

    private static IServiceCollection AddHttpClientSender(this IServiceCollection services)
    {
        services.AddHttpClient<IClientSender, HttpClientSender>();

        return services;
    }
}