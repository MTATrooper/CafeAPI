using QuanCafeForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class ThongKeDAO
    {
        private ConnectAPI cn = new ConnectAPI();
        public void insertTHONGKE(DONHANG dh)
        {
            List<CHITIETDONHANG> lstCTDH = new DonHangDAO().GetCTDH(dh.ID);
            foreach(CHITIETDONHANG c in lstCTDH)
            {
                THONGKE tk = new THONGKE();
                tk.NGAY = (DateTime)dh.NGAYDAT;
                tk.SANPHAM_ID = c.SANPHAM_ID;     
                List<XUATHANG> lstXH = new XuatHangDAO().GetXuatHang(c.ID);
                foreach (XUATHANG x in lstXH)
                {
                    if (!CheckExitsThongke(tk.SANPHAM_ID, tk.NGAY))
                    {
                        tk.SOLUONGBAN = c.SOLUONG;
                        tk.DOANHTHU = c.SOLUONG * c.DONGIA;
                        tk.LOINHUAN = (x.GIABAN - x.GIANHAP) * x.SOLUONGXUAT;
                        cn.Post<THONGKE>("api/THONGKE/", tk);
                    }
                    else
                    {
                        tk.SOLUONGBAN += c.SOLUONG;
                        tk.DOANHTHU += c.SOLUONG * c.DONGIA;
                        tk.LOINHUAN += (x.GIABAN - x.GIANHAP) * x.SOLUONGXUAT;
                        cn.Put<THONGKE>("api/THONGKE/", tk);
                    }
                }
                
            }
        }
        public bool CheckExitsThongke(int idSP, DateTime ngay)
        {
            List<THONGKE> lstTK = cn.GetObject<List<THONGKE>>("api/THONGKE");
            if (lstTK.Where(x => x.NGAY == ngay && x.SANPHAM_ID == idSP).ToList().Count > 0)
                return true;
            return false;
        }
        
    }
}
