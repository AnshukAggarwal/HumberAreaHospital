namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Application : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        ApplicantFirstName = c.String(),
                        ApplicantLastName = c.String(),
                        ApplicantEmail = c.String(),
                        ApplicantPhone = c.String(),
                        ApplicantAddress = c.String(),
                        ApplicantCity = c.String(),
                        ApplicantProvince = c.String(),
                        ApplicantZipCode = c.String(),
                        ApplicantResume = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationID)
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "JobID", "dbo.Jobs");
            DropIndex("dbo.Applications", new[] { "JobID" });
            DropTable("dbo.Applications");
        }
    }
}
