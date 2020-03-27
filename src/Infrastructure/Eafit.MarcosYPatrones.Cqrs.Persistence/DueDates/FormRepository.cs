using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly IFormSqlGenerator _sqlGenerator;
        private readonly IDbConnection _connection;
        private readonly IDataComparer _dataComparer;

        public FormRepository(
            IDbConnection connection,
            IDataComparer datacomparer,
            IFormSqlGenerator sqlGeneratorBase) :
            base(
                connection,
                datacomparer,
                sqlGeneratorBase)
        {
            _sqlGenerator = sqlGeneratorBase;
            _connection = connection;
            _dataComparer = datacomparer;
        }

        public async Task<IEnumerable<Form>> GetByNameAsync(string name )
        {
            CreateUpdateActions();

            var existingEntitiesMatchingCondition = Current.Where(p => p.Entity.Name == name).Select(p => p.Entity).ToList();

            string getByNameSqlQuery = _sqlGenerator.BuildGetByNameSqlQuery(typeof(Form).Name);
            IEnumerable<Form> resultsFromDataBase = await _connection.QueryAsync<Form>(getByNameSqlQuery, new { Name = name, ExcludedIds = existingEntitiesMatchingCondition.Select(p => p.Id).ToArray() }).ConfigureAwait(false);
            existingEntitiesMatchingCondition.AddRange(resultsFromDataBase);

            IEnumerable<CleanEntity<Form>> propertiesDictionaries = from entity in resultsFromDataBase
                                                                    let cleanEntity = _dataComparer.ConvertObjectToPropertiesDictionary(entity)
                                                                    select new CleanEntity<Form>(entity, cleanEntity);

            Current.AddRange(propertiesDictionaries);

            return existingEntitiesMatchingCondition;
        }
    }
}
