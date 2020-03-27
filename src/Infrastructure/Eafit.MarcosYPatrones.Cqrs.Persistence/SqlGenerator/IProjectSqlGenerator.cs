using System;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public interface IProjectSqlGenerator : ISqlGenerator
    {
        string BuildGetProjectsByTeamIdSqlQuery(Type type);
    }
}
