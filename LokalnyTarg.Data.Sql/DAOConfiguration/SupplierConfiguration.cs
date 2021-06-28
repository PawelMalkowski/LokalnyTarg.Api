using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(c => c.SupplierId).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(40);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
            builder.Property(c => c.AddressId).IsRequired();
            builder.HasOne(c => c.Address).WithMany(c => c.Suppliers).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.AddressId);

        }
    }
}

