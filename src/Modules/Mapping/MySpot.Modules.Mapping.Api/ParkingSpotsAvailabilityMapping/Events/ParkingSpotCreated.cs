using MySpot.Shared.Abstractions.Events;

namespace MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Events;

public record ParkingSpotCreated(Guid ParkingSpotId) : IEvent;