using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<IEnumerable<Form>> GetByNameAsync(string name);
    }
}
