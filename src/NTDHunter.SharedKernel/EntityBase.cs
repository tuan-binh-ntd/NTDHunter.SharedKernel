namespace NTDHunter.SharedKernel;

/// <summary>
/// A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
/// </summary>
public abstract class EntityBase : HasDomainEventBase
{
    /// <summary>
    /// Identity of entity
    /// </summary>
    public int Id { get; set; }
}

/// <summary>
/// A base class for DDD Entities. Includes support for domain events dispatched post-persistence.
/// If you need to support both GUID and int IDs, change to EntityBase&lt;TId&gt; and use TId as the type for Id.
/// </summary>
/// <typeparam name="TId">Type of identity. Eg: StrongType, Guid, etc.</typeparam>
public abstract class EntityBase<TId> : HasDomainEventBase
    where TId : struct, IEquatable<TId>
{

    /// <summary>
    /// Identity of entity
    /// </summary>
    public TId Id { get; set; }
}

