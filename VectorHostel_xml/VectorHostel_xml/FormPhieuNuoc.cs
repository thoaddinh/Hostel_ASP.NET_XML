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
    public partial class FormPhieuNuoc : Form
    {
        public FormPhieuNuoc()
        {
            InitializeComponent();
        }
        PhieuNuocDTO phieuNuocDTO = new PhieuNuocDTO();
        PhieuNuocDLL phieuNuocDLL = new PhieuNuocDLL();
        String path = "../../xml/VectorHostel1.xml";
        private void btnThem_Click(object sender, EventArgs e)
        {

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable phieunuoc = dataSet.Tables["phieuthunuoc"];
            DataRow[] dt = phieunuoc.Select("maphong = '" + cbbMaPhong.SelectedValue + "' and thang =" + (int.Parse(txtthang.Text) - 1) + " and nam =" + (int.Parse(txtnam.Text)));

            phieuNuocDTO.Maphong = cbbMaPhong.Text;

            phieuNuocDTO.Csm = double.Parse(txtCSM.Text);
            phieuNuocDTO.Thang = int.Parse(txtthang.Text);
            phieuNuocDTO.Nam = int.Parse(txtnam.Text);
            double csc = 0;

            if (dt.Length != 0)
            {
                csc = double.Parse(dt[0]["chisomoi"].ToString());
            }
            if (phieuNuocDTO.Csm > csc)
            {
                phieuNuocDLL.Them(phieuNuocDTO);
                MessageBox.Show("Đã thêm thành công");
                phieuNuocDLL.HienThi(dgv_pnuoc);
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
            DataTable phieunuoc = dataSet.Tables["phieuthunuoc"];
            DataRow[] dt = phieunuoc.Select("maphong = '" + cbbMaPhong.SelectedValue + "' and thang =" + (int.Parse(txtthang.Text) + 1) + " and nam =" + (int.Parse(txtnam.Text)));

            phieuNuocDTO.Maphong = cbbMaPhong.Text;
            phieuNuocDTO.Maptn = txtMaPhieuNuoc.Text;
            phieuNuocDTO.Csc = double.Parse(txtCSC.Text);
            phieuNuocDTO.Csm = double.Parse(txtCSM.Text);
            phieuNuocDTO.Thang = int.Parse(txtthang.Text);
            phieuNuocDTO.Nam = int.Parse(txtnam.Text);

            int maxthang = int.MaxValue;
            foreach (DataRow item in phieunuoc.Rows)
            {
                int thang = int.Parse(item["thang"].ToString());
                maxthang = Math.Max(maxthang, thang);
            }
            if (phieuNuocDTO.Thang < maxthang)
            {//chi so moi cua thang sau;
                double csm = phieuNuocDTO.Csm + 1;
                if (dt.Length != 0)
                {
                    csm = double.Parse(dt[0]["chisomoi"].ToString());
                }
                if (phieuNuocDTO.Csm < csm)
                {
                    phieuNuocDLL.Sua(phieuNuocDTO);
                    MessageBox.Show("Đã sửa thành công");
                    phieuNuocDLL.HienThi(dgv_pnuoc);
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
            if (txtMaPhieuNuoc.Text.Trim() != "")
            {
                phieuNuocDTO.Maptn = txtMaPhieuNuoc.Text;
                phieuNuocDLL.Xoa(phieuNuocDTO);
                phieuNuocDLL.HienThi(dgv_pnuoc);
            }
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            dgv_pnuoc.Rows.Clear();
            txtCSC.Text = "";
            txtCSM.Text = "";
            txtMaPhieuNuoc.Text = "";
            txtnam.Text = "";
            txtthang.Text = "";
            phieuNuocDLL.HienThi(dgv_pnuoc);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgv_pnuoc.Rows.Clear();
            phieuNuocDTO.Maphong = cbbMaPhong.SelectedValue.ToString();
            phieuNuocDLL.Timkiem(phieuNuocDTO, dgv_pnuoc);
        }

        private void dgv_pnuoc_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_pnuoc.CurrentRow == null) return;
            int index = e.RowIndex; //lay chi so dong dc chon

            txtMaPhieuNuoc.Text = dgv_pnuoc.Rows[index].Cells[0].Value.ToString();
            cbbMaPhong.Text = dgv_pnuoc.Rows[index].Cells[1].Value.ToString();
            txtCSC.Text = dgv_pnuoc.Rows[index].Cells[2].Value.ToString();
            txtCSM.Text = dgv_pnuoc.Rows[index].Cells[3].Value.ToString();
            txtthang.Text = dgv_pnuoc.Rows[index].Cells[4].Value.ToString();
            txtnam.Text = dgv_pnuoc.Rows[index].Cells[5].Value.ToString();
        }

        private void FormPhieuNuoc_Load(object sender, EventArgs e)
        {
            phieuNuocDLL.HienThi(dgv_pnuoc);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable tenphong = dataSet.Tables["phongtro"];
            cbbMaPhong.DataSource = tenphong;
            cbbMaPhong.DisplayMember = "maphong";
            cbbMaPhong.ValueMember = "maphong";

            txtMaPhieuNuoc.Enabled = false;
            txtCSC.Enabled = false;
        }
    }
}
