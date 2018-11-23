namespace Telecon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        employeeID = c.Int(nullable: false),
                        c_razonsocial = c.String(nullable: false, maxLength: 128),
                        c_nombres = c.String(maxLength: 72),
                        c_apellidos = c.String(maxLength: 72),
                        c_telefono = c.String(nullable: false, maxLength: 72),
                        c_direccion = c.String(nullable: false, maxLength: 128),
                        c_comentarios = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Telecon.Usuarios", t => t.employeeID, cascadeDelete: true)
                .Index(t => t.employeeID);
            
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
                        opt_password = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.u_username, unique: true)
                .Index(t => t.u_email, unique: true);
            
            CreateTable(
                "Telecon.LoginAttempts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Attempt_IP = c.String(nullable: false, maxLength: 50),
                        Attempt_Ammounts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Attempt_IP, unique: true);
            
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
                "Telecon.AccRecovery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Request_Email = c.Int(nullable: false),
                        Request_Date = c.DateTime(nullable: false),
                        Request_Counter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Telecon.Usuarios", t => t.Request_Email, cascadeDelete: true)
                .Index(t => t.Request_Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Telecon.AccRecovery", "Request_Email", "Telecon.Usuarios");
            DropForeignKey("Telecon.Clientes", "employeeID", "Telecon.Usuarios");
            DropIndex("Telecon.AccRecovery", new[] { "Request_Email" });
            DropIndex("Telecon.Productos", new[] { "p_name" });
            DropIndex("Telecon.LoginAttempts", new[] { "Attempt_IP" });
            DropIndex("Telecon.Usuarios", new[] { "u_email" });
            DropIndex("Telecon.Usuarios", new[] { "u_username" });
            DropIndex("Telecon.Clientes", new[] { "employeeID" });
            DropTable("Telecon.AccRecovery");
            DropTable("Telecon.Productos");
            DropTable("Telecon.LoginAttempts");
            DropTable("Telecon.Usuarios");
            DropTable("Telecon.Clientes");
            DropTable("Telecon.App_Settings");
        }
    }
}
