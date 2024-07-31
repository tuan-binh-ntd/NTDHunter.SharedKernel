using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// Implement of <see cref="IDomainEventDispatcher"/>
/// </summary>
/// <param name="mediator">Mediator pattern</param>
public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Handle trigger domain events
    /// </summary>
    /// <param name="entitiesWithEvents">List of domain event in entities</param>
    /// <returns></returns>
    public async Task DispatchAndClearEvents(IReadOnlyCollection<EntityBase> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Handle trigger domain events
    /// </summary>
    /// <typeparam name="TId">Type of identity</typeparam>
    /// <param name="entitiesWithEvents">List of domain event in entities</param>
    /// <returns></returns>
    public async Task DispatchAndClearEvents<TId>(IEnumerable<EntityBase<TId>> entitiesWithEvents)
    where TId : struct, IEquatable<TId>
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
