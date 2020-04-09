namespace QuanCafeForm.Models
{
    using System;
    using System.Collections.Generic;

    public class NHACUNGCAP
    {
        public NHACUNGCAP()
        {
            //NHAPHANG = new HashSet<NHAPHANG>();
        }

        public int ID { get; set; }

        public string TEN { get; set; }

        public string DIACHI { get; set; }

        public string SDT { get; set; }

        //public virtual ICollection<NHAPHANG> NHAPHANG { get; set; }
    }
}
