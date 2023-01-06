using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IGroupService
    {
        Task<BaseResponse> CreateGroup(CreateGroupRequestModel model);
        Task<BaseResponse> UpdateGroup(UpdateGroupRequestModel model, int id);
        Task<BaseResponse> DeleteGroup(int id);
        Task<GroupsResponseModel> GetByContent(string content);
        Task<GroupResponseModel> GetGroup(int id);
        Task<GroupsResponseModel> GetAll();
    }
}
