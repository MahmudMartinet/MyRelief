namespace Relief.DTOs.ResponseModel
{
    public class RequestDonationResponseModel : BaseResponse
    {
        public RequestDonationDTO Data { get; set; }
    }

    public class RequestDonationsResponseModel : BaseResponse
    {
        public ICollection<RequestDonationDTO> Data { get; set; } = new HashSet<RequestDonationDTO>();
    }
}
