using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeClientWeb.Models;
using CafeClientWeb.DAO;

namespace CafeClientWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            List<HANGSX> list = db.HANGSX.ToList();
            return PartialView(list);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Find(string ten)
        {
            return RedirectToAction("AdvancedSearchView", "Search", new { name = ten });
        }
        public ActionResult Login()
        {
            db.CHITIETDONHANG = new ConnectApi().DeserializeObject<List<CHITIETDONHANG>>("api/CHITIETDONHANG/");
            db.DONHANG = new ConnectApi().DeserializeObject<List<DONHANG>>("api/DONHANG/");
            db.TRANGTHAI = new ConnectApi().DeserializeObject<List<TRANGTHAI>>("api/TRANGTHAIDH/");
            return View();
        }

        [HttpPost]
        public ActionResult Register(string TENDANGNHAP, string TEN, string SDT, string DIACHI, string MATKHAU, string EMAIL)
        {
            User user = new User(TEN, TENDANGNHAP, SDT, DIACHI, EMAIL, MATKHAU);
            var db = new UserDAO();
            var kq = db.CreateUSer(user);
            if (kq == 1)
            {
                //dang ki thanh cong
                TempData["Success"] = "Thêm tài khoản thành công";
                Login(user.TENDANGNHAP, user.MATKHAU);
                return RedirectToAction("Index", "Home");
            }
            else if (kq == 0)
            {
                TempData["Error"] = "Email đã được đăng kí";
            }
            else if (kq == -1)
            {
                TempData["Error"] = "Tài khoản đã tồn tại";
            }
            else
            {
                ModelState.AddModelError("", "Thêm tài khoản thất bại");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int kq = new UserDAO().LoginClient(username, password);
            var user = new UserDAO().FindUserName(username);
            if (kq == -1)
            {
                TempData["Error"] = "Tài khoản không tồn tại";
            }
            else if (kq == 0)
            {
                TempData["Error"] = "Tài khoản đã bị khóa";
            }
            else if (kq == 1)
            {
                Session["Member"] = username;
                Session["MemberID"] = user.ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Đăng nhập không thành công";
            }
            return View("Register");
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            Session["Error"] = null;
            Session["Success"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult TinBai()
        {
            return View();
        }
        public ActionResult mail()
        {
            return View();
        }
    }
}