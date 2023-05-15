using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiThu.BLL;
using ThiThu.Models;

namespace ThiThu
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadCBBHocPhan();
            LoadCBBSort();
            LoadData();
        }

        public void LoadCBBHocPhan()
        {
            cbbHocPhan.Items.Add(new HocPhan {MaHP="All", TenHP="All" });
            cbbHocPhan.Items.AddRange(HocPhanBLL.Instance.GetAllHocPhan().ToArray());
            cbbHocPhan.SelectedIndex = 0;
        }
        public void LoadCBBSort()
        {
            List<string> list = new List<string>();
            list.Add("Tên");
            list.Add("Điểm tổng kết");
            cbbSort.DataSource = list;  

        }
        public void LoadData(string maHp = "All",string search = "", int optionSort = -1)
        {
            dataGridView1.DataSource = SinhVienBLL.Instance.GetSVs(maHp, search, optionSort);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        private void cbbHocPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hocPhan = cbbHocPhan.SelectedItem as HocPhan;
            LoadData(hocPhan.MaHP);
            txtSearch.Text = "";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var hocPhan = cbbHocPhan.SelectedItem as HocPhan;
            LoadData(hocPhan.MaHP, txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DetailForm detailForm = new DetailForm();
            detailForm.ShowDialog();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string maSv = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string lopHp = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                DetailForm detailForm = new DetailForm(maSv, lopHp);
                detailForm.ShowDialog();
                LoadData();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("Bạn có muốn xóa", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    List<HP_SV> listDel = new List<HP_SV>();
                    foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                    {
                        string maSv = item.Cells[0].Value.ToString();
                        string maHp = item.Cells[1].Value.ToString();
                        listDel.Add(new HP_SV
                        {
                            MaSV = maSv,
                            MaHP = maHp
                        });
                    }
                    if(SinhVienBLL.Instance.DelHocPhans(listDel))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData(); 
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            var hocPhan = cbbHocPhan.SelectedItem as HocPhan;
            LoadData(hocPhan.MaHP, txtSearch.Text, cbbSort.SelectedIndex);
        }
    }
}
