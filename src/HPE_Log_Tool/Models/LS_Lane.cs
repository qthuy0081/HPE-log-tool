namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LS_Lane
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LaneID { get; set; }

        public int StationID { get; set; }

        [Required]
        [StringLength(20)]
        public string LaneCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public short DirectionType { get; set; }

        public int LaneType { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public bool IsUsed { get; set; }

        public virtual LS_Station LS_Station { get; set; }
    }
}
