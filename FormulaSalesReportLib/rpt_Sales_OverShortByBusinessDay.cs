using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace FormulaSalesReportLib
{
    public partial class rpt_Sales_OverShortByBusinessDay : rpt
    {
        public rpt_Sales_OverShortByBusinessDay()
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


            

            lblData.DataBindings.Add("Text", this.DataSource, "Data");
            lblData1.DataBindings.Add("Text", this.DataSource, "Data1");
            lblData2.DataBindings.Add("Text", this.DataSource, "Data2");
            lblData3.DataBindings.Add("Text", this.DataSource, "Data3");
            lblData4.DataBindings.Add("Text", this.DataSource, "Data4");
            lblData5.DataBindings.Add("Text", this.DataSource, "Data5");
            lblData6.DataBindings.Add("Text", this.DataSource, "Data6");
            lblData7.DataBindings.Add("Text", this.DataSource, "Data7");
            lblData8.DataBindings.Add("Text", this.DataSource, "Data8");

            //total quantity
            XRSummary XrSummary1 = new XRSummary();
            lblTotalData1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data1") });
            XrSummary1.FormatString = "{0}";
            XrSummary1.Func = SummaryFunc.Sum;
            XrSummary1.Running = SummaryRunning.Report;
            lblTotalData1.Summary = XrSummary1;

            XRSummary XrSummary2 = new XRSummary();
            lblTotalData2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data2") });
            XrSummary2.FormatString = "{0}";
            XrSummary2.Func = SummaryFunc.Sum;
            XrSummary2.Running = SummaryRunning.Report;
            lblTotalData2.Summary = XrSummary2;

            XRSummary XrSummary3 = new XRSummary();
            lblTotalData3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
            XrSummary3.FormatString = "{0}";
            XrSummary3.Func = SummaryFunc.Sum;
            XrSummary3.Running = SummaryRunning.Report;
            lblTotalData3.Summary = XrSummary3;

            XRSummary XrSummary4 = new XRSummary();
            lblTotalData4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data4") });
            XrSummary4.FormatString = "{0}";
            XrSummary4.Func = SummaryFunc.Sum;
            XrSummary4.Running = SummaryRunning.Report;
            lblTotalData4.Summary = XrSummary4;


        }
    }
}
