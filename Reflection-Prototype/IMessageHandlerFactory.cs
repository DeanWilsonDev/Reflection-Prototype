namespace Reflection_Prototype;

public interface IMessageHandlerFactory<TRequest, TResponse>
{
  public IMessageHandler<TRequest, TResponse> Create();
  
}