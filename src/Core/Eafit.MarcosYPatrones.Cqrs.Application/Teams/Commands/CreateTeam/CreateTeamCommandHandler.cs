using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Application.Teams.Repositories;
using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using System;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Team> HandleAsync(CreateTeamCommand command)
        {
            var team = new Team(command.Name);

            if (await _unitOfWork.Teams.ExistsByNameAsync(command.Name).ConfigureAwait(false))
            {
                throw new EntityAlreadyExistsException("Name already exists in the database.");
            }

            _unitOfWork.Teams.Add(team);

            await _unitOfWork
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return team;
        }
    }
}
