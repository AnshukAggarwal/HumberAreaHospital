namespace HumberAreaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointment_Staff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        appointmentId = c.Int(nullable: false, identity: true),
                        appointmentFname = c.String(),
                        appointmentLname = c.String(),
                        appointmentPhone = c.String(),
                        appointmentEmail = c.String(),
                        appointmentDate = c.DateTime(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.appointmentId)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        staffId = c.Int(nullable: false, identity: true),
                        staffFname = c.String(),
                        staffLname = c.String(),
                        staffEmail = c.String(),
                        staffExt = c.String(),
                        SpecialtyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.staffId)
                .ForeignKey("dbo.Specialities", t => t.SpecialtyID, cascadeDelete: true)
                .Index(t => t.SpecialtyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "SpecialtyID", "dbo.Specialities");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Staffs", new[] { "SpecialtyID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Appointments");
        }
    }
}
