using System.ComponentModel.DataAnnotations;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : ICommand
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public CreateTeamCommand()
        {
            Name = string.Empty;
        }

        public CreateTeamCommand(string name)
        {
            Name = name;
        }
    }
}
