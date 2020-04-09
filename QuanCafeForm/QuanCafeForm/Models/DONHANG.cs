namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class DONHANG
    {
        public DONHANG()
        {
            //CHITIETDONHANG = new HashSet<CHITIETDONHANG>();
        }

        public int ID { get; set; }

        public DateTime NGAYDAT { get; set; }

        public string TENNGUOINHAN { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public int TONGTIEN { get; set; }

        public int TRANGTHAI_ID { get; set; }

        public int? KHACHHANG_ID { get; set; }

        //public virtual ICollection<CHITIETDONHANG> CHITIETDONHANG { get; set; }

        //public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        //public virtual TRANGTHAIDH TRANGTHAIDH { get; set; }
    }
}
