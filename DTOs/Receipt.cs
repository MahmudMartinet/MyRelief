namespace Relief.DTOs
{
    public class Receipt
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ReceiptData Data { get; set; }
    }

    public class ReceiptData
    {
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Currency { get; set; }
        public string Domain { get; set; }
        public int Id { get; set; }
        public int Integration { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Reason { get; set; }
        public string RecipientCode { get; set; }
        public string Type { get; set; }
        public BankVerificationData Details { get; set; }
    }
}
