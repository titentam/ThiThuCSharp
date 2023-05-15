using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiThu.BLL;
using ThiThu.DAL;
using ThiThu.Models;

namespace ThiThu.BLL
{
    public class SinhVienBLL
    {
        private static SinhVienBLL instance;

        private SinhVienBLL() { }

        public static SinhVienBLL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SinhVienBLL();
                }
                return instance;
            }
        }
        public DataTable GetSVs(string maHp, string search, int optionSort)
        {
            var listSv = SinhVienDAL.Instance.GetAllSV();
            if (!string.IsNullOrEmpty(search))
            {
                listSv = listSv.Where(x=>x.TenSV.Contains(search)).ToList();
            }

            DataTable res = new DataTable();
            res.Columns.Add("MaSV", typeof(string));
            res.Columns.Add("MaHP", typeof(string));

            res.Columns.Add("STT", typeof(int)); 
            res.Columns.Add("Tên SV", typeof(string)); 
            res.Columns.Add("Lớp SH", typeof(string)); 
            res.Columns.Add("Tên học phần", typeof(string)); 
            res.Columns.Add("Điểm BT", typeof(double)); 
            res.Columns.Add("Điểm GK", typeof(double)); 
            res.Columns.Add("Điểm CK", typeof(double)); 
            res.Columns.Add("Tổng kết", typeof(double)); 
            res.Columns.Add("Ngày thi", typeof(DateTime)); 
            
            int stt = 1;
            List<DataRow> listSort = new List<DataRow>();   
            foreach (var sv in listSv)
            {
                var listHocPhan = sv.HocPhans.ToList();

                //fillter HocPhan
                if(maHp!="All" )
                {
                    listHocPhan = listHocPhan.Where(x=>x.MaHP == maHp).ToList();    
                }

                foreach (var hp_sv in listHocPhan)
                {
                    var hocPhan = HocPhanBLL.Instance.GetHocPhanById(hp_sv.MaHP);
                    var diemTongKet = (hp_sv.DiemBT??0) * 0.2 + (hp_sv.DiemGK??0) * 0.2 + (hp_sv.DiemCK??0) * 0.6;
                    DataRow record = res.NewRow();
                    record["MaSV"] = sv.MaSV;
                    record["MaHP"] = hocPhan.MaHP;

                    record["STT"] = stt++;
                    record["Tên SV"] = sv.TenSV;
                    record["Lớp SH"] = sv.LopSh;
                    record["Tên học phần"] = hocPhan.TenHP;
                    record["Điểm BT"] = hp_sv.DiemBT;
                    record["Điểm GK"] = hp_sv.DiemGK;
                    record["Điểm CK"] = hp_sv.DiemCK;
                    record["Tổng kết"] = diemTongKet;
                    record["Ngày thi"] = hp_sv.NgayThi;
                    listSort.Add(record);
                }
            }
            // sort
            if(optionSort == 0) // sort ten
            {
                listSort.Sort(CompareDataRowsByTen);
            }
            else if(optionSort == 1)
            {
                listSort.Sort(CompareDataRowsByTongKet);
            }
            foreach(var item in listSort)
            {
                res.Rows.Add(item);
            }
            return res;
        }
        public List<string> GetLSHs()
        {
            return SinhVienDAL.Instance.GetAllLSH().Distinct().ToList();
        }

        public SinhVien GetSinhVienById(string id)
        {
            return SinhVienDAL.Instance.GetSinhVienById(id); 
        }
        public HP_SV GetHocPhan(string maSV, string maHp)
        {
            return SinhVienDAL.Instance.GetHocPhan(maSV, maHp);
        }
        public bool AddSV(SinhVien sv)
        {
            return SinhVienDAL.Instance.AddSV(sv);
        }
        public bool AddHocPhan(HP_SV hp)
        {
            return SinhVienDAL.Instance.AddHocPhan(hp);
        }
        public bool DelHocPhans(List<HP_SV> listDel)
        {
            return SinhVienDAL.Instance.DelHocPhans(listDel);
        }
        public static int CompareDataRowsByTen(DataRow x, DataRow y)
        {
            return x["Tên SV"].ToString().CompareTo(y["Tên SV"].ToString());
        }
        public static int CompareDataRowsByTongKet(DataRow x, DataRow y)
        {
            return Convert.ToDouble(x["Tổng kết"]).CompareTo(Convert.ToDouble(y["Tổng kết"]));
        }
    }
}
