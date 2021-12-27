using Api.Infrastructure;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using SeedWork;

namespace Api.Controllers
{
    public class GetData : IRequest<Result<bool, CommandErrorResponse>>
    {
        public string Name { get; set; }
    }
    public class GetDataHandler : IRequestHandler<GetData, Result<bool, CommandErrorResponse>>
    {
        private readonly ILogger<GetData> _logger;
        private readonly ConnectionStringOptions _opt;

        public GetDataHandler(ILogger<GetData> logger, IOptions<ConnectionStringOptions> opt)
        {
            _logger = logger;
            _opt = opt.Value;

        }

        public async Task<Result<bool,CommandErrorResponse>> Handle(GetData query, CancellationToken cancellationToken)
        {
            try
            {
                var name = query.Name;
                var options = _opt;

                return ResultCustom.Success(true);
            }catch (Exception ex)
            {
                return ResultCustom.Error<bool>(ex);
            }
        }
    }
}