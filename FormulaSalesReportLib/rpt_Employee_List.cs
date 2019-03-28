using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_Employee_List : rpt
    {
        public rpt_Employee_List()
        {
            InitializeComponent();
        }

        private void rpt_Employee_List_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // report info --------------------------------------------------------------------
            lblStoreName.Text = StoreInformation.StoreName;
            lblStoreAddress.Text = "Address: " + StoreInformation.StoreAddress;
            lblStorePhone.Text = "Phone Number: " + StoreInformation.StoreNumber;

            //string _Date = "";
            //if (ParamDate.Count == 2)
            //{ _Date = ParamDate[0].date.ToShortDateString() + " to " + ParamDate[1].date.ToShortDateString(); }
            //else
            //{
            //    for (int i = 0; i < ParamDate.Count; i++)
            //    { _Date += Helpers.ConvertMyDate(ParamDate[i].date) + (i == ParamDate.Count - 1 ? "" : ", "); }
            //}
            //xrLabel2.Text = _Date;
            // report info --------------------------------------------------------------------


            try
            {
                // Create bound labels, and add them to the report's bands.
                lblData.DataBindings.Add("Text", DataSource, "Data");
                lblData1.DataBindings.Add("Text", DataSource, "Data1");
                lblData2.DataBindings.Add("Text", DataSource, "Data2");
                lblData3.DataBindings.Add("Text", DataSource, "Data3");
                
            }
            catch { }

        }
    }
}
