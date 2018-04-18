using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_Employee_PayrollReport : rpt
    {
        public rpt_Employee_PayrollReport()
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
                XRSummary XrSummary = new XRSummary();
                //lblTotalData.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data") });
                //XrSummary.FormatString = "{0}";
                //XrSummary.Func = SummaryFunc.Sum;
                //XrSummary.Running = SummaryRunning.Report;
                //lblTotalData.Summary = XrSummary;

                //XRSummary XrSummary1 = new XRSummary();
                //lblTotalData1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data1") });
                //XrSummary1.FormatString = "{0}";
                //XrSummary1.Func = SummaryFunc.Sum;
                //XrSummary1.Running = SummaryRunning.Report;
                //lblTotalData1.Summary = XrSummary1;

                //XRSummary XrSummary2 = new XRSummary();
                //lblTotalData2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data2") });
                //XrSummary2.FormatString = "{0}";
                //XrSummary2.Func = SummaryFunc.Count;
                //XrSummary2.Running = SummaryRunning.Report;
                //lblTotalData2.Summary = XrSummary2;

                //XRSummary XrSummary3 = new XRSummary();
                //lblTotalData3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
                //XrSummary3.FormatString = "{0}";
                //XrSummary3.Func = SummaryFunc.Count;
                //XrSummary3.Running = SummaryRunning.Report;
                //lblTotalData3.Summary = XrSummary3;

                //XrSummary = new XRSummary();
                //blT4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 4)) });
                //XrSummary.FormatString = "{0}";
                //XrSummary.Func = SummaryFunc.Sum;
                //XrSummary.Running = SummaryRunning.Report;
                //blT4.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data5") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT5.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data6") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT6.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data7") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT7.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data8") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT8.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data9") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT9.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data10") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT10.Summary = XrSummary;

                XrSummary = new XRSummary();
                blT11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data11") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT11.Summary = XrSummary;


                XrSummary = new XRSummary();
                blT12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data12") });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                blT12.Summary = XrSummary;
            }
            catch { }
        }
    }
}
