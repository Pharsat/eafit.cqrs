using Eafit.MarcosYPatrones.Cqrs.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator
{
    public class ProjectSqlGenerator : BaseSqlGenerator, IProjectSqlGenerator
    {
        public ProjectSqlGenerator(IDataComparer dataComparer) : base(dataComparer) { }

        public string BuildGetProjectsByTeamIdSqlQuery(Type type)
        {
            return $"SELECT [P].[Id], [P].[Name] FROM [{type.Name}] AS P INNER JOIN [dbo].[ProjectTeams] AS TP ON TP.ProjectId = P.Id WHERE TP.TeamId = @TeamId;";
        }
    }
}
