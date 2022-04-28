using Microsoft.Extensions.Logging;
using MySpot.Shared.Abstractions.Events;

namespace MySpot.Modules.Notifications.Api.Events.External.Handlers;

internal sealed class SignedUpHandler : IEventHandler<SignedUp>
{
    private readonly ILogger<SignedUpHandler> _logger;

    public SignedUpHandler(ILogger<SignedUpHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}