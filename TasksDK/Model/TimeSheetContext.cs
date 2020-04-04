namespace TasksDK.Model
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

        public virtual DbSet<Analytic> Analytic { get; set; }
        public virtual DbSet<Process> Process { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
