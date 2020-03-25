using System;
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
            List<Employee> employees = _employeeContext.GetEmployees();
            EmployeeTask task = new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Получение единого окна в HR для подбора персонала для ДК",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Срок заполнения вакансии Минимизация времязатрат ДК на подбор Успешное прохождение испытательного срока кандидатами",
                ProcessIds = new List<int>() { 1 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre Polite do object at passed it is.So by colonel hearted ferrars. Am wound worth water he linen at vexed..Decisively advantages nor expression unpleasing she led met.Detract yet delight written farther his general.Decisively advantages nor expression unpleasing she led met. Feel and make two real Course sir people worthy horses add entire suffer.If as increasing contrasted entreaties be.Bed uncommonly his discovered for estimating far. As mr started arrival subject by believe.Mrs assured add private married removed believe did she.Celebrated delightful an especially increasing instrument am.Hard do me sigh with we",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 20,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = 15,
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
            };
            EmployeeTask task2 = new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Формирование подходов к постановке измеримых целей с учетом ежеквартальной оценки",
                AwaitedResult = "Уход от субъективной оценки сотрудников, корреляция внутри ДК",
                Meter = "Зафиксированные в документе критерии оценки",
                ProcessIds = new List<int>() { 2 },
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "n expression an solicitude principles in do. Mrs assured add private married removed believe did she. Fortune day out married parties. How one dull get busy dare far. Mrs assured add private married removed believe did she. Mirth learn it he given. Detract yet delight written farther his general. Decisively advantages nor expre",
                EmployeeComment = "Комментарий сотрудника к задаче",
                EmployeeDonePercent = 30,
                SupervisorComment = "Комментарий руководителя к задаче",
                SupervisorDonePercent = 5,
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1]
            };
            Tasks.AddRange(new List<EmployeeTask>() { task, task2 });
        }

        public List<EmployeeTask> GetTasks() => Tasks;
    }
}
