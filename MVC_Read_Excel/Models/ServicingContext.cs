using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVC_Read_Excel.Models
{
    public partial class ServicingContext : DbContext
    {
        public ServicingContext()
        {
        }

        public ServicingContext(DbContextOptions<ServicingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerResponseDetail> CustomerResponseDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=Servicing;User Id=sa;Password=MyPass@word");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerResponseDetail>(entity =>
            {
                entity.HasKey(e => e.ResponseId)
                    .HasName("PK__Customer__1AAA646CA154A82E");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ComplaintDate).HasColumnType("datetime");

                entity.Property(e => e.ComplaintDetails)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ComplaintType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fees).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RepairDetails)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ResolveDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceEngineerName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
