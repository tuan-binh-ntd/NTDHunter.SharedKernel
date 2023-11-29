namespace NTDHunter.SharedKernel;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IReadOnlyCollection<EntityBase> entitiesWithEvents);
}

