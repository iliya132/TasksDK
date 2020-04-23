using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;

namespace TasksDK.Model.DataProviders
{
    public class EFTaskProvider : ITaskProvider
    {
        DataContext context = new DataContext();
        TimeSheetContext TSContext = new TimeSheetContext();

        List<EmployeeTask> _tasks = new List<EmployeeTask>();
        public EFTaskProvider()
        {
            _tasks = context.Tasks.
                Include("Processes").
                Include("Reporter").
                Include("Assignee").
                Include("Owner").
                ToList();
        }
        public void AddTask(EmployeeTask newTask)
        {
            _tasks.Add(newTask);
            context.Tasks.Add(newTask);
            
        }

        public void AddTask(List<EmployeeTask>newTasks)
        {
            _tasks.AddRange(newTasks);
            context.Tasks.AddRange(newTasks);

        }


        public void Commit()
        {
            context.SaveChanges();
        }

        public List<Process> GetProcesses() 
        {
            List<TimeSheetProcess> processes = TSContext.Process.ToList();
            return processes.Select(i=>new Process
                {
                    Block_id=i.Block_id,
                    Comment=i.Comment,
                    CommentNeeded=i.CommentNeeded,
                    Id=i.id,
                    ProcessType_id=i.ProcessType_id,
                    Name=i.procName,
                    Result_id=i.Result_id,
                    SubBlockId=i.SubBlockId
                }).ToList(); 
        }

        public List<EmployeeTask> GetTasks() => _tasks;

        public List<EmployeeTask> GetTasks(EmployeeTask parentTask)
        {
            if (parentTask == null)
            {
                return _tasks.Where(i => i.ParentTask == null).ToList();
            }
            else
            {
                return _tasks.Where(i => i.ParentTask.Id == parentTask.Id).ToList();
            }
        }

        public void Remove(EmployeeTask task)
        {
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
    }
}
