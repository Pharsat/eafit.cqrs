using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Domain.Teams;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTeamCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> HandleAsync(DeleteTeamCommand command)
        {
            _unitOfWork.Teams.Remove(new Team { Id = command.Id });

            var rowsAffected = await _unitOfWork
                .SaveChangesAsync()
                .ConfigureAwait(false);

            if (rowsAffected == 0)
            {
                throw new EntityDoesNotExistsException();
            }

            return true;
        }
    }
}
