namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_author_article_volunteer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        ArticleTitle = c.String(),
                        ArticleBody = c.String(),
                        Date = c.DateTime(nullable: false),
                        Featured = c.String(),
                        AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        AuthorFname = c.String(),
                        AuthorLname = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        VolunteerFname = c.String(),
                        VolunteerLname = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        Skills = c.String(),
                        Day = c.String(),
                        Time = c.String(),
                        Department = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.VolunteerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "AuthorID", "dbo.Authors");
            DropIndex("dbo.Articles", new[] { "AuthorID" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Authors");
            DropTable("dbo.Articles");
        }
    }
}
