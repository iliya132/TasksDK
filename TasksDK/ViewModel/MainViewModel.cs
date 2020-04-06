using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// ������� ������. �������� �� ITaskProvider.
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
        public ObservableCollection<Employee> Analytics { get=> _analytics; set=> _analytics=value; }
        private ObservableCollection<string> _analytics_str = new ObservableCollection<string>();
        public ObservableCollection<string> Analytics_str { get => _analytics_str; set => _analytics_str = value; }


        /// <summary>
        /// ����� ������. �������� �� ���� AddTask
        /// </summary>
        private EmployeeTask newTask = new EmployeeTask();
        public EmployeeTask NewTask { get => newTask; set => newTask = value; }
        private EmployeeTask _selectedTask;
        public EmployeeTask SelectedTask { get => _selectedTask; set => _selectedTask = value; }
        private ObservableCollection<Process> _selectedProcesses = new ObservableCollection<Process>();
        public ObservableCollection<Process> SelectedProcesses { get => _selectedProcesses; set => _selectedProcesses=value; }

        #endregion

        #region Commands
        public RelayCommand AddNewTaskCommand { get; set; }
        public RelayCommand AddNewSubTaskCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand<EmployeeTask> ViewTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> DeleteTask { get; set; }
        public RelayCommand<EmployeeTask> SelectParentCommand { get; set; }
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
            if (addTaskWindow.ShowDialog() == true)
            {
                _tasks.AddTask(newTask.AsCopy());
                _currentTask.ChildTasks.Add(newTask.AsCopy());
                CurrentTasks.Add(NewTask.AsCopy());
            }
            newTask = new EmployeeTask();
        }

        private void AddSubTask()
        {
            if (SelectedTask != null)
            {
                AddTask addTaskWindow = new AddTask();
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
            foreach(Employee empl in _employees.GetEmployees())
            {
                Analytics.Add(empl);
                Analytics_str.Add(empl.FIO);
            }
            _currentTask = _mainTask;
            TaskStack.Push(_mainTask);
            CurrentTasks = new ObservableCollection<EmployeeTask>(_mainTask.ChildTasks);
            if(_mainTask.ChildTasks.Count>0)
                SelectedTask = _mainTask.ChildTasks[0];
        }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            AddNewSubTaskCommand = new RelayCommand(AddSubTask);
            ViewTaskCommand = new RelayCommand<EmployeeTask>(ViewTask);
            SelectParentCommand = new RelayCommand<EmployeeTask>(SelectParent);
            BackCommand = new RelayCommand(Back);
            SelectTaskCommand = new RelayCommand<EmployeeTask>(SelectTask);
            DeleteTask = new RelayCommand<EmployeeTask>(DeleteTaskMethod);
            StoreProcessSelection = new RelayCommand<ICollection<object>>(StoreProcesses);
        }

        private void StoreProcesses(ICollection<object> obj)
        {
            List<ProcessProxy> _processes = new List<ProcessProxy>();
            foreach(object item in obj)
            {
                if(item is Process)
                {
                    _processes.Add(new ProcessProxy
                    {
                        ProcessId=(item as Process).Id
                    });
                }
            }
            newTask.Processes = _processes;
            
        }

        private void DeleteTaskMethod(EmployeeTask task)
        {
            EmployeeTask deletedTask = null;
            foreach(EmployeeTask _task in CurrentTasks)
            {
                if (_task.Equals(task))
                {
                    deletedTask = _task;
                    break;
                }
            }
            if (deletedTask != null)
            {
                CurrentTasks.Remove(deletedTask);
                _tasks.Remove(deletedTask);
                _currentTask.ChildTasks.Remove(deletedTask);
                if (_currentTask.ChildTasks.Count < 1)
                {
                    Back();
                }
                UpdateTasks();
            }
        }

        private void SelectTask(EmployeeTask task)
        {
            SelectedTask = task;
            RaisePropertyChanged("SelectedTask");
            SelectedProcesses.Clear();
            foreach(ProcessProxy processProxy in SelectedTask.Processes)
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
        private void ViewTask(EmployeeTask task)
        {
            EmployeeTask tempTask = task.AsCopy();
            newTask = task;
            AddTask addTaskWindow = new AddTask();
            if (addTaskWindow.ShowDialog() == false)
            {
                task.CopyFields(tempTask);
            }
            else
            {
                UpdateTasks();
            }
            newTask = new EmployeeTask();
        }
    }
}