using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface ICreateUserService
    {
        Task<BaseResponse> CreateAdmin(AddAdminRequestModel model);
        Task<BaseResponse> CreateDonor(CreateUserRequestModel model);
        Task<BaseResponse> CreateNgo(CreateUserRequestModel model);
        Task<CreateUserResponseModel> GetUser(string email);
        Task<BaseResponse> VerifyUser(VerifyUserRequestModel model);
    }
}
