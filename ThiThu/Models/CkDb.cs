using System;
using System.Data.Entity;
using System.Linq;

namespace ThiThu.Models
{
    public class CkDb : DbContext
    {
        public CkDb()
            : base("name=CkDb")
        {
        }

        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<HP_SV> HP_SVs { get; set; }

        
    }
}