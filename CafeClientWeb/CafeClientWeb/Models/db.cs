using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeClientWeb.Models
{
    public class db
    {
        public static ConnectApi connect = new ConnectApi();
        public static List<HANGSX> HANGSX = new List<HANGSX>();
        public static List<SANPHAM> SANPHAM = new List<SANPHAM>();
        public static List<PRICE> PRICE = new List<PRICE>();
        public static List<LOAISP> LOAISP = new List<LOAISP>();

        public static List<User> Users = new List<User>();
        public static List<DONHANG> DONHANG = new List<DONHANG>();
        //public static List<BAIBAO> BAIBAO = connect.DeserializeObject<List<BAIBAO>>("BAIBAO/").ToList();
        //public static List<BINHLUAN> BINHLUAN = connect.DeserializeObject<List<BINHLUAN>>("BINHLUAN/").ToList();
        public static List<CHITIETDONHANG> CHITIETDONHANG = new List<CHITIETDONHANG>();
        public static List<TRANGTHAI> TRANGTHAI = new List<TRANGTHAI>();


    }
}