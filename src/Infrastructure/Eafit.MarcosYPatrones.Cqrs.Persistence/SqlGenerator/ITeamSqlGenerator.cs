using System;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface ITeamSqlGenerator : ISqlGenerator
    {
        string BuildExistsByNameSqlCommand(Type type);
    }
}