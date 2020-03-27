using Eafit.MarcosYPatrones.Cqrs.Application;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class TaxYearSqlGenerator : BaseSqlGenerator, ITaxYearSqlGenerator
    {
        public TaxYearSqlGenerator(IDataComparer dataComparer) : base(dataComparer) { }

        public string BuildGetByTaxYearSqlQuery(string tableName)
        {
            return $"SELECT [F].[Id], [F].[Year] FROM [{tableName}] AS [F] WHERE [F].[Year] = @Year AND [F].[Id] NOT IN @ExcludedIds";
        }
    }
}
