using System;
using System.Collections.Generic;

namespace TasksDK.Model.Entities
{
    public class EmployeeTask
    {
        public EmployeeTask() : this(new Employee(), new Employee(), new Employee(), new List<EmployeeTask>()) { }
        public EmployeeTask(Employee owner, Employee assignee, Employee reporter, List<EmployeeTask> childTasks)
        {
            Owner = owner;
            Assignee = assignee;
            Reporter = reporter;
            ChildTasks = childTasks;
        }
        public string Name { get; set; }
        public EmployeeTask ParentTask { get; set; }
        public List<EmployeeTask> ChildTasks { get; set; }
        public List<int> ProcessIds { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Employee Owner { get; set; }
        public Employee Assignee { get; set; }
        public Employee Reporter { get; set; }
        public string Comment { get; set; }
        public int Weight { get; set; }
        public string EmployeeComment { get; set; }
        public string SupervisorComment { get; set; }
        public int EmployeeDonePercent { get; set; }
        public int SupervisorDonePercent { get; set; }
        public string AwaitedResult { get; set; }
        public string Meter { get; set; }


    }
}
