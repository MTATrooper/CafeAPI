using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class DonHangDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public List<DONHANG> GetListDH()
        {
            return cn.GetObject<List<DONHANG>>("api/DONHANG");
        }

        public DONHANG GetDHById(int id)
        {
            return cn.GetObject<DONHANG>("api/DONHANG/" + id);
        }

        public List<DONHANG> GetListDhByTT(int id)
        {
            List<DONHANG> lst = GetListDH();
            return lst.Where(x => x.TRANGTHAI_ID == id).ToList();
        }
        
        public List<TRANGTHAIDH> GetListTTDH()
        {
            return cn.GetObject<List<TRANGTHAIDH>>("api/TRANGTHAIDH");
        }

        public TRANGTHAIDH GetTTbyId(int id)
        {
            return cn.GetObject<TRANGTHAIDH>("api/TRANGTHAIDH/" + id);
        }
        
        public List<CHITIETDONHANG> GetCTDH(int idDH)
        {
            List<CHITIETDONHANG> lstCTDH = cn.GetObject<List<CHITIETDONHANG>>("api/CHITIETDONHANG");
            List<CHITIETDONHANG> lstCTDH1 = lstCTDH.Where(x => x.DONHANG_ID == idDH).ToList();
            foreach(CHITIETDONHANG i in lstCTDH1)
            {
                i.THANHTIEN = i.SOLUONG * i.DONGIA;
            }
            return lstCTDH1;
        }

        public CHITIETDONHANG GetCTDHbyId(int id)
        {
            CHITIETDONHANG CTDH = cn.GetObject<CHITIETDONHANG>("api/CHITIETDONHANG/" + id);
            return CTDH;
        }
        public string GetTenKH(int idDH)
        {
            DONHANG DH = cn.GetObject<DONHANG>("api/DONHANG/" + idDH);
            List<DONHANG> lstDH = new List<DONHANG>();
            lstDH.Add(DH);
            List<NGUOIDUNG> lstKH = cn.GetObject<List<NGUOIDUNG>>("api/NGUOIDUNG").Where(x => x.QUYEN == 0).ToList();
            //string tenKH = lstDH.Join(lstKH, s => s.KHACHHANG_ID, i => i.ID, (s, i) => new { i.TEN }).ToString();
            var lstTen = (from d in lstDH
                          join k in lstKH
                          on d.KHACHHANG_ID equals k.ID
                          select k.TEN).ToList();
            if(lstTen.Count != 0)
            {
                return lstTen.FirstOrDefault();
            }
            return null;
        }

        public bool CheckXuatHang(int idDH)
        {
            List<CHITIETDONHANG> lstCTDH = cn.GetObject<List<CHITIETDONHANG>>("api/CHITIETDONHANG").Where(x => x.DONHANG_ID == idDH).ToList();
            foreach (CHITIETDONHANG ctdh in lstCTDH)
            {
                List<XUATHANG> lstXH = cn.GetObject<List<XUATHANG>>("api/XUATHANG").Where(x => x.CHITIETDH_ID == ctdh.ID).ToList();
                if (lstXH.Count == 0) return false;
            }
            return true;
        }

        public void UpdateDH(DONHANG dh)
        {
            cn.Put<DONHANG>("api/DONHANG/", dh);
        }
    }
}
