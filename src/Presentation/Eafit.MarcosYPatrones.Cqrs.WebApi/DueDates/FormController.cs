using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Forms.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IQueryHandler<GetAllFormsQuery, IEnumerable<Form>> _getAllFormsQueryHandler;

        public FormController(IQueryHandler<GetAllFormsQuery, IEnumerable<Form>> getAllFormsQueryHandler) =>
            _getAllFormsQueryHandler = getAllFormsQueryHandler;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllFormsQuery = new GetAllFormsQuery();
            return Ok(await _getAllFormsQueryHandler.HandleAsync(getAllFormsQuery).ConfigureAwait(false));
        }
    }
}