using CafeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class ChiTietDH_DAO
    {
        private Connection cn = new Connection();
        public void InsertCHITIETDH(CHITIETDONHANG CTDH)
        {
            string query = "insertCTDH";
            string[] para = new string[4] { "@idSP", "@idDH", "@soluong", "@giaban" };
            object[] value = new object[4] { CTDH.SANPHAM_ID, CTDH.DONHANG_ID, CTDH.SOLUONG, CTDH.DONGIA };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}