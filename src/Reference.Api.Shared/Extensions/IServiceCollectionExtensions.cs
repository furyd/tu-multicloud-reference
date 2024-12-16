using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Reference.Api.Shared.Constants;
using Reference.Api.Shared.Features.HelloWorld.Registration;
using Reference.Api.Shared.Models;

namespace Reference.Api.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static void Bootstrap(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddServices();
    }

    private static void AddOpenApi(this IServiceCollection services)
    {
        services.EnrichProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.RegisterHelloWorldService();
    }

    private static void EnrichProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options => options.CustomizeProblemDetails = CustomiseProblemDetails);
    }

    private static void CustomiseProblemDetails(ProblemDetailsContext context)
    {

        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd(ProblemDetailsExtensionFields.RequestId, context.HttpContext.TraceIdentifier);
        context.ProblemDetails.Extensions.TryAdd(ProblemDetailsExtensionFields.CorrelationId, context.HttpContext.Request.Headers[AdditionalHeaders.CorrelationId].ToString());

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>();
        var cloud = (Cloud?)context.HttpContext.RequestServices.GetService(typeof(Cloud));

        context.ProblemDetails.Extensions.TryAdd(ProblemDetailsExtensionFields.TraceId, activity?.Activity.Id);
        context.ProblemDetails.Extensions.TryAdd(ProblemDetailsExtensionFields.Cloud, cloud?.Name);
    }
}
