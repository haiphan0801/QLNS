﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanlynhansuEntities : DbContext
    {
        public QuanlynhansuEntities()
            : base("name=QuanlynhansuEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_BANGCONG> tb_BANGCONG { get; set; }
        public virtual DbSet<tb_BOPHAN> tb_BOPHAN { get; set; }
        public virtual DbSet<tb_CHUCVU> tb_CHUCVU { get; set; }
        public virtual DbSet<tb_CONGTY> tb_CONGTY { get; set; }
        public virtual DbSet<tb_DANTOC> tb_DANTOC { get; set; }
        public virtual DbSet<tb_HOPDONG> tb_HOPDONG { get; set; }
        public virtual DbSet<tb_KHENTHUONG_KYLUAT> tb_KHENTHUONG_KYLUAT { get; set; }
        public virtual DbSet<tb_LOAICA> tb_LOAICA { get; set; }
        public virtual DbSet<tb_LOAICONG> tb_LOAICONG { get; set; }
        public virtual DbSet<tb_NHANVIEN> tb_NHANVIEN { get; set; }
        public virtual DbSet<tb_PHONGBAN> tb_PHONGBAN { get; set; }
        public virtual DbSet<tb_TANGCA> tb_TANGCA { get; set; }
        public virtual DbSet<tb_TONGIAO> tb_TONGIAO { get; set; }
        public virtual DbSet<tb_TRINHDO> tb_TRINHDO { get; set; }
        public virtual DbSet<tb_UNGLUONG> tb_UNGLUONG { get; set; }
    }
}
