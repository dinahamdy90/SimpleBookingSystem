using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.UseCases.Resource;

namespace SimpleBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourceController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResourceModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetResources()
        {
            ResourceQuery resourceQuery = new ResourceQuery();
            var resources = await _mediator.Send(resourceQuery);
            return Ok(resources);
        }
    }
}
