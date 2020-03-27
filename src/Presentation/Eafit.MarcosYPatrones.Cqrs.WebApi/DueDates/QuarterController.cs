using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Quarters.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterController : ControllerBase
    {
        private readonly IQueryHandler<GetAllQuartersQuery, IEnumerable<Quarter>> _getAllQuartersQueryHandler;

        public QuarterController(IQueryHandler<GetAllQuartersQuery, IEnumerable<Quarter>> getAllQuartersQueryHandler) =>
            _getAllQuartersQueryHandler = getAllQuartersQueryHandler;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllQuartersQuery = new GetAllQuartersQuery();
            return Ok(await _getAllQuartersQueryHandler.HandleAsync(getAllQuartersQuery).ConfigureAwait(false));
        }
    }
}