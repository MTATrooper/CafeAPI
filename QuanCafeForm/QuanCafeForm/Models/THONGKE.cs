namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class THONGKE
    {
        public int ID { get; set; }

        public DateTime NGAY { get; set; }

        public int SANPHAM_ID { get; set; }

        public int SOLUONGBAN { get; set; }

        public int DOANHTHU { get; set; }

        public int LOINHUAN { get; set; }

        //public virtual SANPHAM SANPHAM { get; set; }
    }
}
