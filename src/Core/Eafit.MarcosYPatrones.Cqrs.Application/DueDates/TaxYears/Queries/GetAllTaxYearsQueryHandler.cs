using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears.Queries
{
    public class GetAllTaxYearsQueryHandler : IQueryHandler<GetAllTaxYearsQuery, IEnumerable<TaxYear>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTaxYearsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async  Task<IEnumerable<TaxYear>> HandleAsync(GetAllTaxYearsQuery query)
        {
            return await _unitOfWork.TaxYears.GetAllAsync().ConfigureAwait(false);
        }
    }
}
