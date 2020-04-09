namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class CHITIETDONHANG
    {
        public CHITIETDONHANG()
        {
           // XUATHANG = new HashSet<XUATHANG>();
        }

        public int ID { get; set; }

        public int SANPHAM_ID { get; set; }

        public int DONHANG_ID { get; set; }

        public int SOLUONG { get; set; }

        public int DONGIA { get; set; }

        public int THANHTIEN { get; set; }

        //public virtual DONHANG DONHANG { get; set; }

        //public virtual SANPHAM SANPHAM { get; set; }

        //public virtual ICollection<XUATHANG> XUATHANG { get; set; }
    }
}
