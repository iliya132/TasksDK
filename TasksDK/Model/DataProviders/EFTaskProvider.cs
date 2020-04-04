using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;
using TasksDK.Model.Entities.DTO;

namespace TasksDK.Model.DataProviders
{
    public class EFTaskProvider : ITaskProvider
    {
        DataContext context = new DataContext();
        TimeSheetContext TimeSheetContext = new TimeSheetContext();
        public void AddTask(EmployeeTask newTask)=> context.Tasks.Add(newTask.ToDTO());


        public List<Process> GetProcesses() => TimeSheetContext.Process.ToList();

        public List<EmployeeTask> GetTasks()
        {
            List<EmployeeTask> _tasks = new List<EmployeeTask>();

        }

        private EmployeeTask LoadTask(int id)
        {
            TaskDTO _taskDto = context.Tasks.FirstOrDefault(i => i.Id == id);
            EmployeeTask task = new EmployeeTask
            {
                Id=id,
                Meter = _taskDto.Meter,
                Owner=_taskDto.Owner,
                DueDate = _taskDto.DueDate,
                Reporter = _taskDto.Reporter,
                SupervisorComment = _taskDto.SupervisorComment,
                SupervisorDonePercent = _taskDto.SupervisorDonePercent,
                Weight = _taskDto.Weight,
                Name = _taskDto.Name,
                EmployeeDonePercent = _taskDto.EmployeeDonePercent,
                EmployeeComment = _taskDto.EmployeeComment,
                CreationDate = _taskDto.CreationDate,
                Comment = _taskDto.Comment,
                Assignee = _taskDto.Assignee,
                AwaitedResult = _taskDto.AwaitedResult
            };
            string[] childIdsstr = _taskDto.ChildTasks.Split(',');
            int[] childIds = new int[childIdsstr.Length];
            for (int i = 0; i < childIdsstr.Length; i++)
            {
                childIds[i] = int.Parse(childIdsstr[i]);
            }
            


            return null;
        }

        public void Remove(EmployeeTask task)
        {
            throw new NotImplementedException();
        }
    }
}
