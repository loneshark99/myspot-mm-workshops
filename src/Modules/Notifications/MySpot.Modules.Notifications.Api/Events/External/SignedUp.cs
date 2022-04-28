using MySpot.Shared.Abstractions.Contracts;
using MySpot.Shared.Abstractions.Events;
using MySpot.Shared.Abstractions.Messaging;

namespace MySpot.Modules.Notifications.Api.Events.External;

[Message("users")]
public record SignedUp(Guid UserId, string Email) : IEvent;

internal class SignedUpContract : Contract<SignedUp>
{
    public SignedUpContract()
    {
        RequireAll();
    }
}
