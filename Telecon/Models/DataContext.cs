namespace Telecon.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
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
        public virtual DbSet<Settings> appSettings { get; set; }
        public virtual DbSet<Product> Productos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Telecon");
        }


    }

    public partial class AddUniqueConstrains : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Telecon.App_Settings ADD CONSTRAINT Price_Rules CHECK (precioMinimo < precioMaximo)");
        }

    }
}