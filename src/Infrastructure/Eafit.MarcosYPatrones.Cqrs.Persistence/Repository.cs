using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public event ActionCreatedEventHandler<ActionCreatedEventArgs> ActionCreated;

        private readonly IDbConnection _connection;
        private readonly IDataComparer _dataComparer;
        private readonly ISqlGenerator _sqlGeneratorBase;

        public List<CleanEntity<TEntity>> Current { get; }

        public Repository(
            IDbConnection connection,
            IDataComparer datacomparer,
            ISqlGenerator sqlGeneratorBase)
        {
            Current = new List<CleanEntity<TEntity>>();
            ActionCreated = delegate { };
            _connection = connection;
            _dataComparer = datacomparer;
            _sqlGeneratorBase = sqlGeneratorBase;
        }

        protected virtual void OnActionCreated(ActionCreatedEventArgs e)
        {
            ActionCreated.Invoke(this, e);
        }

        public void Add(TEntity entity)
        {
            CreateUpdateActions();

            var entityAction = new EntityAction(entity, EntityState.Added, entity.GetType().Name, _dataComparer.ConvertObjectToPropertiesDictionary(entity));

            IDictionary<string, string> cleanProperties = _dataComparer.ConvertObjectToPropertiesDictionary(entity);
            Current.Add(new CleanEntity<TEntity>(entity, cleanProperties));

            OnActionCreated(new ActionCreatedEventArgs(entityAction));
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            CreateUpdateActions();

            if (Current.Any(p => p.Entity.Id == id))
            {
                return Current.Single(p => p.Entity.Id == id).Entity;
            }

            string selectByIdQuery = _sqlGeneratorBase.BuildGetByIdSqlCommand(typeof(TEntity), nameof(Entity.Id));

            IEnumerable<TEntity> resultSet = await _connection.QueryAsync<TEntity>(selectByIdQuery, new { Id = id }).ConfigureAwait(false);

            if (!resultSet.Any())
            {
                throw new EntityDoesNotExistsException($"{typeof(TEntity)} does not exist.");
            }

            TEntity entityResult = resultSet.Single();

            IDictionary<string, string> cleanProperties = _dataComparer.ConvertObjectToPropertiesDictionary(entityResult);
            Current.Add(new CleanEntity<TEntity>(entityResult, cleanProperties));

            return entityResult;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            CreateUpdateActions();

            IEnumerable<int> excludedIds = Current.Select(p => p.Entity.Id);

            string selectAllQuery = _sqlGeneratorBase.BuildGetAllSqlQuery(typeof(TEntity));

            IEnumerable<TEntity> resultSet = (await _connection.QueryAsync<TEntity>(selectAllQuery, new { ExcludedIds = excludedIds }).ConfigureAwait(false)).ToList();

            IEnumerable<CleanEntity<TEntity>> propertiesDictionaries = from entity in resultSet
                                                                       let cleanEntity = _dataComparer.ConvertObjectToPropertiesDictionary(entity)
                                                                       select new CleanEntity<TEntity>(entity, cleanEntity);

            Current.AddRange(propertiesDictionaries);

            return Current.Select(p => p.Entity);
        }

        public void Remove(TEntity entity)
        {
            CreateUpdateActions();

            var entityAction = new EntityAction(entity, EntityState.Deleted, entity.GetType().Name);
            OnActionCreated(new ActionCreatedEventArgs(entityAction));
            Current.Remove(Current.Single(p => p.Entity.Equals(entity)));
        }

        public void CreateUpdateActions()
        {
            foreach (var cleanEntity in Current)
            {
                var changedValues = _dataComparer.GetChangedValues(cleanEntity.Entity, cleanEntity.CleanProperties);
                if (changedValues.Count > 0)
                {
                    var action = new EntityAction(cleanEntity.Entity, EntityState.Modified, cleanEntity.Entity.GetType().Name, _dataComparer.GetChangedValues(cleanEntity.Entity, cleanEntity.CleanProperties));
                    OnActionCreated(new ActionCreatedEventArgs(action));
                    cleanEntity.RefreshCleanProperties(_dataComparer.ConvertObjectToPropertiesDictionary(cleanEntity.Entity));
                }
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            string existsByIdQuery = _sqlGeneratorBase.BuildExistsByIdSqlCommand(typeof(TEntity), nameof(Entity.Id));
            IEnumerable<TEntity> resultSet = await _connection.QueryAsync<TEntity>(existsByIdQuery, new { Id = id }).ConfigureAwait(false);
            return resultSet.Any();
        }

        public void RemoveById(int id)
        {
            CreateUpdateActions();

            var entityAction = new EntityAction(new Entity { Id = id }, EntityState.Deleted, typeof(TEntity).Name);
            OnActionCreated(new ActionCreatedEventArgs(entityAction));
            Current.RemoveAll(p => p.Entity.Id == id);
        }
    }
}
