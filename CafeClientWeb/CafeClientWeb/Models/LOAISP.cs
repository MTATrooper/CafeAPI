using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class LOAISP
    {
        public int ID { get; set; }

        public string TEN { get; set; }
        public string ANH { get; set; }

        public string MOTA { get; set; }

        public DateTime? NGAYTAO { get; set; }

        public int? HANGSX_ID { get; set; }

        public string TRANGTHAI { get; set; }

        public LOAISP(string TEN, string ANH, string MOTA, DateTime? NGAYTAO, int? HANGSX_ID, string TRANGTHAI)
        {
            this.TEN = TEN;
            this.ANH = ANH;
            this.MOTA = MOTA;
            this.NGAYTAO = NGAYTAO;
            this.HANGSX_ID = HANGSX_ID;
            this.TRANGTHAI = TRANGTHAI;
        }
    }
}