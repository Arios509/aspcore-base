namespace Domain.Aggregate.SmsMessage
{
    public interface ISmsMessageRepository
    {
        void Add(SmsMessage smsMessage);
    }
}
