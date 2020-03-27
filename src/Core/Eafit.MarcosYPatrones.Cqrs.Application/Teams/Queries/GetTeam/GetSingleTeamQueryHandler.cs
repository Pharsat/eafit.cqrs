using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.GetTeam
{
    public class GetSingleTeamQueryHandler : IQueryHandler<GetSingleTeamQuery, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleTeamQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Team> HandleAsync(GetSingleTeamQuery query)
        {
            return await _unitOfWork.Teams.GetByIdAsync(query.Id).ConfigureAwait(false);
        }
    }
}
