using CafeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class NHACUNGCAP_DAO
    {
        private Connection cn = new Connection();
        public void InsertNCC(NHACUNGCAP ncc)
        {
            string query = "insertNCC";
            string[] para = new string[3] { "@ten", "@diachi", "@sdt" };
            object[] value = new object[3] { ncc.TEN, ncc.DIACHI, ncc.SDT };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}