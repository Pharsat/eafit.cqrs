using System.ComponentModel.DataAnnotations;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Projects.Queries
{
    public class GetProjectsByTeamIdQuery : IQuery
    {
        [Required]
        public int TeamId { get; set; }
    }
}
