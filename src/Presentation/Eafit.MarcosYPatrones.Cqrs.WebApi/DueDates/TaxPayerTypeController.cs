using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eafit.MarcosYPatrones.Cqrs.Application;
using Eafit.MarcosYPatrones.Cqrs.Application.DueDates.TaxPayerTypes.Queries;
using Eafit.MarcosYPatrones.Cqrs.Domain.DueDates;
using Microsoft.AspNetCore.Mvc;

namespace Eafit.MarcosYPatrones.Cqrs.WebApi.DueDates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPayerTypeController : ControllerBase
    {
        private readonly IQueryHandler<GetAllTaxPayerTypeQuery, IEnumerable<TaxPayerType>> _getAllTaxPayerTypeQueryHandler;

        public TaxPayerTypeController(IQueryHandler<GetAllTaxPayerTypeQuery, IEnumerable<TaxPayerType>> getAllTaxPayerTypeQueryHandler) =>
            _getAllTaxPayerTypeQueryHandler = getAllTaxPayerTypeQueryHandler;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllQuery = new GetAllTaxPayerTypeQuery();
            return Ok(await _getAllTaxPayerTypeQueryHandler.HandleAsync(getAllQuery).ConfigureAwait(false));
        }
    }
}