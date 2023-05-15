using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiThu.Models
{
    public class HocPhan
    {
        public HocPhan()
        {
            SinhViens = new HashSet<HP_SV>();
        }
        [Key]
        public string MaHP { get; set; }
        [StringLength(50)]
        public string TenHP { get; set; }
        public virtual ICollection<HP_SV> SinhViens { get; set; }

        public override string ToString()
        {
            return this.TenHP;
        }
    }
}
