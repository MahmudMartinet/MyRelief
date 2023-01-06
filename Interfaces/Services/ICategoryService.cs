using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse> AddCategory(CreateCategoryRequestModel model);
        Task<BaseResponse> UpdateCategory(UpdateCategoryRequestModel model, int id);
        Task<CategoriesResponseModel> GetAll();
        Task<CategoryResponseModel> GetById(int id);
        Task<CategoriesResponseModel> GetCategoriesByName(string name);
        Task<CategoriesResponseModel> GetAllWithInfo();
    }
}
