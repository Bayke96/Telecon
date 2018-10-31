namespace Telecon.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContext : DbContext
    {
      
        public DataContext()
            : base("Telecon-Erickson")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public virtual DbSet<User> Usuarios { get; set; }
        public virtual DbSet<Customer> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Telecon");
        }


    }
}