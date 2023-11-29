using MediatR;

namespace NTDHunter.SharedKernel;

public interface ICommandHandler<TCommand, TReponse> : IRequestHandler<TCommand, TReponse>
    where TCommand : ICommand<TReponse>
{
}
