using Ardalis.Specification;

namespace NTDHunter.SharedKernel;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

