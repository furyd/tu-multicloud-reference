using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reference.Api.Shared.Features.HelloWorld.Models;
using Reference.Domain.Services.Interfaces;

namespace Reference.Api.Shared.Features.HelloWorld.Controllers;

public static class HelloWorld
{
    public static IResult Get() => Results.Ok(new HelloWorldResponseModel("hello world"));

    public static IResult Post(
        [FromServices] IValidator<HelloWorldRequestModel> validator,
        [FromServices] IHelloWorldService service,
        [FromBody] HelloWorldRequestModel model
        )
    {
        var validation = validator.Validate(model);

        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary(), statusCode: StatusCodes.Status422UnprocessableEntity);
        }

        var response = service.SayHelloWorldTo(model);

        return Results.Ok(new HelloWorldResponseModel(response));
    }
}
