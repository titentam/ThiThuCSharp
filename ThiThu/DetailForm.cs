using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThu.BLL;
using ThiThu.Models;

namespace ThiThu
{
    public partial class DetailForm : Form
    {
        public string _MaSV;
        public string _LopHp;
        public DetailForm(string maSV="", string lopHp = "")
        {
            InitializeComponent();
            _MaSV = maSV;
            _LopHp = lopHp;
            LoadForm();



        }
        public void LoadForm()
        {
            LoadCbbHocPhan();
            LoadCbbLSH();
            if (!string.IsNullOrEmpty(_MaSV))
            {
                txtMSSV.Enabled = false;
                cbbHocPhan.Enabled = false;
                using(CkDb db = new CkDb()) 
                {
                    var sv = db.SinhViens.Find(_MaSV);
                    var hocPhan = db.HocPhans.Where(x => x.TenHP == _LopHp).SingleOrDefault();
                    var hp_sv = db.HP_SVs.Where(x => x.MaSV == sv.MaSV && x.MaHP == hocPhan.MaHP).SingleOrDefault();


                    txtMSSV.Text = sv.MaSV;
                    txtName.Text = sv.TenSV;
                    cbbLSH.SelectedIndex = cbbLSH.Items.IndexOf(sv.LopSh);

                    
                    foreach(var item in cbbHocPhan.Items)
                    {
                        var tmp = item as HocPhan;
                        if(tmp.MaHP == hocPhan.MaHP)
                        {
                            cbbHocPhan.SelectedItem = item; break;
                        }
                    }
                    if (sv.GioiTinh) rbtnMale.Checked = true;
                    else rbtnFemale.Checked = true;

                   
                    dtpNgayThi.Value = (DateTime)hp_sv.NgayThi;
                    txtDiemBT.Text = hp_sv.DiemBT.ToString();
                    txtDiemGK.Text = hp_sv.DiemGK.ToString();
                    txtDiemCK.Text = hp_sv.DiemCK.ToString();
                    txtTongKet.Text = ((hp_sv.DiemBT ?? 0) * 0.2 + (hp_sv.DiemGK ?? 0) * 0.2
                                    + (hp_sv.DiemCK ?? 0) * 0.6).ToString();
                }
                
            }
        }
        public void LoadCbbLSH()
        {
            cbbLSH.DataSource = SinhVienBLL.Instance.GetLSHs();
        }
        public void LoadCbbHocPhan()
        {
            cbbHocPhan.Items.AddRange(HocPhanBLL.Instance.GetAllHocPhan().ToArray());
            cbbHocPhan.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var svMoi = new SinhVien
            {
                MaSV = txtMSSV.Text,
                TenSV = txtName.Text,
                LopSh = cbbLSH.SelectedItem.ToString(),
                GioiTinh = rbtnMale.Checked,
            };
            try
            {
                var hp_svMoi = new HP_SV
                {
                    MaHP = (cbbHocPhan.SelectedItem as HocPhan).MaHP,
                    DiemBT = Convert.ToDouble(txtDiemBT.Text.ToString()),
                    DiemGK = Convert.ToDouble(txtDiemGK.Text.ToString()),
                    DiemCK = Convert.ToDouble(txtDiemCK.Text.ToString()),
                    NgayThi = dtpNgayThi.Value
                };
                if (txtMSSV.Enabled) // add
                {
                    var sv = SinhVienBLL.Instance.GetSinhVienById(svMoi.MaSV);
                    if (sv == null) // khong trung
                    {
                        SinhVienBLL.Instance.AddSV(svMoi);
                        hp_svMoi.MaSV = svMoi.MaSV;
                        SinhVienBLL.Instance.AddHocPhan(hp_svMoi);
                        MessageBox.Show("Thêm thành công");
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Trùng mã sv");
                        return;
                    }
                    
                }
                else // edit
                {
                    using (CkDb db = new CkDb())
                    {
                        var sv = SinhVienBLL.Instance.GetSinhVienById(svMoi.MaSV);
                        sv.TenSV = svMoi.TenSV;
                        sv.LopSh = svMoi.LopSh;
                        sv.GioiTinh = svMoi.GioiTinh;

                        var hp_sv = SinhVienBLL.Instance.GetHocPhan(svMoi.MaSV, hp_svMoi.MaHP);
                        hp_sv.DiemBT = hp_svMoi.DiemBT;
                        hp_sv.DiemGK = hp_svMoi.DiemGK;
                        hp_sv.DiemCK = hp_svMoi.DiemCK;
                        hp_sv.NgayThi = hp_svMoi.NgayThi;

                        db.Entry(sv).State = EntityState.Modified;
                        db.Entry(hp_sv).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công");
                        this.Dispose();
                    }
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Điểm không hợp lệ");
            }
            
        }
    }
}
