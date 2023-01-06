using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IDonorService
    {
        Task<BaseResponse> Register(CreateDonorRequestModel model);
        Task<BaseResponse> Update(UpdateDonorRequestModel model, int id);
        Task<DonorsResponseModel> GetAll();
        Task<int> GetAllCount();
        Task<DonorResponseModel> GetDonor(int id);
        Task<DonorsResponseModel> GetByName(string name);
        Task<DonorsResponseModel> GetDeletedDonors();
        Task<DonorsResponseModel> GetBannedDonors();
        Task<int> GetBannedDonorsCount();
        Task<DonorsResponseModel> GetActiveDonors();
        Task<int> GetActiveDonorsCount();
        Task<BaseResponse> DeleteDonor(int donorId);
        Task<BaseResponse> BanDonor(int donorId, int adminId);
        Task<BaseResponse> UnbanDonor(int donorId, int adminId);
    }
}
