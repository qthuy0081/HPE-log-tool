namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LS_Shift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShiftID { get; set; }

        [Required]
        [StringLength(20)]
        public string ShiftCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(8)]
        public string EndTime { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public bool IsUsed { get; set; }
    }
}
