using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregate.SmsMessage
{
    public class SmsMessage
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Otp { get; set; }
        public string BankType { get; set; }
        public int Amount { get; set; }

        public SmsMessage(string title = "", string otp = "", string bankType = "", int amount = 0)
        {
            Title = title;
            Otp = otp;
            BankType = bankType;
            CreatedDateTime = DateTime.UtcNow;
            Amount = amount;
        }
        public static SmsMessage CreateNew(string title, string otp, string bankType, int amount) =>
            new(title, otp, bankType, amount);
    }
}
