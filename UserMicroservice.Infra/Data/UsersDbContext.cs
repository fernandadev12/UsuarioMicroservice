using Microsoft.EntityFrameworkCore;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.ValueObjects;
using UserMicroservice.Infra.Data.Mapping;

namespace UserMicroservice.Infra.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

            base.OnModelCreating(modelBuilder);

            // Seed usando objetos anônimos. Para tipos owned (Email, Password) use OwnsOne().HasData
            var adminId = Guid.NewGuid();
            var gamerId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new
                {
                    Id = adminId,
                    Username = "admin",
                    Role = "Administrador",
                    LastAccess = DateTime.UtcNow
                },
                new
                {
                    Id = gamerId,
                    Username = "usuarioGamer",
                    Role = "Usuario",
                    LastAccess = DateTime.UtcNow
                }
            );

            // Seed para o Value Object Email
            modelBuilder.Entity<User>().OwnsOne(u => u.Email).HasData(
                new { UserId = adminId, Address = "admin@system.com" },
                new { UserId = gamerId, Address = "gamer@system.com" }
            );

            // Seed para o Value Object Password
            modelBuilder.Entity<User>().OwnsOne(u => u.Password).HasData(
                new { UserId = adminId, Value = "Admin@123" },
                new { UserId = gamerId, Value = "gamer@123" }
            );

            // Permissões iniciais
            var perm1 = new UserPermission("admin", "Pode gerenciar usuários");
            var perm2 = new UserPermission("usuario", "Pode visualizar relatórios");

            modelBuilder.Entity<UserPermission>().HasData(
                new
                {
                    Id = perm1.Id,
                    Name = perm1.Name,
                    Description = perm1.Description,
                    UserId = adminId
                },
                new
                {
                    Id = perm2.Id,
                    Name = perm2.Name,
                    Description = perm2.Description,
                    UserId = adminId
                }
            );
        }
    }

}
