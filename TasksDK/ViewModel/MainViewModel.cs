using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;

namespace TasksDK.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private IEmployeeProvider _employees;
        private ITaskProvider _tasks;

        #region currentValues
        private ObservableCollection<EmployeeTask> _currentTasks = new ObservableCollection<EmployeeTask>();
        public ObservableCollection<EmployeeTask> CurrentTasks { get => _currentTasks; set => _currentTasks = value; }

        #endregion

        public MainViewModel(IEmployeeProvider employeeProvider, ITaskProvider taskProvider)
        {
            _employees = employeeProvider;
            _tasks = taskProvider;
            for (int i = 0; i < 3; i++)
            {
                foreach(EmployeeTask task in _tasks.GetTasks())
                {
                    CurrentTasks.Add(task);
                }
            }

        }
    }
}