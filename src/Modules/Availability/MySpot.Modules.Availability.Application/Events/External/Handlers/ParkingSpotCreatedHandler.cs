using System.Threading;
using System.Threading.Tasks;
using MySpot.Modules.Availability.Application.Exceptions;
using MySpot.Modules.Availability.Core.Entities;
using MySpot.Modules.Availability.Core.Repositories;
using MySpot.Modules.Availability.Core.ValueObjects;
using MySpot.Shared.Abstractions.Events;
using MySpot.Shared.Abstractions.Messaging;

namespace MySpot.Modules.Availability.Application.Events.External.Handlers;

internal sealed class ParkingSpotCreatedHandler : IEventHandler<ParkingSpotCreated>
{
    private readonly IResourcesRepository _resourcesRepository;
    private readonly IMessageBroker _messageBroker;

    public ParkingSpotCreatedHandler(IResourcesRepository resourcesRepository, IMessageBroker messageBroker)
    {
        _resourcesRepository = resourcesRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(ParkingSpotCreated @event, CancellationToken cancellationToken = default)
    {
        var resourceId = @event.ParkingSpotId;
        if (await _resourcesRepository.ExistsAsync(resourceId))
        {
            throw new ResourceAlreadyExistsException(resourceId);
        }

        var resource = Resource.Create(resourceId, 2, new []{new Tag("parking_spot")});
        await _resourcesRepository.AddAsync(resource);
        await _messageBroker.PublishAsync(new ResourceAdded(resourceId), cancellationToken);
    }
}