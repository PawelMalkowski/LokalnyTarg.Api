using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LokalnyTarg.Data.Sql.DAOConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<DAO.Category>
    {
        public void Configure(EntityTypeBuilder<DAO.Category> builder)
        {
            builder.Property(c => c.CategoryId).IsRequired();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}
