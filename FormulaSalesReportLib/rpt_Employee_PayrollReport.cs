using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
                // Create a group field, 
                // and assign it to the group header band.
                GroupField groupField1 = new GroupField("Data");
                GroupHeader1.GroupFields.Add(groupField1);
                // Create bound labels, and add them to the report's bands.
                xrLabel5.DataBindings.Add("Text", DataSource, "Data");


                lblData.DataBindings.Add("Text", DataSource, "Data1");
                lblData1.DataBindings.Add("Text", DataSource, "Data2");
                lblData2.DataBindings.Add("Text", DataSource, "Data3");
                lblData3.DataBindings.Add("Text", DataSource, "Data4");
                lblData4.DataBindings.Add("Text", DataSource, "Data5");
                //lblData5.DataBindings.Add("Text", DataSource, "Data6");
                lblData6.DataBindings.Add("Text", DataSource, "Data7");
                //lblData7.DataBindings.Add("Text", DataSource, "Data8");

                //running total
                XRSummary XrSummary1 = new XRSummary();
                lblF1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data5") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Group;
                lblF1.Summary = XrSummary1;

                XrSummary1 = new XRSummary();
                lblF2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data6") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Group;
                lblF2.Summary = XrSummary1;

                XrSummary1 = new XRSummary();
                lblF3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data7") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Group;
                lblF3.Summary = XrSummary1;

                XrSummary1 = new XRSummary();
                lblF4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data8") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Group;
                lblF4.Summary = XrSummary1;

                XrSummary1 = new XRSummary();
                lblF5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data9") });
                XrSummary1.FormatString = "{0}";
                XrSummary1.Func = SummaryFunc.Sum;
                XrSummary1.Running = SummaryRunning.Group;
                lblF5.Summary = XrSummary1;
                
                DataTable DT = (DataTable)MyObject;
                //lblF1.Text = DT.Rows[0][0].ToString();
                //lblF2.Text = DT.Rows[1][0].ToString();
                //lblF3.Text = DT.Rows[2][0].ToString();
                //lblF4.Text = DT.Rows[3][0].ToString();
                //lblF5.Text = DT.Rows[4][0].ToString();
                lblF6.Text = DT.Rows[0][0].ToString();
                lblF7.Text = DT.Rows[1][0].ToString();

                //XRSummary XrSummary2 = new XRSummary();
                //lblTotalData3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
                //XrSummary2.FormatString = "{0}";
                //XrSummary2.Func = SummaryFunc.Count;
                //XrSummary2.Running = SummaryRunning.Report;
                //lblTotalData3.Summary = XrSummary2;

                //XRSummary XrSummary3 = new XRSummary();
                //lblTotalData4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data3") });
                //XrSummary3.FormatString = "{0}";
                //XrSummary3.Func = SummaryFunc.Sum;
                //XrSummary3.Running = SummaryRunning.Report;
                //lblTotalData4.Summary = XrSummary3;

                //XRSummary XrSummary4 = new XRSummary();
                //lblTotalData5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data5") });
                //XrSummary4.FormatString = "{0}";
                //XrSummary4.Func = SummaryFunc.Sum;
                //XrSummary4.Running = SummaryRunning.Report;
                //lblTotalData5.Summary = XrSummary4;
            }
            catch { }

        }
    }
}
