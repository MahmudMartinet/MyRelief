namespace Relief.DTOs.ResponseModel
{
    public class AdminResponseModel : BaseResponse
    {
        public AdminDTO Data { get; set; }
    }

    public class AdminsResponseModel : BaseResponse
    {
        public ICollection<AdminDTO> Data { get; set; } = new HashSet<AdminDTO>();
    }
}
