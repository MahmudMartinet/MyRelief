using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Entities;
using Relief.Interfaces.Repositories;

namespace Relief.Implementations.Repositories
{
    public class AppointmentRepository : BaseRepository<DonationAppointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IList<DonationAppointment>> GetAll()
        {
            var appointments = await _context.DonationAppointments.Include(x => x.Donor).Include(x => x.NGO).Include(x => x.RequestDonation).Where(x => x.IsDeleted == false).ToListAsync();
            return appointments;
        }

        public async Task<IList<DonationAppointment>> GetByDonorId(int id)
        {
            var appointments = await _context.DonationAppointments.Include(x => x.NGO).Include(x => x.RequestDonation).Where(x => x.DonorId == id).ToListAsync();
            return appointments;
        }

        public async Task<DonationAppointment> GetById(int id)
        {
            var appointment = await _context.DonationAppointments.Include(x => x.Donor).Include(x => x.NGO).Include(x => x.RequestDonation).SingleOrDefaultAsync(x => x.Id == id);
            return appointment;
        }

        public async Task<IList<DonationAppointment>> GetByNgoId(int id)
        {
            return  await _context.DonationAppointments.Include(x => x.Donor).Include(x => x.RequestDonation).Where(x => x.NgoId == id).ToListAsync();
            
        }

        public async Task<IList<DonationAppointment>> GetByRequestId(int id)
        {
            return await _context.DonationAppointments.Include(x => x.Donor).Include(x => x.NGO).Where(x => x.RequestId == id).ToListAsync();

        }

        public async Task<IList<DonationAppointment>> GetByVenue(string venue)
        {
            var appointments = await _context.DonationAppointments.Include(x => x.Donor).Include(x => x.NGO).Include(x => x.RequestDonation).Where(x => x.Venue.ToLower().Contains(venue.ToLower())).ToListAsync();
            return appointments;
        }

        public async Task<IList<DonationAppointment>> GetUnaccomplished()
        {
            var appointment = await _context.DonationAppointments.Where(x => x.IsAccomplished == false).Include(x => x.Donor).Include(x => x.NGO).Include(x => x.RequestDonation).ToListAsync();
            return appointment;
        }
    }
}
