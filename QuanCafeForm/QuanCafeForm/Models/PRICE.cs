namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class PRICE
    {
        public int ID { get; set; }

        public int GIABAN { get; set; }

        public DateTime BATDAU { get; set; }

        public DateTime? KETTHUC { get; set; }

        public int SANPHAM_ID { get; set; }

        //public virtual SANPHAM SANPHAM { get; set; }
    }
}
