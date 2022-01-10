using Api.Infrastructure.Repositories;
using CSharpFunctionalExtensions;
using Domain.Aggregate.SmsMessage;
using MediatR;
using Seedwork;

namespace Api.Controllers
{
    public class GetSmsMessageQuery : IRequest<Result<SmsMessage, CommandErrorResponse>>
    {
        public string BankType { get; set; }
        public int Amount { get; set; }
    }
    public class GetSmsMessageQueryHandler : IRequestHandler<GetSmsMessageQuery, Result<SmsMessage, CommandErrorResponse>>
    {
        private readonly ILogger<GetSmsMessageQuery> _logger;
        private readonly DataContext _context;

        public GetSmsMessageQueryHandler(
            ILogger<GetSmsMessageQuery> logger, 
            DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<SmsMessage,CommandErrorResponse>> 
            Handle(GetSmsMessageQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var smsMessage = _context.SmsMessage.Where(d => d.BankType == query.BankType && d.Amount == query.Amount).Single();

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