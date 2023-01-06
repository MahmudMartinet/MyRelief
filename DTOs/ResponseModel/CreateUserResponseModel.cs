namespace Relief.DTOs.ResponseModel
{
    public class CreateUserResponseModel : BaseResponse
    {
        public CreateUserDTO Data { get; set; }
    }

    public class CreateUsersResponseModel : BaseResponse
    {
        public List<CreateUserDTO> Data { get; set; }
    }
}
