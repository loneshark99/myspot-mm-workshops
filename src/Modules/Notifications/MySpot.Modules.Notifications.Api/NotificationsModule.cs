using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Modules.Notifications.Api.Clients;
using MySpot.Modules.Notifications.Api.Commands;
using MySpot.Modules.Notifications.Api.DAL;
using MySpot.Modules.Notifications.Api.Events.External;
using MySpot.Shared.Abstractions.Dispatchers;
using MySpot.Shared.Abstractions.Modules;
using MySpot.Shared.Infrastructure;
using MySpot.Shared.Infrastructure.Contracts;
using MySpot.Shared.Infrastructure.Modules;
using MySpot.Shared.Infrastructure.Postgres;

namespace MySpot.Modules.Notifications.Api;

internal sealed class NotificationsModule : IModule
{
    public string Name { get; } = "Notifications";
    
    public IEnumerable<string> Policies { get; } = new[]
    {
        "notifications"
    };
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<NotificationsDbContext>(configuration)
            .AddInitializer<NotificationsInitializer>()
            .AddSingleton<IEmailApiClient, EmailApiClient>();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
            .Subscribe<SendEmail>("emails/send", async (command, serviceProvider, ct) =>
            {
                await Task.CompletedTask;
            });

        app.UseContracts()
            .Register<SignedUpContract>();
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/emails/send", async (SendEmail command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command);
            return Results.NoContent();
        }).WithTags("Emails").WithName("Send email");;
    }
}