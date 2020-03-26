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
            CreationDate = DateTime.Now;
            DueDate = DateTime.Now;
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
        private int _supervisorDonePercent = 0;
        public int SupervisorDonePercent
        {
            get
            {
                int donePercent = 0;
                if (ChildTasks.Count > 0)
                {
                    foreach(EmployeeTask _subtask in ChildTasks)
                    {
                        donePercent += _subtask.SupervisorDonePercent;
                    }
                    return donePercent/ChildTasks.Count;
                }
                else
                {
                    return _supervisorDonePercent;
                }
            }
            set
            {
                _supervisorDonePercent = value;
            }
        }
        public string AwaitedResult { get; set; }
        public string Meter { get; set; }

        public EmployeeTask AsCopy()
        {
            return new EmployeeTask
            {
                Name = this.Name,
                Assignee = this.Assignee,
                AwaitedResult = this.AwaitedResult,
                ChildTasks = this.ChildTasks,
                Comment = this.Comment,
                CreationDate = this.CreationDate,
                DueDate = this.DueDate,
                EmployeeComment = this.EmployeeComment,
                EmployeeDonePercent = this.EmployeeDonePercent,
                Meter = this.Meter,
                Owner = this.Owner,
                ParentTask = this.ParentTask,
                ProcessIds = this.ProcessIds,
                Reporter = this.Reporter,
                SupervisorComment = this.SupervisorComment,
                SupervisorDonePercent = this.SupervisorDonePercent,
                Weight = this.Weight
            };
        }
        public void CopyFields(EmployeeTask task)
        {
            Name = task.Name;
            Assignee = task.Assignee;
            AwaitedResult = task.AwaitedResult;
            ChildTasks = task.ChildTasks;
            Comment = task.Comment;
            CreationDate = task.CreationDate;
            DueDate = task.DueDate;
            EmployeeComment = task.EmployeeComment;
            EmployeeDonePercent = task.EmployeeDonePercent;
            Meter = task.Meter;
            Owner = task.Owner;
            ParentTask = task.ParentTask;
            ProcessIds = task.ProcessIds;
            Reporter = task.Reporter;
            SupervisorComment = task.SupervisorComment;
            SupervisorDonePercent = task.SupervisorDonePercent;
            Weight = task.Weight;
        }

    }
}
