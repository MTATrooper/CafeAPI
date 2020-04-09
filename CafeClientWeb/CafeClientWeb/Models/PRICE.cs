using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class PRICE
    {
        public int ID { get; set; }
        public int? GIABAN { get; set; }
        public DateTime? BATDAU { get; set; }
        public DateTime? KETTHUC { get; set; }

        public int? SANPHAM_ID { get; set; }
        public PRICE() { }
        public PRICE(int? GIABAN, DateTime? BATDAU, DateTime? KETTHUC, int? SANPHAM_ID)
        {
            this.GIABAN = GIABAN;
            this.BATDAU = BATDAU;
            this.KETTHUC = KETTHUC;
            this.SANPHAM_ID = SANPHAM_ID;
        }
    }
}