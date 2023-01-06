using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<BaseResponse> RegisterDocument(string model);
    }
}
