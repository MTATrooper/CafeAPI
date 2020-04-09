using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanCafeForm.Models;
using QuanCafeForm.DAO;

namespace QuanCafeForm.GUI
{
    public partial class FormLayHang : DevExpress.XtraEditors.XtraForm
    {
        private CHITIETDONHANG CTDH = new CHITIETDONHANG();
        private XuatHangDAO xhDAO = new XuatHangDAO();
        private List<LAYHANG> lstLAYHANG = new List<LAYHANG>();
        public FormLayHang(CHITIETDONHANG CTDH)
        {
            InitializeComponent();
            this.CTDH = CTDH;
        }

        private void FormLayHang_Load(object sender, EventArgs e)
        {
            txtTen.Text = xhDAO.GetTenLSP(CTDH);
            txtKL.Text = xhDAO.GetKLSP(CTDH).ToString();
            txtSL.Text = CTDH.SOLUONG.ToString();
            txtGia.Text = CTDH.DONGIA.ToString();
            txtTT.Text = (CTDH.SOLUONG * CTDH.DONGIA).ToString();
            List<CHITIETNHAPHANG> lstCTNH = xhDAO.GetListCTNhapHang(CTDH);
            List<int> lstIdCTNH = (from s in lstCTNH select s.ID).ToList();
            cbBID.DataSource = lstIdCTNH;
        }

        private void cbBID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCTNH = Convert.ToInt32(cbBID.Text.Trim());
            txtNgaynhap.Text = xhDAO.GetNgayNhap(idCTNH).ToShortDateString();
            CHITIETNHAPHANG CTNH = xhDAO.GetCTNHbyID(idCTNH);
            txtSLNhap.Text = CTNH.SOLUONGNHAP.ToString();
            txtSLCon.Text = CTNH.SOLUONGCONLAI.ToString();
            txtGianhap.Text = CTNH.GIANHAP.ToString();
        }

        private void SLLay_TextChanged(object sender, EventArgs e)
        {
            int idCTNH = Convert.ToInt32(cbBID.Text.Trim());
            CHITIETNHAPHANG CTNH = xhDAO.GetCTNHbyID(idCTNH);
            int soluonglay;
            if(int.TryParse(SLLay.Text,out soluonglay))
            {
                soluonglay = Convert.ToInt32(SLLay.Text);
                txtSLCon.Text = (CTNH.SOLUONGCONLAI - soluonglay).ToString();
            }
        }

        private void btnLay_Click(object sender, EventArgs e)
        {
            LAYHANG lh = new LAYHANG();
            lh.IDNHAP = Convert.ToInt32(cbBID.Text);
            lh.NGAYNHAP = Convert.ToDateTime(txtNgaynhap.Text);
            lh.GIANHAP = Convert.ToInt32(txtGianhap.Text);
            lh.SOLUONGLAY = Convert.ToInt32(SLLay.Text);
            lstLAYHANG.Add(lh);
            grCtrlXuat.DataSource = null;
            grCtrlXuat.DataSource = lstLAYHANG;
        }

        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            foreach(LAYHANG l in lstLAYHANG)
            {
                XUATHANG xh = new XUATHANG();
                xh.NHAPHANG_ID = l.IDNHAP;
                xh.CHITIETDH_ID = CTDH.ID;
                xh.SOLUONGXUAT = l.SOLUONGLAY;
                xh.GIANHAP = l.GIANHAP;
                xh.GIABAN = CTDH.DONGIA;
                xhDAO.insertXUATHANG(xh);
                CHITIETNHAPHANG ctnh = new NhapHangDAO().GetCTNHbyId(xh.NHAPHANG_ID);
                ctnh.SOLUONGCONLAI = ctnh.SOLUONGNHAP - xh.SOLUONGXUAT;
                new NhapHangDAO().updateNHAPHANG(ctnh);
            }
            MessageBox.Show("Xong!");
        }
    }
    public class LAYHANG
    {
        public int IDNHAP { get; set; }
        public DateTime NGAYNHAP { get; set; }
        public int GIANHAP { get; set; }
        public int SOLUONGLAY { get; set; }
    }
}