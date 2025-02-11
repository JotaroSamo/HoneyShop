using MediatR;

namespace HoneyShop.Application.Core.Queries.Contracts
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}