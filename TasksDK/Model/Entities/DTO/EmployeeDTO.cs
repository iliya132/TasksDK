using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksDK.Model.Entities.DTO
{
    [Table("Employee")]
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FIO { get; set; }
    }
}
