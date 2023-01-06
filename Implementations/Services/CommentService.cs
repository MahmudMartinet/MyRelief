using Relief.DTOs;
using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Services;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly INgoRepository _ngoRepository;
        private readonly IRequestDonationRepository _requestDonationRepository;

        public CommentService(ICommentRepository commentRepository, IDonorRepository donorRepository, INgoRepository ngoRepository, IDocumentRepository documentRepository, IRequestDonationRepository requestDonationRepository)
        {
            _commentRepository = commentRepository;
            _donorRepository = donorRepository;
            _ngoRepository = ngoRepository;
            _documentRepository = documentRepository;
            _requestDonationRepository = requestDonationRepository;
        }

        public async Task<BaseResponse> CreateComment(CreateCommentRequestModel model, int donorId, int ngoId)
        {
            var donor = await _donorRepository.Get(x => x.Id == donorId);
            if (donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            if (model.Detail == null)
            {
                return new BaseResponse
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }
            
            var ngo = await _ngoRepository.Get(x => x.Id == ngoId);
            if (ngo == null)
            {
                return new BaseResponse
                {
                    Message = "NGO not found",
                    Success = false
                };
            }

            var comment = new Comment
            {
                Detail = model.Detail,
                NGO = ngo,
                NGOId = ngoId,
                Donor = donor,
                DonorId = donor.Id,
                CreatedBy = donor.Id,
                LastModifiedBy = donor.Id,
            };
            var comm = await _commentRepository.Register(comment);

            foreach (var img in model.Documents)
            {
                var image = new Document
                {
                    Path = img,
                    NgoId = ngoId,
                    CommentId = comm.Id,
                };
                await _documentRepository.Register(image);
            }
            return new BaseResponse
            {
                Message = "Comment posted successfully",
                Success = true
            };

        }

        public async Task<BaseResponse> CreateRequestComment(CreateCommentRequestModel model, int donorId, int requestId)
        {
            var donor = await _donorRepository.Get(x => x.Id == donorId);
            if (donor == null)
            {
                return new BaseResponse
                {
                    Message = "Donor not found",
                    Success = false
                };
            }

            if (model.Detail == null)
            {
                return new BaseResponse
                {
                    Message = "Field cannot be empty",
                    Success = false
                };
            }

            var request = await _requestDonationRepository.Get(x => x.Id == requestId);
            if (request == null)
            {
                return new BaseResponse
                {
                    Message = "Request not found",
                    Success = false
                };
            }

            var comment = new Comment
            {
                Detail = model.Detail,
                Request = request,
                RequestId = requestId,
                Donor = donor,
                DonorId = donor.Id,
                CreatedBy = donor.Id,
                LastModifiedBy = donor.Id,
            };
            var comm = await _commentRepository.Register(comment);
            foreach (var img in model.Documents)
            {
                var image = new Document
                {
                    Path = img,
                    RequestId = requestId,
                    CommentId = comm.Id,
                };
                await _documentRepository.Register(image);
            }
            return new BaseResponse
            {
                Message = "Comment posted successfully",
                Success = true
            };

        }

        public async Task<CommentsResponseModel> GetAll()
        {
            var list = await _commentRepository.GetAll();
            var comments = list.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Id = x.Id,
                Detail = x.Detail,
                NgoName = x.NGO.Name,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                
            }).ToList();
            if(comments == null)
            {
                return new CommentsResponseModel
                {
                    Message = "No available comment",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = comments.ToHashSet(),
                Message = "List of comments",
                Success = true
            };
        }

        public async Task<CommentResponseModel> GetComment(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
            {
                return new CommentResponseModel
                {
                    Message = "Comment not available",
                    Success = false
                };
            }
            var commentDto = new CommentDTO
            {
                Detail = comment.Detail,
                DonorName = $"{comment.Donor.FirstName} {comment.Donor.LastName}",
                NgoName = comment.NGO.Name,
                Id = comment.Id,
            };
            return new CommentResponseModel
            {
                Data = commentDto,
                Message = "",
                Success = true
            };
        }

        public async Task<CommentsResponseModel> GetByRequestId(int id)
        {
            var comments = await _commentRepository.GetByRequestId(id);
            var result = comments.Select(x => new CommentDTO
            {
                Detail = x.Detail,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorImage = x.Donor.Image,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                DonorId = x.DonorId,
            }).ToHashSet();
            if (result.Count == 0)
            {
                return new CommentsResponseModel
                {
                    Message = "No comment found",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = result,
                Message = "",
                Success = true
            };
        }

        public async Task<CommentsResponseModel> GetCommentByNgoId(int id)
        {
            var comments = await _commentRepository.GetCommentByNgoId(id);
            var result = comments.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Detail = x.Detail,
                NgoName = x.NGO.Name,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                DonorImage = x.Donor.Image,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
            }).ToHashSet();
            if(result.Count == 0)
            {
                return new CommentsResponseModel
                {
                    Message = "No comment found",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = result,
                Message = "",
                Success = true
            };
        }

        public async Task<CommentsResponseModel> GetCommentsByContent(string content)
        {
            var list = await _commentRepository.GetCommentsByContent(content);
            var comments = list.Where(x => x.IsDeleted == false).Select(x => new CommentDTO
            {
                Detail = x.Detail,
                NgoName = x.NGO.Name,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                Id = x.Id,
            }).ToHashSet();
            if (comments.Count == 0)
            {
                return new CommentsResponseModel
                {
                    Message = "No comment found",
                    Success = false
                };
            }
            return new CommentsResponseModel
            {
                Data = comments,
                Message = "",
                Success = true
            };
        }

        public async Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, int id)
        {
            var comment = await _commentRepository.Get(x => x.Id == id);
            if (comment == null)
            {
                return new BaseResponse
                {
                    Message = "Comment not found",
                    Success = false
                };
            }
            comment.Detail = model.Detail;
            comment.LastModifiedOn = DateTime.Now;
            var comm = await _commentRepository.Update(comment);
            if (model.Documents != null)
            {
                foreach (var img in model.Documents)
                {
                    var image = new Document
                    {
                       Path = img,
                       CommentId = comm.Id,
                    };
                    await _documentRepository.Register(image);
                }
            }
            
            return new BaseResponse
            {
                Message = "Comment updated successfully",
                Success = true
            };
        }
    }
}
