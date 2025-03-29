using Aviv.Base.UI.Services;

namespace Aviv.Base.UI.Helper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPageBreadcrumbService(this IServiceCollection services)
    {
        // Register as scoped instead of singleton
        services.AddScoped<PageBreadcrumbService>();
        return services;
    }
}