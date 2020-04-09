using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.Models
{
    class THONGTINCHITIETDH
    {
        public THONGTINCHITIETDH(CHITIETDONHANG CTDH, SANPHAM sp)
        {
            ID = CTDH.ID;
            LOAISP = sp.LOAISP_ID;
            KHOILUONG = sp.KHOILUONG;
            SOLUONG = CTDH.SOLUONG;
            DONGIA = CTDH.DONGIA;
            THANHTIEN = CTDH.THANHTIEN;
        }
        public THONGTINCHITIETDH() { }
        public int ID { get; set; }
        public int LOAISP { get; set; }
        public int KHOILUONG { get; set; }
        public int SOLUONG { get; set; }
        public int DONGIA { get; set; }
        public int THANHTIEN { get; set; }
    }
}
