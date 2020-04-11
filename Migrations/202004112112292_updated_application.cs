namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_application : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applications", "ApplicationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applications", "ApplicationDate", c => c.String());
        }
    }
}
