using System.ComponentModel.DataAnnotations;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : ICommand
    {
        [Required]
        public int Id { get; set; }
    }
}
