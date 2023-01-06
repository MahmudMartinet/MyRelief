using Relief.DTOs.RequestModel;
using Relief.DTOs.ResponseModel;

namespace Relief.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<BaseResponse> CreateAppointment(CreateAppointmentRequestModel model, int donorId, int requestId);
        Task<BaseResponse> UpdateAppointment(UpdateAppointmentRequestModel model, int id);
        Task<AppointmentResponseModel> GetById(int id);
        Task<AppointmentsResponseModel> GetAll();
        Task<AppointmentsResponseModel> GetByVenue(string venue);
        Task<AppointmentsResponseModel> GetByNgoId(int id);
        Task<AppointmentsResponseModel> GetByDonorId(int id);
        Task<AppointmentsResponseModel> GetByRequestId(int id);
        Task<AppointmentsResponseModel> GetAccomplishedByDonorId(int id);
        Task<AppointmentsResponseModel> GetAccomplishedByNgoId(int id);
        Task<AppointmentsResponseModel> GetUnaccomplishedByNgoId(int id);
        Task<AppointmentsResponseModel> GetUnaccomplishedByDonorId(int id);
        Task<BaseResponse> ApproveAppointment(int id);
        Task<AppointmentsResponseModel> GetUnapprovedByNgoId(int id);
        Task<BaseResponse> MarkAccomplished(int id);
        Task<BaseResponse> CancelAppointment(int id);
    }
}
