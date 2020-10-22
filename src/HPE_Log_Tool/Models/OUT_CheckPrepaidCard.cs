namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OUT_CheckPrepaidCard
    {
        [Key]
        public Guid OutCheckPrePaidCardID { get; set; }

        [Required]
        [StringLength(30)]
        public string TransactionID { get; set; }

        [Required]
        [StringLength(20)]
        public string SmartCardID { get; set; }

        [StringLength(32)]
        public string IDCard { get; set; }

        [StringLength(50)]
        public string ReceiptNo { get; set; }

        public DateTime CheckDate { get; set; }

        public int BeginBalance { get; set; }

        public int TollFee { get; set; }

        public int ChargeAmount { get; set; }

        public int CashAmount { get; set; }

        public int Balance { get; set; }

        public int TopUpAmount { get; set; }

        public int? TicketTypeID { get; set; }

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

        [StringLength(15)]
        public string RegisPlateNumber { get; set; }

        public short? RecogResultType { get; set; }

        public short? TransactionStatus { get; set; }

        public short? PrecheckStatus { get; set; }

        public short? PreSupervisionStatus { get; set; }

        public short? SupervisionStatus { get; set; }

        public int? ErrorID { get; set; }

        [StringLength(15)]
        public string EntryPlateNumber { get; set; }

        public int? EntryLaneID { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        public int? ChargeType { get; set; }

        public bool? IsOnlineCheck { get; set; }

        public int? ChargeSequenceID { get; set; }

        public int? TopUpSequenceID { get; set; }

        [StringLength(50)]
        public string SecurityBlock1 { get; set; }

        [StringLength(50)]
        public string SecurityBlock2 { get; set; }

        [StringLength(50)]
        public string SecurityBlock3 { get; set; }

        [StringLength(16)]
        public string F1 { get; set; }

        [StringLength(16)]
        public string F2 { get; set; }
    }
}
