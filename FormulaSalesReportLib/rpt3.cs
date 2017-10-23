using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt3 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt3()
        {
            InitializeComponent();
        }

        public string StoreName { get; set; }
        public List<ParamDate> ParamDate { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmt { get; set; }
        public double TotalPercent { get; set; }
        public double DiscountAmt { get; set; }

        private void rpt3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (StoreName != null)
                xrLabel3.Text = StoreName;

            string _Date = "";
            if (ParamDate.Count == 2)
            { _Date = ParamDate[0].date.ToShortDateString() + " to " + ParamDate[1].date.ToShortDateString(); }
            else
            {
                for (int i = 0; i < ParamDate.Count; i++)
                { _Date += Helpers.ConvertMyDate(ParamDate[i].date) + (i == ParamDate.Count - 1 ? "" : ", "); }
            }
            xrLabel2.Text = "Report generated with date range of " + _Date;

            //FindControl("xrLabel21", true).DataBindings.Add("Text", this.DataSource, "Name");
            //FindControl("xrLabel22", true).DataBindings.Add("Text", this.DataSource, "Quantity");
            //FindControl("xrLabel23", true).DataBindings.Add("Text", this.DataSource, "Amount");
            //FindControl("xrLabel24", true).DataBindings.Add("Text", this.DataSource, "Percent", "{0:p}");
            //FindControl("xrLabel25", true).DataBindings.Add("Text", this.DataSource, "Average", "{0:#,##0.00}");

            xrLabel15.DataBindings.Add("Text", this.DataSource, "Name");
            xrLabel16.DataBindings.Add("Text", this.DataSource, "Quantity", "{0:#,##0}");
            xrLabel17.DataBindings.Add("Text", this.DataSource, "Amount", "{0:#,##0.00}");
            xrLabel18.DataBindings.Add("Text", this.DataSource, "Percent", "{0:p}");

            //totals ----------
            XRLabel lblTotalQty = xrLabel10;
            lblTotalQty.Text = TotalQty.ToString("#,##0.00");

            XRLabel lblTotalAmt = xrLabel11;
            lblTotalAmt.Text = TotalAmt.ToString("#,##0.00");

            XRLabel lblTotalPercent = xrLabel14;
            lblTotalPercent.Text = TotalPercent.ToString("#,##0.00");

            //other calculations ----------
            //Discount Total
            XRLabel lblDiscntTotalAmt = xrLabel13;
            lblDiscntTotalAmt.Text = DiscountAmt.ToString("#,##0.00");

            //Net Sales
            XRLabel lblNetSales = xrLabel7;
            lblNetSales.Text = (Convert.ToDouble(lblTotalAmt.Text) - Convert.ToDouble(lblDiscntTotalAmt.Text)).ToString("#,##0.00");
            
        }
    }
}
