namespace TasksDK.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Process")]
    public partial class Process
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("procName")]
        public string Name { get; set; }

        public string Comment { get; set; }

        public bool? CommentNeeded { get; set; }

        public int Block_id { get; set; }

        public int SubBlockId { get; set; }

        public int ProcessType_id { get; set; }

        public int Result_id { get; set; }
    }
}
