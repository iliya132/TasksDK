using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TaskCanvas.xaml
    /// </summary>
    public partial class TaskCanvas : UserControl
    {

        #region ItemsSource

        public IEnumerable<EmployeeTask> ItemsSource
        {
            get { return (IEnumerable<EmployeeTask>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = 
            DependencyProperty.Register("ItemsSource", 
                typeof(IEnumerable<EmployeeTask>), 
                typeof(TaskCanvas), 
                new PropertyMetadata(OnItemsSourcePropertyChanged));
        private EmployeeTask _selectedTask;

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TaskCanvas control = sender as TaskCanvas;
            if (control != null)
                control.OnItemsSourceChanged((IEnumerable<EmployeeTask>)e.OldValue, (IEnumerable<EmployeeTask>)e.NewValue);
        }

        private void OnItemsSourceChanged(IEnumerable<EmployeeTask> oldValue, IEnumerable<EmployeeTask> newValue)
        {

            INotifyCollectionChanged oldValueINotifyPropertyChanged = oldValue as INotifyCollectionChanged;
            if (oldValueINotifyPropertyChanged != null)
            {
                oldValueINotifyPropertyChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
            }
            else
            {
                Update();
            }
            INotifyCollectionChanged newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newValueINotifyCollectionChanged != null)
            {
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
                Update();
            }

        }

        void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Update();
        }

        #endregion

        public TaskCanvas()
        {
            InitializeComponent();
            
        }

        private void Update()
        {
            MainStackPanel.Children.Clear();
            bool selected = false;
            
            foreach (EmployeeTask task in ItemsSource)
            {
                TaskControl taskControl = new TaskControl(task);
                taskControl.Margin = new Thickness(10);
                taskControl.OnClick += TaskControl_OnClick;
                //taskControl.SetBinding(WidthProperty, "ActualWidth");
                MainStackPanel.Children.Add(taskControl);
                if (task.Equals(_selectedTask))
                {
                    taskControl.Select();
                    selected = true;
                }
            }
            if (MainStackPanel.Children.Count > 0 && !selected)
            {
                (MainStackPanel.Children[0] as TaskControl).Select();
            }
        }

        private void TaskControl_OnClick(object sender)
        {
            TaskControl control = sender as TaskControl;
            if (!control.IsSelected)
            {
                foreach (TaskControl taskControl in MainStackPanel.Children)
                {
                    taskControl.UnSelect();
                }
                _selectedTask = control.Task;
                control.Select();
            }
        }
        
    }
}
