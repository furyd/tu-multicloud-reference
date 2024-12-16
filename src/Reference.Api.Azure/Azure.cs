using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Reference.Api.Shared.Extensions;
using Reference.Api.Shared.Models;
using Serilog;
using System.Fabric;

namespace Reference.Api.Azure
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance.
    /// </summary>
    internal sealed class Azure(StatelessServiceContext context) : StatelessService(context)
    {

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return
            [
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        var builder = WebApplication.CreateBuilder();

                        builder.Services.AddSingleton(serviceContext);
                        builder.WebHost
                                    .UseKestrel()
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url);

                        builder.Services.AddApplicationInsightsTelemetry();

                        builder.Host.UseSerilog((context, services, configuration) =>
                        {
                            var telemetryConfiguration = services.GetRequiredService<TelemetryConfiguration>();
                            configuration.WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
                        });


                        builder.Services.AddSingleton(new Cloud("Azure"));
                        builder.Services.Bootstrap();

                        var app = builder.Build();
                        app.Bootstrap();


                        return app;

                    }))
            ];
        }
    }
}
