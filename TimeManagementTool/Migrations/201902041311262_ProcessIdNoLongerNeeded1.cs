namespace TimeManagementTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessIdNoLongerNeeded1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProcessCategories");
            AlterColumn("dbo.ProcessCategories", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ProcessCategories", "Name");
            DropColumn("dbo.ProcessCategories", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessCategories", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ProcessCategories");
            AlterColumn("dbo.ProcessCategories", "Name", c => c.String());
            AddPrimaryKey("dbo.ProcessCategories", "Id");
        }
    }
}
