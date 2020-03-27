using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Queries.CheckTeam
{
    public class CheckIfExitsByNameQueryHandler : IQueryHandler<CheckIfExitsByNameQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIfExitsByNameQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> HandleAsync(CheckIfExitsByNameQuery query)
        {
            return await _unitOfWork.Teams.ExistsByNameAsync(query.Name).ConfigureAwait(false);
        }
    }
}
