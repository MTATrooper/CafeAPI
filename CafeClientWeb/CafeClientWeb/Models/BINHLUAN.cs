using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class BINHLUAN
    {
        public int ID { get; set; }

        public string NOIDUNG { get; set; }

        public int? SOSAO { get; set; }

        public int? SANPHAM_ID { get; set; }

        public int? KHACHHANG_ID { get; set; }
        public BINHLUAN() { }
        public BINHLUAN(string NOIDUNG, int? SOSAO, int? SANPHAM_ID, int? KHACHHANG_ID)
        {
            this.NOIDUNG = NOIDUNG;
            this.SOSAO = SOSAO;
            this.SANPHAM_ID = SANPHAM_ID;
            this.KHACHHANG_ID = KHACHHANG_ID;
        }
    }
}