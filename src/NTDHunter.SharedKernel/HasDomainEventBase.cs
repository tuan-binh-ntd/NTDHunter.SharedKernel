using System.ComponentModel.DataAnnotations.Schema;

namespace NTDHunter.SharedKernel;

public abstract class HasDomainEventBase
{
    private readonly List<DomainEventBase> _domainEvents = [];
    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    internal void ClearDomainEvents() => _domainEvents.Clear();
}
