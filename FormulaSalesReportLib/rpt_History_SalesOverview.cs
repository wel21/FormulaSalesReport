using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;

namespace FormulaReportsLib
{
    public partial class rpt_History_SalesOverview : rpt
    {
        public rpt_History_SalesOverview()
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

        protected override void OnDataSourceChanging()
        {
            base.OnDataSourceChanging();

            for (int i = 0; i < OptionalColumnsToSomeReports.Count; i++)
            {
                try
                {
                    XRLabel lT = (XRLabel)FindControl("lblT" + (i + 1) + "t3", true);
                    lT.BeforePrint -= PercTotal_BeforePrint;
                }
                catch { }
            }
        }

        List<XRLabel> addedlabels = new List<XRLabel>();

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

            XRSummary XrSummary = new XRSummary();
            if (OptionalColumnsToSomeReports.Count > 4)
            {
                PaperKind = System.Drawing.Printing.PaperKind.Custom;
                PageWidth = 1300;
            }

            lblData.DataBindings.Add("Text", this.DataSource, "Data");

            float x = lblCt0.LeftF;
            float x1 = lblCt1.LeftF;
            float x2 = lblCt2.LeftF;
            float x3 = lblCt3.LeftF;
            int dataindex = 1;

            xrPanel1.LeftF = x;
            xrPanel2.LeftF = x;
            xrPanel3.LeftF = x;

            //dispose added labels
            if (addedlabels.Count > 0)
                foreach (XRLabel lbladded in addedlabels)
                    lbladded.Dispose();

            //clear addedlabels
            addedlabels.Clear();

            for (int i = 0; i < OptionalColumnsToSomeReports.Count; i++)
            {
                // add column to header
                // header title
                XRLabel lC = new lbl(lblCt0);
                lC.Name = "lblC" + (i + 1) + "";
                lC.LocationF = new PointF(x, 0);
                lC.Text = OptionalColumnsToSomeReports[i].Text;
                PageHeader.Controls.Add(lC);
                addedlabels.Add(lC);
                // column 1
                lC = new lbl(lblCt1);
                lC.Name = "lblC" + (i + 1) + "t1";
                lC.LocationF = new PointF(x1, lblCt1.TopF);
                lC.Text = "#"; //OptionalColumnsToSomeReports[i].SubColumns[0];
                PageHeader.Controls.Add(lC);
                addedlabels.Add(lC);
                // column 2
                lC = new lbl(lblCt2);
                lC.Name = "lblC" + (i + 1) + "t2";
                lC.LocationF = new PointF(x2, lblCt2.TopF);
                lC.Text = "Amount"; //OptionalColumnsToSomeReports[i].SubColumns[1];
                PageHeader.Controls.Add(lC);
                addedlabels.Add(lC);
                // column 3
                lC = new lbl(lblCt3);
                lC.Name = "lblC" + (i + 1) + "t3";
                lC.LocationF = new PointF(x3, lblCt2.TopF);
                lC.Text = "%"; //OptionalColumnsToSomeReports[i].SubColumns[1];
                PageHeader.Controls.Add(lC);
                addedlabels.Add(lC);




                // add column to row
                // row 1
                XRLabel lR = new lbl(lblDt1);
                lR.Name = "lblD" + (i + 1) + "t1";
                lR.LocationF = new PointF(x1, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (dataindex));
                Detail.Controls.Add(lR);
                addedlabels.Add(lR);
                // row 2
                lR = new lbl(lblDt2);
                lR.Name = "lblD" + (i + 1) + "t2";
                lR.LocationF = new PointF(x2, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (dataindex + 1));
                Detail.Controls.Add(lR);
                addedlabels.Add(lR);
                // row 3
                lR = new lbl(lblDt3);
                lR.Name = "lblD" + (i + 1) + "t2";
                lR.LocationF = new PointF(x3, 0);
                lR.DataBindings.Add("Text", this.DataSource, "Data" + (dataindex + 2));
                Detail.Controls.Add(lR);
                addedlabels.Add(lR);




                // add column to total
                // total 1
                XRLabel lT = new lbl(lblTDt1);
                if (FindControl("lblT" + (i + 1) + "t1", false) == null)
                {
                    lT.Name = "lblT" + (i + 1) + "t1";
                    lT.LocationF = new PointF(x1, 0);
                    //---------
                    lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex)) });
                    XrSummary.FormatString = "{0}";
                    XrSummary.Func = SummaryFunc.Sum;
                    XrSummary.Running = SummaryRunning.Report;
                    lT.Summary = XrSummary;
                    //---------
                    GroupFooter1.Controls.Add(lT);
                    addedlabels.Add(lT);
                }
                else
                {
                    lT = (XRLabel)FindControl("lblT" + (i + 1) + "t1", true);
                    lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex)) });
                    XrSummary.FormatString = "{0}";
                    XrSummary.Func = SummaryFunc.Sum;
                    XrSummary.Running = SummaryRunning.Report;
                    lT.Summary = XrSummary;
                }

                // total 2
                if (FindControl("lblT" + (i + 1) + "t2", false) == null)
                {
                    lT = new lbl(lblTDt2);
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
                    addedlabels.Add(lT);
                }
                else
                {
                    lT = (XRLabel)FindControl("lblT" + (i + 1) + "t2", true);
                    XrSummary = new XRSummary();
                    lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex + 1)) });
                    XrSummary.FormatString = "{0}";
                    XrSummary.Func = SummaryFunc.Sum;
                    XrSummary.Running = SummaryRunning.Group;
                    lT.Summary = XrSummary;
                }

                // total 3
                if (FindControl("lblT" + (i + 1) + "t3", false) == null)
                {
                    lT = new lbl(lblTDt3);
                    lT.Name = "lblT" + (i + 1) + "t3";
                    lT.LocationF = new PointF(x3, 0);
                    //---------
                    //XrSummary = new XRSummary();
                    //lT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (dataindex + 2)) });
                    //XrSummary.FormatString = "{0}";
                    //XrSummary.Func = SummaryFunc.Sum;
                    //XrSummary.Running = SummaryRunning.Group;
                    //lT.Summary = XrSummary;
                    //---------
                    GroupFooter1.Controls.Add(lT);
                    addedlabels.Add(lT);
                    lT.BeforePrint += PercTotal_BeforePrint;
                }
                else
                {
                    lT.BeforePrint += PercTotal_BeforePrint;
                }


                dataindex += 3;
                x += lblCt0.WidthF;
                x1 += lblCt1.WidthF + lblCt2.WidthF + lblCt3.WidthF;
                x2 += lblCt2.WidthF + lblCt3.WidthF + lblCt1.WidthF;
                x3 += lblCt3.WidthF + lblCt1.WidthF + lblCt2.WidthF;

                xrPanel1.LeftF = x;
                xrPanel2.LeftF = x;
                xrPanel3.LeftF = x;
            }

            int fi = OptionalColumnsToSomeReports.Count * 3;
            lblD1.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 1));
            lblD2.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 2));
            lblD3.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 3));
            lblD4.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 4));
            lblD5.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 5));
            lblD6.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 6));
            lblD7.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 7));
            lblD8.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 8));
            lblD9.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 9));
            lblD10.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 10));
            lblD11.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 11));
            lblD12.DataBindings.Add("Text", this.DataSource, "Data" + (fi + 12));

            //total
            XrSummary = new XRSummary();
            blT1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 1)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT1.Summary = XrSummary;
            
            XrSummary = new XRSummary();
            blT2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 2)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT2.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 3)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT3.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 4)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT4.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 5)) });
            XrSummary.FormatString = "{0.00}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT5.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 6)) });
            XrSummary.FormatString = "{0.00}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT6.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 7)) });
            XrSummary.FormatString = "{0.00}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT7.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT8.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 8)) });
            XrSummary.FormatString = "{0.00}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT8.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT9.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 9)) });
            XrSummary.FormatString = "{0.0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT9.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT10.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 10)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT10.Summary = XrSummary;

            XrSummary = new XRSummary();
            blT11.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 11)) });
            XrSummary.FormatString = "{0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT11.Summary = XrSummary;


            XrSummary = new XRSummary();
            blT12.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DataSource, "Data" + (fi + 12)) });
            XrSummary.FormatString = "{0.0}";
            XrSummary.Func = SummaryFunc.Sum;
            XrSummary.Running = SummaryRunning.Report;
            blT12.Summary = XrSummary;
        }

        private void PercTotal_BeforePrint(object sender, PrintEventArgs e)
        {
            //int idx = 0;
            XRLabel myperc = (XRLabel)sender;
            //idx = Convert.ToInt32(myperc.Name.Replace("lblT","").Substring(0, 1));
            XRLabel myamt = (XRLabel)FindControl(myperc.Name.Substring(0, myperc.Name.Length - 1) + "2", true);
            ((XRLabel)sender).Text = ((Helpers.NullToFlt(myamt.Text) / Helpers.NullToFlt(blT5.Text)) * 100).DecimalPlace(0) + "";
            //// Create an array of labels which the total summary will be calculated for.
            //XRLabel[] monthLabels = new XRLabel[] {totalJanuary,
            //    totalFebruary, totalMarch, totalApril, totalMay};

            //        // Create a total variable.
            //        double total = 0;

            //        // Use the Summary.GetResult method of each summary label to calculate
            //        // the total of all the subtotals for the first 5 months in the year.
            //        foreach (XRLabel monthLabel in monthLabels)
            //        {
            //            total += Convert.ToDouble(monthLabel.Summary.GetResult());
            //        }

            //// Set the result to the grandTotal label's text.
            //((XRLabel)sender).Text = total.ToString("n2");
        }
    }
}
