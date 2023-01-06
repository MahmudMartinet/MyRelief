using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;
using static Relief.DTOs.RequestModel.NgoRequestModel;

namespace Relief.Interfaces.Services
{
    public interface INgoService
    {
        Task<BaseResponse> Register(CreateNgoRequestModel model);
        Task<BaseResponse> Update(UpdateNgoRequestModel model, int id);
        Task<NGOsResponseModel> GetAll();
        Task<int> GetAllCount();
        Task<NGOsResponseModel> GetBannedNgos();
        Task<NGOResponseModel> GetNgo(int id);
        Task<NGOResponseModel> GetNgoByEmail(string email);
        Task<NGOsResponseModel> GetNgoByName(string name);
        Task<NGOsResponseModel> GetByDescriptionContent(string content);
        Task<NGOsResponseModel> GetAllWithCategory();
        Task<NGOsResponseModel> GetUnapprovedNgos();
        Task<int> GetUnapprovedNgosCount();
        Task<BaseResponse> UploadDocuments(UploadRequestModel model, int ngoId);
        Task<BaseResponse> ApproveNgo(int id);
        Task<BaseResponse> BanNgo(int id);
        Task<BaseResponse> UnbanNgo(int id);
        Task<BaseResponse> DeleteNgo(int id);
        Task<BaseResponse> UpdateBankDetails(AccountDetailsRequestModel model, int id);
        Task<BaseResponse> UpdateAddress(AddressRequestModel model, int id);
    }
}
