using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Infra.Data.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.LastAccess)
                   .IsRequired();

            // Value Object: Email
            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address)
                     .IsRequired()
                     .HasMaxLength(200)
                     .HasColumnName("Email");
            });

            // Value Object: Password
            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Value)
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnName("PasswordHash");
            });

            // Relacionamento UserPermission
            builder.HasMany<UserPermission>()
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}