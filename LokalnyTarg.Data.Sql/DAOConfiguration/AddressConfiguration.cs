using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(c => c.AddressId).IsRequired();
            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Street).HasMaxLength(50);
            builder.Property(c => c.Number).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Postcode).IsRequired().HasMaxLength(6);
        }
    }
}
