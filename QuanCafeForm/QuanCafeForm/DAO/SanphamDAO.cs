using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class SanphamDAO
    {
        private ConnectAPI cnAPI = new ConnectAPI();
        public List<SANPHAM> getListSANPHAM()
        {
            List<SANPHAM> lst = cnAPI.GetObject<List<SANPHAM>>("api/SANPHAM/");
            return lst;
        }
        public List<SANPHAM> getListSPByLSP(int id)
        {
            List<SANPHAM> lst = cnAPI.GetObject<List<SANPHAM>>("api/SANPHAM/LoaiSP?id=" + id);
            return lst;
        }

        public SANPHAM getSanphamById(int id)
        {
            SANPHAM sp = cnAPI.GetObject<SANPHAM>("api/SANPHAM/" + id);
            return sp;
        }
        public int getPrice(int id)
        {
            return cnAPI.GetObject<int>("api/SANPHAM/Price?id=" + id);
        }
        public void insertSANPHAM(SANPHAM1 sp)
        {
            cnAPI.Post<SANPHAM1>("api/SANPHAM/", sp);
        }
        public void updateSANPHAM(SANPHAM1 sp)
        {
            cnAPI.Put<SANPHAM1>("api/SANPHAM/", sp);
        }
        public SANPHAM GetLastSanpham()
        {
            return getListSANPHAM().Last();
        }
    }
}
