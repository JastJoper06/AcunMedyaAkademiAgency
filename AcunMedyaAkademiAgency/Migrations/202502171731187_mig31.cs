namespace AcunMedyaAkademiAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Name", c => c.String());
            AddColumn("dbo.Admins", "SurName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "SurName");
            DropColumn("dbo.Admins", "Name");
        }
    }
}
