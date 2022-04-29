using System;
using System.Threading.Tasks;
using Chronicle;
using MySpot.Modules.Saga.Api.Messages;
using MySpot.Shared.Abstractions.Messaging;

namespace MySpot.Modules.Saga.Api.Sagas;

public class ReservationSagaData
{
    public Guid UserId { get; set; }
}

public class ReservationSaga : Saga<ReservationSagaData>,
    ISagaStartAction<ParkingSpotReserved>,
    ISagaAction<ResourceReserved>
{
    private readonly IMessageBroker _messageBroker;

    public ReservationSaga(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            ParkingSpotReserved m => $"{m.ParkingSpotId}:{m.Date:d}",
            ResourceReserved m =>  $"{m.ResourceId}:{m.Date:d}",
            _ => throw new InvalidOperationException("Invalid saga ID.")
        };

    public async Task HandleAsync(ParkingSpotReserved message, ISagaContext context)
    {
        Data.UserId = message.UserId;
        await Task.CompletedTask;
    }

    public Task CompensateAsync(ParkingSpotReserved message, ISagaContext context)
        => Task.CompletedTask;

    public async Task HandleAsync(ResourceReserved message, ISagaContext context)
    {
        await CompleteAsync();
    }

    public Task CompensateAsync(ResourceReserved message, ISagaContext context)
        => Task.CompletedTask;
}