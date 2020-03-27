using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.Jurisdictions.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        private readonly IQueryHandler<GetAllJurisdictionsQuery, IEnumerable<Jurisdiction>> _getAllJurisdictionsQueryHandler;

        public JurisdictionController(IQueryHandler<GetAllJurisdictionsQuery, IEnumerable<Jurisdiction>> getAllJurisdictionsQueryHandler) =>
            _getAllJurisdictionsQueryHandler = getAllJurisdictionsQueryHandler;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllJurisdictionsQuery = new GetAllJurisdictionsQuery();
            return Ok(await _getAllJurisdictionsQueryHandler.HandleAsync(getAllJurisdictionsQuery).ConfigureAwait(false));
        }
    }
}