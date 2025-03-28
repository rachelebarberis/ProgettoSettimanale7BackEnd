using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Models;
using Microsoft.AspNetCore.Identity;

namespace ProgettoSettimanale7BackEnd.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Evento> Eventi { get; set; }
        public DbSet<Biglietto> Biglietti { get; set; }
        public DbSet<Artista> Artisti { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Evento>()
           .HasOne(e => e.Artista)
           .WithMany(a => a.Eventi)
           .HasForeignKey(e => e.ArtistaId);

            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.Evento)
                .WithMany(e => e.Biglietti)
                .HasForeignKey(b => b.EventoId);

            modelBuilder.Entity<Biglietto>()
                .HasOne(b => b.ApplicationUser)
                .WithMany(u => u.Biglietti)
                .HasForeignKey(b => b.UserId);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);

            var adminId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminId
                },
                new ApplicationRole
                {
                    Id = userId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userId
                }
            );
        }
    }
}
