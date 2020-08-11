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
    public class phongDLL
    {
        XmlDocument doc = new XmlDocument();
        
        XmlElement root;
        string path = "../../xml/VectorHostel1.xml";

        public phongDLL()
        {
            doc.Load(path);
            root = doc.DocumentElement;
        }
        public void themPhong(PhongTroDTO phongThem)
        {
           
            
            XmlElement phong = doc.CreateElement("phongtro");

            XmlElement maphong = doc.CreateElement("maphong");
            //lay ds ma phong
            XmlNodeList maphongs = root.SelectNodes("phongtro/maphong");
            int mamax = 0;
            foreach (XmlNode item in maphongs)
            {
                int mp = int.Parse(item.InnerText.Substring(1));
                if (mp > mamax) mamax = mp;
            }
            mamax++;
            string _maphong = "00" + mamax.ToString();
            maphong.InnerText = "P" + _maphong.Substring(mamax.ToString().Length - 1);
            
            phong.AppendChild(maphong);

            XmlElement tenphong = doc.CreateElement("tenphong");
            tenphong.InnerText = phongThem.Tenphong;
            phong.AppendChild(tenphong);

            XmlElement tang = doc.CreateElement("tang");
            tang.InnerText = phongThem.Tang.ToString();
            phong.AppendChild(tang);

            XmlElement tinhtrang = doc.CreateElement("tinhtrang");
            tinhtrang.InnerText = phongThem.Tinhtrang;
            phong.AppendChild(tinhtrang);

            XmlElement mota = doc.CreateElement("mota");
            mota.InnerText = phongThem.Mota;
            phong.AppendChild(mota);

            XmlElement malp = doc.CreateElement("malp");
            malp.InnerText = phongThem.Loaiphong;
            phong.AppendChild(malp);

            root.InsertAfter(phong, root.SelectSingleNode("phongtro[last()]"));
            doc.Save(path);
        }
      
       public void Sua(PhongTroDTO phongsua)
        {
            XmlNode phongcu = root.SelectSingleNode("phongtro[maphong = '" + phongsua.Maphong + "']");
            if(phongcu != null)
            {
                XmlNode phongsuamoi = doc.CreateElement("phongtro");

                XmlElement maphong = doc.CreateElement("maphong");
                maphong.InnerText = phongsua.Maphong;
                phongsuamoi.AppendChild(maphong);

                XmlElement tenphong = doc.CreateElement("tenphong");
                tenphong.InnerText = phongsua.Tenphong;
                phongsuamoi.AppendChild(tenphong);

                XmlElement tang = doc.CreateElement("tang");
                tang.InnerText = phongsua.Tang.ToString();
                phongsuamoi.AppendChild(tang);

                XmlElement tinhtrang = doc.CreateElement("tinhtrang");
                tinhtrang.InnerText = phongsua.Tinhtrang;
                phongsuamoi.AppendChild(tinhtrang);

                XmlElement mota = doc.CreateElement("mota");
                mota.InnerText = phongsua.Mota;
                phongsuamoi.AppendChild(mota);

                XmlElement malp = doc.CreateElement("malp");
                malp.InnerText = phongsua.Loaiphong;
                phongsuamoi.AppendChild(malp);

                root.ReplaceChild(phongsuamoi,phongcu);
                doc.Save(path);
            }
        }
        public bool xoa(PhongTroDTO phongxoa)
        {
            XmlNode phongcanxoa = root.SelectSingleNode("phongtro[maphong = '" + phongxoa.Maphong + "']");
            if ( (phongcanxoa != null) && (phongcanxoa.SelectSingleNode("tinhtrang").InnerText != "full"))
            {
                if(root.SelectSingleNode("phieuthue[maphong = '"+phongxoa.Maphong+"']")==null)
                {
                    root.RemoveChild(phongcanxoa);
                    doc.Save(path);
                    return true;
                }
            }
            return false;
        }
        public void timkiemphongtrong(PhongTroDTO phongtim, DataGridView dgv)

        {
            int index = 0;
            XmlNodeList phongcantim = root.SelectNodes("phongtro[tinhtrang = 'empty']");
            
            foreach (XmlNode item in phongcantim)
            {
                if (phongcantim != null)
                {
                    dgv.ColumnCount = 6;
                    dgv.Rows.Add();
                    XmlNode maphong = item.SelectSingleNode("maphong");
                    dgv.Rows[index].Cells[0].Value = maphong.InnerText;

                    XmlNode tenphong = item.SelectSingleNode("tenphong");
                    dgv.Rows[index].Cells[1].Value = tenphong.InnerText;

                    XmlNode tang = item.SelectSingleNode("tang");
                    dgv.Rows[index].Cells[2].Value = tang.InnerText;

                    XmlNode tinhtrang = item.SelectSingleNode("tinhtrang");
                    dgv.Rows[index].Cells[3].Value = tinhtrang.InnerText;

                    XmlNode mota = item.SelectSingleNode("mota");
                    dgv.Rows[index].Cells[4].Value = mota.InnerText;

                    string var = item.SelectSingleNode("malp").InnerText; ;
                    switch (var)
                    {
                        case "L01":
                            dgv.Rows[index].Cells[5].Value = "phòng thường";
                            break;
                        case "L02":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 1";
                            break;
                        case "L03":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 2";
                            break;
                        case "L04":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 3";
                            break;
                        case "L05":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 4";
                            break;
                    }
                    index++;
                }
            }
        }
             public void timkiemphongfull(PhongTroDTO phongtim, DataGridView dgv)

        {
            int index = 0;
            XmlNodeList phongcantim = root.SelectNodes("phongtro[tinhtrang = 'full']");
            foreach (XmlNode item in phongcantim)
            {


                if (phongcantim != null)
                {
                    dgv.ColumnCount = 6;
                    dgv.Rows.Add();

                    dgv.Rows[index].Cells[0].Value = item.SelectSingleNode("maphong").InnerText;

                    XmlNode tenphong = item.SelectSingleNode("tenphong");
                    dgv.Rows[index].Cells[1].Value = tenphong.InnerText;

                    XmlNode tang = item.SelectSingleNode("tang");
                    dgv.Rows[index].Cells[2].Value = tang.InnerText;

                    XmlNode tinhtrang = item.SelectSingleNode("tinhtrang");
                    dgv.Rows[index].Cells[3].Value = tinhtrang.InnerText;

                    XmlNode mota = item.SelectSingleNode("mota");
                    dgv.Rows[index].Cells[4].Value = mota.InnerText;

                    
                    string var = item.SelectSingleNode("malp").InnerText; ;
                    switch (var)
                    {
                        case "L01":
                            dgv.Rows[index].Cells[5].Value = "phòng thường";
                            break;
                        case "L02":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 1";
                            break;
                        case "L03":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 2";
                            break;
                        case "L04":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 3";
                            break;
                        case "L05":
                            dgv.Rows[index].Cells[5].Value = "phòng vip 4";
                            break;
                    }
                    index++;
                }
            }
        }
        
        public void HienThi(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.ColumnCount = 6;
            int index = 0; //luu chi so dong
            XmlNodeList ds = root.SelectNodes("phongtro");
            foreach (XmlNode item in ds)
            {
                dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = item.SelectSingleNode("maphong").InnerText;
                dgv.Rows[index].Cells[1].Value = item.SelectSingleNode("tenphong").InnerText;
                dgv.Rows[index].Cells[2].Value = item.SelectSingleNode("tang").InnerText;
                dgv.Rows[index].Cells[3].Value = item.SelectSingleNode("tinhtrang").InnerText;
                dgv.Rows[index].Cells[4].Value = item.SelectSingleNode("mota").InnerText;
                string var = item.SelectSingleNode("malp").InnerText; ;
                switch (var)
                {
                    case "L01":
                        dgv.Rows[index].Cells[5].Value = "phòng thường";
                        break;
                    case "L02":
                        dgv.Rows[index].Cells[5].Value = "phòng vip 1";
                        break;
                    case "L03":
                        dgv.Rows[index].Cells[5].Value = "phòng vip 2";
                        break;
                    case "L04":
                        dgv.Rows[index].Cells[5].Value = "phòng vip 3";
                        break;
                    case "L05":
                        dgv.Rows[index].Cells[5].Value = "phòng vip 4";
                        break;
                }
                
                index++;
            }
        }
    }
}
