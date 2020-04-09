using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class User
    {
        public int ID { get; set; }

        public string TEN { get; set; }

        public string TENDANGNHAP { get; set; }

        public string MATKHAU { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public string EMAIL { get; set; }

        public int? QUYEN { get; set; }
        public User() { }
        public User(string TEN, string TENDANGNHAP, string SDT, string DIACHI, string EMAIL,string MATKHAU)
        {
            this.TEN = TEN;
            this.TENDANGNHAP = TENDANGNHAP;
            this.SDT = SDT;
            this.DIACHI = DIACHI;
            this.EMAIL = EMAIL;
            this.MATKHAU = MATKHAU;
        }
    }
}