namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_FAQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        FAQID = c.Int(nullable: false, identity: true),
                        FAQCategoryID = c.Int(nullable: false),
                        FAQQuestion = c.Int(nullable: false),
                        FAQAnswer = c.Int(nullable: false),
                        FAQLikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FAQID)
                .ForeignKey("dbo.FAQCategories", t => t.FAQCategoryID, cascadeDelete: true)
                .Index(t => t.FAQCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FAQs", "FAQCategoryID", "dbo.FAQCategories");
            DropIndex("dbo.FAQs", new[] { "FAQCategoryID" });
            DropTable("dbo.FAQs");
        }
    }
}
