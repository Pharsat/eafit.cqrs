using Eafit.MarcosYPatrones.Cqrs.Application;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class FormSqlGenerator : BaseSqlGenerator, IFormSqlGenerator
    {
        public FormSqlGenerator(IDataComparer dataComparer) : base(dataComparer) { }

        public string BuildGetByNameSqlQuery(string tableName)
        {
            return $"SELECT [F].[Id], [F].[Name] FROM [{tableName}] AS [F] WHERE [F].[Name] = @Name AND [F].[Id] NOT IN @ExcludedIds";
        }
    }
}
