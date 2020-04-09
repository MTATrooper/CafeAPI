namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;

    public class CHITIETNHAPHANG
    {
        public int ID { get; set; }

        public int SANPHAM_ID { get; set; }

        public int NHAPHANG_ID { get; set; }

        public int SOLUONGNHAP { get; set; }

        public int GIANHAP { get; set; }

        public int SOLUONGCONLAI { get; set; }

        //public virtual NHAPHANG NHAPHANG { get; set; }

        //public virtual SANPHAM SANPHAM { get; set; }
    }
}
