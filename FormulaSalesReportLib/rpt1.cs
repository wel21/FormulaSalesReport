using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt1()
        {
            InitializeComponent();
        }

        public string StoreName { get; set; }
        public List<ParamDate> ParamDate { get; set; } 
        public double TotalQty { get; set; }
        public double TotalAmt { get; set; }
        public double DeliveryCharge { get; set; }
        public double Tax { get; set; }
        public double DiscountQty { get; set; }
        public double DiscountAmt { get; set; }

        private void rpt1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

            xrLabel21.DataBindings.Add("Text", this.DataSource, "Name");
            xrLabel22.DataBindings.Add("Text", this.DataSource, "Quantity", "{0:#,##0}");
            xrLabel23.DataBindings.Add("Text", this.DataSource, "Amount", "{0:#,##0.00}");
            xrLabel24.DataBindings.Add("Text", this.DataSource, "Percent", "{0:p}");
            xrLabel25.DataBindings.Add("Text", this.DataSource, "Average", "{0:#,##0.00}");

            //totals ----------
            XRLabel lblTotalQty = xrLabel10;
            lblTotalQty.Text = String.Format("{0:#,##0.00}", TotalQty);// TotalQty.ToString("#,##0.00"); //

            XRLabel lblTotalAmt = xrLabel11;
            lblTotalAmt.Text = String.Format("{0:#,##0.00}", TotalAmt);// TotalAmt.ToString("#,##0.00");

            //other calculations ----------
            //Net Sales
            XRLabel lblNetSales = xrLabel13;
            lblNetSales.Text = String.Format("{0:#,##0.00}", TotalAmt);// TotalAmt.ToString("#,##0.00");
            //Sales Tax
            XRLabel lblSalesTax = xrLabel8;
            lblSalesTax.Text = String.Format("{0:#,##0.00}", Tax);//  Tax.ToString("#,##0.00");
            //Delivery
            XRLabel lblDelivery = xrLabel14;
            lblDelivery.Text = String.Format("{0:#,##0.00}", DeliveryCharge);// DeliveryCharge.ToString("#,##0.00");

            //Gross Sales
            XRLabel lblGrossSalesQty = xrLabel16;
            lblGrossSalesQty.Text = String.Format("{0:#,##0.00}", TotalQty);// TotalQty.ToString("#,##0.00");
            XRLabel lblGrossSalesAmt = xrLabel17;
            double dgrsslsamt = Convert.ToDouble(TotalAmt) + Convert.ToDouble(Tax) + Convert.ToDouble(DeliveryCharge);
            lblGrossSalesAmt.Text = String.Format("{0:#,##0.00}", dgrsslsamt);// (Convert.ToDouble(TotalAmt) + Convert.ToDouble(Tax) + Convert.ToDouble(DeliveryCharge)).ToString("#,##0.00");

            //Discount Total
            XRLabel lblDiscntTotalQty = xrLabel18;
            lblDiscntTotalQty.Text = String.Format("{0:#,##0.00}", DiscountQty);// DiscountQty.ToString("#,##0.00");
            XRLabel lblDiscntTotalAmt = xrLabel20;
            lblDiscntTotalAmt.Text = String.Format("{0:#,##0.00}", DiscountAmt);// DiscountAmt.ToString("#,##0.00");

        }
    }
}
