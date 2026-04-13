using Microsoft.Extensions.DependencyInjection;

namespace PanicDb;

public static class Extensions
{
    public static IServiceCollection AddPanicDb(this IServiceCollection services)
    {
        return services;
    }
}