namespace Aula12.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autenticacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autenticacaos",
                c => new
                    {
                        AutenticacaoId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AutenticacaoId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Autenticacaos", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Autenticacaos", new[] { "UsuarioId" });
            DropTable("dbo.Autenticacaos");
        }
    }
}
