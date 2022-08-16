using Common.Extensions;
using Search.Config;
using Search.Services;
using Search.Storage;

namespace Search.Extensions;
public static class ServicesCollectionExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddSingleton<ISearcher, Searcher>()
            .AddSingleton<IRateSearcher, RateSearcher>()
            .AddSingleton<IOfferRetriever, DaprOfferRetriever>()
            .AddKeyGeneratorService()
            .AddDaprClientService()
            .AddConfiguration(configuration);
    }

    private static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Configure<DaprOptions>(configuration.GetSection("DaprOptions"))
            .Configure<StaticData>(configuration.GetSection("StaticData"));


        return services;
    }

    private static IServiceCollection AddDaprClientService(this IServiceCollection services)
    {
        services.AddDaprClient(builder => builder
            .UseHttpEndpoint($"http://search:60001")
            .UseGrpcEndpoint("http://search:60000"));

        return services;
    }
}