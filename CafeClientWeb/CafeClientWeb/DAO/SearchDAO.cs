using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeClientWeb.Models;

namespace CafeClientWeb.DAO
{
    public class SearchDAO
    {
        public List<HOMELOAISP> AdvancedSearch(string name, string hangsx, int? minprice, int? maxprice, int? sapxep)
        {
            var lst = (from p in db.LOAISP.OrderByDescending(x => x.NGAYTAO)
                       select new HOMELOAISP()
                       {
                           ID = p.ID,
                           TEN = p.TEN,
                           MOTA = "Tốt",
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

                       });
            if (!string.IsNullOrEmpty(name))
                lst = lst.Where(u => u.TEN.ToUpper().Contains(name.ToUpper()));
            if (!string.IsNullOrEmpty(hangsx))
                lst = (from p in lst where p.HANGSX.Equals(hangsx) select p);

            if (minprice != null)
                lst = (from p in lst where p.GIA >= minprice select p);
            if (maxprice != null)
                lst = (from p in lst where p.GIA <= maxprice select p);

            if (sapxep == 2) lst = lst.OrderByDescending(p => p.GIA);
            else if (sapxep == 1) lst = lst.OrderBy(p => p.GIA);
            else lst = lst.OrderBy(p => p.TEN);
            return lst.ToList();
        }
    }
}