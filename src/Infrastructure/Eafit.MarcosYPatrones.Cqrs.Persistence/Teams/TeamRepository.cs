    using Dapper;
    using Eafit.MarcosYPatrones.Cqrs.Application;
    using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
    using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
    using Eafit.MarcosYPatrones.Cqrs.Persistence.SqlGenerator;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Persistence.Teams
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly IDbConnection _connection;
        private readonly ITeamSqlGenerator _sqlGenerator;

        public TeamRepository(
            IDbConnection connection,
            IDataComparer dataComparer,
            ITeamSqlGenerator sqlGenerator)
            : base(
                  connection,
                  dataComparer,
                  sqlGenerator) => (_connection, _sqlGenerator) = (connection, sqlGenerator);

        public async Task<bool> ExistsByNameAsync(string name)
        {
            string existsByNameQuery = _sqlGenerator.BuildExistsByNameSqlCommand(typeof(Team));
            IEnumerable<int> resultSet = await _connection.QueryAsync<int>(existsByNameQuery, new { Name = name }).ConfigureAwait(false);
            return resultSet.Any();
        }
    }
}