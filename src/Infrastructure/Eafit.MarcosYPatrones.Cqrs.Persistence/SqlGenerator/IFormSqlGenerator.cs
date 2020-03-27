namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface IFormSqlGenerator : ISqlGenerator
    {
        string BuildGetByNameSqlQuery(string tableName);
    }
}
