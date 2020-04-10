namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Job : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobType = c.String(),
                        Description = c.String(),
                        Requirements = c.String(),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Jobs");
        }
    }
}
