using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates
{
    public interface IDueDateRepository : IRepository<DueDate>
    {
        Task<DueDate> GetCompleteByIdAsync(int id);
    }
}
