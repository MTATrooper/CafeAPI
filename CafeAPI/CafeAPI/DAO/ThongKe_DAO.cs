using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CafeAPI.Models;

namespace CafeAPI.DAO
{
    public class ThongKe_DAO
    {
        private Connection cn = new Connection();
        public List<THONGKE> getThongKe (ThongKeDate tkDate)
        {
            string query = "select * from dbo.getThongKe(@fromYear,@fromMonth,@toYear,@toMonth)";
            string[] para = new string[4] { "@fromYear", "@fromMonth", "@toYear", "@toMonth" };
            object[] value = new object[4] { tkDate.fromYear, tkDate.fromMonth, tkDate.toYear, tkDate.toMonth };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            List<THONGKE> results = new List<THONGKE>();
            foreach (DataRow row in tb.Rows)
            {
                THONGKE temp = new THONGKE();
                temp.ID = Convert.ToInt32(row["ID"]);
                temp.NGAY = Convert.ToDateTime(row["NGAY"]);
                temp.SANPHAM_ID = Convert.ToInt32(row["SANPHAM_ID"]);
                temp.SOLUONGBAN = Convert.ToInt32(row["SOLUONGBAN"]);
                temp.DOANHTHU = Convert.ToInt32(row["DOANHTHU"]);
                temp.LOINHUAN = Convert.ToInt32(row["LOINHUAN"]);
                results.Add(temp);
            }
            return results;
        }

        public List<SanPhamBanChay> getSanPhamBanChay(ThongKeDate tkDate)
        {
            string query = "select * from dbo.getSanPhamBanChay(@fromYear,@fromMonth,@toYear,@toMonth)";
            string[] para = new string[4] { "@fromYear", "@fromMonth", "@toYear", "@toMonth" };
            object[] value = new object[4] { tkDate.fromYear, tkDate.fromMonth, tkDate.toYear, tkDate.toMonth };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            List<SanPhamBanChay> results = new List<SanPhamBanChay>();
            int i = 1;
            foreach (DataRow row in tb.Rows)
            {
                SanPhamBanChay temp = new SanPhamBanChay();
                temp.STT = i++;
                temp.TenSP = row["TenSP"].ToString();
                temp.KhoiLuong = Convert.ToInt32(row["KhoiLuong"]);
                temp.SoLuongBan = Convert.ToInt32(row["SoLuongBan"]);
                temp.SoLuongHoaDon = Convert.ToInt32(row["SoLuongHoaDon"]);
                results.Add(temp);
            }
            return results;
        }
    }
}