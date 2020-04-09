using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeClientWeb.DAO;

namespace CafeClientWeb.Models
{
    public class CartItem
    {
        public HOMELOAISP ProductModel { get; set; }
        public int Quantity { get; set; }
    }
}