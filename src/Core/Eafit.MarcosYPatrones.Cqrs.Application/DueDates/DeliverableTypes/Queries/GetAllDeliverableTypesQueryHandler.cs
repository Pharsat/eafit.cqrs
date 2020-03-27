using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes.Queries
{
    public class GetAllDeliverableTypesQueryHandler : IQueryHandler<GetAllDeliverableTypesQuery, IEnumerable<DeliverableType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDeliverableTypesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<DeliverableType>> HandleAsync(GetAllDeliverableTypesQuery query)
        {
            return await _unitOfWork.DeliverableTypes.GetAllAsync().ConfigureAwait(false);
        }
    }
}
