namespace Reflection_Prototype;

public interface IMessageHandlerFactory<T>
{
  public IMessageHandler<T> Create();
  
}