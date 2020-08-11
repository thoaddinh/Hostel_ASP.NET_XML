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
    public partial class FormPhieuThanhToan : Form
    {
        public FormPhieuThanhToan()
        {
            InitializeComponent();
        }
        ThanhToanDLL thanhToanDLL = new ThanhToanDLL();
        ThanhToanDTO thanhToanDTO = new ThanhToanDTO();
        String path = "../../xml/VectorHostel1.xml";
        private void FormPhieuThanhToan_Load(object sender, EventArgs e)
        {
            thanhToanDLL.HienThi(ptt);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable tenphong = dataSet.Tables["phongtro"];
            cbbTimKiem.DataSource = tenphong;

            cbbTimKiem.DisplayMember = "maphong";
            cbbTimKiem.ValueMember = "maphong";
            txtmaphieutt.Enabled = false;
            DataSet dataset1 = new DataSet();
            dataset1.ReadXml(path);
            DataTable tenphong1 = dataset1.Tables["phongtro"];
            cbbMaPhong.DataSource = tenphong1;
            cbbMaPhong.DisplayMember = "maphong";
            cbbMaPhong.ValueMember = "maphong";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            thanhToanDTO.MaTT = txtmaphieutt.Text;
            thanhToanDTO.Maphong = cbbMaPhong.Text;
            thanhToanDTO.Tiendien = txttiendien.Text;
            thanhToanDTO.Tiennuoc = txttiennuoc.Text;
            thanhToanDTO.Thang = int.Parse(txtthang.Text);
            thanhToanDTO.Nam = int.Parse(txtnam.Text);

            thanhToanDLL.Them(thanhToanDTO);
            thanhToanDLL.HienThi(ptt);
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (txtmaphieutt.Text.Trim() != "")
            {
                thanhToanDTO.MaTT = txtmaphieutt.Text;
                thanhToanDTO.Maphong = cbbMaPhong.Text;
                thanhToanDTO.Tiendien = txttiendien.Text;
                thanhToanDTO.Tiennuoc = txttiennuoc.Text;
                thanhToanDTO.Thang = int.Parse(txtthang.Text);
                thanhToanDTO.Nam = int.Parse(txtnam.Text);
            }
            thanhToanDLL.Sua(thanhToanDTO);
            thanhToanDLL.HienThi(ptt);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtmaphieutt.Text.Trim() != "")
            {
                thanhToanDTO.MaTT = txtmaphieutt.Text;
                thanhToanDLL.Xoa(thanhToanDTO);
                thanhToanDLL.HienThi(ptt);
            }
        }

        private void bntQuayLai_Click(object sender, EventArgs e)
        {
            txtmaphieutt.Clear();
            txtthang.Clear();
            txtnam.Clear();
            txttiendien.Clear();
            txttiennuoc.Clear();
            ptt.Rows.Clear();
            thanhToanDLL.HienThi(ptt);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ptt.Rows.Clear();
            thanhToanDTO.Maphong = cbbTimKiem.SelectedValue.ToString();
            thanhToanDLL.timkiem(thanhToanDTO, ptt);
        }

        private void ptt_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ptt.CurrentRow == null) return;
            int index = e.RowIndex; //lay chi so dong dc chon

            txtmaphieutt.Text = ptt.Rows[index].Cells[0].Value.ToString();
            cbbMaPhong.Text = ptt.Rows[index].Cells[1].Value.ToString();
            txttiendien.Text = ptt.Rows[index].Cells[2].Value.ToString();
            txttiennuoc.Text = ptt.Rows[index].Cells[3].Value.ToString();
            txtthang.Text = ptt.Rows[index].Cells[4].Value.ToString();
            txtnam.Text = ptt.Rows[index].Cells[5].Value.ToString();
        }

        private void cbbMaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttiendien.Clear();
            txttiennuoc.Clear();
            DataSet dsdien = new DataSet();
            dsdien.ReadXml(path);
            DataRow[] phieudien = dsdien.Tables["phieuthudien"].Select("maphong ='" + cbbMaPhong.SelectedValue.ToString() + "'");
            foreach (DataRow item in phieudien)
            {
                if ((txtthang.Text == item["thang"].ToString()) && (txtnam.Text == item["nam"].ToString()))
                {
                    txttiendien.Text = ((double.Parse(item["chisomoi"].ToString()) - double.Parse(item["chisocu"].ToString())) * 5000).ToString();
                }

            }


            DataRow[] phieunuoc = dsdien.Tables["phieuthunuoc"].Select("maphong ='" + cbbMaPhong.SelectedValue.ToString() + "'");

            foreach (DataRow item in phieunuoc)
            {
                if ((txtthang.Text == item["thang"].ToString()) && (txtnam.Text == item["nam"].ToString()))
                {
                    txttiennuoc.Text = ((double.Parse(item["chisomoi"].ToString()) - double.Parse(item["chisocu"].ToString())) * 3000).ToString();
                }
            }
        }
    }
}
