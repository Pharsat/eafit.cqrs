using Eafit.MarcosYPatrones.Cqrs.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        event ActionCreatedEventHandler<ActionCreatedEventArgs> ActionCreated;

        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void CreateUpdateActions();

        Task<bool> ExistsAsync(int id);

        void RemoveById(int id);
    }
}
