using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// A type of query in CQRS
/// Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TResponse">Type of response</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
