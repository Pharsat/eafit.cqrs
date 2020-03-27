using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class DueDateRepository : Repository<DueDate>, IDueDateRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDueDateSqlGenerator _sqlGenerator;
        private readonly IDataComparer _dataComparer;

        public DueDateRepository(
            IDbConnection connection,
            IDataComparer dataComparer,
            IDueDateSqlGenerator sqlGenerator) : base(connection, dataComparer, sqlGenerator) 
                => (_connection, _dataComparer, _sqlGenerator) = (connection, dataComparer, sqlGenerator);

        public async Task<DueDate> GetCompleteByIdAsync(int id)
        {
            CreateUpdateActions();

            if (Current.Any(p => p.Entity.Id == id))
            {
                return Current.Single(p => p.Entity.Id == id).Entity;
            }

            string selectByIdQuery = _sqlGenerator.BuildGetCompleteByIdSqlCommand();

            var resultSet = await _connection
                .QueryAsync<DueDate, TaxPayerType, DeliverableType, Jurisdiction, Form, TaxYear, Quarter, DueDate>(
                    selectByIdQuery,
                    (dueDate, taxPayerType, deliverableType, jurisdiction, form, taxyear, quarter) =>
                    {
                        return new DueDate(taxPayerType
                                            , deliverableType
                                            , jurisdiction
                                            , form
                                            , taxyear
                                            , dueDate.StatutoryDueDate
                                            , dueDate.ExtensionDate
                                            , quarter
                                            , dueDate.IsManuallyAdded
                                            ) { Id = id };
                    }, new { Id = id })
                .ConfigureAwait(false);

            if (!resultSet.Any())
            {
                throw new EntityDoesNotExistsException("Due date does not exist.");
            }

            DueDate entityResult = resultSet.Single();

            IDictionary<string, string> cleanProperties = _dataComparer.ConvertObjectToPropertiesDictionary(entityResult);

            Current.Add(new CleanEntity<DueDate>(entityResult, cleanProperties));

            return entityResult;
        }
    }
}
