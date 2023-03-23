namespace Reflection_Prototype;

public class MessageHandlerFactory<T> : IMessageHandlerFactory<T>
{
  public IMessageHandler<T> Create()
  {
    return new MessageHandlerBase<T>();
  }
}