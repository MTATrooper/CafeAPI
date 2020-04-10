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
using System.IO;
using System.Threading;

namespace QuanCafeForm.GUI
{
    public partial class ucSANPHAM : UserControl
    {
        private SanphamDAO spDAO = new SanphamDAO();
        private HangSxDAO hangDAO = new HangSxDAO();
        private LoaiSpDAO lspDAo = new LoaiSpDAO();
        private SANPHAM sp = new SANPHAM();
        private int flag = 0;
        public ucSANPHAM()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            List<SANPHAM> lst = spDAO.getListSANPHAM();
            foreach (SANPHAM s in lst)
            {
                //string imagepath = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + s.ANH;
                //s.image = Image.FromFile(imagepath);
                PictureBox p = new PictureBox();
                p.Load(spDAO.getUrlImage(s.ANH));
                s.image = p.Image;
                s.price = spDAO.getPrice(s.ID);
            }
            grCtrlSP.DataSource = lst;
        }
        private void ucSANPHAM_Load(object sender, EventArgs e)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            webMota.Url = new Uri(dir + @"\Scripts\ckeditor\ckeditor.html");
            List<LOAISP> lstLSP = lspDAo.getListLOAISP(); ;
            cbBLSP.DataSource = lstLSP;
            cbBLSP.DisplayMember = "TEN";
            cbBLSP.ValueMember = "ID";
            LoadData();
            RepositoryItemLookUpEdit ribm = new RepositoryItemLookUpEdit();
            ribm.DataSource = lstLSP;
            ribm.ValueMember = "ID";
            ribm.DisplayMember = "TEN";
            grViewSP.Columns["LOAISP_ID"].ColumnEdit = ribm;
            
        }

        private void Hienthi()
        {
            cbBLSP.SelectedValue = sp.LOAISP_ID;
            spKL.Value = sp.KHOILUONG;
            txtGia.Text = spDAO.getPrice(sp.ID).ToString();
            //string imagepath = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + sp.ANH;
            PictureBox p = new PictureBox();
            p.Load(spDAO.getUrlImage(sp.ANH));
            picAnh.Image = p.Image;
            //picAnh.Image = Image.FromFile(imagepath);
            //picAnh.ImageLocation = imagepath;
            webMota.Document.InvokeScript("setValue", new[] { sp.MOTA });
        }

        private void grViewSP_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            sp = spDAO.getSanphamById((int)grViewSP.GetFocusedRowCellValue("ID"));
            Hienthi();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (oFile.ShowDialog() == DialogResult.OK)
            {
                string filepath = oFile.FileName;
                picAnh.Image = Image.FromFile(filepath);
                picAnh.ImageLocation = filepath;

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            spKL.Value = 0;txtGia.Text = "";
            cbBLSP.Focus();
            picAnh.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;cbBLSP.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Hienthi();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SANPHAM1 sp1 = new SANPHAM1();
            sp1.KHOILUONG = (int)spKL.Value;
            sp1.ANH = "/Resources/Image/" + System.IO.Path.GetFileName(picAnh.ImageLocation);
            sp1.MOTA = webMota.Document.InvokeScript("getValue").ToString();
            sp1.LOAISP_ID = (int)cbBLSP.SelectedValue;
            
            int row = flag == 1 ? grViewSP.RowCount : grViewSP.FocusedRowHandle;
            
            if (flag == 1)
            {
                sp1.SOLUONG = 0;
                spDAO.insertSANPHAM(sp1);
                SANPHAM sp2 = spDAO.GetLastSanpham();
                PRICE pr = new PRICE();
                pr.GIABAN = Convert.ToInt32(txtGia.Text);
                pr.BATDAU = DateTime.Now;
                pr.KETTHUC = (DateTime?)null;
                pr.SANPHAM_ID = sp2.ID;
                new PriceDAO().insertPrice(pr);
                MessageBox.Show("Thêm thành công!");
            }
            if(flag == 2)
            {
                sp1.ID = sp.ID;
                sp1.SOLUONG = sp.SOLUONG;
                spDAO.updateSANPHAM(sp1);
                PRICE pr = new PriceDAO().getPriceBySanphamId(sp.ID);
                pr.GIABAN = Convert.ToInt32(txtGia.Text);
                new PriceDAO().updatePrice(pr);
                MessageBox.Show("Update thành công!");
            }
            string despath = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + sp1.ANH;
            if (!File.Exists(despath))
                File.Copy(picAnh.ImageLocation, despath);
            //grCtrlSP.DataSource = spDAO.getListSANPHAM();
            
            flag = 0;
            LoadData();
            grViewSP.FocusedRowHandle = row;
            Hienthi();
        }
    }
}
