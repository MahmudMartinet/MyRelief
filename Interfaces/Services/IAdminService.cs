using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;

namespace Relief.Interfaces.Services
{
    public interface IAdminService
    {
        Task<BaseResponse> RegisterAdmin(CreateAdminRequestModel model);
        Task<BaseResponse> UpdateAdmin(UpdateAdminRequestModel model, int id);
        Task<AdminsResponseModel> GetAll();
        Task<AdminResponseModel> GetById(int id);
        Task<AdminDashBoard> ShowData();
    }
}
