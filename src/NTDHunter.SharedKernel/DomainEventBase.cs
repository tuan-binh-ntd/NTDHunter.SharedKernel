using MediatR;

namespace NTDHunter.SharedKernel;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; set; } = DateTime.UtcNow;
}
