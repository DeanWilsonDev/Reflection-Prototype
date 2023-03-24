namespace Reflection_Prototype;

public record ExampleQuery();

public class IdResponse
{
  public Guid Id { get; set; }
}

public class QueryHandler : IMessagable<ExampleQuery, IdResponse> { }