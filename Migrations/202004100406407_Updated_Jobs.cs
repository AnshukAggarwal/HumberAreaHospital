namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_Jobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "JobCategory");
        }
    }
}
