using MediatR;

namespace Application.Models;

public record CommandBase<T> : IRequest<T>;
