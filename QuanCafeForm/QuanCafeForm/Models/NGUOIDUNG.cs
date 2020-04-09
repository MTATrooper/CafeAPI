namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class NGUOIDUNG
    {
        public NGUOIDUNG()
        {
            //BINHLUAN = new HashSet<BINHLUAN>();
            //DONHANG = new HashSet<DONHANG>();
        }

        public int ID { get; set; }

        public string TEN { get; set; }

        public string TENDANGNHAP { get; set; }

        public string MATKHAU { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public string EMAIL { get; set; }

        public int QUYEN { get; set; }

        //public virtual ICollection<BINHLUAN> BINHLUAN { get; set; }

        //public virtual ICollection<DONHANG> DONHANG { get; set; }
    }
}
