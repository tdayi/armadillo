using Armadillo.Application.Contract.DTO.Command.Discovery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Armadillo.Web.Controllers
{
    public class DiscoveryController : Controller
    {
        private readonly IMediator mediator;

        public DiscoveryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/discovery/start")]
        public async Task<IActionResult> StartAsync([FromBody] CreateDiscoveryRequest request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }
    }
}
