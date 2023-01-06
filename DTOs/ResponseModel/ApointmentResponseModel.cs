namespace Relief.DTOs.ResponseModel
{
    public class AppointmentResponseModel : BaseResponse
    {
        public AppointmentDTO Data { get; set; }
    }

    public class AppointmentsResponseModel : BaseResponse
    {
        public ICollection<AppointmentDTO> Data { get; set; } = new HashSet<AppointmentDTO>();
    }
}
