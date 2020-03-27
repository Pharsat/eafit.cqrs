using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.QuarterDueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IDataComparer _dataComparer;
        private readonly ISqlGenerator _sqlGeneratorBase;

        private Queue<EntityAction> ActionsQueue { get; }

        public ITeamRepository Teams { get; }
        public IDueDateRepository DueDates { get; }
        public IProjectRepository Projects { get; }
        public ITaxPayerTypeRepository TaxPayerTypes { get; }
        public IDeliverableTypeRepository DeliverableTypes { get; }
        public IJurisdictionRepository Jurisdictions { get; }
        public IFormRepository Forms { get; }
        public ITaxYearRepository TaxYears { get; }
        public IQuarterRepository Quarters { get; }
        public IQuarterDueDateRepository QuarterDueDates { get; }

        public UnitOfWork(
                IDbConnection connection,
                ITeamRepository teams,
                IProjectRepository projects,
                IDueDateRepository duedates,
                ITaxPayerTypeRepository taxPayerTypes,
                IDeliverableTypeRepository deliverableTypes,
                IJurisdictionRepository jurisdictions,
                IFormRepository forms,
                ITaxYearRepository taxYears,
                IQuarterRepository quarters,
                IQuarterDueDateRepository quarterDueDates,
                ISqlGenerator sqlGeneratorBase,
                IDataComparer datacomparer)
        {
            _connection = connection;

            Teams = teams;
            DueDates = duedates;
            Projects = projects;
            TaxPayerTypes = taxPayerTypes;
            DeliverableTypes = deliverableTypes;
            Jurisdictions = jurisdictions;
            Forms = forms;
            TaxYears = taxYears;
            Quarters = quarters;
            QuarterDueDates = quarterDueDates;

            Teams.ActionCreated += ActionCreated;
            DueDates.ActionCreated += ActionCreated;
            Projects.ActionCreated += ActionCreated;
            TaxPayerTypes.ActionCreated += ActionCreated;
            DeliverableTypes.ActionCreated += ActionCreated;
            Jurisdictions.ActionCreated += ActionCreated;
            Forms.ActionCreated += ActionCreated;
            TaxYears.ActionCreated += ActionCreated;
            Quarters.ActionCreated += ActionCreated;
            QuarterDueDates.ActionCreated += ActionCreated;

            _sqlGeneratorBase = sqlGeneratorBase;
            _dataComparer = datacomparer;
            ActionsQueue = new Queue<EntityAction>();
        }

        public async Task<int> SaveChangesAsync()
        {
            var rowsAffected = 0;
            _connection.Open();
            var transaction = _connection.BeginTransaction();
            try
            {
                while (ActionsQueue.Count > 0)
                {
                    EntityAction entityAction = ActionsQueue.Dequeue();

                    var dbArguments = _dataComparer.TransformPropertiesToDynamicParameters(entityAction.Changes);

                    switch (entityAction.EntityState)
                    {
                        case EntityState.Added:
                            if (entityAction.Entity is NotAutoIdentityEntity)
                            {
                                string insertSqlQuery = _sqlGeneratorBase.BuildAddNotAutoidentitySqlCommand(entityAction.Entity);
                                await _connection.ExecuteAsync(insertSqlQuery, dbArguments, transaction).ConfigureAwait(false);
                            }
                            else
                            {
                                string insertSqlQuery = _sqlGeneratorBase.BuildAddSqlCommand(entityAction.Entity);
                                var entityAdded = await _connection.QueryAsync<int>(insertSqlQuery, dbArguments, transaction).ConfigureAwait(false);
                                entityAction.Entity.Id = entityAdded.Single();
                            }
                            break;
                        case EntityState.Deleted:
                            string deleteSqlQuery = _sqlGeneratorBase.BuildDeleteSqlCommand(entityAction.Entity, entityAction.EntityTypeName);
                            rowsAffected += await _connection.ExecuteAsync(deleteSqlQuery, new { entityAction.Entity.Id }, transaction).ConfigureAwait(false);
                            break;
                        case EntityState.Modified:
                            dbArguments.Add(nameof(entityAction.Entity.Id), entityAction.Entity.Id);
                            string updateSqlQuery = _sqlGeneratorBase.BuildUpdateSqlCommand(entityAction.Entity, entityAction.Changes);
                            rowsAffected += await _connection.ExecuteAsync(updateSqlQuery, dbArguments, transaction).ConfigureAwait(false);
                            break;
                    }
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
            return rowsAffected;
        }

        public void EnqueueAction(EntityAction action)
        {
            ActionsQueue.Enqueue(action);
        }

        public void ActionCreated(object sender, ActionCreatedEventArgs e)
        {
            ActionsQueue.Enqueue(e.Action);
        }
    }
}
