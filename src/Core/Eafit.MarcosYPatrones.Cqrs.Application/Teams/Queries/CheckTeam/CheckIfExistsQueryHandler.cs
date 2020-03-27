using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.CheckTeam
{
    public class CheckIfExistsQueryHandler : IQueryHandler<CheckIfExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIfExistsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> HandleAsync(CheckIfExistsQuery query)
        {
            return await _unitOfWork.Teams.ExistsAsync(query.Id).ConfigureAwait(false);
        }
    }
}