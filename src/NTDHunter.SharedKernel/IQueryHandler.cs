using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// A class define handle logic of <see cref="IQuery{TResponse}"/> in CQRS
/// Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TQuery">Type of command</typeparam>
/// <typeparam name="TResponse">Type of response</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
