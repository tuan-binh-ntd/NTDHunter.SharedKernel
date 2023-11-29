namespace NTDHunter.SharedKernel;

public abstract class EntityBase : HasDomainEventBase
{
    public int Id { get; set; }
}

public abstract class EntityBase<TPrimaryKey> : HasDomainEventBase
    where TPrimaryKey : struct, IEquatable<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}

