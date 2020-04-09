using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class CHITIETDONHANG
    {
        public int ID { get; set; }

        public int? SANPHAM_ID { get; set; }

        public int? DONHANG_ID { get; set; }

        public int? SOLUONG { get; set; }

        public int? DONGIA { get; set; }

        public CHITIETDONHANG() { }
        public CHITIETDONHANG(int? SANPHAM_ID, int? DONHANG_ID, int? SOLUONG)
        {
            this.SANPHAM_ID = SANPHAM_ID;
            this.DONHANG_ID = DONHANG_ID;
            this.SOLUONG = SOLUONG;
        }
    }
}