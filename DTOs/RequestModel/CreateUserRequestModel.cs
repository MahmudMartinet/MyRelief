namespace Relief.DTOs.RequestModel
{
    public class CreateUserRequestModel
    {
        public string Email { get; set; }
    }

    public class VerifyUserRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }

    public class AddAdminRequestModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
