using CafeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class NhapHangDAO
    {
        private Connection cn = new Connection();
        public int[] ConvertStringToArray(string s)
        {
            string[] a = s.Split(',');
            int[] so = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                so[i] = Convert.ToInt32(a[i].Trim());
            }
            return so;
        }
        public int InsertNhapHang(NHAPHANG nh)
        {
            try
            {
                string query = "insertNhaphang";
                string[] para = new string[1] { "@idNCC" };
                object[] value = new object[1] { nh.NHACC_ID };
                cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
                query = "select dbo.getNewIdNhapHang()";
                DataTable tb = cn.LoadTable(query);
                int id = Convert.ToInt32(tb.Rows[0][0]);
                return id;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
        public void InsertCTNhapHang(CHITIETNHAPHANG ct)
        {
            string query = "insertCHITIETNHAPHANG";
            string[] para = new string[4] { "@idSanPham", "@idNhapHang", "@soluongnhap", "@gianhap" };
            object[] value = new object[4] { ct.SANPHAM_ID, ct.NHAPHANG_ID, ct.SOLUONGNHAP, ct.GIANHAP };
            cn.Excute_Sql(query, CommandType.StoredProcedure, para, value);
        }
    }
}