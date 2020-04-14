namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_FAQ_Removed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FAQs", "FAQLikes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FAQs", "FAQLikes", c => c.Int(nullable: false));
        }
    }
}
