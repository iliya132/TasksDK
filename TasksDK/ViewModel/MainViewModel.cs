using System;
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

        /// <summary>
        /// Новая задача. Привязка из окна AddTask
        /// </summary>
        private EmployeeTask newTask = new EmployeeTask();
        public EmployeeTask NewTask { get => newTask; set => newTask = value; }

        #endregion

        #region Commands
        public RelayCommand AddNewTaskCommand { get; set; } 
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
                CurrentTasks.Add(NewTask.AsCopy());
            }
            newTask = new EmployeeTask();
        }

        private void InitializeData()
        {
            CurrentTasks = new ObservableCollection<EmployeeTask>(_tasks.GetTasks());
        }

        private void InitializeCommands()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            ViewTaskCommand = new RelayCommand<EmployeeTask>(ViewTask);
            SelectParentCommand = new RelayCommand<EmployeeTask>(SelectParent);
        }



        private void SelectParent(EmployeeTask task)
        {
            if (task != null)
            {
                Console.WriteLine(task.Name);
                CurrentTasks = new ObservableCollection<EmployeeTask>(task.ChildTasks);
            }
            else
            {
                Console.WriteLine("Task  was null");
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