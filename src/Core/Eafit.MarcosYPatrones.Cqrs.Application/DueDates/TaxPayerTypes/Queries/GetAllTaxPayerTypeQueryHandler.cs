using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes.Queries
{
    public class GetAllTaxPayerTypeQueryHandler : IQueryHandler<GetAllTaxPayerTypeQuery, IEnumerable<TaxPayerType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTaxPayerTypeQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<TaxPayerType>> HandleAsync(GetAllTaxPayerTypeQuery query)
        {
            return await _unitOfWork.TaxPayerTypes.GetAllAsync().ConfigureAwait(false);
        }
    }
}
