using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VectorHostel_xml.DLL;
using VectorHostel_xml.DTO;

namespace VectorHostel_xml
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        phongDLL phongDll = new phongDLL();
        PhongTroDTO phongDTO = new PhongTroDTO();

        string path = "../../xml/VectorHostel1.xml";
        private void Form1_Load(object sender, EventArgs e)
        {
            phongDll.HienThi(dgv_phong);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            this.comboBox2.DataSource = dataSet.Tables["loaiphong"];
            this.comboBox2.DisplayMember = "tenlp";
            this.cbbTinhTrang.Items.Add("full");
            this.cbbTinhTrang.Items.Add("empty");
            this.comboBox1.Items.Add("phòng trống");
            this.comboBox1.Items.Add("phòng ko trống");
            txtMaPhong.Enabled = false;

           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            if (txtTenPhong.Text.Trim() == "" || txtTang.Text == ""||comboBox2.Text=="" || comboBox1.Text =="")
            {
                lblThongBao.Text = "*Vui lòng nhập đầy đủ thông tin";
                return;
            }
            
            else
            {
                phongDTO.Maphong = txtMaPhong.Text;
                phongDTO.Tenphong = txtTenPhong.Text;
                phongDTO.Tang = int.Parse(txtTang.Text);
                phongDTO.Tinhtrang = cbbTinhTrang.GetItemText(this.cbbTinhTrang.SelectedItem);

                phongDTO.Mota = txtMoTa.Text;
                string var = comboBox2.Text;
                switch(var)
                {
                    case "phòng thường":
                        phongDTO.Maphong = "L00";
                    break;
                    case "phòng vip 1":
                        phongDTO.Loaiphong = "L01";
                    break;
                    case "phòng vip 2":
                        phongDTO.Loaiphong = "L02";
                    break;
                    case "phòng vip 3":
                        phongDTO.Loaiphong = "L03";
                    break;
                    case "phòng vip 4":
                        phongDTO.Loaiphong = "L04";
                    break;
                }
                phongDll.themPhong(phongDTO);
                phongDll.HienThi(dgv_phong);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            dgv_phong.Rows.Clear();
            string var;
            var = comboBox1.Text;

            if (var == "phòng trống")
            {

                phongDll.timkiemphongtrong(phongDTO, dgv_phong);
            }
            else
            {

                phongDll.timkiemphongfull(phongDTO, dgv_phong);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            if (txtMaPhong.Text.Trim() != "")
            {
                phongDTO.Maphong = txtMaPhong.Text;
                phongDTO.Tenphong = txtTenPhong.Text;
                phongDTO.Tang = int.Parse(txtTang.Text);
                phongDTO.Tinhtrang = cbbTinhTrang.GetItemText(this.cbbTinhTrang.SelectedItem);

                phongDTO.Mota = txtMoTa.Text;
                string var = comboBox2.Text;
                switch (var)
                {
                    case "phòng thường":
                        phongDTO.Maphong = "L00";
                        break;
                    case "phòng vip 1":
                        phongDTO.Loaiphong = "L01";
                        break;
                    case "phòng vip 2":
                        phongDTO.Loaiphong = "L02";
                        break;
                    case "phòng vip 3":
                        phongDTO.Loaiphong = "L03";
                        break;
                    case "phòng vip 4":
                        phongDTO.Loaiphong = "L04";
                        break;
                }

                phongDll.Sua(phongDTO);
                phongDll.HienThi(dgv_phong);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            if (txtMaPhong.Text.Trim() != "")
            {

                phongDTO.Maphong = txtMaPhong.Text;
                DialogResult dr=  MessageBox.Show("Bạn có chắc chắn muốn xóa phòng: " + txtMaPhong.Text ,"Xác nhận xóa phòng",MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (phongDll.xoa(phongDTO) == false) lblThongBao.Text = "*Không được xóa phòng này";
                    else
                        lblThongBao.Text = "*Đã xóa phòng " + txtMaPhong.Text;
                    phongDll.HienThi(dgv_phong);
                }
                else
                {
                    return;
                }
            }

        }

        private void dgv_phong_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgv_phong.CurrentRow == null) return;
            int index = e.RowIndex;
            txtMaPhong.Text = dgv_phong.Rows[index].Cells[0].Value.ToString();
            txtTenPhong.Text = dgv_phong.Rows[index].Cells[1].Value.ToString();
            txtTang.Text = dgv_phong.Rows[index].Cells[2].Value.ToString();
            cbbTinhTrang.Text = dgv_phong.Rows[index].Cells[3].Value.ToString();
            txtMoTa.Text = dgv_phong.Rows[index].Cells[4].Value.ToString();
            comboBox2.Text = dgv_phong.Rows[index].Cells[5].Value.ToString();
            
            
        }

        private void dgv_phong_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)

        {
            lblThongBao.Text = "";
            txtMaPhong.Text = "";
            txtMoTa.Text = "";
            txtTang.Text = "";
            txtTenPhong.Text = "";
        }
    }
}