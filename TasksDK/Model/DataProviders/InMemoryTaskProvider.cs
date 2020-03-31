﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;

namespace TasksDK.Model.DataProviders
{
    public class InMemoryTaskProvider : ITaskProvider
    {
        private IEmployeeProvider _employeeContext;
        private List<EmployeeTask> _tasks = new List<EmployeeTask>();
        public List<EmployeeTask> Tasks { get => _tasks; set => _tasks = value; }

        private List<Process> _processes = new List<Process>();
        public List<Process> Processes { get => _processes; set => _processes = value; }
        public InMemoryTaskProvider(IEmployeeProvider EmployeeProvider)
        {
            _employeeContext = EmployeeProvider;
            GenerateTasks();
        }

        public void AddTask(EmployeeTask newTask)
        {
            Tasks.Add(newTask);
        }

        

        public void GenerateTasks()
        {
            Processes.Add(new Process() { Id=1, Name="Процесс1"});
            Processes.Add(new Process() { Id=2, Name="Процесс2"});
            Processes.Add(new Process() { Id=3, Name="Процесс3"});
            Processes.Add(new Process() { Id=4, Name="Процесс4"});
            Processes.Add(new Process() { Id=5, Name="Процесс5"});
            Processes.Add(new Process() { Id=6, Name="Процесс6"});
            Processes.Add(new Process() { Id=7, Name="Процесс7"});
            Processes.Add(new Process() { Id=8, Name="Процесс8"});
            Processes.Add(new Process() { Id=9, Name="Процесс9"});
            Processes.Add(new Process() { Id=10, Name="Процесс10"});
            List<Employee> employees = _employeeContext.GetEmployees();
            Random random = new Random();
            EmployeeTask task = new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Получение единого окна в HR для подбора персонала для ДК",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Количественный",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0,100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process>() { Processes[0], Processes[1]}
            };
            task.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача первой задачи",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Количественный",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[2], Processes[3] }
            });
            task.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача первой задачи2",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Количественный",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[4], Processes[5] }
            });
            EmployeeTask task2 = new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Формирование подходов к постановке измеримых целей с учетом ежеквартальной оценки",
                AwaitedResult = "Уход от субъективной оценки сотрудников, корреляция внутри ДК",
                Meter = "Временной",
                ProcessIds = new List<int>() { 2 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 30,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process>() { Processes[6], Processes[7] }
            };
            task2.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача второй задачи",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Временной",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[8], Processes[9] }
            });
            task2.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача второй задачи2",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Временной",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[9], Processes[3] }
            });
            task2.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача второй задачи3",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Временной",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[4], Processes[2] }
            });
            task2.ChildTasks.Add(new EmployeeTask
            {
                Assignee = employees[1],
                Name = "Подзадача второй задачи4",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Ручной режим",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[0],
                Reporter = employees[0],
                Processes = new List<Process>() { Processes[7], Processes[4] }
            });
            Tasks.AddRange(new List<EmployeeTask>() { task, task2 });
        }

        public List<EmployeeTask> GetTasks() => Tasks;
        public List<Process> GetProcesses() => Processes;

        public void Remove(EmployeeTask task)
        {
            Tasks.Remove(task);
        }
    }
}
