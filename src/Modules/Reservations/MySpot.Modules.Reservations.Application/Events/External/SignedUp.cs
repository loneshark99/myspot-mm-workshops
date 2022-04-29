using MySpot.Shared.Abstractions.Events;

namespace MySpot.Modules.Reservations.Application.Events.External;

public record SignedUp(Guid UserId, string Email, string JobTitle) : IEvent;
