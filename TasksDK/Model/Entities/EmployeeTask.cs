using System;
using System.Collections.Generic;
using System.ComponentModel;
using TasksDK.Model.Entities.DTO;

namespace TasksDK.Model.Entities
{
    public class EmployeeTask: INotifyPropertyChanged
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
            Processes = new List<ProcessProxy>();
        }

        public int Id { get; set; }
        public List<ProcessProxy> Processes { get; set; }
        public string Name { get; set; }
        public virtual EmployeeTask ParentTask { get; set; }
        public List<EmployeeTask> ChildTasks { get; set; }
        public List<int> ProcessIds { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        private Employee _owner = new Employee();
        public Employee Owner 
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }
        public Employee Assignee { get; set; }
        public Employee Reporter { get; set; }
        public string Comment { get; set; }
        public int Weight { get; set; }
        private string _employeeComment = string.Empty;
        public string EmployeeComment
        {
            get => _employeeComment;
            set
            {
                _employeeComment = value;
                OnPropertyChanged(nameof(EmployeeComment));
            }
        }
        private string _reporterCommend = string.Empty;
        public string SupervisorComment { get => _reporterCommend;
            set
            {
                _reporterCommend = value;
                OnPropertyChanged(nameof(SupervisorComment));
            }
        }
        public int EmployeeDonePercent { get; set; }
        private int _supervisorDonePercent = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

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
            List<ProcessProxy> procProxies = new List<ProcessProxy>();
            foreach (ProcessProxy proxy in this.Processes)
            {
                procProxies.Add(new ProcessProxy
                {
                    Id = proxy.Id,
                    ProcessId = proxy.ProcessId
                });
            };

            return new EmployeeTask
            {
                Name = this.Name,
                Assignee = this.Assignee,
                AwaitedResult = this.AwaitedResult,
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
                Weight = this.Weight,
                Processes = procProxies

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
            Processes = task.Processes;
        }

        public override bool Equals(object otherEmployeeTask)
        {
            if(otherEmployeeTask is EmployeeTask)
            {
                EmployeeTask comparableTask = otherEmployeeTask as EmployeeTask;
                return this.Name.Equals(comparableTask.Name) ? true : false;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(Processes);
            hash.Add(Name);
            hash.Add(ParentTask);
            hash.Add(ChildTasks);
            hash.Add(ProcessIds);
            hash.Add(CreationDate);
            hash.Add(DueDate);
            hash.Add(Owner);
            hash.Add(Assignee);
            hash.Add(Reporter);
            hash.Add(Comment);
            hash.Add(Weight);
            hash.Add(EmployeeComment);
            hash.Add(SupervisorComment);
            hash.Add(EmployeeDonePercent);
            hash.Add(_supervisorDonePercent);
            hash.Add(SupervisorDonePercent);
            hash.Add(AwaitedResult);
            hash.Add(Meter);
            return hash.ToHashCode();
        }
    }
}
