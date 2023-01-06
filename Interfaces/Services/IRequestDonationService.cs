using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IRequestDonationService
    {
        Task<BaseResponse> CreateRequest(CreateRequestDonationRequestModel model, int ngoId);
        Task<BaseResponse> UpdateRequest(UpdateRequestDonationRequestModel model, int id);
        Task<RequestDonationResponseModel> GetRequest(int id);
        Task<RequestDonationsResponseModel> GetAll();
        Task<int> GetAllCount();
        Task<RequestDonationsResponseModel> GetRequestDonationsByDetail(string detail);
        Task<RequestDonationsResponseModel> GetCompletedRequests();
        Task<int> GetCompletedRequestsCount();
        Task<RequestDonationsResponseModel> GetUncompletedRequests();
        Task<int> GetUncompletedRequestsCount();
        Task<RequestDonationsResponseModel> GetRequestByNgo(int id);
        Task<RequestDonationsResponseModel> GetUncompletedRequestByNgo(int id);
        Task<RequestDonationsResponseModel> GetCompletedRequestByNgo(int id);
        Task<int> GetCompletedByNgoCount(int id);
        Task<int> GetUncompletedByNgoCount(int id);
        Task<int> GetRequestByNgoCount(int id);
    }
}
