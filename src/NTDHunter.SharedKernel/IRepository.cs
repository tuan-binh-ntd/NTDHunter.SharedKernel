using Ardalis.Specification;

namespace NTDHunter.SharedKernel;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
