using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;

namespace TasksDK.Model.DataProviders
{
    public class EFEmployeeProvider : IEmployeeProvider
    {
        TimeSheetContext _context = new TimeSheetContext();

        public Analytic GetCurrentAnalytic()
        {
            string _userId = Environment.UserName.ToUpper();
            return _context.Analytics.FirstOrDefault(i => i.userName.ToUpper().Equals(_userId));
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> _employees = new List<Employee>();
            foreach(Analytic analytic in _context.Analytics)
            {
                _employees.Add(new Employee
                {
                    FIO = $"{analytic.LastName} {analytic.FirstName} {analytic.FatherName}",
                    AnalyticId = analytic.Id
                });
            }
            return _employees;
        }

        /// <summary>
        /// Получить список сотрудников в подчинении
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public ObservableCollection<Analytic> GetMyAnalyticsData(Analytic currentUser)
        {
            ObservableCollection<Analytic> analytics = new ObservableCollection<Analytic>();
            switch (currentUser.RoleTableId)
            {
                case (1):
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.Where(i => i.DepartmentsId == currentUser.DepartmentsId).ToArray());
                    break;
                case (2):
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.Where(i => i.DirectionsId == currentUser.DirectionsId).ToArray());
                    break;
                case (3):
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.Where(i => i.UpravlenieTableId == currentUser.UpravlenieTableId).ToArray());
                    break;
                case (4):
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.Where(i => i.OtdelTableId == currentUser.OtdelTableId).ToArray());
                    break;
                case (5):
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.ToArray());
                    break;
                default:
                    analytics = new ObservableCollection<Analytic>(_context.Analytics.Where(i => i.Id == currentUser.Id).ToArray());
                    break;
            }
            return analytics;
        }
    }
}
