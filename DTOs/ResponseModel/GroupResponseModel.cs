namespace Relief.DTOs.ResponseModel
{
    public class GroupResponseModel : BaseResponse
    {
        public GroupDTO Data { get; set; }
    }

    public class GroupsResponseModel : BaseResponse
    {
        public ICollection<GroupDTO> Data { get; set; } = new HashSet<GroupDTO>();
    }
}
