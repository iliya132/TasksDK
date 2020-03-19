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
        EmployeeTask _task;
        public TaskControl(EmployeeTask task)
        {
            _task = task;
            InitializeComponent();
            NameTextBlock.Text = _task.Name;
            NameTextBlock.Text = $"{NameTextBlock.Text} ({_task.SupervisorDonePercent}%)";
            CommentTextBlock.Text = _task.Comment;
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                CustomizeTextBlock_ContextMenu.Visibility = Visibility.Visible;
                CustomizeTextBlock_ContextMenu.IsOpen = true;
            }
            else
            {
                CustomizeTextBlock_ContextMenu.Visibility = Visibility.Collapsed;
                CustomizeTextBlock_ContextMenu.IsOpen = false;
            }

        }

        private void CustomizeTextBlock_ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            CustomizeTextBlock_ContextMenu.Visibility = Visibility.Collapsed;
        }
    }
}
