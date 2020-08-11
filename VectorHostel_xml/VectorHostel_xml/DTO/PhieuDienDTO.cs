using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorHostel_xml.DTO
{
    public class PhieuDienDTO
    {
        private String _maptd;
        private String _maphong;
        private Double _csc;
        private Double _csm;
        private int _thang;
        private int _nam;

        public string Maptd
        {
            get
            {
                return _maptd;
            }

            set
            {
                _maptd = value;
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

        public double Csc
        {
            get
            {
                return _csc;
            }

            set
            {
                _csc = value;
            }
        }

        public double Csm
        {
            get
            {
                return _csm;
            }

            set
            {
                _csm = value;
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
