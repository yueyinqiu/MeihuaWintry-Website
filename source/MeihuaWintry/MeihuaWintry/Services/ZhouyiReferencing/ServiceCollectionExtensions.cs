namespace MeihuaWintry.Services.ZhouyiReferencing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZhouyiProvider(
        this IServiceCollection services, string baseAddress)
    {
        _ = services.AddSingleton(new ZhouyiProvider(baseAddress));
        return services;
    }
}
