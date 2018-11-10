namespace Telecon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Telecon.App_Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        precioMinimo = c.Double(),
                        precioMaximo = c.Double(),
                        adicionProductos = c.Boolean(),
                        modificacionProductos = c.Boolean(),
                        eliminarProductos = c.Boolean(),
                        fotosPerfil = c.Boolean(),
                        cambioContraseña = c.Boolean(),
                        cambioCorreo = c.Boolean(),
                        accesoUsuario = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);

            Sql("ALTER TABLE Telecon.App_Settings ADD CONSTRAINT Price_Rules CHECK (precioMinimo < precioMaximo)");

            CreateTable(
                "Telecon.Clientes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        c_razonsocial = c.String(nullable: false, maxLength: 128),
                        c_nombres = c.String(maxLength: 72),
                        c_apellidos = c.String(maxLength: 72),
                        c_telefono = c.String(nullable: false, maxLength: 72),
                        c_direccion = c.String(nullable: false, maxLength: 128),
                        c_comentarios = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Telecon.Productos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        p_name = c.String(nullable: false, maxLength: 128),
                        p_description = c.String(nullable: false, maxLength: 128),
                        p_price = c.Double(nullable: false),
                        p_mainimage = c.String(nullable: false, maxLength: 128),
                        p_secondimageA = c.String(maxLength: 128),
                        p_secondimageB = c.String(maxLength: 128),
                        p_secondimageC = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.p_name, unique: true);
            
            CreateTable(
                "Telecon.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        u_username = c.String(nullable: false, maxLength: 128),
                        u_password = c.String(nullable: false, maxLength: 128),
                        u_firstnames = c.String(nullable: false, maxLength: 72),
                        u_lastnames = c.String(nullable: false, maxLength: 72),
                        u_email = c.String(nullable: false, maxLength: 50),
                        u_age = c.Int(nullable: false),
                        u_address = c.String(maxLength: 128),
                        u_number = c.String(maxLength: 72),
                        u_admin = c.Boolean(nullable: false),
                        u_picture = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.u_username, unique: true)
                .Index(t => t.u_email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("Telecon.Usuarios", new[] { "u_email" });
            DropIndex("Telecon.Usuarios", new[] { "u_username" });
            DropIndex("Telecon.Productos", new[] { "p_name" });
            DropTable("Telecon.Usuarios");
            DropTable("Telecon.Productos");
            DropTable("Telecon.Clientes");
            DropTable("Telecon.App_Settings");
        }
    }
}
