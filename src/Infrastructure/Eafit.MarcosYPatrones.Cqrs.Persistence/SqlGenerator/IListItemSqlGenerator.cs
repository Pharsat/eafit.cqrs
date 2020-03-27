namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface IListItemSqlGenerator
    {
        string BuildGetAllSqlQuery(string tableName);
    }
}
