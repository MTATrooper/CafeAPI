using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeClientWeb.Models;

namespace CafeClientWeb.DAO
{
    public class OrderDetailDAO
    {
        ConnectApi connect = new ConnectApi();
        public bool Create(CHITIETDONHANG orderDetail)
        {
            try
            {
                //giảm số lượng khi đặt sản phẩm
                var product = db.SANPHAM.Find(x => x.ID == orderDetail.SANPHAM_ID);
                product.SOLUONG = product.SOLUONG - orderDetail.SOLUONG;
                connect.SendPostRequest(orderDetail, "api/CHITIETDONHANG/");
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public List<CHITIETDONHANG> GetCTDH(int ID_DH)
        {
            var orderDetail = db.CHITIETDONHANG.Where(x => x.DONHANG_ID == ID_DH);
            return orderDetail.ToList();
        }
    }
}