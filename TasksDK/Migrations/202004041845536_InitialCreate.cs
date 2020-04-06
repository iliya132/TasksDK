namespace TasksDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Weight = c.Int(nullable: false),
                        EmployeeComment = c.String(),
                        SupervisorComment = c.String(),
                        EmployeeDonePercent = c.Int(nullable: false),
                        SupervisorDonePercent = c.Int(nullable: false),
                        AwaitedResult = c.String(),
                        Meter = c.String(),
                        Assignee_Id = c.Int(),
                        ParentTask_Id = c.Int(),
                        Owner_Id = c.Int(),
                        Reporter_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Assignee_Id)
                .ForeignKey("dbo.EmployeeTasks", t => t.ParentTask_Id)
                .ForeignKey("dbo.Employees", t => t.Owner_Id)
                .ForeignKey("dbo.Employees", t => t.Reporter_Id)
                .Index(t => t.Assignee_Id)
                .Index(t => t.ParentTask_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Reporter_Id);
            
            CreateTable(
                "dbo.Process",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        procName = c.String(nullable: false),
                        Comment = c.String(),
                        CommentNeeded = c.Boolean(),
                        Block_id = c.Int(nullable: false),
                        SubBlockId = c.Int(nullable: false),
                        ProcessType_id = c.Int(nullable: false),
                        Result_id = c.Int(nullable: false),
                        EmployeeTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EmployeeTasks", t => t.EmployeeTask_Id)
                .Index(t => t.EmployeeTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeTasks", "Reporter_Id", "dbo.Employees");
            DropForeignKey("dbo.Process", "EmployeeTask_Id", "dbo.EmployeeTasks");
            DropForeignKey("dbo.EmployeeTasks", "Owner_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeTasks", "ParentTask_Id", "dbo.EmployeeTasks");
            DropForeignKey("dbo.EmployeeTasks", "Assignee_Id", "dbo.Employees");
            DropIndex("dbo.Process", new[] { "EmployeeTask_Id" });
            DropIndex("dbo.EmployeeTasks", new[] { "Reporter_Id" });
            DropIndex("dbo.EmployeeTasks", new[] { "Owner_Id" });
            DropIndex("dbo.EmployeeTasks", new[] { "ParentTask_Id" });
            DropIndex("dbo.EmployeeTasks", new[] { "Assignee_Id" });
            DropTable("dbo.Process");
            DropTable("dbo.EmployeeTasks");
            DropTable("dbo.Employees");
        }
    }
}
