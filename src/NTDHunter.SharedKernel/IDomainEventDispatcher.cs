namespace NTDHunter.SharedKernel;

/// <summary>
/// A simple interface for sending domain events. Can use MediatR or any other implementation.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Handle trigger domain events
    /// </summary>
    /// <param name="entitiesWithEvents">List of domain event in entities</param>
    /// <returns></returns>
    Task DispatchAndClearEvents(IReadOnlyCollection<EntityBase> entitiesWithEvents);

    /// <summary>
    /// Handle trigger domain events
    /// </summary>
    /// <typeparam name="TId">Type of identity</typeparam>
    /// <param name="entitiesWithEvents">List of domain event in entities</param>
    /// <returns></returns>
    Task DispatchAndClearEvents<TId>(IEnumerable<EntityBase<TId>> entitiesWithEvents) where TId : struct, IEquatable<TId>;
}

