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
        public EmployeeTask Task
        {
            get 
            { 
                return (EmployeeTask)GetValue(TaskProperty); 
            }
            set 
            { 
                SetValue(TaskProperty, value); 
            }
        }
        
        public delegate void ClickEventHandler(object sender);
        public event ClickEventHandler OnClick;
        protected virtual void RaiseClickEvent()
        {
            OnClick?.Invoke(this);
        }
        public void UnSelect()
        {
            SelectBox.Visibility = Visibility.Hidden;
            IsSelected = false;
        }
        public bool IsSelected { get; set; }
        public void Select()
        {
            SelectBox.Visibility = Visibility.Visible;
            IsSelected = true;
        }

        public static DependencyProperty TaskProperty = DependencyProperty.Register("Task", typeof(EmployeeTask), typeof(TaskControl), new PropertyMetadata(new EmployeeTask()));
        public TaskControl(EmployeeTask task)
        {
            Task = task;
            InitializeComponent();
            NameTextBlock.Text = Task.Name;
            CurrentProgressBar.Value = Task.SupervisorDonePercent;
            NameTextBlock.Text = NameTextBlock.Text;

            ChildCountTextBlock.Text = $"{Task.ChildTasks.Count}";
            menu1.CommandParameter = Task;
            MoreMenuItem.CommandParameter = Task;
            DeleteMenuItem.CommandParameter = Task;

        }
        public TaskControl() : this(new EmployeeTask())
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
        }
    }
   
}
