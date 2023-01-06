using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }


        public async Task<BaseResponse> CreateGroup(CreateGroupRequestModel model)
        {
            var check = await _groupRepository.GetByName(model.Name);
            if (check != null )
            {
                return new BaseResponse
                {
                    Message = "A group with this name already existed",
                    Success = false
                };
            }
            var group = new Group
            {
                Name = model.Name,
                Description = model.Description,
            };
            await _groupRepository.Register(group);
            return new BaseResponse
            {
                Success = true,
                Message = "Group created successfully"
            };
        }

        public Task<BaseResponse> DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupsResponseModel> GetAll()
        {
            var groups = await _groupRepository.GetAll();
            if (groups == null)
            {
                return new GroupsResponseModel
                {
                    Message = "No group found",
                    Success = false
                };
            }
            var dto = groups.Where(x => x.IsDeleted == false).Select(x => new GroupDTO
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id
            }).ToList();

            return new GroupsResponseModel
            {
                Data = dto.ToHashSet(),
                Success = true,
                Message = "List of groups"
            };
        }

        public async Task<GroupsResponseModel> GetByContent(string content)
        {
            var groups = await _groupRepository.GetByContent(content);
            if (groups == null || groups.Count == 0)
            {
                return new GroupsResponseModel
                {
                    Message = "No group found",
                    Success = false
                };
            }
            var dto = groups.Where(x => x.IsDeleted == false).Select(x => new GroupDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToHashSet();
            return new GroupsResponseModel
            {
                Data = dto,
                Success = true,
                Message = "List of groups"
            };
        }

        public async Task<GroupResponseModel> GetGroup(int id)
        {
            var group = await _groupRepository.GetGroup(id);
            if (group == null)
            {
                return new GroupResponseModel
                {
                    Message = "Group not found",
                    Success = false
                };
            }
            var dto = new GroupDTO
            {
                Description = group.Description,
                Name = group.Name,
                Id = group.Id
            };
            return new GroupResponseModel
            {
                Data = dto,
                Success = true,
                Message = ""
            };
        }

        public async Task<BaseResponse> UpdateGroup(UpdateGroupRequestModel model, int id)
        {
            var group = await _groupRepository.GetGroup(id);
            if(group == null)
            {
                return new BaseResponse
                {
                    Message = "Group not found",
                    Success = false
                };
            }
            group.Name = model.Name;
            group.Description = model.Description;

            await _groupRepository.Update(group);
            return new BaseResponse
            {
                Success = true,
                Message = "Updated successfully"
            };
        }

        
    }
}
