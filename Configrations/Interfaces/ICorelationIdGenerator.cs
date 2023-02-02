using Microsoft.Extensions.Primitives;

public interface ICorelationIdGenerator
{
    void Set(String correlationId);

    String Get();
}