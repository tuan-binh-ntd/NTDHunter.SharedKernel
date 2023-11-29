using MediatR;

namespace NTDHunter.SharedKernel;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
