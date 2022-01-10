using Api.Infrastructure;
using Api.Infrastructure.Repositories;
using CSharpFunctionalExtensions;
using Domain.Aggregate.SmsMessage;
using MediatR;
using Microsoft.Extensions.Options;
using Seedwork;

namespace Api.Controllers
{
    public class GetSmsMessagesQuery : IRequest<Result<SmsMessage[], CommandErrorResponse>>
    {
        public string Name { get; set; }
    }
    public class GetSmsMessagesQueryHandler : IRequestHandler<GetSmsMessagesQuery, Result<SmsMessage[], CommandErrorResponse>>
    {
        private readonly ILogger<GetSmsMessagesQuery> _logger;
        private readonly ConnectionStringOptions _opt;
        private readonly DataContext _context;

        public GetSmsMessagesQueryHandler(
            ILogger<GetSmsMessagesQuery> logger, 
            IOptions<ConnectionStringOptions> opt,
            DataContext context)
        {
            _logger = logger;
            _opt = opt.Value;
            _context = context;
        }

        public async Task<Result<SmsMessage[],CommandErrorResponse>> 
            Handle(GetSmsMessagesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var smsMessages = _context.SmsMessage.ToArray();
                
                return ResultCustom.Success(smsMessages);

            }catch (Exception ex)
            {
                return ResultCustom.Error<SmsMessage[]>(ex);
            }
        }
    }
}