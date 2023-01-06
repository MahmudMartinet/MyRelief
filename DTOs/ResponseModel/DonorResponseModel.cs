namespace Relief.DTOs.ResponseModel
{
    public class DonorResponseModel : BaseResponse
    {
        public DonorDTO Data { get; set; }
    }

    public class DonorsResponseModel : BaseResponse
    {
        public ICollection<DonorDTO> Data { get; set; } = new HashSet<DonorDTO>();
    }
}
