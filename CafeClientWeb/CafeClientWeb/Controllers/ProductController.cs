using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeClientWeb.DAO;
using CafeClientWeb.Models;

namespace CafeClientWeb.Controllers
{
    public class ProductController : Controller
    {
        private const string ProductSession = "ProductSession";
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult NewProduct()
        {
            LoaiSPDAO sp = new LoaiSPDAO();
            var model = sp.GetNewProduct();
            return PartialView(model);
        }
        public ActionResult RecentProduct()
        {
            var product = Session[ProductSession];
            var list = new List<HOMELOAISP>();
            if (product != null)
            {
                list = (List<HOMELOAISP>)product;
            }
            return PartialView(list);
        }
       
        public ActionResult Detail(int LoaiSpID, int SanPhamID)
        {
            LoaiSPDAO sp = new LoaiSPDAO();
            var model = sp.Detail(LoaiSpID, SanPhamID);
            var recent = sp.RecentProduct(LoaiSpID);
            List<int?> listKLSP = (from p in db.SANPHAM
                                where p.LOAISP_ID == LoaiSpID
                                select p.KHOILUONG
                                ).ToList();
            ViewBag.listKLSP = listKLSP;
            ViewBag.LoaiSpId = LoaiSpID;

            var product = Session[ProductSession];
            if(product != null)
            {
                var list = (List<HOMELOAISP>) product;
                if(list.Exists(x => x.ID == LoaiSpID) == false)
                {
                    list.Add(recent);
                }
                Session[ProductSession] = list;
            }
            else
            {
                var list = new List<HOMELOAISP>();
                list.Add(recent);
                Session[ProductSession] = list;
            }
            return View(model);
        }

        public JsonResult DetailKL(int LoaiSpID, int KhoiLuong)
        {
            LoaiSPDAO sp = new LoaiSPDAO();
            int SanPhamID = db.SANPHAM.Find(x => x.KHOILUONG == KhoiLuong).ID;
            ViewBag.SanPhamID = SanPhamID;
            var model = sp.Detail(LoaiSpID, SanPhamID);
            return Json(model);
        }
    }
}