using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt_Employee_LaborReport : rpt
    {
        public rpt_Employee_LaborReport()
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



            // Create a group field, 
            // and assign it to the group header band.
            GroupField groupField1 = new GroupField("Data");
            GroupField groupField2 = new GroupField("Data1");
            GroupHeader1.GroupFields.Add(groupField1);
            GroupHeader1.GroupFields.Add(groupField2);
            // Create bound labels, and add them to the report's bands.
            xrLabel5.DataBindings.Add("Text", DataSource, "Data");
            xrLabel4.DataBindings.Add("Text", DataSource, "Data1");


            lblData.DataBindings.Add("Text", DataSource, "Data");
            lblData1.DataBindings.Add("Text", DataSource, "Data1");
            lblData2.DataBindings.Add("Text", DataSource, "Data2");
            lblData3.DataBindings.Add("Text", DataSource, "Data3");
            lblData4.DataBindings.Add("Text", DataSource, "Data4");
            lblData5.DataBindings.Add("Text", DataSource, "Data5");
            lblData6.DataBindings.Add("Text", DataSource, "Data6");

            //total quantity
            XRSummary XrSummary1 = new XRSummary();
            xrLabel11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data2") });
            XrSummary1.FormatString = "{0} Entries";
            XrSummary1.Func = SummaryFunc.Count;
            XrSummary1.Running = SummaryRunning.Group;
            xrLabel11.Summary = XrSummary1;

            XrSummary1 = new XRSummary();
            xrLabel25.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", DataSource, "Data5") });
            XrSummary1.FormatString = "{0}";
            XrSummary1.Func = SummaryFunc.Sum;
            XrSummary1.Running = SummaryRunning.Group;
            xrLabel25.Summary = XrSummary1;

            DataTable DT = (DataTable)MyObject;
            lblF1.Text = DT.Rows[0][0].ToString();
            lblF2.Text = DT.Rows[1][0].ToString();
            lblF3.Text = DT.Rows[2][0].ToString();
            lblF4.Text = DT.Rows[3][0].ToString();
            lblF5.Text = DT.Rows[4][0].ToString();
            lblF6.Text = DT.Rows[5][0].ToString();


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
    }
}
