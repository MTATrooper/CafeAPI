namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;


    public class LOAISP
    {
        public LOAISP()
        {
            //SANPHAM = new HashSet<SANPHAM>();
        }

        public int ID { get; set; }

        public string TEN { get; set; }

        public string ANH { get; set; }

        public string MOTA { get; set; }

        public DateTime NGAYTAO { get; set; }

        public int HANGSX_ID { get; set; }

        public string TRANGTHAI { get; set; }

        //public virtual HANGSX HANGSX { get; set; }

        //public virtual ICollection<SANPHAM> SANPHAM { get; set; }
    }
}
