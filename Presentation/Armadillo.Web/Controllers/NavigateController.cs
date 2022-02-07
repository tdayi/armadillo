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

        [HttpPost("/navigate/command/do")]
        public async Task<IActionResult> DoNavigatedAsync([FromBody] DoNavigateRequest request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("/navigate/query/position")]
        public async Task<IActionResult> DoNavigatedAsync([FromBody] GetPositionRequest request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }
    }
}
