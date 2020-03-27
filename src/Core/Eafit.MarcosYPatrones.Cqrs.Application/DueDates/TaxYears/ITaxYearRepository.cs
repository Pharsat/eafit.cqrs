using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears
{
    public interface ITaxYearRepository : IRepository<TaxYear>
    {
        Task<IEnumerable<TaxYear>> GetByYearAsync(int year);
    }
}
