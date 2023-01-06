namespace Relief.DTOs.ResponseModel
{
    public class CommentResponseModel : BaseResponse
    {
        public CommentDTO Data { get; set; }
    }

    public class CommentsResponseModel : BaseResponse
    {
        public ICollection<CommentDTO> Data { get; set; } = new HashSet<CommentDTO>();
    }
}
