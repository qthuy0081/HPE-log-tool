namespace HPE_Log_Tool
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LS_Station
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LS_Station()
        {
            LS_Lane = new HashSet<LS_Lane>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StationID { get; set; }

        [Required]
        [StringLength(20)]
        public string StationCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int CompanyID { get; set; }

        public int StationType { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Telephone { get; set; }

        [StringLength(100)]
        public string Fax { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public bool IsUsed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LS_Lane> LS_Lane { get; set; }
    }
}
