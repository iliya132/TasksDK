namespace TasksDK.Model.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TimeSheetContext : DbContext
    {
        public TimeSheetContext()
            : base("name=TimeSheetContext")
        {
        }

        public virtual DbSet<TimeSheetProcess> Process { get; set; }
        public virtual DbSet<Analytic> Analytics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
