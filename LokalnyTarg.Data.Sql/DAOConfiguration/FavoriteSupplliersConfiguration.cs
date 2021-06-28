using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class FavoriteSuppillerConfiguration : IEntityTypeConfiguration<FavoriteSuppillier>
    {
        public void Configure(EntityTypeBuilder<FavoriteSuppillier> builder)
        {
            builder.Property(c => c.FavoriteSuppillierId).IsRequired();
            builder.Property(c => c.SuppilierId).IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.HasOne(c => c.User).WithMany(c => c.FavoriteSuppilliers).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Suppllier).WithMany(c => c.FavoriteSuppilliers).OnDelete(DeleteBehavior.Cascade).HasForeignKey(c => c.SuppilierId);
        }
    }
}