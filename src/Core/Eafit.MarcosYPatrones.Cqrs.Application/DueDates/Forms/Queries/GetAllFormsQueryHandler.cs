using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms.Queries
{
    public class GetAllFormsQueryHandler : IQueryHandler<GetAllFormsQuery, IEnumerable<Form>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFormsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Form>> HandleAsync(GetAllFormsQuery query)
        {
            return await _unitOfWork.Forms.GetAllAsync().ConfigureAwait(false);
        }
    }
}
