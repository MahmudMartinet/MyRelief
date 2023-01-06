using Relief.DTOs.ResponseModel;
using Relief.Entities;
using Relief.Interfaces.Repositories;
using Relief.Interfaces.Services;

namespace Relief.Implementations.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<BaseResponse> RegisterDocument(string model)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Value cannot be null"
                };
            }

            var doc = new Document
            {
                Path = model
            };
            await _documentRepository.Register(doc);

            return new BaseResponse
            {
                Success = true,
                Message = "New Image succussfully added"
            };
        }
    }
}
