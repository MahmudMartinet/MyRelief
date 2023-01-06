namespace Relief.DTOs.ResponseModel
{
    public class ReportResponseModel : BaseResponse
    {
        public ReportDTO Data { get; set; }
    }

    public class ReportsResponseModel : BaseResponse
    {
        public ICollection<ReportDTO> Data { get; set; } = new HashSet<ReportDTO>();
    }
}
