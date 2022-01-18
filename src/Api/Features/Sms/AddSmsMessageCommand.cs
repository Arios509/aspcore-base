using CSharpFunctionalExtensions;
using Domain.Aggregate.SmsMessage;
using MediatR;
using Seedwork;

namespace Api.Controllers
{
    public class AddSmsMessageCommand : IRequest<Result<bool, CommandErrorResponse>>
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string OriginalMessage { get; set; }
        public string SmsMessageId { get; set; }
        public DateTime ReceivedDateTime { get; set; }
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

        public async Task<Result<bool, CommandErrorResponse>>
            Handle(AddSmsMessageCommand command, CancellationToken cancellationToken)
        {
            try
            {
                //var existedSmsMessage = await _smsMessageRepository.Get(command.SmsMessageId);
                //if (existedSmsMessage != null)
                //    return ResultCustom.Error<bool>("Duplicated sms");

                var smsMessage = SmsMessage.CreateNew(
                    sender: command.Sender,
                    receiver: command.Receiver,
                    smsMessageId: command.SmsMessageId,
                    originalMessage: command.OriginalMessage,
                    receivedDateTime: command.ReceivedDateTime.ToUniversalTime());

                _smsMessageRepository.Add(smsMessage);

                return ResultCustom.Success(true);

            }
            catch (Exception ex)
            {
                return ResultCustom.Error<bool>(ex);
            }
        }
    }
}