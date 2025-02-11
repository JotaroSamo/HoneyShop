using MediatR;

namespace HoneyShop.Application.Core.Commands.Contracts
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}