namespace TasksDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "AnalyticId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "AnalyticId");
        }
    }
}
