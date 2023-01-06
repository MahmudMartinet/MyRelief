using System.ComponentModel.DataAnnotations;

namespace Relief.DTOs.RequestModel
{
    public class ReportRequestModel
    {
       
    }
    public class CreateReportRequestModel
    {
        [Required]
        [StringLength(maximumLength: 3000, MinimumLength = 3)]
        public string Detail { get; set; }
        public IList<string>? DocumentDTOs { get; set; } = new List<string>();
    }
    public class UpdateReportRequestModel
    {
        public string Detail { get; set; }
        public IList<string>? DocumentDTOs { get; set; } = new List<string>();
    }
}
