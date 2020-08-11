using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorHostel_xml.DLL;
using VectorHostel_xml.DTO;

namespace VectorHostel_xml
{
    public partial class FormPhieuThue : Form
    {
        public FormPhieuThue()
        {
            InitializeComponent();
        }
        PhieuThueDTO phieuDTO = new PhieuThueDTO();
        PhieuThueDLL phieuDLL = new PhieuThueDLL();
        string path = "../../xml/VectorHostel1.xml";
        private void FormPhieuThue_Load(object sender, EventArgs e)
        {
            phieuDLL.HienThi(dgv_PhieuThue);
            txtMaKH.Enabled = false;
            rbNam.Checked = true;
            cbbLoaiPhong.Text = "";
            txtGiaPhong.Enabled = false;
            txtMaPhieu.Enabled = false;
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            this.cbbLoaiPhong.DataSource = dataSet.Tables["loaiphong"];
            this.cbbLoaiPhong.ValueMember = "malp";
            this.cbbLoaiPhong.DisplayMember = "tenlp";


            string expression = "tinhtrang ='empty'";
            DataRow[] foundRows;

            foundRows = dataSet.Tables["phongtro"].Select(expression);
            foreach (DataRow row in foundRows)
            {
                cbbMaPhong.Items.Add(row["tenphong"].ToString());

            }


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtCmnd.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập Cmnd";
            }
            if (txtDiaChi.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập địa chỉ";
                return;
            }
            if (txtHoTen.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập họ tên";
                return;
            }
            phieuDTO.Cmnd = txtCmnd.Text;
            phieuDTO.Diachi = txtDiaChi.Text;
            phieuDTO.Ngaysinh = dtpNgaySinh.Value;
            phieuDTO.Tenkh = txtHoTen.Text;
            if (rbNam.Checked == true)
                phieuDTO.Gioitinh = true;
            else phieuDTO.Gioitinh = false;
            phieuDTO.Sdt = txtSDT.Text;
            if (txtSDT.Text == "" || txtSDT.Text.Length > 11)
            {
                lblThongBao1.Text = "*SDT ko hợp lệ";
                return;
            }

            phieuDTO.Tenphong = cbbMaPhong.SelectedItem.ToString();
            phieuDTO.Ngaythue = dtpNgayThue.Value;
            phieuDTO.Ngaytra = dtpNgayTra.Value;
            if (dtpNgayTra.Value < dtpNgayThue.Value)
            {
                lblThongBao1.Text = "*Ngày trả phải lớn hơn ngày thuê";
                return;
            }
            phieuDLL.ThemPhieu(phieuDTO);
            phieuDLL.HienThi(dgv_PhieuThue);
            lblThongBao1.Text = "*Đã thêm thành công!";
        }

        private void cbbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMaPhong.Items.Clear();
            cbbMaPhong.Text = "";
            string lp_selected = cbbLoaiPhong.SelectedValue.ToString();
            string expression = "tinhtrang ='empty' and malp='" + lp_selected + "'";
            DataRow[] foundRows;
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            foundRows = ds.Tables["phongtro"].Select(expression);


            //////cbbMaPhong.DataSource = foundRows;
            foreach (DataRow row in foundRows)
            {
                cbbMaPhong.Items.Add(row["tenphong"].ToString());
            }
            DataRow[] drs = ds.Tables["loaiphong"].Select("malp='" + lp_selected + "'");
            foreach (DataRow dr in drs)
            {
                txtGiaPhong.Text = dr["dongia"].ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtCmnd.Text = "";
            txtDiaChi.Text = "";
            txtGiaPhong.Text = "";
            txtMaPhieu.Text = "";
            txtSDT.Text = "";
            lblThongBao1.Text = "";
        }

        private void txtSDT_Leave(object sender, EventArgs e)
        {
            txtSDT.Text = txtSDT.Text.Insert(4, ".");
        }

        private void dgv_PhieuThue_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_PhieuThue.CurrentRow == null) return;
            int index = e.RowIndex;

            txtMaPhieu.Text = dgv_PhieuThue.Rows[index].Cells[0].Value.ToString();
            txtMaKH.Text = dgv_PhieuThue.Rows[index].Cells[1].Value.ToString();
            txtHoTen.Text = dgv_PhieuThue.Rows[index].Cells[2].Value.ToString();
            txtCmnd.Text = dgv_PhieuThue.Rows[index].Cells[3].Value.ToString();
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            DataRow[] drs = ds.Tables["khach"].Select("makh = '" + txtMaKH.Text + "'");
            if (drs[0]["gioitinh"].ToString() == "nam") rbNam.Checked = true;
            else rbNu.Checked = true;

            dtpNgaySinh.Value = DateTime.ParseExact(drs[0]["ngaysinh"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            txtDiaChi.Text = drs[0]["diachi"].ToString();

            txtSDT.Text = drs[0]["sdt"].ToString();
            //lay ten loai phong hien thi tren combobox
            //lay ma phong tu dgv
            string maphong = dgv_PhieuThue.Rows[index].Cells[4].Value.ToString();
            DataRow[] phongtro = ds.Tables["phongtro"].Select("maphong = '" + maphong + "'");
            //tiep theo lay ma lp tu bang  phong sau do so voi malp trong loaiphong
            string malp = phongtro[0]["malp"].ToString();
            DataRow[] loaiphong = ds.Tables["loaiphong"].Select("malp ='" + malp + "'");
            cbbLoaiPhong.Text = loaiphong[0]["tenlp"].ToString();

            cbbMaPhong.Text = phongtro[0]["tenphong"].ToString();
            dtpNgayThue.Value = DateTime.ParseExact(dgv_PhieuThue.Rows[index].Cells[5].Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            dtpNgayTra.Value = DateTime.ParseExact(dgv_PhieuThue.Rows[index].Cells[6].Value.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            phieuDTO.Maphieu = txtMaPhieu.Text;
            phieuDLL.xoaPhieu(phieuDTO);
            lblThongBao1.Text = "*Đã xóa phiếu " + txtMaPhieu.Text;
            phieuDLL.HienThi(dgv_PhieuThue);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            phieuDTO.Makh = txtMaKH.Text;
            phieuDTO.Maphieu = txtMaPhieu.Text;

            if (txtCmnd.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập Cmnd";
                return;
            }
            if (txtDiaChi.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập địa chỉ";
                return;
            }
            if (txtHoTen.Text == "")
            {
                lblThongBao1.Text = "Vui lòng nhập họ tên";
                return;
            }
            phieuDTO.Cmnd = txtCmnd.Text;
            phieuDTO.Diachi = txtDiaChi.Text;
            phieuDTO.Ngaysinh = dtpNgaySinh.Value;
            phieuDTO.Tenkh = txtHoTen.Text;
            if (rbNam.Checked == true)
                phieuDTO.Gioitinh = true;
            else phieuDTO.Gioitinh = false;
            phieuDTO.Sdt = txtSDT.Text;
            if (txtSDT.Text == "" || txtSDT.Text.Length > 11)
            {
                lblThongBao1.Text = "SDT ko hợp lệ";
                return;
            }
           
            phieuDTO.Tenphong = cbbMaPhong.SelectedItem.ToString();
            phieuDTO.Ngaythue = dtpNgayThue.Value;
            phieuDTO.Ngaytra = dtpNgayTra.Value;
            if (dtpNgayTra.Value < dtpNgayThue.Value)
            {
                lblThongBao1.Text = "Ngày trả phải lớn hơn ngày thuê";
                return;
            }
            phieuDLL.suaPhieu(phieuDTO);
            phieuDLL.HienThi(dgv_PhieuThue);
        }
    }
}
