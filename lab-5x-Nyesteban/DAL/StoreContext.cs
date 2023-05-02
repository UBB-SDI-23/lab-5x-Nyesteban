using lab_1_Nyesteban.Models;
using lab_5x_Nyesteban.Models;
using Microsoft.EntityFrameworkCore;
namespace lab_1_Nyesteban.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevelopmentDetail>().HasKey(dd => new { dd.CompanyId, dd.AppId });
        }

        public DbSet<App> Apps { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<DevelopmentDetail> DevelopmentDetails { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RegistrationCode> RegistrationCodes { get; set; }

        public Company Company
        {
            get => default;
            set
            {
            }
        }

        public DevelopmentDetail DevelopmentDetail
        {
            get => default;
            set
            {
            }
        }

        public Game Game
        {
            get => default;
            set
            {
            }
        }

        public App App
        {
            get => default;
            set
            {
            }
        }

        public User User
        {
            get => default;
            set
            {
            }
        }

        public RegistrationCode RegistrationCode
        {
            get => default;
            set
            {
            }
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevelopmentDetail>()
                .HasKey(dd => new { dd.CompanyId, dd.AppId });
            modelBuilder.Entity<DevelopmentDetail>()
                .HasOne(dd => dd.Company)
                .WithMany(c => c.DevelopmentDetails)
                .HasForeignKey(dd => dd.CompanyId);
            modelBuilder.Entity<DevelopmentDetail>()
                .HasOne(dd => dd.App)
                .WithMany(a => a.DevelopmentDetails)
                .HasForeignKey(dd => dd.AppId);
        }*/

    }
}
