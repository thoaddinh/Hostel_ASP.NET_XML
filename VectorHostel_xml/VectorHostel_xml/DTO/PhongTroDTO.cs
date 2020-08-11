using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorHostel_xml.DTO
{
    public class PhongTroDTO
    {
        private String _maphong;
        private String _tenphong;
        private int _tang;
        private String _tinhtrang;
        private String _mota;
        private String _loaiphong;

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

        public string Tenphong
        {
            get
            {
                return _tenphong;
            }

            set
            {
                _tenphong = value;
            }
        }

        public int Tang
        {
            get
            {
                return _tang;
            }

            set
            {
                _tang = value;
            }
        }

        public string Tinhtrang
        {
            get
            {
                return _tinhtrang;
            }

            set
            {
                _tinhtrang = value;
            }
        }

        public string Mota
        {
            get
            {
                return _mota;
            }

            set
            {
                _mota = value;
            }
        }

        public string Loaiphong
        {
            get
            {
                return _loaiphong;
            }

            set
            {
                _loaiphong = value;
            }
        }
    }
}
