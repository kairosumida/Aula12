namespace Aula12.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusUsuarios",
                c => new
                    {
                        StatusUsuarioId = c.Int(nullable: false, identity: true),
                        NomeStatus = c.String(),
                    })
                .PrimaryKey(t => t.StatusUsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        UltimoAcesso = c.DateTime(nullable: false),
                        StatusUsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.StatusUsuarios", t => t.StatusUsuarioId, cascadeDelete: true)
                .Index(t => t.StatusUsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "StatusUsuarioId", "dbo.StatusUsuarios");
            DropIndex("dbo.Usuarios", new[] { "StatusUsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.StatusUsuarios");
        }
    }
}
