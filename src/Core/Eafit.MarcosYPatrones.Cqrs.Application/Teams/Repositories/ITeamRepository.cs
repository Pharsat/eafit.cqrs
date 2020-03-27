using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
