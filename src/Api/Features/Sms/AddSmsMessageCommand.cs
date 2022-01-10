using CSharpFunctionalExtensions;
using Domain.Aggregate.SmsMessage;
using MediatR;
using Seedwork;

namespace Api.Controllers
{
    public class AddSmsMessageCommand : IRequest<Result<bool, CommandErrorResponse>>
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
    public class AddSmsMessageCommandHandler : IRequestHandler<AddSmsMessageCommand, Result<bool, CommandErrorResponse>>
    {
        private readonly ILogger<AddSmsMessageCommand> _logger;
        private readonly ISmsMessageRepository _smsMessageRepository;

        public AddSmsMessageCommandHandler(
            ILogger<AddSmsMessageCommand> logger, 
            ISmsMessageRepository smsMessageRepository)
        {
            _logger = logger;
            _smsMessageRepository = smsMessageRepository;
        }

        public async Task<Result<bool,CommandErrorResponse>> 
            Handle(AddSmsMessageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var smsMessage = SmsMessage.CreateNew(command.Title, "123456", "GIMP", 1000);

                _smsMessageRepository.Add(smsMessage);

                return ResultCustom.Success(true);

            }catch (Exception ex)
            {
                return ResultCustom.Error<bool>(ex);
            }
        }
    }
}