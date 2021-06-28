using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class AnnoucmentConfiguration : IEntityTypeConfiguration<Annoucement>
    {
        public void Configure(EntityTypeBuilder<Annoucement> builder)
        {
            builder.Property(c => c.AnnoucementId).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(70);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
            builder.Property(c => c.SuppilerId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.HasOne(c => c.Supplier).WithMany(c => c.Annoucements).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.SuppilerId);
        }
    }
}
