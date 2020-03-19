using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<EmployeeTask> ItemsSource
        {
            get
            {
                return (ObservableCollection<EmployeeTask>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<EmployeeTask>), typeof(TaskCanvas), new UIPropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TaskCanvas control = (TaskCanvas)d;
            control.Update();
        }


        public TaskCanvas()
        {
            InitializeComponent();
        }

        private void ItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            MainStackPanel.Children.Clear();
            foreach (EmployeeTask task in ItemsSource)
            {
                TaskControl taskControl = new TaskControl(task);
                taskControl.Margin = new Thickness(10);
                MainStackPanel.Children.Add(taskControl);
            }
        }

        

    }
}
