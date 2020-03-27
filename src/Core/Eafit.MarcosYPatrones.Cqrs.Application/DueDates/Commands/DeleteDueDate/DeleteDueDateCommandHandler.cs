using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Eafit.MarcosYPatrones.Cqrs.Domain;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.DeleteDueDate
{
    public class DeleteDueDateCommandHandler : IVoidCommandHandler<DeleteDueDateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDueDateCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task HandleAsync(DeleteDueDateCommand command)
        {
            _unitOfWork.DueDates.RemoveById(command.Id);

            var rowsAffected = await _unitOfWork
                .SaveChangesAsync()
                .ConfigureAwait(false);

            if (rowsAffected == 0)
            {
                throw new EntityDoesNotExistsException("Due date requested for deletion does not exist.");
            }
        }
    }
}
