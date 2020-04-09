using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class XuatHangDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public string GetTenLSP(CHITIETDONHANG CTDH)
        {
            SANPHAM sp = cn.GetObject<SANPHAM>("api/SANPHAM/" + CTDH.SANPHAM_ID);
            LOAISP lsp = cn.GetObject<LOAISP>("api/LOAISP/" + sp.LOAISP_ID);
            return lsp.TEN;
        }

        public int GetKLSP(CHITIETDONHANG CTDH)
        {
            SANPHAM sp = cn.GetObject<SANPHAM>("api/SANPHAM/" + CTDH.SANPHAM_ID);
            return sp.KHOILUONG;
        }

        public List<CHITIETNHAPHANG> GetListCTNhapHang(CHITIETDONHANG CTDH)
        {
            SANPHAM sp = cn.GetObject<SANPHAM>("api/SANPHAM/" + CTDH.SANPHAM_ID);
            List<CHITIETNHAPHANG> lstNH = cn.GetObject<List<CHITIETNHAPHANG>>("api/CHITIETNHAPHANG")
                                          .Where(x => x.SANPHAM_ID == sp.ID && x.SOLUONGCONLAI > 0).ToList();
            return lstNH;
        }

        public CHITIETNHAPHANG GetCTNHbyID(int id)
        {
            return cn.GetObject<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/" + id);
        }
        public DateTime GetNgayNhap(int idCTNH)
        {
            CHITIETNHAPHANG CTNH = cn.GetObject<CHITIETNHAPHANG>("api/CHITIETNHAPHANG/" + idCTNH);
            DateTime ngaynhap = cn.GetObject<NHAPHANG>("api/NHAPHANG/" + CTNH.NHAPHANG_ID).NGAYNHAP;
            return ngaynhap;
        }

        public void insertXUATHANG(XUATHANG xh)
        {
            cn.Post<XUATHANG>("api/XUATHANG/", xh);
        }

        public List<XUATHANG> GetXuatHang(int idCTDH)
        {
            List<XUATHANG> lst = cn.GetObject<List<XUATHANG>>("api/XUATHANG");
            return lst.Where(x => x.CHITIETDH_ID == idCTDH).ToList();
        }
    }
}
