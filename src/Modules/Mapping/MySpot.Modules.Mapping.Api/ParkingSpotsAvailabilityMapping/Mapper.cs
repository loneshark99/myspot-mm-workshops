using MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Commands;
using MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping.Events;
using MySpot.Shared.Abstractions.Events;
using MySpot.Shared.Abstractions.Messaging;

namespace MySpot.Modules.Mapping.Api.ParkingSpotsAvailabilityMapping;

internal sealed class Mapper : IEventHandler<ParkingSpotCreated>, IEventHandler<ParkingSpotDeleted>
{
    private const int ParkingSpotCapacity = 2;
    private readonly IMessageBroker _messageBroker;

    public Mapper(IMessageBroker messageBroker)
        => _messageBroker = messageBroker;

    public async Task HandleAsync(ParkingSpotCreated @event, CancellationToken cancellationToken = default)
    {
        var tags = new[] {"parking_spot"};
        await _messageBroker.PublishAsync(new AddResource(@event.ParkingSpotId, ParkingSpotCapacity, tags) , cancellationToken);
    }

    public async Task HandleAsync(ParkingSpotDeleted @event, CancellationToken cancellationToken = default)
    {
        await _messageBroker.PublishAsync(new DeleteResource(@event.ParkingSpotId) , cancellationToken);
    }
}