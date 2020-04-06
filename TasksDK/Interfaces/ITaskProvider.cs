using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Model;
using TasksDK.Model.Entities;

namespace TasksDK.Interfaces
{
    public interface ITaskProvider
    {
        List<EmployeeTask> GetTasks();
        List<EmployeeTask> GetTasks(EmployeeTask parentTast);
        List<Process> GetProcesses();
        void AddTask(EmployeeTask newTask);
        void Remove(EmployeeTask task);
        void Commit();
    }
}
