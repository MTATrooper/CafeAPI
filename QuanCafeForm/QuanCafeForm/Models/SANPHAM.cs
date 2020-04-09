namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SANPHAM
    {
        public SANPHAM()
        {
            //BAIBAO = new HashSet<BAIBAO>();
            //BINHLUAN = new HashSet<BINHLUAN>();
            //CHITIETDONHANG = new HashSet<CHITIETDONHANG>();
            //CHITIETNHAPHANG = new HashSet<CHITIETNHAPHANG>();
            //PRICE = new HashSet<PRICE>();
            //THONGKE = new HashSet<THONGKE>();
        }

        public int ID { get; set; }

        public int KHOILUONG { get; set; }

        public string ANH { get; set; }

        public string MOTA { get; set; }

        public int SOLUONG { get; set; }

        public int LOAISP_ID { get; set; }

        //public virtual ICollection<BAIBAO> BAIBAO { get; set; }

        //public virtual ICollection<BINHLUAN> BINHLUAN { get; set; }

        //public virtual ICollection<CHITIETDONHANG> CHITIETDONHANG { get; set; }

        //public virtual ICollection<CHITIETNHAPHANG> CHITIETNHAPHANG { get; set; }

        //public virtual LOAISP LOAISP { get; set; }

        //public virtual ICollection<PRICE> PRICE { get; set; }

        //public virtual ICollection<THONGKE> THONGKE { get; set; }
        public Image image { get; set; }
        public int price { get; set; }
    }
}
