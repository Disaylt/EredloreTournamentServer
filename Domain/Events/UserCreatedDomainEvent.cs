using Domain.Entities;
using MediatR;

namespace Domain.Events;

public sealed record UserCreatedDomainEvent(UserEntity User) : INotification;
