namespace MeihuaWintry.Services.CaseStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCaseStorage(this IServiceCollection services)
    {
        _ = services.AddScoped<CaseStore>();
        return services;
    }
}
