using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class NhapHangDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public void updateNHAPHANG(CHITIETNHAPHANG CTNH)
        {
            cn.Put<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/", CTNH);
        }

        public CHITIETNHAPHANG GetCTNHbyId(int id)
        {
            return cn.GetObject<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/" + id);
        }
        public Object getListNhapHang()
        {
            List<NHAPHANG> lstNH = cn.GetObject<List<NHAPHANG>>("api/NHAPHANG/");
            List<NHACUNGCAP> lstNCC = cn.GetObject<List<NHACUNGCAP>>("api/NHACUNGCAP/");
            var lst = from n in lstNH join ncc in lstNCC on n.NHACC_ID equals ncc.ID
                       select new
                       {
                           ID = n.ID,
                           NGAYNHAP = n.NGAYNHAP,
                           NHACUNGCAP = ncc.TEN,
                           TONGTIEN = n.TONGTIEN,
                           DIACHI = ncc.DIACHI,
                           SDT = ncc.SDT
                       };
            return lst.ToList().OrderByDescending(x => x.ID);
        }
        public Object getListNhapHangByTime(DateTime start, DateTime end)
        {
            List<NHAPHANG> lstNH = cn.GetObject<List<NHAPHANG>>("api/NHAPHANG/")
                .Where(x => x.NGAYNHAP >= start && x.NGAYNHAP <= end).ToList();
            List<NHACUNGCAP> lstNCC = cn.GetObject<List<NHACUNGCAP>>("api/NHACUNGCAP/");
            var lst = lstNH.Join(lstNCC, n => n.NHACC_ID, ncc => ncc.ID, (n, ncc) => new
                {
                    ID = n.ID,
                    NGAYNHAP = n.NGAYNHAP,
                    NHACUNGCAP = ncc.TEN,
                    DIACHI = ncc.DIACHI,
                    SDT = ncc.SDT
                });
            return lst;
        }
        public List<InFoCTNhapHang> getListCTNhapHangByIdNH(int idNH)
        {
            List<CHITIETNHAPHANG> lstCTNH = cn.GetObject<List<CHITIETNHAPHANG>>("api/CHITIETNHAPHANG/")
                .Where(x => x.NHAPHANG_ID == idNH).ToList();
            List<SANPHAM> lstSP = cn.GetObject<List<SANPHAM>>("api/SANPHAM/");
            List<LOAISP> lstLSP = cn.GetObject<List<LOAISP>>("api/LOAISP/");
            List<HANGSX> lstHSX = cn.GetObject<List<HANGSX>>("api/HANGSX/");
            List<InFoCTNhapHang> result = (from c in lstCTNH
                   join s in lstSP on c.SANPHAM_ID equals s.ID
                   join l in lstLSP on s.LOAISP_ID equals l.ID
                   join h in lstHSX on l.HANGSX_ID equals h.ID
                   select new InFoCTNhapHang
                   {
                       ID = c.ID,
                       SANPHAM = l.TEN,
                       HANGSX = h.TEN,
                       KHOILUONG = s.KHOILUONG,
                       SOLUONGNHAP = c.SOLUONGNHAP,
                       GIANHAP = c.GIANHAP,
                       SOLUONGCONLAI = c.SOLUONGCONLAI
                   }).ToList();
            return result;
        }
        public NHAPHANG InsertNhapHang(NHAPHANG nh)
        {
            return cn.Post<NHAPHANG>("api/NHAPHANG/", nh);
        }
        public void InsertChiTietNhapHang(CHITIETNHAPHANG ct)
        {
            cn.Post<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/", ct);
        }
        public NHAPHANG getNhapHangById(int id)
        {
            NHAPHANG nh = cn.GetObject<NHAPHANG>("api/NHAPHANG/" + id);
            return nh;
        }
    }
}
