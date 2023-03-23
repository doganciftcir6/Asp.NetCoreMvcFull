using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCookieBased.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //tablo belirli datalarla ayaklansın.
            builder.HasData(new AppUser
            {
                Id = 1,
                Username = "yavuz",
                Password = "1"
            });

            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(250).IsRequired();
        }
    }
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            //tablo belirli datalarla ayaklansın.
            builder.HasData(new AppRole
            {
                Id=1,
                Definition= "Admin"
            });

            builder.Property(x => x.Definition).HasMaxLength(200).IsRequired();
        }
    }
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            //tablo belirli datalarla ayaklansın.
            builder.HasData(new AppUserRole
            {
                RoleId = 1,
                UserId = 1,
            });

            //tekrarlı kayıt olmasın diye primary key belirtmek.
            builder.HasKey(x => new { x.UserId, x.RoleId });
            //ilişkiyi belirtmek.
            builder.HasOne(x => x.AppRole).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
        }
    }
}
