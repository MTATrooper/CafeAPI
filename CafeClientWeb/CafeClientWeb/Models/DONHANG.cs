using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class DONHANG
    {
        public int ID { get; set; }

        public DateTime? NGAYDAT { get; set; }

        public string TENNGUOINHAN { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public int? TONGTIEN { get; set; }

        public int? TRANGTHAI_ID { get; set; }

        public int? KHACHHANG_ID { get; set; }

        public DONHANG() { }
        public DONHANG(DateTime? NGAYDAT, string TENNGUOINHAN, string SDT, string DIACHI, int? TONGTIEN, int? TRANGTHAI_ID, int? KHACHHANG_ID)
        {
            this.NGAYDAT = NGAYDAT;
            this.TENNGUOINHAN = TENNGUOINHAN;
            this.SDT = SDT;
            this.DIACHI = DIACHI;
            this.TONGTIEN = TONGTIEN;
            this.TRANGTHAI_ID = TRANGTHAI_ID;
            this.KHACHHANG_ID = KHACHHANG_ID;
        }
    }
}