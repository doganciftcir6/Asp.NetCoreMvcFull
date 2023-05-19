using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //bir rolün birden fazla userı olabilir.
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUsers).HasForeignKey(x => x.AppRoleId).OnDelete(DeleteBehavior.NoAction);
            //OnDelete(DeleteBehavior.NoAction) yaparak bir rol silinirse o role sahip tüm kullanıcıların silinmesinden kurtuluruz. Veriler değerlidir silinmesini istemeyiz bu çok önemli o yüzden.
        }
    }
}
