public class CorrelationIdGenerator : ICorelationIdGenerator
{

    private String _correlationId = Guid.NewGuid().ToString();

    public string Get() {
        return _correlationId;
    }
    

    public void Set(string correlationId){ 
        _correlationId = correlationId;
    }
}
    