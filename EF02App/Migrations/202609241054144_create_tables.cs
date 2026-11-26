namespace EF02App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PERSON",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NATURAL_PERSON",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CPF = c.String(nullable: false),
                        Salary = c.Single(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.LEGAL_PERSON",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CNPJ = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LEGAL_PERSON", "Id", "dbo.PERSON");
            DropForeignKey("dbo.NATURAL_PERSON", "Id", "dbo.PERSON");
            DropIndex("dbo.LEGAL_PERSON", new[] { "Id" });
            DropIndex("dbo.NATURAL_PERSON", new[] { "Id" });
            DropTable("dbo.LEGAL_PERSON");
            DropTable("dbo.NATURAL_PERSON");
            DropTable("dbo.PERSON");
        }
    }
}
