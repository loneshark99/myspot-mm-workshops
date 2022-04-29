using System;
using System.Threading.Tasks;
using Chronicle;
using MySpot.Modules.Saga.Api.Messages;
using MySpot.Shared.Abstractions.Messaging;

namespace MySpot.Modules.Saga.Api.Sagas;

internal sealed class ReservationSagaData
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
}

internal sealed class ReservationSaga : Saga<ReservationSagaData>,
    ISagaStartAction<ParkingSpotReserved>,
    ISagaAction<ResourceReserved>,
    ISagaAction<ResourceReservationFailed>
{
    private const int Capacity = 2;
    private readonly IMessageBroker _messageBroker;

    public ReservationSaga(IMessageBroker messageBroker) => _messageBroker = messageBroker;

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            ParkingSpotReserved m => $"{m.ParkingSpotId}:{m.Date:d}",
            ResourceReserved m => $"{m.ResourceId}:{m.Date:d}",
            ResourceReservationFailed m => $"{m.ResourceId}:{m.Date:d}",
            _ => throw new InvalidOperationException("Unsupported message.")
        };

    public async Task HandleAsync(ParkingSpotReserved message, ISagaContext context)
    {
        Data.ReservationId = message.ReservationId;
        Data.UserId = message.UserId;
        await _messageBroker.PublishAsync(new ReserveResource(message.ParkingSpotId, message.ReservationId,
            Capacity, message.Date, 1));
    }

    public Task CompensateAsync(ParkingSpotReserved message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(ResourceReserved message, ISagaContext context)
        => CompleteAsync();

    public Task CompensateAsync(ResourceReserved message, ISagaContext context)
        => Task.CompletedTask;

    public async Task HandleAsync(ResourceReservationFailed message, ISagaContext context)
    {
        await _messageBroker.PublishAsync(new RemoveReservation(Data.UserId, Data.ReservationId));
    }

    public Task CompensateAsync(ResourceReservationFailed message, ISagaContext context)
        => Task.CompletedTask;
}