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
    class PhieuNuocDLL
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
        string path = "../../xml/VectorHostel1.xml";
        public PhieuNuocDLL()
        {
            doc.Load(path);
            root = doc.DocumentElement;
        }
        public void Them(PhieuNuocDTO thempn)
        {
            XmlElement phieunuoc = doc.CreateElement("phieuthunuoc");

            XmlNodeList maptn = root.SelectNodes("phieuthunuoc/maptn");

            int mamax = 0;

            foreach (XmlNode item in maptn)
            {
                int mapd1 = int.Parse(item.InnerText.Substring(3));
                if (mapd1 > mamax)
                {
                    mamax = mapd1;

                }
            }
            mamax++;
            string _ptn = string.Concat("0", mamax.ToString());
            string _mapn = "PTN" + _ptn.Substring(mamax.ToString().Length - 1);
            XmlElement mapn = doc.CreateElement("maptn");

            mapn.InnerText = _mapn;
            phieunuoc.AppendChild(mapn);

            XmlElement maphong = doc.CreateElement("maphong");
            maphong.InnerText = thempn.Maphong;
            phieunuoc.AppendChild(maphong);

            XmlElement chisocu = doc.CreateElement("chisocu");
            if (root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (thempn.Thang - 1).ToString() + "']/chisomoi") != null)
                chisocu.InnerText = root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (thempn.Thang - 1).ToString() + "']/chisomoi").InnerText;
            else chisocu.InnerText = "0";
            phieunuoc.AppendChild(chisocu);

            XmlElement chisomoi = doc.CreateElement("chisomoi");
            chisomoi.InnerText = thempn.Csm.ToString();
            phieunuoc.AppendChild(chisomoi);

            XmlElement thang = doc.CreateElement("thang");
            thang.InnerText = thempn.Thang.ToString();
            phieunuoc.AppendChild(thang);

            XmlElement nam = doc.CreateElement("nam");
            nam.InnerText = thempn.Nam.ToString();
            phieunuoc.AppendChild(nam);
            if (root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (thempn.Thang + 1).ToString() + "']/chisocu") != null)
            {
                root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (thempn.Thang + 1).ToString() + "']/chisocu").InnerText = chisomoi.InnerText;
            }

            root.InsertAfter(phieunuoc, root.SelectSingleNode("phieuthunuoc[last()]"));
            doc.Save(path);
        }

        public void Sua(PhieuNuocDTO phongsua)
        {
            XmlNode phongcu = root.SelectSingleNode("phieuthunuoc[maptn = '" + phongsua.Maptn + "']");
            if (phongcu != null)
            {
                XmlNode phongsuamoi = doc.CreateElement("phieuthunuoc");

                XmlElement Maptn = doc.CreateElement("maptn");
                Maptn.InnerText = phongsua.Maptn;
                phongsuamoi.AppendChild(Maptn);

                XmlElement maphong = doc.CreateElement("maphong");
                maphong.InnerText = phongsua.Maphong;
                phongsuamoi.AppendChild(maphong);

                XmlElement chisocu = doc.CreateElement("chisocu");
                chisocu.InnerText = phongsua.Csc.ToString();
                phongsuamoi.AppendChild(chisocu);

                XmlElement chisomoi = doc.CreateElement("chisomoi");
                chisomoi.InnerText = phongsua.Csm.ToString();
                phongsuamoi.AppendChild(chisomoi);

                XmlElement thang = doc.CreateElement("thang");
                thang.InnerText = phongsua.Thang.ToString();
                phongsuamoi.AppendChild(thang);

                XmlElement nam = doc.CreateElement("nam");
                nam.InnerText = phongsua.Nam.ToString();
                phongsuamoi.AppendChild(nam);

                if (root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang + 1).ToString() + "']/chisocu") != null)
                {
                    root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang + 1).ToString() + "']/chisocu").InnerText = chisomoi.InnerText;
                }
                if (root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi") != null)
                {
                    root.SelectSingleNode("phieuthunuoc[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi").InnerText = chisocu.InnerText;
                }

                root.ReplaceChild(phongsuamoi, phongcu);
                doc.Save(path);
            }
        }
        public void Xoa(PhieuNuocDTO phongxoa)
        {
            XmlNode phieucanxoa = root.SelectSingleNode("phieuthunuoc[maptn = '" + phongxoa.Maptn + "']");
            if (phieucanxoa != null)
            {
                root.RemoveChild(phieucanxoa);
                doc.Save(path);
            }
        }
        public void Timkiem(PhieuNuocDTO phieutim, DataGridView dgv)
        {
            XmlNodeList phieutn = root.SelectNodes("phieuthunuoc[maphong = '" + phieutim.Maphong + "']");

            int index = 0;
            if (phieutn == null) return;

            foreach (XmlNode item in phieutn)
            {
                if (phieutn != null)
                {
                    dgv.ColumnCount = 6;
                    dgv.Rows.Add();
                    XmlNode mapn = item.SelectSingleNode("maptn");
                    dgv.Rows[index].Cells[0].Value = mapn.InnerText;
                    XmlNode maphong = item.SelectSingleNode("maphong");
                    dgv.Rows[index].Cells[1].Value = maphong.InnerText;
                    XmlNode chisocu = item.SelectSingleNode("chisocu");
                    dgv.Rows[index].Cells[2].Value = chisocu.InnerText;
                    XmlNode chisomoi = item.SelectSingleNode("chisomoi");
                    dgv.Rows[index].Cells[3].Value = chisomoi.InnerText;
                    XmlNode thang = item.SelectSingleNode("thang");
                    dgv.Rows[index].Cells[4].Value = thang.InnerText;
                    XmlNode nam = item.SelectSingleNode("nam");
                    dgv.Rows[index].Cells[5].Value = nam.InnerText;

                    index++;
                }
            }
        }


        public void HienThi(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.ColumnCount = 6;
            int index = 0; //luu chi so dong
            XmlNodeList ds = root.SelectNodes("phieuthunuoc");
            foreach (XmlNode item in ds)
            {
                dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = item.SelectSingleNode("maptn").InnerText;
                dgv.Rows[index].Cells[1].Value = item.SelectSingleNode("maphong").InnerText;
                dgv.Rows[index].Cells[2].Value = item.SelectSingleNode("chisocu").InnerText;
                dgv.Rows[index].Cells[3].Value = item.SelectSingleNode("chisomoi").InnerText;
                dgv.Rows[index].Cells[4].Value = item.SelectSingleNode("thang").InnerText;
                dgv.Rows[index].Cells[5].Value = item.SelectSingleNode("nam").InnerText;

                index++;
            }
        }

    }
}

