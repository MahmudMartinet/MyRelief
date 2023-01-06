namespace Relief.DTOs.ResponseModel
{
    public class PaystackResponseModel
    {

    }

    public class PaystackResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PaystackData Data { get; set; }
    }
    public class PaystackData
    {
        public string Authorization_url { get; set; }
        public string Access_code { get; set; }
        public string Reference { get; set; }
    }
}
