using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reference.Api.Shared.Features.HelloWorld.Constants;
using Reference.Api.Shared.Features.HelloWorld.Models;
using Reference.Api.Shared.Features.HelloWorld.Validators;
using Reference.Domain.Services.Implementation;
using Reference.Domain.Services.Interfaces;

namespace Reference.Api.Shared.Features.HelloWorld.Registration;

internal static class ComposeDependencies
{
    internal static void RegisterHelloWorldService(this IServiceCollection services)
    {
        services.TryAddSingleton<IHelloWorldService, DefaultHelloWorldService>();
        services.TryAddSingleton<IValidator<HelloWorldRequestModel>, HelloWorldRequestModelValidator>();
    }

    internal static void AddHelloWorldFeature(this WebApplication app)
    {
        app.MapGet(Routes.HelloWorld, Controllers.HelloWorld.Get)
            .Produces<HelloWorldResponseModel>()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName(Endpoints.HelloWorld)
            .WithOpenApi()
            ;

        app.MapPost(Routes.HelloWorld, Controllers.HelloWorld.Post)
            .Produces<HelloWorldResponseModel>()
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName(Endpoints.HelloWorldTo)
            .WithOpenApi()
            ;


    }
}
