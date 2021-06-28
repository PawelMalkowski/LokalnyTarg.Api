using LokalnyTarg.Data.Sql.DAO;
using LokalnyTarg.Data.Sql.DAOConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql
{
    public class LokalnyTargDBContext: DbContext 
    {
        public LokalnyTargDBContext(DbContextOptions<LokalnyTargDBContext> options) : base(options) { }
        public virtual DbSet<Address>Address { get; set; }
        public virtual DbSet<Annoucement> Annoucement { get; set; }
        public virtual DbSet<DAO.Category> Category { get; set; }
        public virtual DbSet<FavoriteSuppillier> FavoriteSuppillier { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new AnnoucmentConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new FavoriteSuppillerConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RateConfiguration());
            builder.ApplyConfiguration(new SupplierConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
