namespace Relief.DTOs
{
    public class MakePayment
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public MakePaymentData Data { get; set; }
    }


    public class MakePaymentData
    {
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
