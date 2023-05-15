using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ThiThu.Models;
using System.Data.Entity;
using System.Globalization;
using System.Windows.Forms;

namespace ThiThu.DAL
{
    public class SinhVienDAL
    {
        private static SinhVienDAL instance;

        private SinhVienDAL() { }

        public static SinhVienDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SinhVienDAL();
                }
                return instance;
            }
        }
        public List<SinhVien> GetAllSV()
        {
            using (CkDb db = new CkDb())
            {
                return db.SinhViens.Include(x=> x.HocPhans).ToList();
                //return db.SinhViens.Include("HocPhans").ToList();
            }
        }
        public List<string> GetAllLSH()
        {
            using (CkDb db = new CkDb())
            {
                return db.SinhViens.Select(x=>x.LopSh).ToList();
            }
        }
        public SinhVien GetSinhVienById(string id)
        {
            using (CkDb db = new CkDb())
            {
                return db.SinhViens.Find(id);
            }
            // khi truy cap obj nay khoi using{} thi obj entity state này là 'detached' 
        }
        public HP_SV GetHocPhan(string maSv, string maHp)
        {
            using (CkDb db = new CkDb())
            {
                return db.HP_SVs.Where(x=>x.MaSV == maSv&& x.MaHP == maHp).SingleOrDefault();
            }
        }
        public bool AddSV(SinhVien sv)
        {
            using (CkDb db = new CkDb())
            {
                try
                {
                    db.SinhViens.Add(sv);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
                     
            }
        }
        public bool AddHocPhan(HP_SV hp)
        {
            using (CkDb db = new CkDb())
            {
                try
                {
                    db.HP_SVs.Add(hp);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
        public bool DelHocPhans(List<HP_SV> list)
        {
            using(CkDb db = new CkDb())
            {
                List<HP_SV> listDel = new List<HP_SV>();
                try
                {
                    foreach (HP_SV item in list)
                    {
                        var hp_sv = GetHocPhan(item.MaSV, item.MaHP);
                        db.Entry<HP_SV>(hp_sv).State = EntityState.Deleted;
                        listDel.Add(hp_sv); 
                    }
                    db.HP_SVs.RemoveRange(listDel.ToArray());   
                    db.SaveChanges();
                    return true;    
                }
                catch (Exception)
                {
                    return false;
                }      
            }
        }
    }
}
