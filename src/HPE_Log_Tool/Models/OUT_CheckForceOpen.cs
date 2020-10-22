namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OUT_CheckForceOpen
    {
        [Key]
        public Guid OutCheckForceOpenID { get; set; }

        [Required]
        [StringLength(30)]
        public string TransactionID { get; set; }

        [Required]
        [StringLength(20)]
        public string TicketID { get; set; }

        public Guid? InCheckForceOpenID { get; set; }

        public Guid? InCheckForceOpenIDManual { get; set; }

        public DateTime CheckDate { get; set; }

        public short ForceOpenType { get; set; }

        public int? VehicleTypeID { get; set; }

        public int? RouteID { get; set; }

        public int? EmployeeID { get; set; }

        public int? ShiftID { get; set; }

        public int? LaneID { get; set; }

        public int? StationID { get; set; }

        [StringLength(50)]
        public string ImageID { get; set; }

        [StringLength(15)]
        public string RecogPlateNumber { get; set; }

        public short? RecogResultType { get; set; }

        public short? TransactionStatus { get; set; }

        public short? PrecheckStatus { get; set; }

        public short? PreSupervisionStatus { get; set; }

        public short? SupervisionStatus { get; set; }

        public int? ErrorID { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        [StringLength(50)]
        public string EntryImageID { get; set; }

        public virtual IN_CheckForceOpen IN_CheckForceOpen { get; set; }
    }
}
