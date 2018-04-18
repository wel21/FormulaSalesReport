using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_History_SquareReport : rpt
    {
        public rpt_History_SquareReport()
        {
            InitializeComponent();
        }

        private void rpt_Sales_CreditCardTrans_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // report info --------------------------------------------------------------------
            lblStoreName.Text = StoreInformation.StoreName;
            lblStoreAddress.Text = "Address: " + StoreInformation.StoreAddress;
            lblStorePhone.Text = "Phone Number: " + StoreInformation.StoreNumber;

            string _Date = "";
            if (ParamDate.Count == 2)
            { _Date = ParamDate[0].date.ToShortDateString() + " to " + ParamDate[1].date.ToShortDateString(); }
            else
            {
                for (int i = 0; i < ParamDate.Count; i++)
                { _Date += Helpers.ConvertMyDate(ParamDate[i].date) + (i == ParamDate.Count - 1 ? "" : ", "); }
            }
            xrLabel2.Text = _Date;
            // report info --------------------------------------------------------------------


            try
            {

                lblData.DataBindings.Add("Text", this.DataSource, "Data");
                lblData1.DataBindings.Add("Text", this.DataSource, "Data1");

                //total
                //XRSummary XrSummary = new XRSummary();
                //lblTotalData.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data") });
                //XrSummary.FormatString = "{0}";
                //XrSummary.Func = SummaryFunc.Sum;
                //XrSummary.Running = SummaryRunning.Report;
                //lblTotalData.Summary = XrSummary;

                XRSummary XrSummary1 = new XRSummary();
                lblTotalData1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data1") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Report;
                lblTotalData1.Summary = XrSummary1;
                
            }
            catch { }
        }
    }
}
