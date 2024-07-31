using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// A class define handle logic of <see cref="ICommand{TResponse}"/> in CQRS
/// Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TCommand">Type of command</typeparam>
/// <typeparam name="TResponse">Type of response</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
