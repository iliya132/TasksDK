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
            Processes.Add(new Process() { id = 1, procName = "Процесс1" });
            Processes.Add(new Process() { id = 2, procName = "Процесс2" });
            Processes.Add(new Process() { id = 3, procName = "Процесс3" });
            Processes.Add(new Process() { id = 4, procName = "Процесс4" });
            Processes.Add(new Process() { id = 5, procName = "Процесс5" });
            Processes.Add(new Process() { id = 6, procName = "Процесс6" });
            Processes.Add(new Process() { id = 7, procName = "Процесс7" });
            Processes.Add(new Process() { id = 8, procName = "Процесс8" });
            Processes.Add(new Process() { id = 9, procName = "Процесс9" });
            Processes.Add(new Process() { id = 10, procName = "Процесс10" });
            List<Employee> employees = _employeeContext.GetEmployees();
            Random random = new Random();
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Получение единого окна в HR для подбора персонала для ДК",
                AwaitedResult = "Повышение скорости нахождения сотрудниками HR квалифицированных сотрудников",
                Meter = "Срок заполнения вакансии Минимизация времязатрат ДК на подбор Успешное прохождение испытательного срока кандидатами",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[1] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Формирование подходов к постановке измеримых целей с учетом ежеквартальной оценки",
                AwaitedResult = "Уход от субъективной оценки сотрудников, корреляция внутри ДК",
                Meter = "Зафиксированные в документе критерии оценки",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0,9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = " EY: Совместные KPI",
                AwaitedResult = "с HR- по обучению, IT- по автоматизации",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[0]}
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "бенч-марк вознаграждения по разработанной модели компетенций",
                AwaitedResult = "план и инструменты ( сертификация,обучение, нематериальная мотивация) ",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "разработка модели компетенций",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "опрос удовлетворенности сотрудников",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Timesheet",
                AwaitedResult = "Контроль учета времени и эффективности сотрудников",
                Meter = "1) корректное заполнение timesheet-ов сотрудниками 2) возможность составления аналитики исходя из информации в timesheet-ов",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });

            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: матрица расходов на комплаенс",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "инструмент для расчета ",
                AwaitedResult = "по итогам timesheet  и отчетов по счетным процессам",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });

            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Разработка и согласование формата фиксирование нормативных изменений",
                AwaitedResult = "Системная информация о законодательных изменениях от инциативы до контроля внедрения",
                Meter = "Ежемесячная демонстрация статусов на регулярных встречах",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Разработка и согласование формата фиксирование нормативных изменений",
                AwaitedResult = "Системная информация о законодательных изменениях от инциативы до контроля внедрения",
                Meter = "Ежемесячная демонстрация статусов на регулярных встречах",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Разработка формата отчетности от комплаенс-партнера по изменениям в бизнесе",
                AwaitedResult = "Информация о новых инициативах своевременно доносится до экспертов ДК",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Документ \"оценка комплаенс - риска продуктов\"",
                AwaitedResult = ",",
                Meter = "дважды в год",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Оценка риска в разрезе бизнес-линий в едином формате",
                AwaitedResult = "единый подход оценке комплаенс риска в разрезе разных бизнес-линий",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Актуализировать подходы к оценке риска с учетом материальности, вероятности, cost of complaince Согласовать подход с ДУР",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Самоценка дк",
                AwaitedResult = "43862",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY взаимоувязка с риск-менеджментом и комитетом по рискам",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "СД - 1 раз в год Правление - 2 раза в год",
                AwaitedResult = "СД- март 2020",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });

            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Аналог \"Единого окна\" для ОРД",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Централизация методологии ПОД/ФТ, за исключением идентификации",
                AwaitedResult = "Идентификация ВПР будет передаваться в УКПБП?",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Внедрение применимых рекомендаций по итогам аудита процессов  (проектный офис)",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "комплаенс-процедуры в СИ и КП ",
                AwaitedResult = "1)ежемесячный статус - ежеквартально?2)участие в УК и планировании ",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = " повышение уровня автоматизации_ доп. ресурсы",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "проекты в срок и в бюджет",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "срок внедрения комплаенс-требований сократить до 4-7 мес",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "комплаенс-скрипты во фронт-офисе",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "отчет о повышении эффективности процессов ",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "роботизация для неавтоматизируемых процессов или в ожидании автоматизации",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "в январе определить какие из созданных форматов и шаблонов будут автоматизированы  ",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Внедрение применимых рекомендаций по итогам аудита процессов ",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Инструмент для контроля",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: Сегментирование сотрудников ДК и повышение компетенций",
                AwaitedResult = "1) Матрица должностных компетенций 2) План обучения 3) Обучения",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "развитие технологических компетенций команды",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: Интегрировать обучение ДК в программу обучения HR на новой платформе",
                AwaitedResult = "Обучение по всем комплаенс-рискам интегрировано в программу обучения HR Создание обуающих материалов для команд внедрения, обчение участие в модернизации программ обучения",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "участие ",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "Развитие культуры совместно с HR, рисками и маркетингом",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: Договориться с УВА и ДВКо зонах контроля за комплаенс-процессами/процедурами",
                AwaitedResult = "Уменьшение контроля со стороны ДКК",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: 4 глаза в текущих процессах ДК",
                AwaitedResult = "снижение риска операционных ошибок",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "EY: единый подход к работе с данными, применяемыми в комплаенс-процессах ",
                AwaitedResult = ",",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });
            Tasks.Add(new EmployeeTask
            {
                Assignee = employees[0],
                Name = "оперативная реакция",
                AwaitedResult = "отсутствие существенных претензий",
                Meter = ",",
                DueDate = DateTime.Parse("31.12.2020"),
                CreationDate = DateTime.Parse("12.03.2020"),
                Comment = "Описание",
                EmployeeDonePercent = 20,
                SupervisorDonePercent = random.Next(0, 100),
                Weight = 100,
                Owner = employees[1],
                Reporter = employees[1],
                Processes = new List<Process> { Processes[random.Next(0, 9)], Processes[random.Next(0, 9)] }
            });

    
        }

        public List<EmployeeTask> GetTasks() => Tasks;
        public List<Process> GetProcesses() => Processes;

        public void Remove(EmployeeTask task)
        {
            Tasks.Remove(task);
        }
    }
}
