namespace Lmyc.Migrations.BoatMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upgrade : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Boats", "RecordCreationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Boats", "RecordCreationDate", c => c.DateTime(nullable: false));
        }
    }
}
