public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCorrelationIdManager(this IServiceCollection services)
    {
        services.AddScoped<ICorelationIdGenerator, CorrelationIdGenerator>();

        return services;
    }
}