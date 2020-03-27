using System.ComponentModel.DataAnnotations;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Queries.GetDueDate
{
    public class GetSingleDueDateQuery : IQuery
    {
        [Required]
        public int Id { get; set; }
    }
}
