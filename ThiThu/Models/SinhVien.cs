using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiThu.Models
{
    public class SinhVien
    {
        public SinhVien()
        {
            HocPhans = new HashSet<HP_SV>();
        }
        [Key]
        public string MaSV { get; set; }
        [StringLength(50)]

        public string TenSV { get; set; }
        public string LopSh { get; set; }
        public bool GioiTinh { get; set; }
        public virtual ICollection<HP_SV> HocPhans { get; set; }

    }
}
