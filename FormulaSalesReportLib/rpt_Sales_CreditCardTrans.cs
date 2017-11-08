using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace FormulaSalesReportLib
{
    public partial class rpt_Sales_CreditCardTrans : rpt
    {
        public rpt_Sales_CreditCardTrans()
        {
            InitializeComponent();
        }
        
        private void rpt_Sales_CreditCardTrans_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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

            //FindControl("xrLabel21", true).DataBindings.Add("Text", this.DataSource, "Name");
            //FindControl("xrLabel22", true).DataBindings.Add("Text", this.DataSource, "Quantity");
            //FindControl("xrLabel23", true).DataBindings.Add("Text", this.DataSource, "Amount");
            //FindControl("xrLabel24", true).DataBindings.Add("Text", this.DataSource, "Percent", "{0:p}");
            //FindControl("xrLabel25", true).DataBindings.Add("Text", this.DataSource, "Average", "{0:#,##0.00}");

            // Create a group field, 
            // and assign it to the group header band.
            GroupField groupField3 = new GroupField("Data");
            GroupHeader3.GroupFields.Add(groupField3);
            // Create bound labels, and add them to the report's bands.
            xrLabel5.DataBindings.Add("Text", this.DataSource, "Data");


            // Create a group field, 
            // and assign it to the group header band.
            GroupField groupField2 = new GroupField("Data1");
            GroupHeader2.GroupFields.Add(groupField2);
            // Create bound labels, and add them to the report's bands.
            xrLabel4.DataBindings.Add("Text", this.DataSource, "Data1");


            // Create a group field, 
            // and assign it to the group header band.
            GroupField groupField1 = new GroupField("Data2");
            GroupHeader1.GroupFields.Add(groupField1);
            // Create bound labels, and add them to the report's bands.
            xrLabel3.DataBindings.Add("Text", this.DataSource, "Data2");


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

            //total quantity
            XRSummary XrSummary1 = new XRSummary();
            xrLabel6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data2") });
            XrSummary1.FormatString = "Total Items: {0}";
            XrSummary1.Func = SummaryFunc.Count;
            XrSummary1.Running = SummaryRunning.Group;
            xrLabel6.Summary = XrSummary1;

            XRSummary XrSummaryAmt = new XRSummary();
            xrLabel9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data7") });
            XrSummaryAmt.FormatString = "{0}";
            XrSummaryAmt.Func = SummaryFunc.Sum;
            XrSummaryAmt.Running = SummaryRunning.Group;
            xrLabel9.Summary = XrSummaryAmt;

            XRSummary XrSummaryTip = new XRSummary();
            xrLabel10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data8") });
            XrSummaryTip.FormatString = "{0}";
            XrSummaryTip.Func = SummaryFunc.Sum;
            XrSummaryTip.Running = SummaryRunning.Group;
            xrLabel10.Summary = XrSummaryTip;

            XRSummary XrSummaryTtl = new XRSummary();
            xrLabel11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data9") });
            XrSummaryTtl.FormatString = "{0}";
            XrSummaryTtl.Func = SummaryFunc.Sum;
            XrSummaryTtl.Running = SummaryRunning.Group;
            xrLabel11.Summary = XrSummaryTtl;

            List<ReportData> DT = (List<ReportData>)this.DataSource;
            foreach (ReportData dtr in DT)
            {

            }

            
        }

    }
}
