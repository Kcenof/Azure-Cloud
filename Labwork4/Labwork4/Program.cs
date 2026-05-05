using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();

builder.Services.
    AddApplicationInsightsTelemetryWorkerService().
    ConfigureFunctionsApplicationInsights().
    AddAzureClients(b =>
    {
        b.AddBlobServiceClient(Environment.GetEnvironmentVariable("blobConn"));
        var endpoint = new Uri(Environment.GetEnvironmentVariable("textAnalyticsEndpoint"));
        var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("textAnalyticskey"));
        b.AddTextAnalyticsClient(endpoint, credential);
    });

builder.Build().Run();