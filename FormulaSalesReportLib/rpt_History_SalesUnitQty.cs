using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public partial class rpt_History_SalesUnitQty : rpt
    {
        public rpt_History_SalesUnitQty()
        {
            InitializeComponent();
        }

        internal class lbl : XRLabel
        {
            public lbl(XRLabel xrlabel)
            {
                SizeF = xrlabel.SizeF;
                Font = xrlabel.Font;
                BackColor = xrlabel.BackColor;
                BorderColor = xrlabel.BorderColor;
                Borders = xrlabel.Borders;
                TextAlignment = xrlabel.TextAlignment;
                Padding = xrlabel.Padding;
            }
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

            float x = lblCt0.LeftF;
            float x1 = lblCt1.LeftF;
            float x2 = lblCt2.LeftF;
            int dataindex = 1;
            for (int i = 0; i < OptionalColumnsToSomeReports.Count; i++)
            {
                // add column to header
                // header title
                XRLabel lC = new lbl(lblCt0);
                lC.Name = "lblC" + (i + 1) + "";
                lC.LocationF = new PointF(x, 0);
                lC.Text = OptionalColumnsToSomeReports[i].Text;
                PageHeader.Controls.Add(lC);
                // column 1
                lC = new lbl(lblCt1);
                lC.Name = "lblC" + (i + 1) + "t1";
                lC.LocationF = new PointF(x1, lblCt1.TopF);
                lC.Text = "#"; //OptionalColumnsToSomeReports[i].SubColumns[0];
                PageHeader.Controls.Add(lC);
                // column 2
                lC = new lbl(lblCt2);
                lC.Name = "lblC" + (i + 1) + "t2";
                lC.LocationF = new PointF(x2, lblCt2.TopF);
                lC.Text = "Sales"; //OptionalColumnsToSomeReports[i].SubColumns[1];
                PageHeader.Controls.Add(lC);




                // add column to row
                // row 1
                XRLabel lR = new lbl(lblDt1);
                lR.Name = "lblD" + (i + 1) + "t1";
                lR.LocationF = new PointF(x1, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (dataindex));
                Detail.Controls.Add(lR);
                // row 2
                lR = new lbl(lblDt2);
                lR.Name = "lblD" + (i + 1) + "t2";
                lR.LocationF = new PointF(x2, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (dataindex + 1));
                Detail.Controls.Add(lR);




                // add column to total
                // total 1
                XRLabel lT = new lbl(lblTotalDt1);
                lT.Name = "lblT" + (i + 1) + "t1";
                lT.LocationF = new PointF(x1, 0);
                //---------
                XRSummary XrSummary = new XRSummary();
                lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex)) });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Report;
                lT.Summary = XrSummary;
                //---------
                GroupFooter1.Controls.Add(lT);
                // total 2
                lT = new lbl(lblTotalDt2);
                lT.Name = "lblT" + (i + 1) + "t2";
                lT.LocationF = new PointF(x2, 0);
                //---------
                XrSummary = new XRSummary();
                lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex + 1)) });
                XrSummary.FormatString = "{0}";
                XrSummary.Func = SummaryFunc.Sum;
                XrSummary.Running = SummaryRunning.Group;
                lT.Summary = XrSummary;
                //---------
                GroupFooter1.Controls.Add(lT);


                dataindex += 2;
                x += lblCt0.WidthF;
                x1 += lblCt1.WidthF + lblCt2.WidthF;
                x2 += lblCt2.WidthF + lblCt1.WidthF;
            }

            //lblD.DataBindings.Add("Text", this.DataSource, "Data1");
            //lblData2.DataBindings.Add("Text", this.DataSource, "Data2");
            //lblData3.DataBindings.Add("Text", this.DataSource, "Data3");
            //lblData4.DataBindings.Add("Text", this.DataSource, "Data4");
            //lblData5.DataBindings.Add("Text", this.DataSource, "Data5");

            //total
            //XRSummary XrSummary = new XRSummary();
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
            //XrSummary3.Func = SummaryFunc.Sum;
            //XrSummary3.Running = SummaryRunning.Report;
            //lblTotalData3.Summary = XrSummary3;

            //XRSummary XrSummary4 = new XRSummary();
            //lblTotalData4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data4") });
            //XrSummary4.FormatString = "{0}";
            //XrSummary4.Func = SummaryFunc.Sum;
            //XrSummary4.Running = SummaryRunning.Report;
            //lblTotalData4.Summary = XrSummary4;

            //XRSummary XrSummary5 = new XRSummary();
            //lblTotalData5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data5") });
            //XrSummary5.FormatString = "{0}";
            //XrSummary5.Func = SummaryFunc.Sum;
            //XrSummary5.Running = SummaryRunning.Report;
            //lblTotalData5.Summary = XrSummary5;

            //XRSummary XrSummary6 = new XRSummary();
            //lblTotalData6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data6") });
            //XrSummary6.FormatString = "{0}";
            //XrSummary6.Func = SummaryFunc.Sum;
            //XrSummary6.Running = SummaryRunning.Report;
            //lblTotalData6.Summary = XrSummary6;

            //XRSummary XrSummary7 = new XRSummary();
            //lblTotalData7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data7") });
            //XrSummary7.FormatString = "{0}";
            //XrSummary7.Func = SummaryFunc.Sum;
            //XrSummary7.Running = SummaryRunning.Report;
            //lblTotalData7.Summary = XrSummary7;

            //XRSummary XrSummary8 = new XRSummary();
            //lblTotalData8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data8") });
            //XrSummary8.FormatString = "{0}";
            //XrSummary8.Func = SummaryFunc.Sum;
            //XrSummary8.Running = SummaryRunning.Report;
            //lblTotalData8.Summary = XrSummary8;

            //XRSummary XrSummary9 = new XRSummary();
            //lblTotalData9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data9") });
            //XrSummary9.FormatString = "{0}";
            //XrSummary9.Func = SummaryFunc.Sum;
            //XrSummary9.Running = SummaryRunning.Report;
            //lblTotalData9.Summary = XrSummary9;

            //XRSummary XrSummary10 = new XRSummary();
            //lblTotalData10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data10") });
            //XrSummary10.FormatString = "{0}";
            //XrSummary10.Func = SummaryFunc.Sum;
            //XrSummary10.Running = SummaryRunning.Report;
            //lblTotalData10.Summary = XrSummary10;
        }
    }
}
