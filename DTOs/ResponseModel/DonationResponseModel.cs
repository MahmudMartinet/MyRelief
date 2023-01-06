namespace Relief.DTOs.ResponseModel
{
    public class DonationResponseModel : BaseResponse
    {
        public DonationDTO Data { get; set; }
    }

    public class DonationsResponseModel : BaseResponse
    {
        public ICollection<DonationDTO> Data { get; set; } = new HashSet<DonationDTO>();
    }
}
