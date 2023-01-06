namespace Relief.DTOs
{
    public class BankVerification
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BankVerificationData Data { get; set; }
    }

    public class BankVerificationData
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankCode { get; set; }
        public int BankId { get; set; }
        public string RecipientCode { get; set; }
        public string Reference { get; set; }
        public int Integration { get; set; }
        public string Domain { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Source { get; set; }
        public string Reason { get; set; }
        public int Recipient { get; set; }
        public string Success { get; set; }
        public string TransferCode { get; set; }
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

    }
}
