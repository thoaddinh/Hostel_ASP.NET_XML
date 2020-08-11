using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorHostel_xml.DLL;
using VectorHostel_xml.DTO;

namespace VectorHostel_xml
{
    public partial class FormPhieuDien : Form
    {
        public FormPhieuDien()
        {
            InitializeComponent();
        }
        PhieuDienDTO phieuDienDTO = new PhieuDienDTO();
        PhieuDienDLL phieuDienDLL = new PhieuDienDLL();
        String path = "../../xml/VectorHostel1.xml";
        private void btnThem_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable phieudien = dataSet.Tables["phieuthudien"];
            DataRow[] dt = phieudien.Select("maphong = '" + cbbMaPhong.SelectedValue + "' and thang =" + (int.Parse(txtthang.Text) - 1) + " and nam =" + (int.Parse(txtnam.Text)));

            phieuDienDTO.Maphong = cbbMaPhong.Text;

            phieuDienDTO.Csm = double.Parse(txtCSM.Text);
            phieuDienDTO.Thang = int.Parse(txtthang.Text);
            phieuDienDTO.Nam = int.Parse(txtnam.Text);
            double csc = 0;

            if (dt.Length != 0)
            {
                csc = double.Parse(dt[0]["chisomoi"].ToString());
            }
            if (phieuDienDTO.Csm > csc)
            {
                phieuDienDLL.Them(phieuDienDTO);
                MessageBox.Show("Đã thêm thành công");
                phieuDienDLL.HienThi(dgv_pdien);
            }
            else
            {
                MessageBox.Show("chỉ số mới phải lớn hơn chỉ số cũ.");
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable phieusua = dataSet.Tables["phieuthudien"];

            DataRow[] dt = phieusua.Select("maphong = '" + cbbMaPhong.SelectedValue + "' and thang =" + (int.Parse(txtthang.Text) + 1) + " and nam =" + (int.Parse(txtnam.Text)));

            phieuDienDTO.Maptd = txtMaPhieuDien.Text;
            phieuDienDTO.Maphong = cbbMaPhong.Text;
            phieuDienDTO.Csc = double.Parse(txtCSC.Text);
            phieuDienDTO.Csm = double.Parse(txtCSM.Text);
            phieuDienDTO.Thang = int.Parse(txtthang.Text);
            phieuDienDTO.Nam = int.Parse(txtnam.Text);

            int maxthang = int.MaxValue;
            foreach (DataRow item in phieusua.Rows)
            {
                int thang = int.Parse(item["thang"].ToString());
                maxthang = Math.Max(maxthang, thang);
            }
            if (phieuDienDTO.Thang < maxthang)
            {//chi so moi cua thang sau;
                double csm = phieuDienDTO.Csm + 1;
                if (dt.Length != 0)
                {
                    csm = double.Parse(dt[0]["chisomoi"].ToString());
                }
                if (phieuDienDTO.Csm < csm)
                {
                    phieuDienDLL.Sua(phieuDienDTO);
                    MessageBox.Show("Đã sửa thành công");
                    phieuDienDLL.HienThi(dgv_pdien);
                }
                else
                {
                    MessageBox.Show("chỉ số mới phải nhỏ hơn chỉ số mới tháng sau.");
                    return;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuDien.Text.Trim() != "")
            {
                phieuDienDTO.Maptd = txtMaPhieuDien.Text;
                phieuDienDLL.Xoa(phieuDienDTO);
                phieuDienDLL.HienThi(dgv_pdien);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgv_pdien.Rows.Clear();
            phieuDienDTO.Maphong = cbbMaPhong.SelectedValue.ToString();
            phieuDienDLL.Timkiem(phieuDienDTO, dgv_pdien);
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            txtMaPhieuDien.Clear();
            txtCSC.Clear();
            txtCSM.Clear();
            txtthang.Clear();
            txtnam.Clear();
            dgv_pdien.Rows.Clear();
            phieuDienDLL.HienThi(dgv_pdien);
        }

        private void dgv_pdien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_pdien.CurrentRow == null) return;
            int index = e.RowIndex; //lay chi so dong dc chon

            txtMaPhieuDien.Text = dgv_pdien.Rows[index].Cells[0].Value.ToString();
            cbbMaPhong.Text = dgv_pdien.Rows[index].Cells[1].Value.ToString();
            txtCSC.Text = dgv_pdien.Rows[index].Cells[2].Value.ToString();
            txtCSM.Text = dgv_pdien.Rows[index].Cells[3].Value.ToString();
            txtthang.Text = dgv_pdien.Rows[index].Cells[4].Value.ToString();
            txtnam.Text = dgv_pdien.Rows[index].Cells[5].Value.ToString();
        }

        private void FormPhieuDien_Load(object sender, EventArgs e)
        {
            phieuDienDLL.HienThi(dgv_pdien);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable tenphong = dataSet.Tables["phongtro"];
            cbbMaPhong.DataSource = tenphong;
            cbbMaPhong.DisplayMember = "maphong";
            cbbMaPhong.ValueMember = "maphong";

            txtMaPhieuDien.Enabled = false;
            txtCSC.Enabled = false;
        }
    }
}
