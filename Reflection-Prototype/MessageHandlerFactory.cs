namespace Reflection_Prototype;

public class MessageHandlerFactory<TRequest, TResponse> : IMessageHandlerFactory<TRequest, TResponse>
{
  public IMessageHandler<TRequest, TResponse> Create()
  {
    return new MessageHandlerBase<TRequest, TResponse>();
  }
}