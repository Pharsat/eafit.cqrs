using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.DeleteDueDate;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Commands.UpdateDueDate;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Queries.GetDueDate;
using Eafit.MarcosYPatrones.Cqrs.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DueDatesController : ControllerBase
    {
        private readonly IVoidCommandHandler<DeleteDueDateCommand> _deleteDueDateCommandHandler;
        private readonly IVoidCommandHandler<UpdateDueDateCommand> _updateDueDateCommandHandler;
        private readonly IQueryHandler<GetSingleDueDateQuery, DueDateViewModel> _getSingleDueDateQueryHandler;

        public DueDatesController(
            IVoidCommandHandler<DeleteDueDateCommand> deleteDueDateCommandHandler,
            IVoidCommandHandler<UpdateDueDateCommand> updateDueDateCommandHandler,
            IQueryHandler<GetSingleDueDateQuery, DueDateViewModel> getSingleDueDateQueryHandler) =>
            (_deleteDueDateCommandHandler, _updateDueDateCommandHandler, _getSingleDueDateQueryHandler) =
            (deleteDueDateCommandHandler, updateDueDateCommandHandler, getSingleDueDateQueryHandler);

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetSingleDueDateQuery getSingleDueDateQuery)
        {
            try
            {
                return Ok(await _getSingleDueDateQueryHandler.HandleAsync(getSingleDueDateQuery).ConfigureAwait(false));
            }
            catch (EntityDoesNotExistsException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteDueDateCommand deleteDueDateCommand)
        {
            try
            {
                await _deleteDueDateCommandHandler
                    .HandleAsync(deleteDueDateCommand)
                    .ConfigureAwait(false);
                return NoContent();
            }
            catch (EntityDoesNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDueDateCommand updateDueDateCommand)
        {
            try
            {
                await _updateDueDateCommandHandler
                    .HandleAsync(updateDueDateCommand)
                    .ConfigureAwait(false);
                return NoContent();
            }
            catch (MultipleResultsForRequestedSingleEntityException ex)
            {
                return Conflict(ex.Message);
            }
            catch (EntityDoesNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}