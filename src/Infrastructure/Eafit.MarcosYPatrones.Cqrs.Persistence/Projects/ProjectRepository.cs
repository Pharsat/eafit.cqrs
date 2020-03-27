using Dapper;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
using Eafit.MarcosYPatrones.Cqrs.Domain.Projects;
using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.Projects
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly IDbConnection _connection;
        private readonly IProjectSqlGenerator _sqlGenerator;

        public ProjectRepository(
            IDbConnection connection,
            IDataComparer dataComparer,
            IProjectSqlGenerator sqlGenerator)
            : base(
                 connection,
                 dataComparer,
                 sqlGenerator) => (_connection, _sqlGenerator) = (connection, sqlGenerator);

        public async Task<IEnumerable<Project>> GetProjectsByTeamId(int TeamId)
        {
            string getProjectsByTeamIdQuery = _sqlGenerator.BuildGetProjectsByTeamIdSqlQuery(typeof(Project));
            IEnumerable<Project> projects = await _connection.QueryAsync<Project>(getProjectsByTeamIdQuery, new { TeamId }).ConfigureAwait(false);
            return projects;
        }
    }
}
