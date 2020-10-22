namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OUT_CheckEtag
    {
        [Key]
        [Column(Order = 0)]
        public Guid OutCheckEtagID { get; set; }

        [Required]
        [StringLength(24)]
        public string EtagId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CheckDate { get; set; }

        public DateTime InCheckDate { get; set; }

        public int CheckTime { get; set; }

        [StringLength(50)]
        public string ReceiptNo { get; set; }

        public int ChargeAmount { get; set; }

        public int TicketTypeID { get; set; }

        public int VehicleTypeID { get; set; }

        public int RouteID { get; set; }

        public int EmployeeID { get; set; }

        public int LaneID { get; set; }

        public int? InLaneID { get; set; }

        public int ShiftID { get; set; }

        public int StationID { get; set; }

        public int? InStationID { get; set; }

        public short? PlateType { get; set; }

        [StringLength(12)]
        public string RegisPlate { get; set; }

        [StringLength(12)]
        public string RecogPlate { get; set; }

        [StringLength(15)]
        public string InRecogPlate { get; set; }

        public short? RecogResultType { get; set; }

        public bool? IsIntelligentVerified { get; set; }

        public short? IntelVerifyResult { get; set; }

        [Required]
        [StringLength(25)]
        public string ImageID { get; set; }

        [StringLength(25)]
        public string InImageID { get; set; }

        public short? ImageType { get; set; }

        public short PeriodTicket { get; set; }

        public short? SupervisionStatus { get; set; }

        public short? PreSupervisionStatus { get; set; }

        public short? F0 { get; set; }

        public short? F1 { get; set; }

        public short? F2 { get; set; }

        public int? FP { get; set; }

        public int? FC { get; set; }

        public short TransactionStatus { get; set; }

        [StringLength(20)]
        public string TicketID { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CommitDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        [Required]
        [StringLength(1)]
        public string ETCStatus { get; set; }

        public int PriceTicketType { get; set; }

        public short? SyncStatus { get; set; }

        public short SyncFeBe { get; set; }

        [StringLength(500)]
        public string SyncReturnMsg { get; set; }

        public int IsOnlineCheck { get; set; }

        [StringLength(48)]
        public string TID { get; set; }

        public int? ErrorID { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
    }
}
