using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters.Queries
{
    public class GetAllQuartersQueryHandler : IQueryHandler<GetAllQuartersQuery, IEnumerable<Quarter>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllQuartersQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Quarter>> HandleAsync(GetAllQuartersQuery query)
        {
            return await _unitOfWork.Quarters.GetAllAsync().ConfigureAwait(false);
        }
    }
}
