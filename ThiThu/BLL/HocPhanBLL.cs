using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThu.DAL;
using ThiThu.Models;

namespace ThiThu.BLL
{
    public class HocPhanBLL
    {
        private static HocPhanBLL instance;

        private HocPhanBLL() { }

        public static HocPhanBLL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HocPhanBLL();
                }
                return instance;
            }
        }

        public List<HocPhan> GetAllHocPhan()
        {
            return HocPhanDAL.Instance.GetAllHocPhan();
           
        }
        public HocPhan GetHocPhanById(string id)
        {
            return HocPhanDAL.Instance.GetHocPhanById(id);
        }
    }
}
