using Relief.Entities;

namespace Relief.Interfaces.Repositories
{
    public interface IAppointmentRepository : IRepository<DonationAppointment>
    {
        Task<IList<DonationAppointment>> GetAll();
        Task<IList<DonationAppointment>> GetByVenue(string venue);
        Task<DonationAppointment> GetById(int id);
        Task<IList<DonationAppointment>> GetByDonorId(int id);
        Task<IList<DonationAppointment>> GetByNgoId(int id);
        Task<IList<DonationAppointment>> GetByRequestId(int id);
        Task<IList<DonationAppointment>> GetUnaccomplished();
    }
}
