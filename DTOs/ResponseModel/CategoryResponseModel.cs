namespace Relief.DTOs.ResponseModel
{
    public class CategoryResponseModel : BaseResponse
    {
        public CategoryDTO Data { get; set; }
    }
    public class CategoriesResponseModel : BaseResponse
    {
        public ICollection<CategoryDTO> Data { get; set; } = new HashSet<CategoryDTO>();

    }
}
