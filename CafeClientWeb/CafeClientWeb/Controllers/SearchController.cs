using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeClientWeb.DAO;
using CafeClientWeb.Models;
using PagedList;

namespace CafeClientWeb.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdvancedSearchView(string name, string hangsx, int? minprice, int? maxprice, int? page, int? sapxep)
        {
            int pageSize = 9;
            ViewBag.Name = name;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            ViewBag.sapxep = sapxep;
            SearchDAO sp = new SearchDAO();
            var model = sp.AdvancedSearch(name, hangsx, minprice, maxprice, sapxep);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            int c1 = (int)model.Count - pageSize * (pageNumber - 1);
            if (c1 > 3)
            {
                ViewBag.Column1 = 3;
                if (c1 > 6)
                {
                    ViewBag.Column2 = 6;
                    if (c1 > 9) ViewBag.Column3 = 9;
                    else ViewBag.Column3 = c1;
                }
                else
                {
                    ViewBag.Column2 = c1;
                    ViewBag.Column3 = 0;
                }
            }
            else
            {
                ViewBag.Column1 = c1;
                ViewBag.Column2 = 0;
                ViewBag.Column3 = 0;
            }
            
            return PartialView(model.ToPagedList(pageNumber, pageSize));
        }
    }
}