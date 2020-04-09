using CafeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class Price_DAO
    {
        private Connection cn = new Connection();
        public void InsertPrice(PRICE price)
        {
            string query = "InsertPrice";
            string[] para = new string[2] { "@idSP", "@giaban" };
            object[] value = new object[2] { price.SANPHAM_ID, price.GIABAN };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
        public void UpdatePrice(PRICE price)
        {
            string query = "UpdatePrice";
            string[] para = new string[2] { "@idSP", "@giaban" };
            object[] value = new object[2] { price.SANPHAM_ID, price.GIABAN };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}