using FSACalculation.Entities;
using Microsoft.EntityFrameworkCore;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FSACalculation.DBContext
{
    public class FSAInfoContext : IdentityDbContext<UserLogin>
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Claims> Claims { get; set; } = null!;

        public FSAInfoContext(DbContextOptions<FSAInfoContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<UserLogin>()
                .HasData(
                new UserLogin
                {
                    FirstName = "John",
                    LastName = "Doe",
                    isAdmin = 0,
                    UserName = "johnDoe",
                    NormalizedUserName = "JOHNDOE",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd!"),
                    empId = 1
                },
                new UserLogin
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    isAdmin = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd!"),
                    empId = 2
                }
            );;

            modelBuilder.Entity<Employee>()
                .HasData(
                new Employee()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    CoverageYear = "2023",
                    FSAAmount = 5000
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Admin",
                    LastName = "Admin",
                    CoverageYear = "2023",
                    FSAAmount = 0
                });

            modelBuilder.Entity<Claims>()
                .HasData(
                new Claims()
                {
                    EmployeeId = 1,
                    ID = 1,
                    DateSubmitted = DateTime.UtcNow,
                    Status = 0,
                    ReceiptDate = Convert.ToDateTime("08/25/2023"),
                    ReceiptNo = "41680764",
                    ReceiptAmount = 1000,
                    ClaimAmount = 1000,
                    TotalClaimAmount = 1000,
                    ReferenceNo = "123456"
                });

        }

    }
}
