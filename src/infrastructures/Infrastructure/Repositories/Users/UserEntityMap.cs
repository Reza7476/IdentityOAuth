using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repositories.Users;

public class UserEntityMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(_ => _.FirstName).IsRequired();
        
        builder.Property(_ => _.LastName).IsRequired();
        
        builder.Property(_ => _.Mobile).IsRequired();
        
        builder.Property(_ => _.Password).IsRequired();
        
        builder.Property(_ => _.UserName).IsRequired();
        
        builder.Property(_ => _.CreateDate).IsRequired();
        
        builder.Property(_ => _.Email).IsRequired(false);
    }

}
