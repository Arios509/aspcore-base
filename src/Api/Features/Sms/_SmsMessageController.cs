using Microsoft.AspNetCore.Mvc;
using MediatR;
using Seedwork;
using HybridModelBinding;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class SmsMessageController : Controller
    {

        private readonly IMediator _mediator;

        public SmsMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSmsMessage([FromQuery] GetSmsMessageQuery command) =>
            this.OkOrError(await _mediator.Send(command));

        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> GetSmsMessages([FromQuery] GetSmsMessagesQuery command) =>
            this.OkOrError(await _mediator.Send(command));

        [HttpPost]
        public async Task<IActionResult> AddSmsMessage([FromBody] AddSmsMessageCommand command) =>
            this.OkOrError(await _mediator.Send(command));
    }
}