using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// A type of command in CQRS
/// Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TResponse">Type of response</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
