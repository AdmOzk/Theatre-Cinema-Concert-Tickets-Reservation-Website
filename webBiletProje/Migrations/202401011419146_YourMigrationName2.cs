namespace webBiletProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationName2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Numberphone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Numberphone");
        }
    }
}
