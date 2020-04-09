using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class SANPHAM
    {
        public int ID { get; set; }

        public int? KHOILUONG { get; set; }

        public string ANH { get; set; }

        public string MOTA { get; set; }

        public int? SOLUONG { get; set; }

        public int? LOAISP_ID { get; set; }

    }
}