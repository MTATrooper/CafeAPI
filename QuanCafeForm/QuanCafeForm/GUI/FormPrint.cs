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

namespace QuanCafeForm.GUI
{
    public partial class FormPrint : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private List<THONGTINCHITIETDH2> lstTT = new List<THONGTINCHITIETDH2>();
        private DONHANG dh = new DONHANG();
        public FormPrint(List<THONGTINCHITIETDH2> lstTT, DONHANG dh)
        {
            InitializeComponent();
            this.lstTT = lstTT;
            this.dh = dh;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            donhang report = new donhang(lstTT, dh);
            documentViewer1.DocumentSource = report;
            report.CreateDocument();
        }
    }
}