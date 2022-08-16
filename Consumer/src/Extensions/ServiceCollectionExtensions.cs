using Common.Extensions;
using Consumer.Config;
using Consumer.Storage;

namespace Consumer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services,
        IConfiguration configuration) =>
            services
                .AddSingleton<IOfferStorageSender, DaprStorageSender>()
                .AddKeyGeneratorService()
                .AddDaprClientService()
                .Configure<DaprOptions>(configuration.GetSection("DaprOptions"));

    private static IServiceCollection AddDaprClientService(this IServiceCollection services)
    {
        services.AddDaprClient(builder => builder
            .UseHttpEndpoint($"http://consumer:60001")
            .UseGrpcEndpoint("http://consumer:60000"));

        return services;
    }
}