using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksDK.Model.Entities.DTO
{
    [Table("ProcessProxy")]
    public class ProcessProxy
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
    }
}
