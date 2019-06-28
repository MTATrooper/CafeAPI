using CafeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CafeAPI.DAO
{
    public class SANPHAM_DAO
    {
        private Connection cn = new Connection();
        public List<SANPHAM> GetSANPHAM()
        {
            List<SANPHAM> lst = cn.ConvertToList<SANPHAM>(GetDataSANPHAM());
            return lst;
        }
        public DataTable GetDataSANPHAM()
        {
            string query = "select * from SANPHAM";
            DataTable tb = cn.LoadTable(query);
            return tb;
        }
        public SANPHAM GetSANPHAMbyId(int id)
        {
            string query = "select * from SANPHAM where id = @id";
            string[] para = new string[1] { "@id" };
            object[] value = new object[1] { id };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            SANPHAM sp = cn.ConvertToList<SANPHAM>(tb)[0];
            return sp;
        }
        public List<SANPHAM> GetSANPHAMByLSP(int id)
        {
            string query = "select s.ID,s.KHOILUONG,s.ANH,s.MOTA,s.SOLUONG,s.LOAISP_ID from SANPHAM s join LOAISP ls on s.LOAISP_ID = ls.ID where ls.ID = @id";
            string[] para = new string[1] { "@id" };
            object[] value = new object[1] { id };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            List<SANPHAM> sp = cn.ConvertToList<SANPHAM>(tb);
            return sp;
        }
        public void InsertSANPHAM(SANPHAM sp)
        {
            string query = "insert into SANPHAM values(@khoiluong, @anh, @mota, @soluong,@idloaisp)";
            string[] para = new string[5] { "@khoiluong", "@anh", "@mota", "@soluong", "@idloaisp" };
            object[] value = new object[5] { sp.KHOILUONG, sp.ANH, sp.MOTA, sp.SOLUONG, sp.LOAISP_ID };
            cn.Excute_Sql(query, CommandType.Text, para, value);
        }
        public void UpdateSANPHAM(SANPHAM sp)
        {
            string query = "update SANPHAM set KHOILUONG = @khoiluong, ANH = @anh, MOTA = @mota, SOLUONG = @soluong, LOAISP_ID = @idloaisp" +
                "where ID = @id";
            string[] para = new string[6] { "@id", "@khoiluong", "@anh", "@mota", "@soluong", "@idloaisp" };
            object[] value = new object[6] { sp.ID, sp.KHOILUONG, sp.ANH, sp.MOTA, sp.SOLUONG, sp.LOAISP_ID };
            cn.Excute_Sql(query, CommandType.Text, para, value);
        }
        public void DeleteSANPHAM(SANPHAM sp)
        {
            string query = "delete SANPHAM where ID=@id";
            string[] para = new string[1] { "@id" };
            object[] value = new object[1] { sp.ID };
            cn.Excute_Sql(query, CommandType.Text, para, value);
        }
        public PRICE GetPriceBySanPham(int id)
        {
            DateTime now = DateTime.Now;
            string query = "select p.ID, p.GIABAN, p.BATDAU, p.KETTHUC, p.SANPHAM_ID from Price p join SANPHAM s on p.SANPHAM_ID = s.ID " +
                "where s.ID = @id and (p.BATDAU <= @now and p.KETTHUC >= @now or p.BATDAU <= @now and p.KETTHUC is null)";
            string[] para = new string[2] { "@id", "@now" };
            object[] value = new object[2] { id, now };
            DataTable tb = cn.FillDataTable(query, CommandType.Text, para, value);
            PRICE pr = cn.ConvertToList<PRICE>(tb)[0];
            return pr;
        }
    }
}