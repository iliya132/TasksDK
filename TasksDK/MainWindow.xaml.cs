using System;
using System.Collections.Generic;
using System.IO;
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
using TaskDK.Services;

namespace TasksDK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CurrentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateService.CheckForUpdate();
        }

        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists($"{Environment.ExpandEnvironmentVariables("%appdata%")}\\Tasks"))
            {
                Directory.CreateDirectory($"{Environment.ExpandEnvironmentVariables("%appdata%")}\\Tasks");
            }
            File.Copy("\\\\moscow\\hdfs\\WORK\\Архив необычных операций\\ОРППА\\Programs\\Tasks\\Help\\TaskHelp.chm", $"{Environment.ExpandEnvironmentVariables("%appdata%")}\\Tasks\\TaskHelp.chm", true);
            System.Diagnostics.Process.Start($"{Environment.ExpandEnvironmentVariables("%appdata%")}\\Tasks\\TaskHelp.chm");
        }
    }
}
