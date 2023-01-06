using Relief.Entities;

namespace Relief.DTOs.RequestModel
{
    public class AppointmentRequestModel
    {
       
    }
    public class CreateAppointmentRequestModel
    {
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        
    }

    public class UpdateAppointmentRequestModel
    {
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public bool IsAccomplished { get; set; }
    }
}
