using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_History_PaymentsBySrvcType : rpt
    {
        public rpt_History_PaymentsBySrvcType()
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

            float x = lblC.LeftF;
            for (int i = 0; i < OptionalColumnsToSomeReports.Count; i++)
            {
                // add column to header
                XRLabel lC = new XRLabel();
                lC.Name = "lblC" + (i + 1);
                lC.LocationF = new PointF(x, 0);
                lC.SizeF = lblC.SizeF;
                lC.Text = OptionalColumnsToSomeReports[i].Text;
                lC.Font = lblC.Font;
                lC.BackColor = lblC.BackColor;
                lC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                lC.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0);
                PageHeader.Controls.Add(lC);

                // add column to row
                XRLabel lR = new XRLabel();
                lR.Name = "lblD" + (i + 1);
                lR.LocationF = new PointF(x, 0);
                lR.SizeF = lblD.SizeF;
                lR.Text = OptionalColumnsToSomeReports[i].Text;
                lR.Font = lblD.Font;
                lR.BackColor = lblD.BackColor;
                lR.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                lR.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (i + 1));
                lR.Borders = lblD.Borders;
                lR.BorderColor = lblD.BorderColor;
                Detail.Controls.Add(lR);

                // add column to total
                XRLabel lT = new XRLabel();
                lT.Name = "lblT" + (i + 1);
                lT.LocationF = new PointF(x, 0);
                lT.SizeF = lblTotalD.SizeF;
                lT.Text = OptionalColumnsToSomeReports[i].Text;
                lT.Font = lblTotalD.Font;
                lT.BackColor = lblTotalD.BackColor;
                lT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                lT.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0);
                lT.Borders = lblTotalD.Borders;
                lT.BorderColor = lblTotalD.BorderColor;
                //---------
                XRSummary XrSummary = new XRSummary();
                lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (i + 1)) });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Group;
                lT.Summary = XrSummary;
                //---------
                GroupFooter1.Controls.Add(lT);

                x += lblC.WidthF;
            }

        }
    }
}
