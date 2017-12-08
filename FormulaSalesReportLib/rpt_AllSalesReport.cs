using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt_AllSalesReport : rpt
    {
        public rpt_AllSalesReport()
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


            AllSalesReport AllSalesTotals = (AllSalesReport)MyObject;

            // Net Sales By Service Type ############################
            lbl1Data.DataBindings.Add("Text", DetailReport.DataSource, "Data");
            lbl1Data1.DataBindings.Add("Text", DetailReport.DataSource, "Data1");
            lbl1Data2.DataBindings.Add("Text", DetailReport.DataSource, "Data2");
            lbl1Data3.DataBindings.Add("Text", DetailReport.DataSource, "Data3");
            lbl1Data4.DataBindings.Add("Text", DetailReport.DataSource, "Data4");

            lbl1TR1C1.Text = AllSalesTotals.rpt1TotalQty.ToString();
            lbl1TR1C2.Text = AllSalesTotals.rpt1TotalAmt.ToString();
            lbl1TR2C2.Text = AllSalesTotals.rpt1Delivery.ToString();
            lbl1TR3C2.Text = "(" + AllSalesTotals.rpt1Reimbursement.ToString() + ")";
            lbl1TR4C2.Text = AllSalesTotals.rpt1NetSales.ToString();
            lbl1TR5C2.Text = AllSalesTotals.rpt1SalesTax.ToString();
            lbl1TR6C1.Text = AllSalesTotals.rpt1GrossQty.ToString();
            lbl1TR6C2.Text = AllSalesTotals.rpt1GrossAmt.ToString();
            lbl1TR7C1.Text = AllSalesTotals.rpt1DiscountQty.ToString();
            lbl1TR7C2.Text = AllSalesTotals.rpt1DiscountAmt.ToString();




            // Gross Sales Summary by Hours #########################
            lbl2Data.DataBindings.Add("Text", DetailReport1.DataSource, "Data");
            lbl2Data1.DataBindings.Add("Text", DetailReport1.DataSource, "Data1");
            lbl2Data2.DataBindings.Add("Text", DetailReport1.DataSource, "Data2");
            lbl2Data3.DataBindings.Add("Text", DetailReport1.DataSource, "Data3");

            lbl2TR1C1.Text = AllSalesTotals.rpt2TotalAmt.ToString();
            lbl2TR2C1.Text = AllSalesTotals.rpt2BestHour.ToString();




            // Net Sales By Category ################################
            lbl3Data.DataBindings.Add("Text", DetailReport2.DataSource, "Data");
            lbl3Data1.DataBindings.Add("Text", DetailReport2.DataSource, "Data1");
            lbl3Data2.DataBindings.Add("Text", DetailReport2.DataSource, "Data2");
            lbl3Data3.DataBindings.Add("Text", DetailReport2.DataSource, "Data3");

            lbl3TR1C1.Text = AllSalesTotals.rpt3TotalQty.ToString();
            lbl3TR1C2.Text = AllSalesTotals.rpt3TotalAmt.ToString();
            lbl3TR2C2.Text = "(" + AllSalesTotals.rpt3DiscountAmt.ToString() + ")";
            lbl3TR3C2.Text = AllSalesTotals.rpt3NetSales.ToString();




            // Discount Summary ####################################
            lbl4Data.DataBindings.Add("Text", DetailReport3.DataSource, "Data");
            lbl4Data1.DataBindings.Add("Text", DetailReport3.DataSource, "Data1");
            lbl4Data2.DataBindings.Add("Text", DetailReport3.DataSource, "Data2");
            lbl4Data3.DataBindings.Add("Text", DetailReport3.DataSource, "Data3");

            lbl4TR1C1.Text = AllSalesTotals.rpt4TotalQty.ToString();
            lbl4TR1C2.Text = AllSalesTotals.rpt4TotalAmt.ToString();
            lbl4TR1C3.Text = AllSalesTotals.rpt4TotalPercent.ToString();

        }
    }
}
