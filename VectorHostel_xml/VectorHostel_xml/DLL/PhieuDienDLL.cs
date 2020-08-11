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
    public class PhieuDienDLL
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
        string path = "../../xml/VectorHostel1.xml";
        public PhieuDienDLL()
        {
            doc.Load(path);
            root = doc.DocumentElement;
        }
        public void Them(PhieuDienDTO thempd)
        {
            XmlElement phieudien = doc.CreateElement("phieuthudien");

            XmlNodeList maptd = root.SelectNodes("phieuthudien/maptd");

            int mamax = 0;

            foreach (XmlNode item in maptd)
            {
                int mapd1 = int.Parse(item.InnerText.Substring(3));
                if (mapd1 > mamax)
                {
                    mamax = mapd1;

                }
            }
            mamax++;
            string _ptd = string.Concat("0", mamax.ToString());
            string _mapd = "PTD" + _ptd.Substring(mamax.ToString().Length - 1);
            XmlElement mapd = doc.CreateElement("maptd");

            mapd.InnerText = _mapd;
            phieudien.AppendChild(mapd);

            XmlElement maphong = doc.CreateElement("maphong");
            maphong.InnerText = thempd.Maphong;
            phieudien.AppendChild(maphong);

            XmlElement chisocu = doc.CreateElement("chisocu");
            if (root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (thempd.Thang - 1).ToString() + "']/chisomoi") != null)
                chisocu.InnerText = root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (thempd.Thang - 1).ToString() + "']/chisomoi").InnerText;
            else chisocu.InnerText = "0";
            phieudien.AppendChild(chisocu);
            
            XmlElement chisomoi = doc.CreateElement("chisomoi");
            chisomoi.InnerText = thempd.Csm.ToString();
            phieudien.AppendChild(chisomoi);

            XmlElement thang = doc.CreateElement("thang");
            thang.InnerText = thempd.Thang.ToString();
            phieudien.AppendChild(thang);

            XmlElement nam = doc.CreateElement("nam");
            nam.InnerText = thempd.Nam.ToString();
            phieudien.AppendChild(nam);

            if (root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (thempd.Thang + 1).ToString() + "']/chisocu") != null)
            {
                root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (thempd.Thang + 1).ToString() + "']/chisocu").InnerText = chisomoi.InnerText;
            }

            root.InsertAfter(phieudien, root.SelectSingleNode("phieuthudien[last()]"));
            doc.Save(path);
        }

        public void Sua(PhieuDienDTO phongsua)
        {
            XmlNode phongcu = root.SelectSingleNode("phieuthudien[maptd = '" + phongsua.Maptd + "']");
            if (phongcu != null)
            {
                XmlNode phongsuamoi = doc.CreateElement("phieuthudien");

                XmlElement Maptd = doc.CreateElement("maptd");
                Maptd.InnerText = phongsua.Maptd;
                phongsuamoi.AppendChild(Maptd);

                XmlElement maphong = doc.CreateElement("maphong");
                maphong.InnerText = phongsua.Maphong;
                phongsuamoi.AppendChild(maphong);


                XmlElement chisocu = doc.CreateElement("chisocu");
                if (root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi") != null)
                    chisocu.InnerText = root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi").InnerText;
                else chisocu.InnerText = phongsua.Csc.ToString();
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
                //   XmlElement csc = doc.CreateElement("chisocu");
                //   csc.InnerText = chisomoi.InnerText;
                //  root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang = '" + int.Parse(thang.InnerText) + 1 + "']")
                //      .ReplaceChild(csc,chisocu);
                if (root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang + 1).ToString() + "']/chisocu") != null)
                {
                    root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang + 1).ToString() + "']/chisocu").InnerText = chisomoi.InnerText;
                }
                if (root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi") != null)
                {
                    root.SelectSingleNode("phieuthudien[maphong = '" + maphong.InnerText + "' and thang ='" + (phongsua.Thang - 1).ToString() + "']/chisomoi").InnerText = chisocu.InnerText;
                }

                root.ReplaceChild(phongsuamoi, phongcu);
                doc.Save(path);
            }
        }
        public void Xoa(PhieuDienDTO phongxoa)
        {
            XmlNode phieucanxoa = root.SelectSingleNode("phieuthudien[maptd = '" + phongxoa.Maptd + "']");
            if (phieucanxoa != null)
            {
                root.RemoveChild(phieucanxoa);
                doc.Save(path);
            }
        }
        public void Timkiem(PhieuDienDTO phieutim, DataGridView dgv)
        {
           
            XmlNodeList phieutd = root.SelectNodes("phieuthudien[maphong = '" + phieutim.Maphong + "']");
            int index = 0;
            if (phieutd == null) return;
            foreach (XmlNode item in phieutd)
            {
                if (phieutd != null)
                {
                    dgv.ColumnCount = 6;
                    dgv.Rows.Add();
                    XmlNode mapd = item.SelectSingleNode("maptd");
                    dgv.Rows[index].Cells[0].Value = mapd.InnerText;
                    XmlNode maphong = item.SelectSingleNode("maphong");
                    dgv.Rows[index].Cells[1].Value = maphong.InnerText;
                    XmlNode tiendien = item.SelectSingleNode("chisocu");
                    dgv.Rows[index].Cells[2].Value = tiendien.InnerText;
                    XmlNode tiennuoc = item.SelectSingleNode("chisomoi");
                    dgv.Rows[index].Cells[3].Value = tiennuoc.InnerText;
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
            XmlNodeList ds = root.SelectNodes("phieuthudien");
            foreach (XmlNode item in ds)
            {
                dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = item.SelectSingleNode("maptd").InnerText;
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
