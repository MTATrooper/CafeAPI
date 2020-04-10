using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class NhaCungCapDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public List<NHACUNGCAP> getListNCC()
        {
            List<NHACUNGCAP> lst = cn.GetObject<List<NHACUNGCAP>>("api/NHACUNGCAP/");
            return lst;
        }
        public void InsertNCC(NHACUNGCAP ncc)
        {
            cn.Post<NHACUNGCAP>("api/NHACUNGCAP", ncc);
        }
    }
}
