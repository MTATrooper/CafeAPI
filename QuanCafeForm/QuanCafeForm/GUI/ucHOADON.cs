using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanCafeForm.DAO;
using QuanCafeForm.Models;
using DevExpress.XtraEditors.Repository;

namespace QuanCafeForm.GUI
{
    public partial class ucHOADON : UserControl
    {
        private DonHangDAO dhDAO = new DonHangDAO();
        private DONHANG dh = new DONHANG();
        public ucHOADON()
        {
            InitializeComponent();
        }

        private void ucHOADON_Load(object sender, EventArgs e)
        {
            List<TRANGTHAIDH> lstTT = dhDAO.GetListTTDH();
            List<string> lstTTStr = (from s in lstTT select s.TEN).ToList();
            cbBTrangthai.DataSource = lstTTStr;
            //cbBTrangthai.ValueMember = "TEN";
            //cbBTrangthai.DisplayMember = "TEN";
            cbBUpdate.DataSource = lstTT;
            cbBUpdate.ValueMember = "ID";
            cbBUpdate.DisplayMember = "TEN";
            RepositoryItemLookUpEdit ribm = new RepositoryItemLookUpEdit();
            ribm.DataSource = lstTT;
            ribm.ValueMember = "ID";
            ribm.DisplayMember = "TEN";
            grViewDH.Columns["TRANGTHAI_ID"].ColumnEdit = ribm;
            List<LOAISP> lstLSP = new LoaiSpDAO().getListLOAISP();
            RepositoryItemLookUpEdit risp = new RepositoryItemLookUpEdit();
            risp.DataSource = lstLSP;
            risp.ValueMember = "ID";
            risp.DisplayMember = "TEN";
            grViewCTDH.Columns["LOAISP"].ColumnEdit = risp;
        }

        private void cbBTrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tt = cbBTrangthai.SelectedIndex + 1;
            grCtrlDH.DataSource = dhDAO.GetListDhByTT(tt);
            if(grViewDH.RowCount > 1)
            {
                grViewDH.FocusedRowHandle = 1;
                grViewDH.FocusedRowHandle = 0;
            }
            else if(grViewDH.RowCount == 1)
            {
                //int idDh = (int)grViewDH.GetRowCellValue(0, "ID");
                //dh = dhDAO.GetDHById(idDh);
                //List<CHITIETDONHANG> lstCTDH = dhDAO.GetCTDH(dh.ID);
                //grCtrlCTDH.DataSource = lstCTDH;
                Hienthi();
            }
        }
        private void Hienthi()
        {
            int idDh = (int)grViewDH.GetFocusedRowCellValue("ID");
            dh = dhDAO.GetDHById(idDh);
            List<CHITIETDONHANG> lstCTDH = dhDAO.GetCTDH(dh.ID);
            List<THONGTINCHITIETDH> lstTTCT = new List<THONGTINCHITIETDH>();
            foreach (CHITIETDONHANG c in lstCTDH)
            {
                SANPHAM s = new SanphamDAO().getSanphamById(c.SANPHAM_ID);
                THONGTINCHITIETDH t = new THONGTINCHITIETDH(c, s);
                lstTTCT.Add(t);
            }
            grCtrlCTDH.DataSource = lstTTCT;
            txtNgaydat.Text = ((DateTime)dh.NGAYDAT).ToShortDateString();
            txtKH.Text = dhDAO.GetTenKH(dh.ID);
            txtDC.Text = dh.DIACHI;
            txtNguoinhan.Text = dh.TENNGUOINHAN;
            txtSDT.Text = dh.SDT;
            txtTong.Text = dh.TONGTIEN.ToString();
            txtTrangthai.Text = dhDAO.GetTTbyId(dh.TRANGTHAI_ID).TEN;
            
        }
        private void grViewDH_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grViewDH.GetFocusedRowCellValue("ID") != null)
            {
                
                Hienthi();
            }
            else
            {
                grCtrlCTDH.DataSource = null;
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dh.TRANGTHAI_ID == 1 && !dhDAO.CheckXuatHang(dh.ID))
            {
                MessageBox.Show("Bạn chưa lấy hàng!");
                return;
            }
            if(dh.TRANGTHAI_ID == 3 || dh.TRANGTHAI_ID == 4)
            {
                MessageBox.Show("Bạn không thể đổi trạng thái đơn hàng này!");
                return;
            }
            //Chuyển trạng thái đơn hàng
            dh.TRANGTHAI_ID = (int)cbBUpdate.SelectedValue;
            dhDAO.UpdateDH(dh);
            //Update lại gridView
            int tt = cbBTrangthai.SelectedIndex + 1;
            grCtrlDH.DataSource = dhDAO.GetListDhByTT(tt);
            if(grViewDH.RowCount > 1)
            {
                grViewDH.FocusedRowHandle = 1;
                grViewDH.FocusedRowHandle = 0;
            }
            else if(grViewDH.RowCount == 1)
            {
                //int idDh = (int)grViewDH.GetRowCellValue(0, "ID");
                //dh = dhDAO.GetDHById(idDh);
                //List<CHITIETDONHANG> lstCTDH = dhDAO.GetCTDH(dh.ID);
                //grCtrlCTDH.DataSource = lstCTDH;
                Hienthi();
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            CHITIETDONHANG CTDH = dhDAO.GetCTDHbyId((int)grViewCTDH.GetFocusedRowCellValue("ID"));
            FormLayHang frmLayHang = new FormLayHang(CTDH);
            frmLayHang.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<CHITIETDONHANG> lstCTDH = dhDAO.GetCTDH(dh.ID);
            List<THONGTINCHITIETDH2> lstTTCT = new List<THONGTINCHITIETDH2>();
            foreach(CHITIETDONHANG c in lstCTDH)
            {
                THONGTINCHITIETDH2 t2 = new THONGTINCHITIETDH2();
                SANPHAM s = new SanphamDAO().getSanphamById(c.SANPHAM_ID);
                THONGTINCHITIETDH t = new THONGTINCHITIETDH(c, s);
                t2.ID = t.ID; t2.KHOILUONG = t.KHOILUONG;
                t2.SOLUONG = t.SOLUONG; t2.DONGIA = t.DONGIA;
                t2.THANHTIEN = t.THANHTIEN;
                t2.TEN = new LoaiSpDAO().getLOAISPById(t.LOAISP).TEN;
                lstTTCT.Add(t2);
            }
            FormPrint formPrint = new FormPrint(lstTTCT, dh);
            formPrint.ShowDialog();
        }
    }
}
