using Relief.DTOs;

namespace Relief.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(UserRequestModel model);
    }
}
