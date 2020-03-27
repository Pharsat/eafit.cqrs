using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.DueDates
{
    public class TaxYearRepository : Repository<TaxYear>, ITaxYearRepository
    {
        private readonly ITaxYearSqlGenerator _sqlGenerator;
        private readonly IDbConnection _connection;
        private readonly IDataComparer _dataComparer;

        public TaxYearRepository(
            IDbConnection connection,
            IDataComparer datacomparer,
            ITaxYearSqlGenerator sqlGeneratorBase) :
            base(
                connection,
                datacomparer,
                sqlGeneratorBase)
        {
            _sqlGenerator = sqlGeneratorBase;
            _connection = connection;
            _dataComparer = datacomparer;
        }

        public async Task<IEnumerable<TaxYear>> GetByYearAsync(int year)
        {
            CreateUpdateActions();

            var existingEntitiesMatchingCondition = Current.Where(p => p.Entity.Year == year).Select(p => p.Entity).ToList();

            string getByNameSqlQuery = _sqlGenerator.BuildGetByTaxYearSqlQuery(typeof(TaxYear).Name);
            IEnumerable<TaxYear> resultsFromDataBase = await _connection.QueryAsync<TaxYear>(getByNameSqlQuery, new { Year = year, ExcludedIds = existingEntitiesMatchingCondition.Select(p => p.Id).ToArray() }).ConfigureAwait(false);
            existingEntitiesMatchingCondition.AddRange(resultsFromDataBase);

            IEnumerable<CleanEntity<TaxYear>> propertiesDictionaries = from entity in resultsFromDataBase
                                                                       let cleanEntity = _dataComparer.ConvertObjectToPropertiesDictionary(entity)
                                                                       select new CleanEntity<TaxYear>(entity, cleanEntity);

            Current.AddRange(propertiesDictionaries);

            return existingEntitiesMatchingCondition;
        }
    }
}
