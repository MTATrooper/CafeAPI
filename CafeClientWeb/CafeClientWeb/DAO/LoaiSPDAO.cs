using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeClientWeb.Models;

namespace CafeClientWeb.DAO
{
    public class LoaiSPDAO
    {
        public static ConnectApi connect = new ConnectApi();
        public List<HOMELOAISP> GetNewProduct()
        {
            db.SANPHAM = connect.DeserializeObject<List<SANPHAM>>("api/SANPHAM/").ToList();
            db.HANGSX = connect.DeserializeObject<List<HANGSX>>("api/HANGSX/").ToList();
            db.LOAISP = connect.DeserializeObject<List<LOAISP>>("api/LOAISP/").ToList();
            db.PRICE = connect.DeserializeObject<List<PRICE>>("api/PRICE/").ToList();
            db.DONHANG = connect.DeserializeObject<List<DONHANG>>("api/DONHANG").ToList();
            db.Users = connect.DeserializeObject<List<User>>("api/NGUOIDUNG").ToList();
            db.CHITIETDONHANG = connect.DeserializeObject<List<CHITIETDONHANG>>("api/CHITIETDONHANG").ToList();
            var product = (from p in db.LOAISP.OrderByDescending(x => x.NGAYTAO)
                           where p.TRANGTHAI == "Đang bán"
                           select new HOMELOAISP()
                           {
                               ID = p.ID,
                               TEN = p.TEN,
                               MOTA = p.MOTA,
                               ANH = p.ANH,
                               NGAYTAO = p.NGAYTAO,
                               HANGSX = ((from z in db.HANGSX
                                          where z.ID == p.HANGSX_ID
                                          select new
                                          {
                                              z.TEN
                                          }).FirstOrDefault().TEN),

                               GIA = (from x in db.PRICE
                                      from y in db.SANPHAM
                                      where y.LOAISP_ID == p.ID && x.SANPHAM_ID == y.ID
                                      orderby x.GIABAN
                                      select new
                                      {
                                          x.GIABAN
                                      }).FirstOrDefault().GIABAN,
                               //SOSAO = (from x in db.BINHLUAN
                               //         join y in db.SANPHAM on x.SANPHAM_ID equals y.ID
                               //         where y.LOAISP_ID == p.ID
                               //         group x.SOSAO by x.SANPHAM_ID into g
                               //         select new
                               //         {
                               //             t = g.Key
                               //         }).FirstOrDefault().t

                           }).ToList();
            var model = product.ToList().Take(7);
            return model.ToList();
        }
        public HOMELOAISP Detail(int LoaiSpID, int SanPhamID)
        {
            var product = (from p in db.SANPHAM
                           where p.LOAISP_ID == LoaiSpID && p.ID == SanPhamID
                           select new HOMELOAISP
                           {
                               ID = p.ID,
                               KHOILUONG = p.KHOILUONG,
                               ANH = p.ANH,
                               MOTA = p.MOTA,
                               SOLUONG = p.SOLUONG,
                               GIA = (from x in db.PRICE
                                      where x.SANPHAM_ID == p.ID
                                      select x.GIABAN
                                      ).FirstOrDefault(),
                               TEN = (from y in db.LOAISP
                                      where y.ID == p.LOAISP_ID
                                      select y.TEN
                                      ).FirstOrDefault()
                           }).FirstOrDefault();
            //HOMELOAISP model = product;
            return (HOMELOAISP)product;
        }
        public HOMELOAISP RecentProduct(int id)
        {
            var product = (from p in db.LOAISP
                           where p.ID == id
                           select new HOMELOAISP
                           {
                               ID = p.ID,
                               TEN = p.TEN,
                               MOTA = p.MOTA,
                               ANH = p.ANH,
                               NGAYTAO = p.NGAYTAO,
                               HANGSX = ((from z in db.HANGSX
                                          where z.ID == p.HANGSX_ID
                                          select new
                                          {
                                              z.TEN
                                          }).FirstOrDefault().TEN),

                               GIA = (from x in db.PRICE
                                      from y in db.SANPHAM
                                      where y.LOAISP_ID == p.ID && x.SANPHAM_ID == y.ID
                                      orderby x.GIABAN
                                      select new
                                      {
                                          x.GIABAN
                                      }).FirstOrDefault().GIABAN,
                           }).FirstOrDefault();
            return (HOMELOAISP)product;
        }
    }
    public class HOMELOAISP
    {
        public int ID { get; set; }
        public string TEN { get; set; }
        public string ANH { get; set; }
        public string MOTA { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public string HANGSX { get; set; }
        public int? GIA { get; set; }

        public int? KHOILUONG { get; set; }
        public int? SOLUONG { get; set; }
        public int? SOSAO { get; set; } 
        public HOMELOAISP() { }
        public HOMELOAISP(string TEN, string ANH, string MOTA, DateTime? NGAYTAO, string HANGSX, int? GIA, int? SOLUONG, int? KHOILUONG, int? SOSAO)
        {
            this.TEN = TEN;
            this.ANH = ANH;
            this.MOTA = MOTA;
            this.NGAYTAO = NGAYTAO;
            this.HANGSX = HANGSX;
            this.GIA = GIA;
            this.SOSAO = SOSAO;
            this.KHOILUONG = KHOILUONG;
            this.SOLUONG = SOLUONG;
        }
    }
}