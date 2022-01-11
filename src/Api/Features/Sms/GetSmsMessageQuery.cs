using CSharpFunctionalExtensions;
using Domain.Aggregate.SmsMessage;
using MediatR;
using Seedwork;

namespace Api.Controllers
{
    public class GetSmsMessageQuery : IRequest<Result<SmsMessage, CommandErrorResponse>>
    {
        public string BankType { get; set; }
    }
    public class GetSmsMessageQueryHandler : IRequestHandler<GetSmsMessageQuery, Result<SmsMessage, CommandErrorResponse>>
    {
        private readonly ILogger<GetSmsMessageQuery> _logger;
        private readonly ISmsMessageRepository _smsMessageRepository;

        public GetSmsMessageQueryHandler(
            ILogger<GetSmsMessageQuery> logger,
            ISmsMessageRepository smsMessageRepository)
        {
            _logger = logger;
            _smsMessageRepository = smsMessageRepository;
        }

        public async Task<Result<SmsMessage,CommandErrorResponse>> 
            Handle(GetSmsMessageQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var smsMessage = await _smsMessageRepository.Get(bankType: query.BankType);

                if (smsMessage == null)
                    return ResultCustom.NotFound<SmsMessage>("Sms message not found");
                
                return ResultCustom.Success(smsMessage);

            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultCustom.Error<SmsMessage>(ex.Message);
            }
        }
    }
}