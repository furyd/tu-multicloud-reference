using Reference.Domain.Services.Interfaces;

namespace Reference.Api.Shared.Features.HelloWorld.Models;

public class HelloWorldRequestModel : IName
{
    public string Name { get; set; } = string.Empty;
}
