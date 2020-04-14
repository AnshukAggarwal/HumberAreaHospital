namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_FAQ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FAQs", "FAQQuestion", c => c.String());
            AlterColumn("dbo.FAQs", "FAQAnswer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FAQs", "FAQAnswer", c => c.Int(nullable: false));
            AlterColumn("dbo.FAQs", "FAQQuestion", c => c.Int(nullable: false));
        }
    }
}
