using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface ICommentService
    {
        Task<BaseResponse> CreateComment(CreateCommentRequestModel model, int donorId, int ngoId);
        Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, int id);
        Task<CommentsResponseModel> GetCommentByNgoId(int id);
        Task<CommentsResponseModel> GetCommentsByContent(string content);
        Task<CommentsResponseModel> GetAll();
        Task<CommentResponseModel> GetComment(int id);
        Task<CommentsResponseModel> GetByRequestId(int id);
        Task<BaseResponse> CreateRequestComment(CreateCommentRequestModel model, int donorId, int requestId);
    }
}
