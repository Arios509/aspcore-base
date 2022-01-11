using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregate.SmsMessage
{
    public class SmsMessage
    {
        [Key]
        public int Id { get; set; }
        public string OriginalMessage { get; set; }
        public string BankType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string OtpNumber { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SmsMessageId { get; set; }
        public DateTime ReceivedDateTime { get; set; }

        public SmsMessage(string sender, string receiver,
            string smsMessageId, string originalMessage, DateTime receivedDateTime,
            string otpNumber = "", string bankType = "")
        {
            Sender = sender;
            Receiver = receiver;
            SmsMessageId = smsMessageId;
            OriginalMessage = originalMessage;
            ReceivedDateTime = receivedDateTime;
            OtpNumber = otpNumber;
            BankType = bankType;
            CreatedDateTime = DateTime.UtcNow;
        }

        public static SmsMessage CreateNew(string sender, string receiver,
            string smsMessageId, string originalMessage,
            DateTime receivedDateTime)
        {
            var splitedOriginalMessage = originalMessage.Split(" ");

            var otpNumber = GetOtpNumber(splitedOriginalMessage: splitedOriginalMessage);
            var bankType = GetBankType(splitedOriginalMessage: splitedOriginalMessage);

            return new(sender, receiver, smsMessageId, originalMessage,
                                receivedDateTime, otpNumber, bankType);
        }

        public static string GetOtpNumber(string[] splitedOriginalMessage, string bank = "") =>
            bank switch
            {
                _ => splitedOriginalMessage[0].Split("OTP=")[1],
            };

        public static string GetBankType(string[] splitedOriginalMessage, string bank = "") =>
           bank switch
           {
               _ => splitedOriginalMessage[1].Split("REF.")[1]
           };
    }
}
