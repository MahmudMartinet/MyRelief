using Microsoft.EntityFrameworkCore;
using Relief.Entities;
using Relief.Identity;

namespace Relief.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<CreateUser> CreateUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Donor> Donors { get; set; }    
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<NGO> NGOs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<RequestDonation> RequestDonations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<DonationAppointment> DonationAppointments { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<DonationPayment> DonationPayments { get; set; }
        public DbSet<RegistrationPayment> RegistrationPayments { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
