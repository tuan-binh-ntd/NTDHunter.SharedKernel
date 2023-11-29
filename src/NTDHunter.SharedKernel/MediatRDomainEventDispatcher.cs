using MediatR;

namespace NTDHunter.SharedKernel;

public class MediatRDomainEventDispatcher(IMediator mediator)
{
    private readonly IMediator _mediator = mediator;

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
}
