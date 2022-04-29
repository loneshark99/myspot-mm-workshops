using System;
using MySpot.Shared.Abstractions.Commands;

namespace MySpot.Modules.Saga.Api.Messages;

public record RemoveReservation(Guid UserId, Guid ReservationId) : ICommand;