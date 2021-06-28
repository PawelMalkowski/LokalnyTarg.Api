using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Price).IsRequired().HasPrecision(12,2);
            builder.Property(c => c.CategoryId).IsRequired();
            builder.HasOne(c => c.Category).WithMany(c => c.Products).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.CategoryId);
        }
    }
}