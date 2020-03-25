using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TasksDK.Model.Entities;

namespace TasksDK.View.Controls
{
    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        private EmployeeTask _task = new EmployeeTask();
        public EmployeeTask Task { get=> _task; set=> _task=value; }
        public TaskControl(EmployeeTask task)
        {
            Task = task;
            InitializeComponent();
            NameTextBlock.Text = Task.Name;
            CurrentProgressBar.Value = Task.SupervisorDonePercent;
            NameTextBlock.Text = NameTextBlock.Text;
            ChildCountTextBlock.Text = $"{Task.ChildTasks.Count}";
        }
    }
}
