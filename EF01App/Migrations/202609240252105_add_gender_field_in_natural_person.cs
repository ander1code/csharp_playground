namespace EF01App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_gender_field_in_natural_person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Gender", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Gender");
        }
    }
}
