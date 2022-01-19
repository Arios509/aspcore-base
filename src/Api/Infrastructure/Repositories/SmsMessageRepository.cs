using Domain.Aggregate.SmsMessage;
using Microsoft.EntityFrameworkCore;
using Seedwork;
using System.Text;

namespace Api.Infrastructure.Repositories
{
    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDapper _dapper;
        public SmsMessageRepository(DataContext dataContext, IDapper dapper)
        {
            _dataContext = dataContext;
            _dapper = dapper;
        }
        public void Add(SmsMessage smsMessage)
        {
            _dataContext.SmsMessage.Add(smsMessage);
            _dataContext.SaveChanges();
        }

        public async Task<SmsMessage> Get(string? smsMessageId = null, string? bankType = null)
        {
            var query = new StringBuilder();

            query.Append("SELECT * FROM \"SmsMessage\"");
            query.Append("WHERE");

            if (!string.IsNullOrEmpty(smsMessageId))
                query = query.Append($"\"SmsMessageId\" = '{smsMessageId}'");

            if (!string.IsNullOrEmpty(bankType))
                query = query.Append($"\"BankType\" = '{bankType}'");

            var result = await _dapper.Get<SmsMessage>(query.ToString(), null, commandType: System.Data.CommandType.Text);
            return result;
        }
    }
}
