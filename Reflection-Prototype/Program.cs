using System.Reflection;
using Reflection_Prototype;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
var services = new ServiceCollection();
var assemblies = Assembly.GetExecutingAssembly();
var targetType = typeof(IMessagable<,>);

var registrations = assemblies.GetTypes()
  .Where(x => x is {IsAbstract: false, IsInterface: false})
  .SelectMany(type => type.GetInterfaces()
    .Where(
      interfaceType => interfaceType.IsGenericType
                       && interfaceType.GetGenericTypeDefinition()
                       == targetType
    )
    .Select(service => new {service, type}));

foreach (var reg in registrations)
{
  Console.WriteLine($"service: {reg.service}, type: {reg.type}\n");

  var typeArgument = reg.service.GetGenericArguments();

  foreach (var type in typeArgument)
  {
    Console.WriteLine($"type {type}");
  }
  
  if (typeArgument[0]
      .IsGenericTypeParameter) continue;

  var messageHandlerFactoryType = typeof(MessageHandlerFactory<,>).MakeGenericType(typeArgument);

  Console.WriteLine($"Created Generic type: {messageHandlerFactoryType}\n");

  dynamic messageHandlerFactory =
    Activator.CreateInstance(messageHandlerFactoryType);

  if (messageHandlerFactory == null) continue;

  var genericInterfaceType =
    typeof(IMessageHandlerFactory<,>).MakeGenericType(typeArgument);

  services.AddScoped(reg.type, sp =>
  {
    var factory = (dynamic) sp.GetService(genericInterfaceType)!;
    return factory.Create();
  });
}

// Build the ServiceProvider
var serviceProvider = services.BuildServiceProvider();


app.Run();