namespace Relief.DTOs.ResponseModel
{
    public class PaymentResponseModel : BaseResponse
    {
        public PaymentDTO Data { get; set; }
    }

    public class PaymentsResponseModel : BaseResponse
    {
        public IList<PaymentDTO> Data { get; set; } = new List<PaymentDTO>();
    }
}
