using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QuanCafeForm.Models;
using System.Collections.Generic;

namespace QuanCafeForm.GUI
{
    public partial class donhang : DevExpress.XtraReports.UI.XtraReport
    {
        //private THONGTINCHITIETDH2 CTDH = new THONGTINCHITIETDH2();
        public donhang(List<THONGTINCHITIETDH2> lstCTDH, DONHANG dh)
        {
            InitializeComponent();
            objectDataSource1.DataSource = lstCTDH;
            pNgay.Value = DateTime.Now.Day.ToString();
            pThang.Value = DateTime.Now.Month.ToString();
            pNam.Value = DateTime.Now.Year.ToString();
            pNgayDat.Value = dh.NGAYDAT;
            pTen.Value = dh.TENNGUOINHAN;
            pSDT.Value = dh.SDT;
            pDiachi.Value = dh.DIACHI;
            pTongtien.Value = dh.TONGTIEN;
        }

        
    }
}
