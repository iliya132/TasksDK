using System;
using System.Collections.Generic;
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
    }
}
