using System.ComponentModel.DataAnnotations;

namespace Relief.DTOs.RequestModel
{
    public class GroupRequestModel
    {
        
    }

    public class CreateGroupRequestModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 3000, MinimumLength = 3)]
        public string Description { get; set; }
    }

    public class UpdateGroupRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
