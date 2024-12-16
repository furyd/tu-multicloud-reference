using Amazon.CloudWatchLogs;
using Reference.Api.Shared.Extensions;
using Reference.Api.Shared.Models;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        var client = new AmazonCloudWatchLogsClient();
        configuration.WriteTo.AmazonCloudWatch((ICloudWatchSinkOptions)AmazonCloudWatchLogsDefaultConfiguration.Standard, client);
    });

    builder.Services.AddSingleton(new Cloud("AWS"));
    builder.Services.Bootstrap();

    var app = builder.Build();

    app.UseHttpsRedirection();

    app.Bootstrap();

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Error(exception, "Error creating service");
}
finally
{
    await Log.CloseAndFlushAsync();
}