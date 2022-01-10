using Domain.Aggregate.SmsMessage;

namespace Api.Infrastructure.Repositories
{
    public class SmsMessageRepository : ISmsMessageRepository
    {
        private readonly DataContext _dataContext;
        public SmsMessageRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }
        public void Add(SmsMessage smsMessage)
        {
            _dataContext.SmsMessage.Add(smsMessage);
            _dataContext.SaveChanges();
        }
    }
}
