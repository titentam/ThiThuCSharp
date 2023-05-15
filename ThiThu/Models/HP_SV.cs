using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiThu.Models
{
    public class HP_SV
    {
        [Key]
        [Column(Order = 0)]
        public string MaHP { get; set; }
        [Key]
        [Column(Order = 1)]
        public string MaSV { get; set; }
        [Range(0, 10)]
        public double? DiemBT { get; set; }
        [Range(0, 10)]
        public double? DiemGK { get; set; }
        [Range(0, 10)]
        public double? DiemCK { get; set; }
        public DateTime? NgayThi { get; set; }

        [ForeignKey("MaHP")]
        public virtual HocPhan HocPhan { get; set; }
        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; }
    }
}
