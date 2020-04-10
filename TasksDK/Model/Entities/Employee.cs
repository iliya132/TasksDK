using System.ComponentModel;

namespace TasksDK.Model.Entities
{
    public class Employee :INotifyPropertyChanged
    {
        public int Id{ get; set; }
        private string _fio = string.Empty;
        public string FIO { get=>_fio; set
            {
                _fio = value;
                OnPropertyChanged("FIO");
            } }
        public int AnalyticId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string Property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
    }
}
