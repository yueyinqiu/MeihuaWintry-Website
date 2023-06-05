namespace MeihuaWintry.Services.ZhouyiReferencing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZhouyiProvider(
        this IServiceCollection services, string baseAddress)
    {
        _ = services.AddScoped((_) => new ZhouyiProvider(baseAddress));
        return services;
    }
}
