using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorHostel_xml.DTO;
using System.Xml;
using System.Windows.Forms;

namespace VectorHostel_xml.DLL
{
    public class PhieuThueDLL
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
        string path = "../../xml/VectorHostel1.xml";
        public PhieuThueDLL()
        {
            doc.Load(path);
            root = doc.DocumentElement;
        }

        public void HienThi(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.ColumnCount = 8;
            int index = 0;

            XmlNodeList dsphieu = root.SelectNodes("phieuthue");
            foreach (XmlNode item in dsphieu)
            {
                dgv.Rows.Add();
                //tao node khachhang dc tham chieu tu ma phieu qua makh
                XmlNode khachhang = root.SelectSingleNode("//khach[makh = //phieuthue[makh='" + item.SelectSingleNode("makh").InnerText + "']/makh]");
                dgv.Rows[index].Cells[1].Value = khachhang.SelectSingleNode("makh").InnerText;
                dgv.Rows[index].Cells[2].Value = khachhang.SelectSingleNode("hoten").InnerText;
                dgv.Rows[index].Cells[3].Value = khachhang.SelectSingleNode("cmnd").InnerText;

                //tao node phong dc tham chieu tu ma phieu qua maphong
                XmlNode phong = root.SelectSingleNode("//phongtro[maphong =//phieuthue[maphong ='" + item.SelectSingleNode("maphong").InnerText + "']/maphong]");

                //tao node loai phong dc tham chieu tu node phong
                XmlNode loaiphong = root.SelectSingleNode("//loaiphong[malp =//phongtro[malp ='" + phong.SelectSingleNode("malp").InnerText + "']/malp]");
                dgv.Rows[index].Cells[0].Value = item.SelectSingleNode("maphieu").InnerText;
                dgv.Rows[index].Cells[4].Value = item.SelectSingleNode("maphong").InnerText;
                dgv.Rows[index].Cells[5].Value = item.SelectSingleNode("ngaythue").InnerText;
                dgv.Rows[index].Cells[6].Value = item.SelectSingleNode("ngaytra").InnerText;
                dgv.Rows[index].Cells[7].Value = loaiphong.SelectSingleNode("tenlp").InnerText;
                index++;
            }
        }
        public void ThemPhieu(PhieuThueDTO phieu)
        {
            //insert bang khach
            //Truong hop chua ton tai khach hang trong csdl
            XmlElement makh = doc.CreateElement("makh");
            XmlElement makh2 = doc.CreateElement("makh");
            if (root.SelectSingleNode("khach[cmnd = '" + phieu.Cmnd + "']") == null)
            {
                
                XmlNodeList makhs = root.SelectNodes("khach/makh");
                int mamax1 = 0;
                foreach (XmlNode item in makhs)
                {
                    int mk = int.Parse(item.InnerText.Substring(2));
                    if (mk > mamax1) mamax1 = mk;
                }
                mamax1++;
                string _makh = "0" + mamax1.ToString();
                makh.InnerText = "KH" + _makh.Substring(mamax1.ToString().Length - 1);
                makh2.InnerText = "KH" + _makh.Substring(mamax1.ToString().Length - 1);


                XmlElement khachhang = doc.CreateElement("khach");
                khachhang.AppendChild(makh);

                XmlElement cmnd = doc.CreateElement("cmnd");
                cmnd.InnerText = phieu.Cmnd;
                khachhang.AppendChild(cmnd);

                XmlElement tenkh = doc.CreateElement("hoten");
                tenkh.InnerText = phieu.Tenkh;
                khachhang.AppendChild(tenkh);

                XmlElement sdt = doc.CreateElement("sdt");
                sdt.InnerText = phieu.Sdt;
                khachhang.AppendChild(sdt);

                XmlElement diachi = doc.CreateElement("diachi");
                diachi.InnerText = phieu.Diachi;
                khachhang.AppendChild(diachi);

                XmlElement gioitinh = doc.CreateElement("gioitinh");
                if (phieu.Gioitinh) gioitinh.InnerText = "nam";
                else gioitinh.InnerText = "nữ";
                khachhang.AppendChild(gioitinh);

                XmlElement ngaysinh = doc.CreateElement("ngaysinh");
                ngaysinh.InnerText = phieu.Ngaysinh.ToString("yyyy-MM-dd");
                khachhang.AppendChild(ngaysinh);

                root.InsertAfter(khachhang, root.SelectSingleNode("khach[last()]"));

            }
            //TH khach da co trong csdl
            else
            {
                makh2.InnerText = root.SelectSingleNode("khach[cmnd='" + phieu.Cmnd + "']/makh").InnerText;

            }

            //insert vao bang phieuthue
            XmlElement phieuthue = doc.CreateElement("phieuthue");

            

            XmlElement maphieu = doc.CreateElement("maphieu");
            XmlNodeList maphieus = root.SelectNodes("phieuthue/maphieu");
            int mamax = 0;
            foreach (XmlNode item in maphieus)
            {
                int mp = int.Parse(item.InnerText.Substring(1));
                if (mp > mamax) mamax = mp;
            }
            mamax++;
            string _maphieu = "0" + mamax.ToString();
            maphieu.InnerText = "P" + _maphieu.Substring(mamax.ToString().Length - 1);
            
            phieuthue.AppendChild(maphieu);

            XmlElement ngaythue = doc.CreateElement("ngaythue");
            ngaythue.InnerText = phieu.Ngaythue.ToString("yyyy-MM-dd");
            phieuthue.AppendChild(ngaythue);

            XmlElement ngaytra = doc.CreateElement("ngaytra");
            ngaytra.InnerText = phieu.Ngaytra.ToString("yyyy-MM-dd");
            phieuthue.AppendChild(ngaytra);

            XmlElement maphong = doc.CreateElement("maphong");
            maphong.InnerText = root.SelectSingleNode("phongtro[tenphong = '" + phieu.Tenphong + "']/maphong").InnerText;
            phieuthue.AppendChild(maphong);

            XmlElement tinhtrang = doc.CreateElement("tinhtrang");
            tinhtrang.InnerText = "full";
            XmlNode oldchild = root.SelectSingleNode("phongtro[maphong='" + maphong.InnerText + "']/tinhtrang");
            root.SelectSingleNode("phongtro[maphong='" + maphong.InnerText + "']").ReplaceChild(tinhtrang, oldchild);
            //do makh da tao o tren
            phieuthue.AppendChild(makh2);
            root.InsertAfter(phieuthue, root.SelectSingleNode("phieuthue[last()]"));

            doc.Save(path);

        }
        public void xoaPhieu(PhieuThueDTO phieuxoa)

        {
            //khi xoa se xoa thong tin phieu, cap nhat lai tinh trang phong.
            phieuxoa.Maphong = root.SelectSingleNode("phieuthue[maphieu='" + phieuxoa.Maphieu + "']/maphong").InnerText;
            XmlElement ttphong = doc.CreateElement("tinhtrang");
            ttphong.InnerText = "empty";
            XmlNode phongtro = root.SelectSingleNode("phongtro[maphong='" + phieuxoa.Maphong + "']");
            phongtro.ReplaceChild(ttphong, phongtro.SelectSingleNode("tinhtrang"));
            root.RemoveChild(root.SelectSingleNode("phieuthue[maphieu = '" + phieuxoa.Maphieu + "']"));
            doc.Save(path);
        }
        public void suaPhieu(PhieuThueDTO phieusua)
        {
            XmlElement khachhang = doc.CreateElement("khach");

            XmlElement makh = doc.CreateElement("makh");
            
            makh.InnerText = phieusua.Makh;
            XmlElement makh2 = doc.CreateElement("makh");
            makh2.InnerText = phieusua.Makh;
            khachhang.AppendChild(makh);

            XmlElement cmnd = doc.CreateElement("cmnd");
            cmnd.InnerText = phieusua.Cmnd;
            khachhang.AppendChild(cmnd);

            XmlElement tenkh = doc.CreateElement("hoten");
            tenkh.InnerText = phieusua.Tenkh;
            khachhang.AppendChild(tenkh);

            XmlElement sdt = doc.CreateElement("sdt");
            sdt.InnerText = phieusua.Sdt;
            khachhang.AppendChild(sdt);

            XmlElement diachi = doc.CreateElement("diachi");
            diachi.InnerText = phieusua.Diachi;
            khachhang.AppendChild(diachi);

            XmlElement gioitinh = doc.CreateElement("gioitinh");
            if (phieusua.Gioitinh) gioitinh.InnerText = "nam";
            else gioitinh.InnerText = "nữ";
            khachhang.AppendChild(gioitinh);

            XmlElement ngaysinh = doc.CreateElement("ngaysinh");
            ngaysinh.InnerText = phieusua.Ngaysinh.ToString("yyyy-MM-dd");
            khachhang.AppendChild(ngaysinh);
            XmlNode khachcu = root.SelectSingleNode("khach[makh ='" + phieusua.Makh + "']");
            root.ReplaceChild(khachhang, khachcu);

            //sua trong bang phieuthue
            XmlElement phieuthue = doc.CreateElement("phieuthue");

            XmlElement maphieu = doc.CreateElement("maphieu");
            maphieu.InnerText = phieusua.Maphieu;
            phieuthue.AppendChild(maphieu);

            XmlElement ngaythue = doc.CreateElement("ngaythue");
            ngaythue.InnerText = phieusua.Ngaythue.ToString("yyyy-MM-dd");
            phieuthue.AppendChild(ngaythue);

            XmlElement ngaytra = doc.CreateElement("ngaytra");
            ngaytra.InnerText = phieusua.Ngaytra.ToString("yyyy-MM-dd");
            phieuthue.AppendChild(ngaytra);

            XmlElement maphong = doc.CreateElement("maphong");
            maphong.InnerText = root.SelectSingleNode("phongtro[tenphong = '" + phieusua.Tenphong + "']/maphong").InnerText;
            phieuthue.AppendChild(maphong);

            phieuthue.AppendChild(makh2);
            //doi tinh trang cua phong tro cu trc khi sua ve empty va sua tinh trang phong tro moi = full
            XmlElement tinhtrangcu = doc.CreateElement("tinhtrang");
            tinhtrangcu.InnerText = "empty";
            string maphongcu = root.SelectSingleNode("phieuthue[maphieu = '" + maphieu.InnerText + "']/maphong").InnerText;
            root.SelectSingleNode("phongtro[maphong = '" + maphongcu + "']").ReplaceChild(tinhtrangcu, root.SelectSingleNode("phongtro[maphong='" + maphongcu + "']/tinhtrang"));

            //tinh trang cua phong tro moi
            XmlElement tinhtrangmoi = doc.CreateElement("tinhtrang");
            tinhtrangmoi.InnerText = "full";
            XmlNode oldchild = root.SelectSingleNode("phongtro[maphong='" + maphong.InnerText + "']/tinhtrang");
            root.SelectSingleNode("phongtro[maphong='" + maphong.InnerText + "']").ReplaceChild(tinhtrangmoi, oldchild);
            //do makh da tao o tren
           
            root.ReplaceChild(phieuthue, root.SelectSingleNode("phieuthue[maphieu ='" + maphieu.InnerText + "']"));

            doc.Save(path);
        }
    }
}
