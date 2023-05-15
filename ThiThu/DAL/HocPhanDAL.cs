using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiThu.Models;

namespace ThiThu.DAL
{
    public class HocPhanDAL
    {
        private static HocPhanDAL instance;

        private HocPhanDAL() { }

        public static HocPhanDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HocPhanDAL();
                }
                return instance;
            }
        }

        public List<HocPhan> GetAllHocPhan()
        {
            CkDb db = new CkDb();
            return db.HocPhans.ToList();
        }
        public HocPhan GetHocPhanById(string id)
        {
            using (CkDb db = new CkDb())
            {
                return db.HocPhans.Find(id);
            }
        }
    }
}
