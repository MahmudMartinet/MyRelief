using Relief.DTOs.ResponseModel;

namespace Relief.DTOs
{
    public class UserDTO
    {
        
    }
    public class UserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserResponseModel : BaseResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public ICollection<RoleDTO> Roles { get; set; } = new HashSet<RoleDTO>();
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
    public class LoginResponse
    {
        public UserResponseModel Data { get; set; }
        public string Token { get; set; }
    }


   
}

