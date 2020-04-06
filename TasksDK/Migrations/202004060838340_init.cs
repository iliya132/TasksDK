namespace TasksDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Process", newName: "ProcessProxy");
            RenameColumn(table: "dbo.ProcessProxy", name: "EmployeeTask_Id", newName: "ParentTask_Id");
            RenameIndex(table: "dbo.ProcessProxy", name: "IX_EmployeeTask_Id", newName: "IX_ParentTask_Id");
            AddColumn("dbo.ProcessProxy", "ProcessId", c => c.Int(nullable: false));
            DropColumn("dbo.ProcessProxy", "procName");
            DropColumn("dbo.ProcessProxy", "Comment");
            DropColumn("dbo.ProcessProxy", "CommentNeeded");
            DropColumn("dbo.ProcessProxy", "Block_id");
            DropColumn("dbo.ProcessProxy", "SubBlockId");
            DropColumn("dbo.ProcessProxy", "ProcessType_id");
            DropColumn("dbo.ProcessProxy", "Result_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessProxy", "Result_id", c => c.Int(nullable: false));
            AddColumn("dbo.ProcessProxy", "ProcessType_id", c => c.Int(nullable: false));
            AddColumn("dbo.ProcessProxy", "SubBlockId", c => c.Int(nullable: false));
            AddColumn("dbo.ProcessProxy", "Block_id", c => c.Int(nullable: false));
            AddColumn("dbo.ProcessProxy", "CommentNeeded", c => c.Boolean());
            AddColumn("dbo.ProcessProxy", "Comment", c => c.String());
            AddColumn("dbo.ProcessProxy", "procName", c => c.String(nullable: false));
            DropColumn("dbo.ProcessProxy", "ProcessId");
            RenameIndex(table: "dbo.ProcessProxy", name: "IX_ParentTask_Id", newName: "IX_EmployeeTask_Id");
            RenameColumn(table: "dbo.ProcessProxy", name: "ParentTask_Id", newName: "EmployeeTask_Id");
            RenameTable(name: "dbo.ProcessProxy", newName: "Process");
        }
    }
}
