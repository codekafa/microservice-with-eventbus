using CatalogService.Api.Core.Filters;
using Domain.CQRS.Catalog.Queries.Request;
using Domain.CQRS.Catalog.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatalogService.Api.Controllers
{
    [AuthFilter]
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {

        private readonly ILogger<CatalogController> _logger;
        private readonly IMediator _mediator;

        public CatalogController(ILogger<CatalogController> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getbrands")]
        public Task<GetBrandListResponse> GetBrands()
        {
            return _mediator.Send(new GetBrandListRequest());
        }
    }
}
