﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEP21.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SEP24Team5Entities : DbContext
    {
        public SEP24Team5Entities()
            : base("name=SEP24Team5Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<DangKyHoatDong> DangKyHoatDongs { get; set; }
        public virtual DbSet<DatThanhTich> DatThanhTiches { get; set; }
        public virtual DbSet<DoanHoiKhoa> DoanHoiKhoas { get; set; }
        public virtual DbSet<GioiThieu> GioiThieux { get; set; }
        public virtual DbSet<HoatDong> HoatDongs { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<LoaiBaiViet> LoaiBaiViets { get; set; }
        public virtual DbSet<LoaiTuyenDung> LoaiTuyenDungs { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<LoginAdmin> LoginAdmins { get; set; }
        public virtual DbSet<NCKH> NCKHs { get; set; }
        public virtual DbSet<NguoiThucHienNCKH> NguoiThucHienNCKHs { get; set; }
        public virtual DbSet<NhanVienKhoa> NhanVienKhoas { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThanhTich> ThanhTiches { get; set; }
        public virtual DbSet<TuyenDung> TuyenDungs { get; set; }
    }
}
