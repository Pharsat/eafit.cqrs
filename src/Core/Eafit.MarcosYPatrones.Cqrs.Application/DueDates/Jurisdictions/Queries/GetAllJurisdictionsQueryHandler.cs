using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions.Queries
{
    public class GetAllJurisdictionsQueryHandler : IQueryHandler<GetAllJurisdictionsQuery, IEnumerable<Jurisdiction>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllJurisdictionsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Jurisdiction>> HandleAsync(GetAllJurisdictionsQuery query)
        {
            return await _unitOfWork.Jurisdictions.GetAllAsync().ConfigureAwait(false);
        }
    }
}
