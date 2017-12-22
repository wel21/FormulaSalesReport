using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_Employee_DriverReimbursement : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_Employee_DriverReimbursement()
        {
            InitializeComponent();
        }

        private void rpt_Employee_DriverReimbursement_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
