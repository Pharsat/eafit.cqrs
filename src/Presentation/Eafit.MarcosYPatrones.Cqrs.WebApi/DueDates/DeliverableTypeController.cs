using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.DeliverableTypes.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverableTypeController : ControllerBase
    {
        private readonly IQueryHandler<GetAllDeliverableTypesQuery, IEnumerable<DeliverableType>> _getAllQueryHandler;

        public DeliverableTypeController(IQueryHandler<GetAllDeliverableTypesQuery, IEnumerable<DeliverableType>> getAllQuery) =>
            _getAllQueryHandler = getAllQuery;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllQuery = new GetAllDeliverableTypesQuery();
            return Ok(await _getAllQueryHandler.HandleAsync(getAllQuery).ConfigureAwait(false));
        }
    }
}