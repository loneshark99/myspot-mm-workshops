using MySpot.Shared.Abstractions.Commands;

namespace MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Commands;

public record DeleteResource(Guid ResourceId) : ICommand;