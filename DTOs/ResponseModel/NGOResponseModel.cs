namespace Relief.DTOs.ResponseModel
{
    public class NGOResponseModel : BaseResponse
    {
        public NgoDTO Data { get; set; }
    }

    public class NGOsResponseModel : BaseResponse
    {
        public IList<NgoDTO> Data { get; set; } = new List<NgoDTO>();
    }
}



