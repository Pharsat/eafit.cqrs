using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IQueryHandler<TQuery, TReturn> : IRequestHandler<TQuery> where TQuery : IQuery
    {
        Task<TReturn> HandleAsync(TQuery query);
    }
}
