namespace Domain.Aggregate.SmsMessage
{
    public interface ISmsMessageRepository
    {
        void Add(SmsMessage smsMessage);
        Task<SmsMessage> Get(string? smsMessageId = null, string? bankType = null);
        Task<SmsMessage> Get();
    }
}
