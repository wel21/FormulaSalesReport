using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace FormulaReportsLib
{
    public partial class rpt_Sales_Voids : rpt
    {
        public rpt_Sales_Voids()
        {
            InitializeComponent();
        }

        //public StoreInfo StoreInformation { get; set; }

        //public List<ParamDate> ParamDate { get; set; }

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




            lblData.DataBindings.Add("Text", this.DataSource, "Data");
            lblData1.DataBindings.Add("Text", this.DataSource, "Data1");
            lblData2.DataBindings.Add("Text", this.DataSource, "Data2");
            lblData3.DataBindings.Add("Text", this.DataSource, "Data3");
            lblData4.DataBindings.Add("Text", this.DataSource, "Data4");
            lblData5.DataBindings.Add("Text", this.DataSource, "Data5");

            //total quantity
            //XRSummary XrSummary1 = new XRSummary();
            //lblTotalData1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data1") });
            //XrSummary1.FormatString = "{0}";
            //XrSummary1.Func = SummaryFunc.Sum;
            //XrSummary1.Running = SummaryRunning.Report;
            //lblTotalData1.Summary = XrSummary1;

            XRSummary XrSummary2 = new XRSummary();
            lblTotalData3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
            XrSummary2.FormatString = "{0}";
            XrSummary2.Func = SummaryFunc.Count;
            XrSummary2.Running = SummaryRunning.Report;
            lblTotalData3.Summary = XrSummary2;

            //XRSummary XrSummary3 = new XRSummary();
            //lblTotalData4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
            //XrSummary3.FormatString = "{0}";
            //XrSummary3.Func = SummaryFunc.Sum;
            //XrSummary3.Running = SummaryRunning.Report;
            //lblTotalData4.Summary = XrSummary3;

            XRSummary XrSummary4 = new XRSummary();
            lblTotalData5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data5") });
            XrSummary4.FormatString = "{0}";
            XrSummary4.Func = SummaryFunc.Sum;
            XrSummary4.Running = SummaryRunning.Report;
            lblTotalData5.Summary = XrSummary4;


        }
    }
}
