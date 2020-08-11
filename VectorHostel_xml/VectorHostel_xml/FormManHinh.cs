using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorHostel_xml
{
    public partial class FormManHinh : Form
    {
        public FormManHinh()
        {
            InitializeComponent();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            Form f = new Form1();
            f.Show();
        }

        private void btnPhieuThue_Click(object sender, EventArgs e)
        {
            Form f = new FormPhieuThue();
            f.Show();
        }

        private void btnPTT_Click(object sender, EventArgs e)
        {
            Form f = new FormPhieuThanhToan();
            f.Show();
        }

        private void btnPhieuNuoc_Click(object sender, EventArgs e)
        {

            Form f = new FormPhieuNuoc();
            f.Show();
        }

        private void btn_phieudien_Click(object sender, EventArgs e)
        {
            Form f = new FormPhieuDien();
            f.Show();
        }

        private void FormManHinh_Load(object sender, EventArgs e)
        {

        }
    }
}
