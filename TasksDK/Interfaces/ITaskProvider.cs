﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Model.Entities;

namespace TasksDK.Interfaces
{
    public interface ITaskProvider
    {
        List<EmployeeTask> GetTasks();
        void AddTask(EmployeeTask newTask);
    }
}
