using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace baochi_test.Models;

public partial class BaochiTestContext : DbContext
{
    public BaochiTestContext()
    {
    }

    public BaochiTestContext(DbContextOptions<BaochiTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiDang> BaiDangs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASUS\\SERVER1;Initial Catalog=baochi_test;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiDang>(entity =>
        {
            entity.ToTable("BaiDang");

            entity.Property(e => e.HinhAnh).HasMaxLength(250);
            entity.Property(e => e.Ten).HasMaxLength(250);

            entity.HasOne(d => d.IdDanhMucNavigation).WithMany(p => p.BaiDangs)
                .HasForeignKey(d => d.IdDanhMuc)
                .HasConstraintName("FK_BaiDang_DanhMuc");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.ToTable("DanhMuc");

            entity.Property(e => e.MoTa).HasMaxLength(250);
            entity.Property(e => e.Ten).HasMaxLength(250);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TaiKhoan1);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TaiKhoan1)
                .HasMaxLength(50)
                .HasColumnName("TaiKhoan");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
