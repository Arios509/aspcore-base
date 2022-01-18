using Domain.Aggregate.SmsMessage;
using Microsoft.EntityFrameworkCore;
using Seedwork;

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

        //public async Task<SmsMessage> Get(string? smsMessageId = null, string? bankType = null)
        //{
        //    List<SmsMessage> results = await _dataContext.SmsMessage.ToListAsync();
        //    IEnumerable<SmsMessage> query = results;

        //    if (!string.IsNullOrEmpty(smsMessageId))
        //        query = query.Where(d => d.SmsMessageId == smsMessageId);

        //    if (!string.IsNullOrEmpty(bankType))
        //        query = query.Where(d => d.BankType == bankType);

        //    query = query.Where(d => d.ReceivedDateTime >= DateTime.Now.Date && d.ReceivedDateTime < DateTime.Now);

        //    return query.FirstOrDefault();

        //    //await _dataContext.SmsMessage.FirstOrDefaultAsync(d => d.SmsMessageId == smsMessageId);
        //}

        public async Task<SmsMessage> Get()
        {
            var query = new StringBuilder();
            query.append("SELECT * FROM \"SmsMessage\"");
            var result = await _dapper.Get<SmsMessage>($"SELECT * FROM \"SmsMessage\"", null, commandType: System.Data.CommandType.Text);
            return result;
        }
    }
}
