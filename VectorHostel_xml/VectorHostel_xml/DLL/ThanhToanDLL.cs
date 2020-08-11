using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using VectorHostel_xml.DTO;

namespace VectorHostel_xml.DLL
{
    public class ThanhToanDLL
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
        string path = "../../xml/VectorHostel1.xml";
        public ThanhToanDLL()
        {
            doc.Load(path);
            root = doc.DocumentElement;
        }
        public void Them(ThanhToanDTO thanhtoanthem)
        {   //tao nut thanh toan 
            XmlElement thanhtoan = doc.CreateElement("phieuthanhtoan");

            XmlNodeList matt = root.SelectNodes("phieuthanhtoan/maphieutt");

            int mamax = 0;

            foreach (XmlNode item in matt)
            {
                int mapd1 = int.Parse(item.InnerText.Substring(3));
                if (mapd1 > mamax)
                {
                    mamax = mapd1;

                }
            }
            mamax++;
            string _ptt = string.Concat("0", mamax.ToString());
            string _maptt = "PTT" + _ptt.Substring(mamax.ToString().Length - 1);
            XmlElement maphieu = doc.CreateElement("maphieutt");
            maphieu.InnerText = _maptt;
            thanhtoan.AppendChild(maphieu);

            XmlElement maphong = doc.CreateElement("maphong");
            maphong.InnerText = thanhtoanthem.Maphong;
            thanhtoan.AppendChild(maphong);

            XmlElement thang = doc.CreateElement("thang");
            thang.InnerText = thanhtoanthem.Thang.ToString();
            thanhtoan.AppendChild(thang);

            XmlElement nam = doc.CreateElement("nam");
            nam.InnerText = thanhtoanthem.Nam.ToString();
            thanhtoan.AppendChild(nam);
            XmlElement tiendien = doc.CreateElement("tiendien");
            tiendien.InnerText = thanhtoanthem.Tiendien.ToString();
            thanhtoan.AppendChild(tiendien);

            XmlElement tiennuoc = doc.CreateElement("tiennuoc");
            tiennuoc.InnerText = thanhtoanthem.Tiennuoc.ToString();
            thanhtoan.AppendChild(tiennuoc);

          

            root.InsertAfter(thanhtoan,root.SelectSingleNode("phieuthanhtoan[last()]"));
            doc.Save(path);
        }
        public void Sua(ThanhToanDTO phieusua)
        {
            XmlNode phieucu = root.SelectSingleNode("phieuthanhtoan[maphieutt = '" + phieusua.MaTT + "']");

            if (phieucu != null)
            {
                XmlNode phieusuamoi = doc.CreateElement("phieuthanhtoan");

                XmlElement maphieu = doc.CreateElement("maphieutt");
                maphieu.InnerText = phieusua.MaTT;
                phieusuamoi.AppendChild(maphieu);

                XmlElement maphong = doc.CreateElement("maphong");
                maphong.InnerText = phieusua.Maphong;
                phieusuamoi.AppendChild(maphong);

                XmlElement tiendien = doc.CreateElement("tiendien");
                tiendien.InnerText = phieusua.Tiendien.ToString();
                phieusuamoi.AppendChild(tiendien);

                XmlElement tiennuoc = doc.CreateElement("tiennuoc");
                tiennuoc.InnerText = phieusua.Tiennuoc.ToString();
                phieusuamoi.AppendChild(tiennuoc);

                XmlElement thang = doc.CreateElement("thang");
                thang.InnerText = phieusua.Thang.ToString();
                phieusuamoi.AppendChild(thang);

                XmlElement nam = doc.CreateElement("nam");
                nam.InnerText = phieusua.Nam.ToString();
                phieusuamoi.AppendChild(nam);

                root.ReplaceChild(phieusuamoi, phieucu);
                doc.Save(path);
            }

        }
        public void Xoa(ThanhToanDTO phieuttxoa)
        {
            XmlNode phieucanxoa = root.SelectSingleNode("phieuthanhtoan[maphieutt = '" + phieuttxoa.MaTT + "']");
            if (phieucanxoa != null)
            {   
                root.RemoveChild(phieucanxoa);
                doc.Save(path);
            }
        }
        public void timkiem(ThanhToanDTO phieutim, DataGridView dgv)
        {
             XmlNodeList phieutt = root.SelectNodes("phieuthanhtoan[maphong = '" + phieutim.Maphong+"']");
            int index = 0;    
            if (phieutt == null) return;
            foreach (XmlNode item in phieutt)
            {
                if (phieutt != null)
                {
                    dgv.ColumnCount = 6;
                    dgv.Rows.Add();
                    XmlNode maptt = item.SelectSingleNode("maphieutt");
                    dgv.Rows[index].Cells[0].Value = maptt.InnerText;
                    XmlNode maphong = item.SelectSingleNode("maphong");
                    dgv.Rows[index].Cells[1].Value = maphong.InnerText;
                    XmlNode tiendien = item.SelectSingleNode("tiendien");
                    dgv.Rows[index].Cells[2].Value = tiendien.InnerText;
                    XmlNode tiennuoc = item.SelectSingleNode("tiennuoc");
                    dgv.Rows[index].Cells[3].Value = tiennuoc.InnerText;
                    XmlNode thang = item.SelectSingleNode("thang");
                    dgv.Rows[index].Cells[4].Value = thang.InnerText;
                    XmlNode nam = item.SelectSingleNode("nam");
                    dgv.Rows[index].Cells[5].Value = nam.InnerText;

                    index++;
                }
            }
            
    
        }
        public void HienThi(DataGridView ptt)
        {
            ptt.Rows.Clear();
            ptt.ColumnCount = 6;
            int index = 0; //luu chi so dong
            XmlNodeList ds = root.SelectNodes("phieuthanhtoan");
            foreach (XmlNode item in ds)
            {
                ptt.Rows.Add();
                ptt.Rows[index].Cells[0].Value = item.SelectSingleNode("maphieutt").InnerText;
                ptt.Rows[index].Cells[1].Value = item.SelectSingleNode("maphong").InnerText;
                ptt.Rows[index].Cells[2].Value = item.SelectSingleNode("tiendien").InnerText;
                ptt.Rows[index].Cells[3].Value = item.SelectSingleNode("tiennuoc").InnerText;
                ptt.Rows[index].Cells[4].Value = item.SelectSingleNode("thang").InnerText;
                ptt.Rows[index].Cells[5].Value = item.SelectSingleNode("nam").InnerText;

                index++;
            }
        }


    }
}
