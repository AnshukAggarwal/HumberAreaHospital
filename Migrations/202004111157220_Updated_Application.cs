namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_Application : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "ApplicationDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "ApplicationDate");
        }
    }
}
