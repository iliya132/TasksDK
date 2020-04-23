namespace TasksDK.Model.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TasksDK.Model.TimeSheet;

    public partial class TimeSheetContext : DbContext
    {
        public TimeSheetContext()
            : base("name=TimeSheetContext")
        {
        }


        public DbSet<Departments> Departments { get; set; }
        public DbSet<Directions> DirectionsSet { get; set; }
        public DbSet<Otdel> OtdelSet { get; set; }
        public DbSet<Positions> PositionsSet { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<Upravlenie> UpravlenieSet { get; set; }
        public virtual DbSet<TimeSheetProcess> Process { get; set; }
        public virtual DbSet<Analytic> Analytics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
