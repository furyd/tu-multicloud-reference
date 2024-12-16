using Reference.Domain.Services.Interfaces;

namespace Reference.Domain.Services.Implementation;

public class DefaultHelloWorldService : IHelloWorldService
{
    public string SayHelloWorldTo(IName model) => $"Hello world {model.Name}";
}
