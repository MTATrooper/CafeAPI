namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;

    public class NHAPHANG
    {
        public NHAPHANG()
        {
            //CHITIETNHAPHANG = new HashSet<CHITIETNHAPHANG>();
            //XUATHANG = new HashSet<XUATHANG>();
        }

        public int ID { get; set; }

        public DateTime NGAYNHAP { get; set; }

        public int NHACC_ID { get; set; }

        //public virtual ICollection<CHITIETNHAPHANG> CHITIETNHAPHANG { get; set; }

        //public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        public virtual ICollection<XUATHANG> XUATHANG { get; set; }
    }
}
