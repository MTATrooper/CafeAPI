using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanCafeForm.GUI;

namespace QuanCafeForm
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        ucLoaiSP ucLSP;
        ucSANPHAM ucSP;
        ucHOADON ucHD;
        ucNHAPHANG ucNH;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ucHD = new ucHOADON();
            pnmain.Controls.Add(ucHD);
            ucHD.Dock = DockStyle.Fill;
            ucHD.BringToFront();
        }

        private void barLSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucLSP = new ucLoaiSP();
            if(!pnmain.Controls.Contains(ucLSP))
            {
                pnmain.Controls.Add(ucLSP);
            }
            ucLSP.Dock = DockStyle.Fill;
            ucLSP.BringToFront();
        }

        private void barSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucSP = new ucSANPHAM();
            if (!pnmain.Controls.Contains(ucSP))
            {
                pnmain.Controls.Add(ucSP);
            }
            ucSP.Dock = DockStyle.Fill;
            ucSP.BringToFront();
        }

        private void barHoadon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucHD = new ucHOADON();
            if (!pnmain.Controls.Contains(ucHD))
            {
                pnmain.Controls.Add(ucHD);
            }
            ucHD.Dock = DockStyle.Fill;
            ucHD.BringToFront();
        }

        private void barNhapHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucNH = new ucNHAPHANG();
            if (!pnmain.Controls.Contains(ucNH))
            {
                pnmain.Controls.Add(ucNH);
            }
            ucNH.Dock = DockStyle.Fill;
            ucNH.BringToFront();
        }
    }
}
