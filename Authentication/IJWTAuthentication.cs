using Relief.DTOs;

namespace Relief.Authentication
{
    public interface IJWTAuthentication
    {
        string GenerateToken(UserResponseModel model);
    }
}
