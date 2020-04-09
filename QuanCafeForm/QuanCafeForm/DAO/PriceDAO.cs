using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class PriceDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public void insertPrice(PRICE pr)
        {
            cn.Post<PRICE>("api/PRICE/", pr);
        }
        public void updatePrice(PRICE pr)
        {
            cn.Put<PRICE>("api/PRICE/", pr);
        }
        public PRICE getPriceBySanphamId(int id)
        {
            List<PRICE> lst = cn.GetObject<List<PRICE>>("api/PRICE");
            foreach(PRICE p in lst)
            {
                if (p.SANPHAM_ID == id) return p;
            }
            return null;
        }
    }
}
