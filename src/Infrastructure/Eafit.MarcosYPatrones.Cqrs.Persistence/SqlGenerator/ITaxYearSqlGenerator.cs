namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface ITaxYearSqlGenerator : ISqlGenerator
    {
        string BuildGetByTaxYearSqlQuery(string tableName);
    }
}
