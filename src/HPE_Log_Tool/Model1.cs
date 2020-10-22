namespace HPE_Log_Tool
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<IN_CheckEtag> IN_CheckEtag { get; set; }
        public virtual DbSet<IN_CheckForceOpen> IN_CheckForceOpen { get; set; }
        public virtual DbSet<IN_CheckPrepaidCard> IN_CheckPrepaidCard { get; set; }
        public virtual DbSet<IN_CheckSmartCard> IN_CheckSmartCard { get; set; }
        public virtual DbSet<LS_Lane> LS_Lane { get; set; }
        public virtual DbSet<LS_Shift> LS_Shift { get; set; }
        public virtual DbSet<LS_Station> LS_Station { get; set; }
        public virtual DbSet<OUT_CheckEtag> OUT_CheckEtag { get; set; }
        public virtual DbSet<OUT_CheckForceOpen> OUT_CheckForceOpen { get; set; }
        public virtual DbSet<OUT_CheckPrepaidCard> OUT_CheckPrepaidCard { get; set; }
        public virtual DbSet<OUT_CheckSmartCard> OUT_CheckSmartCard { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.EtagId)
                .IsUnicode(false);

            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.ImageID)
                .IsUnicode(false);

            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.TicketID)
                .IsUnicode(false);

            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.ETCStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.SyncReturnMsg)
                .IsUnicode(false);

            modelBuilder.Entity<IN_CheckEtag>()
                .Property(e => e.TID)
                .IsUnicode(false);

            modelBuilder.Entity<LS_Station>()
                .HasMany(e => e.LS_Lane)
                .WithRequired(e => e.LS_Station)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.EtagId)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.ReceiptNo)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.ImageID)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.InImageID)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.TicketID)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.ETCStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.SyncReturnMsg)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckEtag>()
                .Property(e => e.TID)
                .IsUnicode(false);

            modelBuilder.Entity<OUT_CheckPrepaidCard>()
                .Property(e => e.IDCard)
                .IsUnicode(false);
        }
    }
}
