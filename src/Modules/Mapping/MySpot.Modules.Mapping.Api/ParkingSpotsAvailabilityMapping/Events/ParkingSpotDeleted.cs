using MySpot.Shared.Abstractions.Events;

namespace MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Events;

public record ParkingSpotDeleted(Guid ParkingSpotId) : IEvent;