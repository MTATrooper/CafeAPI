using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeClientWeb.Models;

namespace CafeClientWeb.DAO
{
    public class OrderDAO
    {
        ConnectApi connect = new ConnectApi();
        public int Create(DONHANG order)
        {           
            connect.SendPostRequest<DONHANG>(order, "api/DONHANG/");
            db.DONHANG = connect.DeserializeObject<List<DONHANG>>("api/DONHANG/");
            int id = db.DONHANG.Max(x => x.ID);
            return id;
        }
        public void EditTotal(int total, int id)
        {
            var x = Detail(id);
            x.TONGTIEN = total;
            connect.SendPutRequest(x, "api/DONHANG/");
        }
        //detail order
        public DONHANG Detail(int id)
        {
            var model = db.DONHANG.SingleOrDefault(x => x.ID == id);
            return model;
        }

        public List<DONHANG> GetDH_User(int ID_User)
        {
            List<DONHANG> model = db.DONHANG.Where(x => x.KHACHHANG_ID == ID_User).ToList();
            return model;
        }
    }
}