using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using QuanCafeForm.DAO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using QuanCafeForm.Models;

namespace QuanCafeForm.GUI
{
    public partial class ucLoaiSP : UserControl
    {
        private LoaiSpDAO lspSAO = new LoaiSpDAO();
        private SanphamDAO spDAO = new SanphamDAO();
        private int flag = 0;
        private LOAISP lsp = new LOAISP();
        public ucLoaiSP()
        {
            InitializeComponent();
        }

        private Image ConvertImage(string path)
        {
            Image img = Image.FromFile(path);
            return img;
        }

        private void ucLoaiSP_Load(object sender, EventArgs e)
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            webMota.Url = new Uri(dir + @"\Scripts\ckeditor\ckeditor.html");
            grCtrlLSP.DataSource = lspSAO.getListLOAISP();
            RepositoryItemLookUpEdit ribm = new RepositoryItemLookUpEdit();
            ribm.DataSource = new HangSxDAO().getListHANGSX();
            ribm.ValueMember = "ID";
            ribm.DisplayMember = "TEN";
            grViewLSP.Columns["HANGSX_ID"].ColumnEdit = ribm;
            cbHangSX.DataSource = new HangSxDAO().getListHANGSX();
            cbHangSX.DisplayMember = "TEN";
            cbHangSX.ValueMember = "ID";
        }
        private void Hienthi()
        {
            lsp = lspSAO.getLOAISPById((int)grViewLSP.GetFocusedRowCellValue("ID"));
            cbHangSX.SelectedValue = lsp.HANGSX_ID;
            txtTen.Text = lsp.TEN;
            dateNgayTao.Value = lsp.NGAYTAO;
            string imagepath = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + lsp.ANH;
            picAnh.Image = Image.FromFile(imagepath);
            picAnh.ImageLocation = imagepath;
            webMota.Document.InvokeScript("setValue", new[] { lsp.MOTA });
        }
        private void grViewLSP_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int id = Convert.ToInt32(grViewLSP.GetRowCellValue(grViewLSP.FocusedRowHandle, "ID"));
            List<SANPHAM> lst = spDAO.getListSPByLSP(id);
            foreach(SANPHAM s in lst)
            {
                s.ANH = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + s.ANH;
                s.image = ConvertImage(s.ANH);
                s.price = spDAO.getPrice(s.ID);
            }
            grCtrlSP.DataSource = lst;
            Hienthi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTen.Text = ""; txtTen.ReadOnly = false;txtTen.Focus();
            dateNgayTao.Enabled = false; dateNgayTao.Value = DateTime.Now;
            picAnh.Image = null;
            btnSua.Enabled = btnXoa.Enabled = false;
            webMota.Document.InvokeScript("setValue", new[] { "" });
            flag = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTen.ReadOnly = false; txtTen.Focus();
            dateNgayTao.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = false;
            flag = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn muốn ngừng bán sản phẩm này?", "Verify!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lsp.TRANGTHAI = "Ngừng bán";
                lspSAO.updateLOAISP(lsp);
                grCtrlLSP.DataSource = lspSAO.getListLOAISP();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtTen.ReadOnly = true;
            dateNgayTao.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = true;
            flag = 0;
            Hienthi();
        }

        private void btnMoban_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn tiếp tục bán sản phẩm này?", "Verify!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lsp.TRANGTHAI = "Đang bán";
                lspSAO.updateLOAISP(lsp);
                grCtrlLSP.DataSource = lspSAO.getListLOAISP();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LOAISP lsp1 = new LOAISP();
            lsp1.HANGSX_ID = (int)cbHangSX.SelectedValue;
            lsp1.TEN = txtTen.Text;
            //lsp1.TRANGTHAI = "Đang bán";
            lsp1.ANH = "/Resources/Image/" + System.IO.Path.GetFileName(picAnh.ImageLocation);
            lsp1.MOTA = webMota.Document.InvokeScript("getValue").ToString();
            int row = flag == 1 ? grViewLSP.RowCount  : grViewLSP.FocusedRowHandle;
            if(flag == 1)
            {
                try
                {
                    lsp1.NGAYTAO = dateNgayTao.Value;
                    lsp1.TRANGTHAI = "Đang bán";
                    lspSAO.insertLOAISP(lsp1);
                    MessageBox.Show("Thêm thành công!");
                }
                catch
                {
                    MessageBox.Show("Không thêm được!");
                }
            }
            if(flag == 2)
            {
                try
                {
                    lsp1.ID = lsp.ID;
                    lsp1.NGAYTAO = lsp.NGAYTAO;
                    lsp1.TRANGTHAI = lsp.TRANGTHAI;
                    lspSAO.updateLOAISP(lsp1);
                    MessageBox.Show("Update thành công!");
                }
                catch
                {
                    MessageBox.Show("Không update được!");
                }
            }
            string despath = @"E:\Projects\DEPLOY IIS\QuanCafeAPI IIS" + lsp1.ANH;
            if (!File.Exists(despath))
                File.Copy(picAnh.ImageLocation, despath);
            grCtrlLSP.DataSource = lspSAO.getListLOAISP();
            grViewLSP.FocusedRowHandle = row;
            flag = 0;
            txtTen.ReadOnly = false;
            dateNgayTao.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            Hienthi();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (oFile.ShowDialog() == DialogResult.OK)
            {
                //txtDuongDan.Text = Environment.CurrentDirectory();
                string filepath = oFile.FileName;
                picAnh.Image = Image.FromFile(filepath);
                picAnh.ImageLocation = filepath;
                
            }
        }
    }
}
