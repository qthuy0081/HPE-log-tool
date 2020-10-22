namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_CheckForceOpen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_CheckForceOpen()
        {
            OUT_CheckForceOpen = new HashSet<OUT_CheckForceOpen>();
        }

        [Key]
        public Guid InCheckForceOpenID { get; set; }

        [Required]
        [StringLength(30)]
        public string TransactionID { get; set; }

        [Required]
        [StringLength(20)]
        public string TicketID { get; set; }

        public DateTime CheckDate { get; set; }

        public short ForceOpenType { get; set; }

        public int? VehicleTypeID { get; set; }

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

        public Guid? WIMID { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        public bool? IsVehicleInfoManual { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OUT_CheckForceOpen> OUT_CheckForceOpen { get; set; }
    }
}
