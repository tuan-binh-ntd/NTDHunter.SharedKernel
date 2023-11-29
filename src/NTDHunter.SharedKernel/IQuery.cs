using MediatR;

namespace NTDHunter.SharedKernel;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
