namespace TasksDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProcessProxy", name: "ParentTask_Id", newName: "EmployeeTask_Id");
            RenameIndex(table: "dbo.ProcessProxy", name: "IX_ParentTask_Id", newName: "IX_EmployeeTask_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProcessProxy", name: "IX_EmployeeTask_Id", newName: "IX_ParentTask_Id");
            RenameColumn(table: "dbo.ProcessProxy", name: "EmployeeTask_Id", newName: "ParentTask_Id");
        }
    }
}
