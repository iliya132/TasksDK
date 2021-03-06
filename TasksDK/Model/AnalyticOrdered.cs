﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksDK.Model
{
    public class AnalyticOrdered : INotifyPropertyChanged
    {
        public Analytic Analytic;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string FIO { get; set; }
        public string userName { get; set; }
        private bool selected = false;
        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public string FirstStructure { get; set; }
        public string SecondStructure { get; set; }
        public string ThirdStructure { get; set; }
        public string FourStructure { get; set; }
        public AnalyticOrdered(Analytic _analytic)
        {
            Analytic = _analytic;

            FIO = $"{Analytic.LastName} {Analytic.FirstName} {Analytic.FatherName}";
            userName = Analytic.userName;

            //Первое подразделение всегда департамент
            FirstStructure = Analytic.Departments.Name;

            //если всё, кроме департамента отсутствует
            if (Analytic.UpravlenieTableId == 4 && Analytic.DirectionsId == 2 && Analytic.OtdelTableId == 19)
            {
                return;
            }

            //Определили второе подразделение (дирекция(если не отсутствует), управление(если нет дирекции), отдел(если нет управления)
            SecondStructure = Analytic.DirectionsId != 2 ? Analytic.Directions.Name :
                Analytic.UpravlenieTableId != 6 ? Analytic.Upravlenie.Name : Analytic.Otdel.Name;

            //если дирекция и управление отсутствуют, значит отдел записан во второе подразделение, и дальше ничего делать не надо
            if (Analytic.DirectionsId == 2 && Analytic.UpravlenieTableId == 6) return;

            //Управление(если не отсутствует), отдел (если отсутствует управление)
            ThirdStructure = Analytic.UpravlenieTableId != 6 ? Analytic.Upravlenie.Name : Analytic.Otdel.Name;
            if (Analytic.UpravlenieTableId == 6) return;

            //Отдел (когда структура полная)
            FourStructure = Analytic.Otdel.Name;
        }
        public override string ToString()
        {
            return $"{Analytic.Id}. {Analytic.FirstName} {Analytic.LastName} {Analytic.FatherName}";
        }
    }
}
