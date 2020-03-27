using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Queries.GetDueDate
{
    public class GetSingleDueDateQueryHandler : IQueryHandler<GetSingleDueDateQuery, DueDateViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleDueDateQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<DueDateViewModel> HandleAsync(GetSingleDueDateQuery query)
        {
            var dueDate = await _unitOfWork.DueDates.GetCompleteByIdAsync(query.Id).ConfigureAwait(false);
            return new DueDateViewModel( dueDate.Id,
                                        dueDate.TaxPayerType.Name,
                                        dueDate.DeliverableType.Name,
                                        dueDate.Jurisdiction.Name,
                                        dueDate.Form.Name,
                                        dueDate.TaxYear.Year,
                                        dueDate.StatutoryDueDate,
                                        dueDate.ExtensionDate,
                                        dueDate.Quarter?.Name,
                                        dueDate.IsManuallyAdded );
        }
    }
}
