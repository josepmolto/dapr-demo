using Common.Key;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKeyGeneratorService(this IServiceCollection services) =>
        services.AddSingleton<IKeyGenerator, KeyGenerator>();
}