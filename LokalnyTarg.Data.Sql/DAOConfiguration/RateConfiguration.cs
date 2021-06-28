using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        { 
            builder.Property(c => c.RateId).IsRequired();
            builder.Property(c => c.RateValue).IsRequired().HasMaxLength(2);
            builder.Property(c => c.SupplierId).IsRequired();
            builder.HasOne(c => c.Supplier).WithMany(c => c.Rates).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.SupplierId);
            // 1 połącznie z suplier z wieloma ocenami, usuwanie kaskaddowe klucz obcy - wskazuję go 
            
            
        }
    }
}

