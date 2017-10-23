using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt2()
        {
            InitializeComponent();
        }

        public string StoreName { get; set; }
        public List<ParamDate> ParamDate { get; set; }
        public double TotalAmt { get; set; }
        public string BestHour { get; set; }

        private void rpt2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            xrLabel5.DataBindings.Add("Text", this.DataSource, "Orders", "{0:#,##0}");
            xrLabel6.DataBindings.Add("Text", this.DataSource, "Amount", "{0:#,##0.00}");
            xrLabel7.DataBindings.Add("Text", this.DataSource, "AverageSalesRepresentation");

            xrLabel11.Text = TotalAmt.ToString("#,##0.00");
            xrLabel13.Text = BestHour;
        }
    }
}
