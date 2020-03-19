using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDK.Interfaces;
using TasksDK.Model.Entities;

namespace TasksDK.Model.DataProviders
{
    public class InMemoryEmployeeProvider : IEmployeeProvider
    {
        public List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    FIO = "Лебедев И.В."
                },new Employee
                {
                    Id = 2,
                    FIO = "Пупкин С.В."
                },new Employee
                {
                    Id = 1,
                    FIO = "Кожемякин П.П."
                },new Employee
                {
                    Id = 1,
                    FIO = "Родригез И.В."
                }
            };
        }
    }
}
