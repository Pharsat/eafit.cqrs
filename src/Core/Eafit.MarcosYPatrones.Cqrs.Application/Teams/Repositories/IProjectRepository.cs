using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Domain.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByTeamId(int id);
    }
}
