using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Reference.Api.Shared.Features.HelloWorld.Registration;

namespace Reference.Api.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static void Bootstrap(this WebApplication app)
    {
        app.AddSwagger();
        app.AddHelloWorldFeature();
    }

    private static void AddSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
