namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class ListItemSqlGenerator : IListItemSqlGenerator
    {
        public string BuildGetAllSqlQuery(string tableName)
        {
            return $"SELECT [F].[Id], [F].[Name] FROM [{tableName}] AS [F] ORDER BY [F].[Name] ASC";
        }
    }
}
