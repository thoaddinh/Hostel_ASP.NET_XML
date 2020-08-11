using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorHostel_xml.DTO
{
    public class ThanhToanDTO
    {
        private String _maTT;
        private String _maphong;
        private String _tiendien;
        private String _tiennuoc;
        private String _tienphong;
        private int _thang;
        private int _nam;

        public string MaTT
        {
            get
            {
                return _maTT;
            }

            set
            {
                _maTT = value;
            }
        }

        public string Maphong
        {
            get
            {
                return _maphong;
            }

            set
            {
                _maphong = value;
            }
        }

        public string Tiendien
        {
            get
            {
                return _tiendien;
            }

            set
            {
                _tiendien = value;
            }
        }

        public string Tiennuoc
        {
            get
            {
                return _tiennuoc;
            }

            set
            {
                _tiennuoc = value;
            }
        }

        public string Tienphong
        {
            get
            {
                return _tienphong;
            }

            set
            {
                _tienphong = value;
            }
        }

        public int Thang
        {
            get
            {
                return _thang;
            }

            set
            {
                _thang = value;
            }
        }

        public int Nam
        {
            get
            {
                return _nam;
            }

            set
            {
                _nam = value;
            }
        }
    }
}
