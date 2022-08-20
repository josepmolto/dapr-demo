using Book.Config;
using Book.Services;
using Common.Extensions;

namespace Book.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IBookService, BookService>()
            .AddSingleton<IOffersJanitor, OffersJanitor>()
            .AddDerbyzoneHttpClient()
            .AddDaprClientService()
            .AddKeyGeneratorService()
            .AddConfiguration(configuration);

        return services;
    }

    private static IServiceCollection AddDerbyzoneHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IBookService, BookService>(httpClient =>
            httpClient.BaseAddress = new Uri("http://derbyzone"));

        return services;
    }

    private static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Configure<DaprOptions>(configuration.GetSection("DaprOptions"));

        return services;
    }

    private static IServiceCollection AddDaprClientService(this IServiceCollection services)
    {
        services.AddDaprClient(builder => builder
            .UseHttpEndpoint($"http://book:60001")
            .UseGrpcEndpoint("http://book:60000"));

        return services;
    }
}