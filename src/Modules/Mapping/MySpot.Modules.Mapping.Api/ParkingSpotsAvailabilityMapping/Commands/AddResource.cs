using MySpot.Shared.Abstractions.Commands;

namespace MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Commands;

public record AddResource(Guid ResourceId, int Capacity, IEnumerable<string> Tags) : ICommand;