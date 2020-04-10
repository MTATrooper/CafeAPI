using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeClientWeb.Models;
using CafeClientWeb.DAO;
using System.Web.Script.Serialization;

namespace CafeClientWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession  = "CartSession";

        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>) cart;
            }
            return View(list);
        }
        public ActionResult CartHeader()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            int TotalPrice = 0;
            int i = 0;
            int dem = 0;
            decimal[] value = new decimal[10000];
            foreach (var item in list)
            {
                i++;
                var price = item.ProductModel.GIA;
                TotalPrice += (int)(price * item.Quantity);
                dem += item.Quantity;
            }
            ViewBag.SL = dem;
            ViewBag.Total = TotalPrice;
            return PartialView(list);
        }
        public ActionResult Success()
        {
            db.CHITIETDONHANG = new ConnectApi().DeserializeObject<List<CHITIETDONHANG>>("api/CHITIETDONHANG/");
            db.DONHANG = new ConnectApi().DeserializeObject<List<DONHANG>>("api/DONHANG/");
            return View();
        }
        public ActionResult AddToCart(int SanPhamID, int LoaiSpID, int quantity)
        {
            var product = new LoaiSPDAO().Detail(LoaiSpID, SanPhamID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.ProductModel.ID == SanPhamID))
                {
                    foreach (var item in list)
                    {
                        if (item.ProductModel.ID == SanPhamID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var tmp = new CartItem();
                    tmp.ProductModel = product;
                    tmp.Quantity = quantity;
                    list.Add(tmp);
                }
                Session[CartSession] = list;
            }
            else
            {
                var tmp = new CartItem();
                tmp.ProductModel = product;
                tmp.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(tmp);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public JsonResult Update(string cartModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có

            int? tong = 0;
            int giaSp = (int)db.PRICE.Where(x => x.SANPHAM_ID == cart.SingleOrDefault().ProductModel.ID).SingleOrDefault().GIABAN;
            int? thanhtien = giaSp * cart.SingleOrDefault().Quantity;
            foreach (var item in sessionCart)
            {//lặp lấy sản phảm update
                var itemCart = cart.SingleOrDefault(x => x.ProductModel.ID == item.ProductModel.ID);
                if (itemCart != null)
                {
                    item.Quantity = itemCart.Quantity;
                }
                tong += item.Quantity * item.ProductModel.GIA;
            }

            return Json(new
            {
                status = true,
                Tong = tong,
                Thanhtien = thanhtien
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];//lấy danh sách các sản phẩm trong giỏ hàng hiện có
            sessionCart.RemoveAll(x => x.ProductModel.ID == id);
            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult Payment(string shipName, string shipPhone, string shipAddress)
        {
            var order = new DONHANG();
            order.TENNGUOINHAN = shipName;
            order.NGAYDAT = DateTime.Now;
            order.DIACHI = shipAddress;
            order.SDT = shipPhone;
            order.TRANGTHAI_ID = 1;
            order.TONGTIEN = 0;
            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var model = new OrderDetailDAO();
                int total = 0;
                foreach (var item in cart)
                {
                    int? price = item.ProductModel.GIA;
                    var orderDetail = new CHITIETDONHANG();
                    orderDetail.SANPHAM_ID = item.ProductModel.ID;
                    orderDetail.DONHANG_ID = id;
                    orderDetail.DONGIA = price;
                    orderDetail.SOLUONG = item.Quantity;
                    //int ThanhTien = (int)(item.Quantity * price);
                    //total += ThanhTien;

                    model.Create(orderDetail);
                    Session[CartSession] = null;
                }
                //new OrderDAO().EditTotal(total, id);               

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }
        public ActionResult PaymentUserLogin()
        {
            var username = Session["Member"];//lấy session gáng giá trị vào order
            var model = new UserDAO().FindUserName(username.ToString());
            var order = new DONHANG();
            order.NGAYDAT = DateTime.Now;
            order.TENNGUOINHAN = model.TEN;
            order.DIACHI = model.DIACHI;
            order.SDT = model.SDT;
            order.KHACHHANG_ID = model.ID;
            order.TRANGTHAI_ID = 1;
            order.TONGTIEN = 0;
            //insert order

            var id = new OrderDAO().Create(order);//trả về id của order
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                var detail = new OrderDetailDAO();
                int total = 0;
                foreach (var item in cart)
                {
                    int? price = item.ProductModel.GIA;
                    var orderDetail = new CHITIETDONHANG();
                    orderDetail.SANPHAM_ID = item.ProductModel.ID;
                    orderDetail.DONHANG_ID = id;
                    orderDetail.DONGIA = price;
                    orderDetail.SOLUONG = item.Quantity;
                    //int ThanhTien = (int)(item.Quantity * price);
                    //total += ThanhTien;

                    detail.Create(orderDetail);
                    Session[CartSession] = null;
                }
                //new OrderDAO().EditTotal(total, id);

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Success", "Cart");
        }

        public ActionResult OrderByUser()
        {
            var username = Session["Member"];//lấy session gáng giá trị vào order
            var x = new UserDAO().FindUserName(username.ToString());
            var listOrder = new OrderDAO().GetDH_User(x.ID);
            List<TheoDoiDonHang> list = (from p in listOrder.ToList()
                        select new TheoDoiDonHang
                        {
                            ID = p.ID,
                            NGAYMUA = p.NGAYDAT,
                            TENSP = (from ct in db.CHITIETDONHANG
                                     from sp in db.SANPHAM
                                     where ct.SANPHAM_ID == sp.ID && ct.DONHANG_ID == p.ID
                                     select sp.LOAISP_ID
                                     ).ToList(),
                            TONGTIEN = p.TONGTIEN,
                            TRANGTHAI = db.TRANGTHAI.Where(m => m.ID == p.TRANGTHAI_ID).FirstOrDefault().TEN
                        }).ToList();
            return View(list);
        }
    }
}


public class TheoDoiDonHang
{
    public int ID { get; set; }
    public DateTime? NGAYMUA { get; set; }
    public List<int?> TENSP { get; set; }
    public int? TONGTIEN { get; set; }
    public string TRANGTHAI { get; set; }
}