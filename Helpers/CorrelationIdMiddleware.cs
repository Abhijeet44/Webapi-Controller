public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    private const String _correlationIdHeader = "X-Correlation-Id";

    public CorrelationIdMiddleware(RequestDelegate next){

        _next = next;

    }

    public async Task Invoke(HttpContext context, ICorelationIdGenerator corelationIdGenerator)
    {
        var correlationId = GetCorrelationIdTrace(context, corelationIdGenerator);
        AddCorrelationIdResponse(context, correlationId);

        await _next(context);
    }

    private static String GetCorrelationIdTrace(HttpContext context, ICorelationIdGenerator correlationIdGenerator)
    {
        if(context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }
        else
        {
            return correlationIdGenerator.Get();
        }

    }

    public static void AddCorrelationIdResponse(HttpContext context, String correlationId)
    {
        context.Response.OnStarting(() => {
            context.Response.Headers.Add(_correlationIdHeader, new[]{correlationId});
            return Task.CompletedTask;
        });
    }

}