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
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace QuanCafeForm.GUI
{
    public partial class ThongKe : UserControl
    {
        public ThongKe()
        {
            InitializeComponent();
        }
        public void ThongKe_TheoNam(ThongKeDate tkDate)
        {
            List<THONGKE> tkList = new ThongKeDAO().getThongKe(tkDate);
            for (int i = 1; i <= 12; i++)
            {
                DateTime from = new DateTime(tkDate.toYear, i, 1);
                DateTime to;
                if (i < 12)
                    to = new DateTime(tkDate.toYear, i + 1, 1);
                else
                    to = new DateTime(tkDate.toYear + 1, 1, 1);
                int doanhthu = tkList.Where(p => p.NGAY >= from && p.NGAY < to).Select(p => p.DOANHTHU).ToList().Sum();
                int loinhuan = tkList.Where(p => p.NGAY >= from && p.NGAY < to).Select(p => p.LOINHUAN).ToList().Sum();
                //this.Controls["txt_dt" + i].Text = doanhthu.ToString();
                //this.Controls["txt_ln" + i].Text = loinhuan.ToString();
                this.Controls.Find("txt_dt" + i, true).FirstOrDefault().Text = string.Format("{0:0,0} VNĐ", doanhthu);
                this.Controls.Find("txt_ln" + i, true).FirstOrDefault().Text = string.Format("{0:0,0} VNĐ", loinhuan);
            }
        }
        public void BieuDo(ThongKeDate tkDate)
        {
            cbx_chart_fromMonth.Text = tkDate.fromMonth.ToString();
            cbx_chart_fromYear.Text = tkDate.fromYear.ToString();
            cbx_chart_toMonth.Text = tkDate.toMonth.ToString();
            cbx_chart_toYear.Text = tkDate.toYear.ToString();

            List<THONGKE> tkList = new ThongKeDAO().getThongKe(tkDate);

            splitContainerControl2.Panel2.Controls.Clear();
            // Create a new chart. 
            ChartControl lineChart = new ChartControl();
            // Create a line series. 
            Series doanhthu_series = new Series("Doanh Thu", ViewType.Line);
            Series loinhuan_series = new Series("Lợi Nhuận", ViewType.Line);

            // Add points to it. 
            for (int year = tkDate.fromYear; year <= tkDate.toYear; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    DateTime from = new DateTime(tkDate.toYear, month, 1);
                    DateTime to;
                    if (month < 12)
                        to = new DateTime(tkDate.toYear, month + 1, 1);
                    else
                        to = new DateTime(tkDate.toYear + 1, 1, 1);
                    int doanhthu = tkList.Where(p => p.NGAY >= from && p.NGAY < to).Select(p => p.DOANHTHU).ToList().Sum();
                    int loinhuan = tkList.Where(p => p.NGAY >= from && p.NGAY < to).Select(p => p.LOINHUAN).ToList().Sum();
                    //this.Controls["txt_dt" + month].Text = doanhthu.ToString();
                    //this.Controls["txt_ln" + month].Text = loinhuan.ToString();
                    doanhthu_series.Points.Add(new SeriesPoint(new DateTime(year, month, 1), doanhthu));
                    loinhuan_series.Points.Add(new SeriesPoint(new DateTime(year, month, 1), loinhuan));
                }
            }

            // Add the series to the chart. 
            lineChart.Series.Add(doanhthu_series);
            lineChart.Series.Add(loinhuan_series);

            // Set the numerical argument scale types for the series, 
            // as it is qualitative, by default. 
            doanhthu_series.ArgumentScaleType = ScaleType.DateTime;
            loinhuan_series.ArgumentScaleType = ScaleType.DateTime;

            // Access the view-type-specific options of the series. 
            ((LineSeriesView)doanhthu_series.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((LineSeriesView)doanhthu_series.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)doanhthu_series.View).LineStyle.DashStyle = DashStyle.Solid;
            ((LineSeriesView)loinhuan_series.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((LineSeriesView)loinhuan_series.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)loinhuan_series.View).LineStyle.DashStyle = DashStyle.Solid;

            // Access the type-specific options of the diagram. 
            ((XYDiagram)lineChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)lineChart.Diagram).EnableAxisXScrolling = true;

            // Set maximum and minimum range of AxisX
            ((XYDiagram)lineChart.Diagram).AxisX.WholeRange.MinValue = new DateTime(tkDate.fromYear, tkDate.fromMonth, 1);
            if (tkDate.toMonth < 12)
                ((XYDiagram)lineChart.Diagram).AxisX.WholeRange.MaxValue = new DateTime(tkDate.toYear, tkDate.toMonth + 1 , 1);
            else
                ((XYDiagram)lineChart.Diagram).AxisX.WholeRange.MaxValue = new DateTime(tkDate.toYear + 1, 1, 1);

            // Set format of show value
            lineChart.SeriesTemplate.CrosshairLabelPattern = "{S}: {V:F2}";
            ((XYDiagram)lineChart.Diagram).AxisY.Label.TextPattern = "{0:0,0} VNĐ";

            // Show the legend (if necessary). 
            lineChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Add a title to the chart (if necessary). 
            lineChart.Titles.Add(new ChartTitle());
            lineChart.Titles[0].Text = "Biểu đồ thống kê";

            // Add the chart to the form. 
            lineChart.Dock = DockStyle.Fill;
            splitContainerControl2.Panel2.Controls.Add(lineChart);
        }
        public void ThongKe_SanPhamBanChay(ThongKeDate tkDate)
        {
            List<SanPhamBanChay> results = new ThongKeDAO().getSanPhamBanChay(tkDate);
            cbx_spbc_fromYear.Text = tkDate.fromYear.ToString();
            cbx_spbc_fromMonth.Text = tkDate.fromMonth.ToString();
            cbx_spbc_toYear.Text = tkDate.toYear.ToString();
            cbx_spbc_toMonth.Text = tkDate.toMonth.ToString();
            dgv_spbc.DataSource = results;

            //GridView gv_spbc = dgv_spbc.MainView as GridView;
            //for (int i = 0; i < results.Count(); i++)
            //{
            //    gv_spbc.SetRowCellValue(i, "STT", (i+1).ToString());
            //}
        }
        private void ThongKe_Load(object sender, EventArgs e)
        {
            ThongKeDate tkDate = new ThongKeDate();
            tkDate.fromYear = 2019; tkDate.fromMonth = 1;
            tkDate.toYear = DateTime.Now.Year; tkDate.toMonth = 12;

            ThongKe_TheoNam(tkDate);
            BieuDo(tkDate);
            ThongKe_SanPhamBanChay(tkDate);
        }

        private void btn_chart_filter_Click(object sender, EventArgs e)
        {
            ThongKeDate tkDate = new ThongKeDate();
            tkDate.fromYear = Convert.ToInt32(cbx_chart_fromYear.Text);
            tkDate.fromMonth = Convert.ToInt32(cbx_chart_fromMonth.Text);
            tkDate.toYear = Convert.ToInt32(cbx_chart_toYear.Text);
            tkDate.toMonth = Convert.ToInt32(cbx_chart_toMonth.Text);
            BieuDo(tkDate);
        }

        private void btn_spbc_filter_Click(object sender, EventArgs e)
        {
            ThongKeDate tkDate = new ThongKeDate();
            tkDate.fromYear = Convert.ToInt32(cbx_spbc_fromYear.Text);
            tkDate.fromMonth = Convert.ToInt32(cbx_spbc_fromMonth.Text);
            tkDate.toYear = Convert.ToInt32(cbx_spbc_toYear.Text);
            tkDate.toMonth = Convert.ToInt32(cbx_spbc_toMonth.Text);
            ThongKe_SanPhamBanChay(tkDate);
        }
    }
}
