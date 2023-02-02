public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
}