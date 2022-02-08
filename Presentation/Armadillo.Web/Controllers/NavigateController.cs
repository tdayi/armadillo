using Armadillo.Application.Contract.DTO.Command.Navigate;
using Armadillo.Application.Contract.DTO.Query.Position;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Armadillo.Web.Controllers
{
    public class NavigateController : Controller
    {
        private readonly IMediator mediator;

        public NavigateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/navigate/position/change")]
        public async Task<IActionResult> MovementAsync([FromBody] DoNavigateRequest request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("/navigate/position/query")]
        public async Task<IActionResult> GetPositionAsync([FromBody] GetPositionRequest request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }
    }
}
