namespace EF01App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Status = c.Boolean(nullable: false),
                        CNPJ = c.String(),
                        CPF = c.String(),
                        Salary = c.Single(),
                        Birthday = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Person_Id = c.Int(),
                        Person_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.People", t => t.Person_Id1)
                .Index(t => t.Person_Id)
                .Index(t => t.Person_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Person_Id1", "dbo.People");
            DropForeignKey("dbo.People", "Person_Id", "dbo.People");
            DropIndex("dbo.People", new[] { "Person_Id1" });
            DropIndex("dbo.People", new[] { "Person_Id" });
            DropTable("dbo.People");
        }
    }
}
