using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class BAIBAO
    {
        public int ID { get; set; }

        public string TIEUDE { get; set; }

        public string NOIDUNG { get; set; }

        public int? SANPHAM_ID { get; set; }

        public BAIBAO() { }
        public BAIBAO(string TIEUDE, string NOIDUNG, int? SANPHAM_ID)
        {
            this.TIEUDE = TIEUDE;
            this.NOIDUNG = NOIDUNG;
            this.SANPHAM_ID = SANPHAM_ID;
        }
    }
}