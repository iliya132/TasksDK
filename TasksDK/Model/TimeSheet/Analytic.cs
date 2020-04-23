namespace TasksDK.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using TasksDK.Model.TimeSheet;

    [Table("Analytic")]
    public partial class Analytic
    {
        public int Id { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FatherName { get; set; }

        public int DepartmentsId { get; set; }

        public int DirectionsId { get; set; }

        public int UpravlenieTableId { get; set; }

        public int OtdelTableId { get; set; }

        public int PositionsId { get; set; }

        public int RoleTableId { get; set; }
        [ForeignKey(nameof(DepartmentsId))]
        public virtual Departments Departments { get; set; }
        [ForeignKey(nameof(DirectionsId))]
        public virtual Directions Directions { get; set; }
        [ForeignKey(nameof(UpravlenieTableId))]
        public virtual Upravlenie Upravlenie { get; set; }
        [ForeignKey(nameof(OtdelTableId))]
        public virtual Otdel Otdel { get; set; }
        [ForeignKey(nameof(PositionsId))]
        public virtual Positions Positions { get; set; }
        [ForeignKey(nameof(RoleTableId))]
        public virtual Role Role { get; set; }
    }
}
