using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IReportService
    {
        Task<BaseResponse> MakeReport(CreateReportRequestModel model, int ngoId, int donorId);
        Task<BaseResponse> UpdateReport(UpdateReportRequestModel model, int id);
        Task<ReportResponseModel> GetReport(int id);
        Task<ReportsResponseModel> GetAll();
        Task<ReportsResponseModel> GetReportsByContent(string content);
        Task<ReportsResponseModel> GetByDonorId(int id);
    }
}
