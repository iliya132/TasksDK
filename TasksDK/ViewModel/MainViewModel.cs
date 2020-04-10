using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TasksDK.Interfaces;
using TasksDK.Model;
using TasksDK.Model.Entities;
using TasksDK.Model.Entities.DTO;
using TasksDK.View;

namespace TasksDK.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private IEmployeeProvider _employees;
        private ITaskProvider _tasks;

        #region currentValues
        /// <summary>
        /// Текущие задачи. Загрузка из ITaskProvider.
        /// </summary>
        private ObservableCollection<EmployeeTask> _currentTasks = new ObservableCollection<EmployeeTask>();
        public ObservableCollection<EmployeeTask> CurrentTasks { get => _currentTasks; set => _currentTasks = value; }
        EmployeeTask _mainTask = new EmployeeTask();
        EmployeeTask _currentTask = new EmployeeTask();
        private List<Process> _processes;
        public List<Process> Processes { get => _processes; set => _processes = value; }

        private Stack<EmployeeTask> _taskStack = new Stack<EmployeeTask>();
        public Stack<EmployeeTask> TaskStack { get => _taskStack; set => _taskStack = value; }
        private ObservableCollection<Employee> _analytics = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Analytics { get => _analytics; set => _analytics = value; }
        private ObservableCollection<string> _analytics_str = new ObservableCollection<string>();
        public ObservableCollection<string> Analytics_str { get => _analytics_str; set => _analytics_str = value; }
        private Analytic _currentAnalytic;
        private Employee _currentEmployee;

        /// <summary>
        /// Новая задача. Привязка из окна AddTask
        /// </summary>
        private EmployeeTask newTask = new EmployeeTask();
        public EmployeeTask NewTask { get => newTask; set => newTask = value; }
        private EmployeeTask _selectedTask;
        public EmployeeTask SelectedTask { get => _selectedTask; set => _selectedTask = value; }
        private ObservableCollection<Process> _selectedProcesses = new ObservableCollection<Process>();
        public ObservableCollection<Process> SelectedProcesses { get => _selectedProcesses; set => _selectedProcesses = value; }

        #endregion

        #region Commands
        public RelayCommand AddNewTaskCommand { get; set; }
        public RelayCommand AddNewSubTaskCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand FilterTasks_My { get; set; }
        public RelayCommand FilterTasks_All { get; set; }
        public RelayCommand<EmployeeTask> ViewTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> DeleteTask { get; set; }
        public RelayCommand<EmployeeTask> SelectParentCommand { get; set; }
        public RelayCommand<EmployeeTask> AddResult { get; set; }
        public RelayCommand<EmployeeTask> SelectTaskCommand { get; set; }
        public RelayCommand<ICollection<object>> StoreProcessSelection { get; set; }

        #endregion

        public MainViewModel(IEmployeeProvider employeeProvider, ITaskProvider taskProvider)
        {

            _employees = employeeProvider;
            _tasks = taskProvider;

            InitializeData();
            InitializeCommands();
        }

        private void AddNewTask()
        {
            AddTask addTaskWindow = new AddTask();
            newTask.Owner = _currentEmployee;
            addTaskWindow.Reporter.IgnoreTextChange = true;
            addTaskWindow.Reporter.Text = _currentEmployee.FIO;
            addTaskWindow.Reporter.IgnoreTextChange = false;
            if (addTaskWindow.ShowDialog() == true)
            {
                Console.WriteLine(newTask.Owner.FIO);
                newTask.Reporter = Analytics.FirstOrDefault(i => i.FIO.Equals(newTask.Reporter.FIO));
                newTask.Assignee = Analytics.FirstOrDefault(i => i.FIO.Equals(newTask.Assignee.FIO));
                newTask.Owner = Analytics.FirstOrDefault(i => i.FIO.Equals(newTask.Reporter.FIO));
                _tasks.AddTask(newTask.AsCopy());
                _currentTask.ChildTasks.Add(newTask.AsCopy());
                CurrentTasks.Add(NewTask.AsCopy());
            }
            newTask = new EmployeeTask();
        }

        private void AddSubTask()
        {
            newTask.Owner = _currentEmployee;

            if (SelectedTask != null)
            {
                AddTask addTaskWindow = new AddTask();
                newTask.Owner = _currentEmployee;
                addTaskWindow.Reporter.IgnoreTextChange = true;
                addTaskWindow.Reporter.Text = _currentEmployee.FIO;
                addTaskWindow.Reporter.IgnoreTextChange = false;
                if (addTaskWindow.ShowDialog() == true)
                {
                    SelectedTask.ChildTasks.Add(newTask.AsCopy());
                    CurrentTasks.Add(newTask.AsCopy());
                    _tasks.Commit();
                }
                newTask = new EmployeeTask();

                UpdateTasks();

            }

        }

        private void InitializeData()
        {
            Processes = _tasks.GetProcesses();
            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks(null));
            _currentAnalytic = _employees.GetCurrentAnalytic();
            _currentEmployee = new Employee
            {
                FIO = $"{_currentAnalytic.LastName} {_currentAnalytic.FirstName} {_currentAnalytic.FatherName}",
                AnalyticId = _currentAnalytic.Id
            };
            foreach (Employee empl in _employees.GetEmployees())
            {
                Analytics.Add(empl);
                Analytics_str.Add(empl.FIO);
            }
            _currentTask = _mainTask;
            TaskStack.Push(_mainTask);
            CurrentTasks = new ObservableCollection<EmployeeTask>(_mainTask.ChildTasks);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
        }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            AddNewSubTaskCommand = new RelayCommand(AddSubTask);
            ViewTaskCommand = new RelayCommand<EmployeeTask>(EditTask);
            SelectParentCommand = new RelayCommand<EmployeeTask>(SelectParent);
            BackCommand = new RelayCommand(Back);
            SelectTaskCommand = new RelayCommand<EmployeeTask>(SelectTask);
            DeleteTask = new RelayCommand<EmployeeTask>(DeleteTaskMethod);
            StoreProcessSelection = new RelayCommand<ICollection<object>>(StoreProcesses);
            FilterTasks_My = new RelayCommand(ShowMyTasks);
            FilterTasks_All = new RelayCommand(ShowAllTAsks);
            AddResult = new RelayCommand<EmployeeTask>(AddResultMethod);
        }

        private void AddResultMethod(EmployeeTask task)
        {
            ResultsWindow dialog = new ResultsWindow();
            int participationIndex;
            if (task.Reporter.FIO.Equals(_currentEmployee.FIO))//пользователь инициатор задачи
            {
                participationIndex = 1;
            }
            else if (task.Assignee.FIO.Equals(_currentEmployee.FIO)) //пользователь как ответственное лицо
            {
                participationIndex = 2;
            }
            else //задача не принадлежит пользователю
            {
                MessageBox.Show("Вы не являетесь участником данной задачи и не можете заявить результат", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if(participationIndex==1 && !string.IsNullOrEmpty(task.SupervisorComment))
            {
                dialog.CommentBox.Text = task.SupervisorComment;
            }
            else if (!string.IsNullOrEmpty(task.SupervisorComment))
            {
                dialog.CommentBox.Text = task.EmployeeComment;
            }

            if (dialog.ShowDialog() == true)
            {
                if (participationIndex == 1)
                {
                    task.SupervisorComment = dialog.CommentBox.Text;
                    
                }
                else
                {
                    task.EmployeeComment = dialog.CommentBox.Text;
                }
                dialog.CommentBox.Text = string.Empty;
                _tasks.Commit();
            }
        }

        private void ShowMyTasks()
        {
            _mainTask = new EmployeeTask();
            string fio = $"{_currentAnalytic.LastName} {_currentAnalytic.FirstName} {_currentAnalytic.FatherName}";
            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks().Where(i => i.Assignee.FIO != null && i.Assignee.FIO.Equals(fio) || i.Owner.FIO != null && i.Assignee.FIO.Equals(fio)));
            _currentTask = _mainTask;
            TaskStack.Clear();
            TaskStack.Push(_mainTask);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            UpdateTasks();
        }

        private void ShowAllTAsks()
        {
            _mainTask = new EmployeeTask();
            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks(null));
            _currentTask = _mainTask;
            TaskStack.Clear();
            TaskStack.Push(_mainTask);
            if (_mainTask.ChildTasks.Count > 0)
                SelectedTask = _mainTask.ChildTasks[0];
            UpdateTasks();
        }

        private void StoreProcesses(ICollection<object> obj)
        {
            List<ProcessProxy> _processes = new List<ProcessProxy>();
            foreach (object item in obj)
            {
                if (item is Process)
                {
                    _processes.Add(new ProcessProxy
                    {
                        ProcessId = (item as Process).Id
                    });
                }
            }
            newTask.Processes = _processes;

        }

        private void DeleteSubTasks(EmployeeTask task)
        {
            while (task.ChildTasks.Count > 0)
            {
                DeleteSubTasks(task.ChildTasks[0]);
            }
            CurrentTasks.Remove(task);
            _tasks.Remove(task);
        }

        private void DeleteTaskMethod(EmployeeTask task)
        {
            EmployeeTask deletedTask = null;
            foreach (EmployeeTask _task in CurrentTasks)
            {
                if (_task.Equals(task))
                {
                    deletedTask = _task;
                    break;
                }
            }
            if (deletedTask != null && deletedTask.ChildTasks != null)
            {
                if (deletedTask.ChildTasks.Count > 0)
                {
                    if (MessageBox.Show("Удаляемая задача содержит подзадачи, которые также будут удалены.\r\n Вы уверены?", "Удаление задачи", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        DeleteSubTasks(deletedTask);
                        _currentTask.ChildTasks.Remove(deletedTask);
                        if (_currentTask.ChildTasks.Count < 1)
                        {
                            Back();
                        }
                        UpdateTasks();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DeleteSubTasks(deletedTask);
                    _currentTask.ChildTasks.Remove(deletedTask);
                    if (_currentTask.ChildTasks.Count < 1)
                    {
                        Back();
                    }
                    UpdateTasks();
                }

            }
        }

        private void SelectTask(EmployeeTask task)
        {
            SelectedTask = task;
            RaisePropertyChanged("SelectedTask");
            SelectedProcesses.Clear();
            foreach (ProcessProxy processProxy in SelectedTask.Processes)
            {
                SelectedProcesses.Add(Processes.FirstOrDefault(i => i.Id == processProxy.ProcessId));
            }
        }

        private void Back()
        {
            if (TaskStack.Count > 1)
            {
                TaskStack.Pop();
                SelectParent(TaskStack.Peek());
                TaskStack.Pop();
            }
        }

        private void SelectParent(EmployeeTask task)
        {
            if (task != null && task.ChildTasks.Count > 0)
            {
                TaskStack.Push(task);
                _currentTask = task;
                SelectTask(task.ChildTasks[0]);
                UpdateTasks();
            }
        }

        private void UpdateTasks()
        {
            CurrentTasks.Clear();

            foreach (EmployeeTask childTask in _currentTask.ChildTasks)
            {
                CurrentTasks.Add(childTask);
            }
        }

        private void EditTask(EmployeeTask task)
        {
            EmployeeTask tempTask = task.AsCopy();
            newTask = task;
            AddTask addTaskWindow = new AddTask();
            
            addTaskWindow.ProcessList.SelectedItemsOverride = SelectedTask.Processes.Select(i=>Processes.FirstOrDefault(n=>n.Id==i.ProcessId)).ToList();
            if (addTaskWindow.ShowDialog() == false)
            {
                task.CopyFields(tempTask);
            }
            else
            {
                _tasks.Commit();
                UpdateTasks();
            }
            newTask = new EmployeeTask();
        }
    }
}