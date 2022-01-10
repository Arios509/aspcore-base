using Microsoft.AspNetCore.Mvc;
using MediatR;
using SeedWork;
using HybridModelBinding;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {

        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetResponse([FromHybrid] GetData command) =>
            this.OkOrError(await _mediator.Send(command));
        
    }
}