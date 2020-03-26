using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;
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

        private Stack<EmployeeTask> _taskStack = new Stack<EmployeeTask>();
        public Stack<EmployeeTask> TaskStack { get => _taskStack; set => _taskStack = value; }


        /// <summary>
        /// Новая задача. Привязка из окна AddTask
        /// </summary>
        private EmployeeTask newTask = new EmployeeTask();
        public EmployeeTask NewTask { get => newTask; set => newTask = value; }

        #endregion

        #region Commands
        public RelayCommand AddNewTaskCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand<EmployeeTask> ViewTaskCommand { get; set; }
        public RelayCommand<EmployeeTask> SelectParentCommand { get; set; }

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

        private void InitializeData()
        {

            _mainTask.ChildTasks = new List<EmployeeTask>(_tasks.GetTasks());
            TaskStack.Push(_mainTask);
            CurrentTasks = new ObservableCollection<EmployeeTask>(_mainTask.ChildTasks);
        }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            ViewTaskCommand = new RelayCommand<EmployeeTask>(ViewTask);
            SelectParentCommand = new RelayCommand<EmployeeTask>(SelectParent);
            BackCommand = new RelayCommand(Back);
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

                CurrentTasks.Clear();
                foreach (EmployeeTask childTask in task.ChildTasks)
                {
                    CurrentTasks.Add(childTask);
                }
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
            newTask = new EmployeeTask();
        }
    }
}