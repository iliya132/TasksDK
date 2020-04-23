using System.Collections.Generic;
using System.Collections.ObjectModel;
using TasksDK.Model;
using TasksDK.Model.Entities;

namespace TasksDK.Interfaces
{
    public interface IEmployeeProvider
    {
        List<Employee> GetEmployees();
        Analytic GetCurrentAnalytic();
        ObservableCollection<Analytic> GetMyAnalyticsData(Analytic currentUser);
    }
}
