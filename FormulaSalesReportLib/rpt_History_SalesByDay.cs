﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt_History_SalesByDay : rpt
    {
        public rpt_History_SalesByDay()
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
            lblData9.DataBindings.Add("Text", this.DataSource, "Data9");
            lblData10.DataBindings.Add("Text", this.DataSource, "Data10");
            lblData11.DataBindings.Add("Text", this.DataSource, "Data11");
            lblData12.DataBindings.Add("Text", this.DataSource, "Data12");

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

            XRSummary XrSummary2 = new XRSummary();
            lblTotalData2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data2") });
            XrSummary2.FormatString = "{0}";
            XrSummary2.Func = SummaryFunc.Count;
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

            XRSummary XrSummary5 = new XRSummary();
            lblTotalData5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data5") });
            XrSummary5.FormatString = "{0}";
            XrSummary5.Func = SummaryFunc.Sum;
            XrSummary5.Running = SummaryRunning.Report;
            lblTotalData5.Summary = XrSummary5;

            XRSummary XrSummary6 = new XRSummary();
            lblTotalData6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data6") });
            XrSummary6.FormatString = "{0}";
            XrSummary6.Func = SummaryFunc.Sum;
            XrSummary6.Running = SummaryRunning.Report;
            lblTotalData6.Summary = XrSummary6;

            XRSummary XrSummary7 = new XRSummary();
            lblTotalData7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data7") });
            XrSummary7.FormatString = "{0}";
            XrSummary7.Func = SummaryFunc.Sum;
            XrSummary7.Running = SummaryRunning.Report;
            lblTotalData7.Summary = XrSummary7;

            XRSummary XrSummary8 = new XRSummary();
            lblTotalData8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data8") });
            XrSummary8.FormatString = "{0}";
            XrSummary8.Func = SummaryFunc.Sum;
            XrSummary8.Running = SummaryRunning.Report;
            lblTotalData8.Summary = XrSummary8;

            XRSummary XrSummary9 = new XRSummary();
            lblTotalData9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data9") });
            XrSummary9.FormatString = "{0}";
            XrSummary9.Func = SummaryFunc.Sum;
            XrSummary9.Running = SummaryRunning.Report;
            lblTotalData9.Summary = XrSummary9;

            XRSummary XrSummary10 = new XRSummary();
            lblTotalData10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data10") });
            XrSummary10.FormatString = "{0}";
            XrSummary10.Func = SummaryFunc.Sum;
            XrSummary10.Running = SummaryRunning.Report;
            lblTotalData10.Summary = XrSummary10;

            XRSummary XrSummary11 = new XRSummary();
            lblTotalData11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data11") });
            XrSummary11.FormatString = "{0}";
            XrSummary11.Func = SummaryFunc.Sum;
            XrSummary11.Running = SummaryRunning.Report;
            lblTotalData11.Summary = XrSummary11;

            XRSummary XrSummary12 = new XRSummary();
            lblTotalData12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data12") });
            XrSummary12.FormatString = "{0}";
            XrSummary12.Func = SummaryFunc.Sum;
            XrSummary12.Running = SummaryRunning.Report;
            lblTotalData12.Summary = XrSummary12;
        }

    }
}