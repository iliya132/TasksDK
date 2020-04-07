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
using System.Windows.Shapes;

namespace TasksDK.View
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private string ErrorMessage = string.Empty;
        public AddTask()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateModel())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show($"Некоторые данные заполнены неверно:\r\n{ErrorMessage}", "Ошибка валидации" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateModel()
        {
            ErrorMessage = string.Empty;
            bool _isValid = true;
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                ErrorMessage += "Не указано название задачи.\r\n";
                _isValid = false;
            }
            if (string.IsNullOrWhiteSpace(AwaitedResultTextBox.Text))
            {
                ErrorMessage += "Не указан ожидаемый результат.\r\n";
                _isValid = false;
            }
            if (string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                ErrorMessage += "Не указано описание задачи(поле комментарий).\r\n";
                _isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Reporter.Text))
            {
                ErrorMessage += "Не указан инициатор задачи.\r\n";
                _isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Assignee.Text))
            {
                ErrorMessage += "Не указан ответственный по задаче.\r\n";
                _isValid = false;
            }
            if (!Reporter.ItemsSource.Any(i => i.Equals(Reporter.Text)))
            {
                ErrorMessage += "Указанный исполнитель отсутствует в БД. Пожалуйста выберите сотрудника из выпадающего списка.\r\n";
                _isValid = false;
            }
            if (!Assignee.ItemsSource.Any(i => i.Equals(Assignee.Text)))
            {
                ErrorMessage += "Указанный ответственный сотрудник отсутствует в БД. Пожалуйста выберите сотрудника из выпадающего списка.\r\n";
                _isValid = false;
            }
            if (string.IsNullOrWhiteSpace(MeterBox.Text))
            {
                ErrorMessage += "Не указан измеритель.\r\n";
                _isValid = false;
            }

            return _isValid;
        }
    }
}
