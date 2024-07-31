using MediatR;

namespace NTDHunter.SharedKernel;

/// <summary>
/// A base type for domain events. Depends on MediatR INotification.
/// </summary>
public abstract class DomainEventBase : INotification
{
    /// <summary>
    /// Occurred event time
    /// </summary>
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
