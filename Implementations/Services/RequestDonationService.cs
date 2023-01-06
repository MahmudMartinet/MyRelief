using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Services
{
    public class RequestDonationService : IRequestDonationService
    {
        private readonly IRequestDonationRepository _requestDonationRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly INgoRepository _ngoRepository;
        private readonly IRequestTypeRepository _requestTypeRepository;
        private readonly IDocumentRepository _documentRepository;

        public RequestDonationService(IRequestDonationRepository requestDonationRepository, IGroupRepository groupRepository, INgoRepository ngoRepository, IRequestTypeRepository requestTypeRepository, IDocumentRepository documentRepository)
        {
            _requestDonationRepository = requestDonationRepository;
            _groupRepository = groupRepository;
            _ngoRepository = ngoRepository;
            _requestTypeRepository = requestTypeRepository;
            _documentRepository = documentRepository;
        }

        public async Task<BaseResponse> CreateRequest(CreateRequestDonationRequestModel model, int ngoId)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Information cannot be empty!",
                    Success = false
                };
            }
            var ngo = await _ngoRepository.GetNgo(ngoId);
            if(ngo.IsApproved == false)
            {
                return new BaseResponse
                {
                    Message = "Your account needs to be verified to access this function",
                    Success = false,
                };
            }
            if(ngo.IsBan == true)
            {
                return new BaseResponse
                {
                    Message = "Your account is cureently under restriction",
                    Success = false,
                };
            }
            var type = await _requestTypeRepository.GetRequestTypeByName(model.Type);
            var group = await _groupRepository.GetByName(model.Group);
            if (group == null)
            {
                return new BaseResponse
                {
                    Message = "Group not found",
                    Success = false
                };
            }



            var request = new RequestDonation
            {
                RequestType = type,
                Details = model.Details,
                DeadLine = model.DeadLine,
                RequestInMoney = model.RequestInMoney,
                RequestQuantity = model.RequestQuantity,
                NgoId = ngo.Id,
                GroupId = group.Id,
                NGO = ngo,
                Group = group,
                StartDate = model.StartDate
            };
            request.CreatedBy = request.Id;
            
            var addReq = await _requestDonationRepository.Register(request);

            foreach (var img in model.Documents)
            {
                var ig = new Document
                {
                    Path = img,
                    RequestId = addReq.Id
                };
                await _documentRepository.Register(ig);

            }
            return new BaseResponse
            {
                Message = "Donation request made successfully",
                Success = true
            };
        }

        public async Task<RequestDonationsResponseModel> GetAll()
        {
            var requests = await _requestDonationRepository.GetAll();
            if (requests == null)
            {
                return new RequestDonationsResponseModel
                {
                    Message = "No request found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithRequest();
            var dtos = requests.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                RequestTypeId = x.RequestTypeId,
                Type = x.RequestType.Name,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                Progress = x.Progress,
                CreatedOn = x.CreatedOn,
                DeadLine = x.DeadLine,
                
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoLogo = x.NGO.Image,
                NgoEmail = x.NGO.Email,
                RequestQuantity = x.RequestQuantity,
                RequestInMoney = x.RequestInMoney,
                
            }) .ToList();

            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dtos,
                Message = "List of requests"
            };
        }

        public async Task<int> GetAllCount()
        {
            var request = await GetAll();
            var count = request.Data.Count;
            return count;
        }


        public async Task<RequestDonationsResponseModel> GetCompletedRequests()
        {
            var requests = await _requestDonationRepository.GetCompletedRequests();
            if (requests == null)
            {
                return new RequestDonationsResponseModel
                {
                    Message = "No request found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = requests.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoLogo = x.NGO.Image,
                NgoEmail = x.NGO.Email,
                RequestInMoney = x.RequestInMoney,
                RequestQuantity = x.RequestQuantity,
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List of completed requests"
            };
        }

        public async Task<int> GetCompletedRequestsCount()
        {
            var request = await GetCompletedRequests();
            var count = request.Data.Count;
            return count;
        }

        public async Task<RequestDonationResponseModel> GetRequest(int id)
        {
            var request = await _requestDonationRepository.GetRequest(id);
            if(request == null)
            {
                return new RequestDonationResponseModel
                {
                    Message = "No request found",
                    Success = false
                };
            }
            var particu = await _documentRepository.GetDocumentsByRequestId(request.Id);
            var dto = new RequestDonationDTO
            {
                Detail = request.Details,
                Id = request.Id,
                GroupName = request.Group.Name,
                IsCompleted = request.IsCompleted,
                Type = request.RequestType.Name,
                Progress = request.Progress,
                DeadLine = request.DeadLine,
                NgoName = request.NGO.Name,
                NgoId = request.NgoId,
                NgoLogo = request.NGO.Image,
                NgoEmail = request.NGO.Email,
                Particulars = particu,
                RequestQuantity = request.RequestQuantity,
                RequestInMoney = request.RequestInMoney,
            };
            return new RequestDonationResponseModel
            {
                Success = true,
                Message = "",
                Data = dto
            };
        }

        public async Task<RequestDonationsResponseModel> GetRequestByNgo(int id)
        {
            var all = await _requestDonationRepository.GetAll();
            var byNgo = all.Where(x => x.NgoId == id);
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = byNgo.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoEmail = x.NGO.Email,
                NgoId = x.NgoId,
                NgoLogo = x.NGO.Image,
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List of requests"
            };
        }

        public async Task<RequestDonationsResponseModel> GetCompletedRequestByNgo(int id)
        {
            var all = await _requestDonationRepository.GetAll();
            var byNgo = all.Where(x => x.NgoId == id && x.IsCompleted == true);
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = byNgo.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoEmail = x.NGO.Email,
                NgoLogo = x.NGO.Image,
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List of requests"
            };
        }

        public async Task<RequestDonationsResponseModel> GetUncompletedRequestByNgo(int id)
        {
            var all = await _requestDonationRepository.GetAll();
            var byNgo = all.Where(x => x.NgoId == id && x.IsCompleted == false);
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = byNgo.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).Select(p => new DocumentDTO
                {
                    Name = p.Name,
                }).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoEmail = x.NGO.Email,
                NgoLogo = x.NGO.Image,
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List of requests"
            };
        }
        public async Task<int> GetCompletedByNgoCount(int id)
        {
            var req = await GetCompletedRequestByNgo(id);
            var count = req.Data.Count;
            return count;
        }

        public async Task<int> GetUncompletedByNgoCount(int id)
        {
            var req = await GetUncompletedRequestByNgo(id);
            var count = req.Data.Count;
            return count;
        }

        public async Task<int> GetRequestByNgoCount(int id)
        {
            var req = await GetRequestByNgo(id);
            var count = req.Data.Count;
            return count;
        }

        public async Task<RequestDonationsResponseModel> GetRequestDonationsByDetail(string detail)
        {
            var requests = await _requestDonationRepository.GetRequestDonationsByDetail(detail);
            if(requests == null)
            {
                return new RequestDonationsResponseModel
                {
                    Message = "No request found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = requests.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoLogo = x.NGO.Image,
                NgoEmail = x.NGO.Email,
                RequestInMoney = x.RequestInMoney,
                RequestQuantity = x.RequestQuantity,
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List of requests"
            };
        }

        public async Task<RequestDonationsResponseModel> GetUncompletedRequests()
        {
            var requests = await _requestDonationRepository.GetUncompletedRequests();
            if (requests == null)
            {
                return new RequestDonationsResponseModel
                {
                    Message = "No Uncompleted Request found",
                    Success = false
                };
            }
            var doc = await _documentRepository.GetAllWithRequest();
            var dto = requests.Select(x => new RequestDonationDTO
            {
                Detail = x.Details,
                Id = x.Id,
                GroupName = x.Group.Name,
                GroupId = x.GroupId,
                IsCompleted = x.IsCompleted,
                Type = x.RequestType.Name,
                Progress = x.Progress,
                Particulars = doc.Where(a => a.RequestId == x.Id).ToList(),
                DeadLine = x.DeadLine,
                NgoName = x.NGO.Name,
                NgoId = x.NgoId,
                NgoLogo = x.NGO.Image,
                NgoEmail = x.NGO.Email,
                RequestInMoney = x.RequestInMoney,
                RequestQuantity = x.RequestQuantity,
                CreatedOn = x.StartDate,
                
            }).ToList();
            return new RequestDonationsResponseModel
            {
                Success = true,
                Data = dto,
                Message = "List Of Uncompleted Requests"
            };
        }

        public async Task<int> GetUncompletedRequestsCount()
        {
            var request = await GetUncompletedRequests();
            var count = request.Data.Count;
            return count;
        }


        public async Task<BaseResponse> UpdateRequest(UpdateRequestDonationRequestModel model, int id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Information cannot be empty",
                    Success = false
                };
            }
            var type = await _requestTypeRepository.GetRequestTypeByName(model.Type);
            var update = await _requestDonationRepository.Get(x => x.Id == id);
            if(update == null)
            {
                return new BaseResponse
                {
                    Message = "This request is not found",
                    Success = false
                };
            }
            update.Details = model.Details ?? update.Details;
            update.RequestType = type ?? update.RequestType;

            if (model.Documents != null)
            {
                var images = await _documentRepository.GetDocumentsByRequestId( update.Id);
                //if (images != null)
                //{
                //    foreach (var item in images)
                //    {
                //        await _documentRepository.Delete(item);
                //    }
                //}

                foreach (var img in model.Documents)
                {
                    var ig = new Document
                    {
                        Path = img,
                        RequestId = update.Id
                    };
                    await _documentRepository.Register(ig);
                }

            }

            await _requestDonationRepository.Update(update);
            return new BaseResponse
            {
                Message = "Donation request updated successfully",
                Success = true
            };
        }
    }
}
