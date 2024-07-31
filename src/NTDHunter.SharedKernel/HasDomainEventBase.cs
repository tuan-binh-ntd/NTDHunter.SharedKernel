using System.ComponentModel.DataAnnotations.Schema;

namespace NTDHunter.SharedKernel;

/// <summary>
/// Support raise and store domain events
/// </summary>
public abstract class HasDomainEventBase
{
    private readonly List<DomainEventBase> _domainEvents = [];
    /// <summary>
    /// List of domain events
    /// </summary>
    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Raise domain event
    /// </summary>
    /// <param name="domainEvent">Domain event</param>
    protected void RaiseDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    internal void ClearDomainEvents() => _domainEvents.Clear();
}
