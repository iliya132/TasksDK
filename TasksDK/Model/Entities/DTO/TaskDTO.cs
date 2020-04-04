using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksDK.Model.Entities.DTO
{
    [Table("Tasks")]
    public class TaskDTO
    {
        public int Id { get; set; }
        public List<Process> Processes { get; set; }
        public string Name { get; set; }
        public List<TaskDTO> ChildTasks { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        [ForeignKey("Employee")]
        public Employee Owner { get; set; }
        [ForeignKey("Employee")]
        public Employee Assignee { get; set; }
        [ForeignKey("Employee")]
        public Employee Reporter { get; set; }
        public string Comment { get; set; }
        public int Weight { get; set; }
        public string EmployeeComment { get; set; }
        public string SupervisorComment { get; set; }
        public int EmployeeDonePercent { get; set; }
        public int SupervisorDonePercent { get; set; }
        public string AwaitedResult { get; set; }
        public string Meter { get; set; }
    }
}
