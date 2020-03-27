using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxYears.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxYearController : ControllerBase
    {
        private readonly IQueryHandler<GetAllTaxYearsQuery, IEnumerable<TaxYear>> _getAllTaxYearsQueryHandler;

        public TaxYearController(IQueryHandler<GetAllTaxYearsQuery, IEnumerable<TaxYear>> getAllTaxYearsQueryHandler) =>
            _getAllTaxYearsQueryHandler = getAllTaxYearsQueryHandler;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllTaxYearQuery = new GetAllTaxYearsQuery();
            return Ok(await _getAllTaxYearsQueryHandler.HandleAsync(getAllTaxYearQuery).ConfigureAwait(false));
        }
    }
}