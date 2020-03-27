using Eafit.MarcosYPatrones.Cqrs.Application;
using System;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class TeamSqlGenerator : BaseSqlGenerator, ITeamSqlGenerator
    {
        public TeamSqlGenerator(IDataComparer dataComparer) : base(dataComparer) { }

        public string BuildExistsByNameSqlCommand(Type type)
        {
            return $"SELECT 1 FROM [{type.Name}] WHERE Name = @Name;";
        }
    }
}
