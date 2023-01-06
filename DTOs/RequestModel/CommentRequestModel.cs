using System.ComponentModel.DataAnnotations;

namespace Relief.DTOs.RequestModel
{
    public class CommentRequestModel
    {
        
    }
    public class CreateCommentRequestModel
    {
        [Required]
        [StringLength(maximumLength: 300, MinimumLength = 5)]
        public string Detail { get; set; }
        public IList<string>? Documents { get; set; } = new List<string>();
       
    }
    public class UpdateCommentRequestModel
    {
        public string Detail { get; set; }
        public IList<string>? Documents { get; set; } = new List<string>();
    }
}
