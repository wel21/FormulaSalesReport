using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace FormulaReportsLib
{
    public partial class rpt_Sales_CreditCardTrans : rpt
    {
        bool bTotalLoaded = false;
        public rpt_Sales_CreditCardTrans()
        {
            InitializeComponent();
        }

        protected override void OnDataSourceChanging()
        {
            base.OnDataSourceChanging();
            bTotalLoaded = false;
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

            try
            {
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
                List<ReportData> DTTotal = new List<ReportData>();
                if (DT != null && bTotalLoaded == false)
                {
                    foreach (ReportData dtr in DT)
                    {
                        if (DTTotal.Count == 0)
                        {
                            //                        Data, Data1,     Data2,     Data3,     Data4
                            DTTotal.Add(new ReportData("1", dtr.Data1, dtr.Data7, dtr.Data8, dtr.Data9));
                            DTTotal[0].SubData.Add(new ReportData("1", dtr.Data2, dtr.Data7, dtr.Data8, dtr.Data9));
                        }
                        else
                        {
                            int ipos = -1;
                            for (int i = 0; i < DTTotal.Count; i++)
                            {
                                if (DTTotal[i].Data1 == dtr.Data1)
                                {
                                    ipos = i;
                                    break;
                                }
                            }
                            if (ipos > -1)
                            {
                                DTTotal[ipos].Data = (Helpers.NullToFlt(DTTotal[ipos].Data) + 1).ToString();
                                DTTotal[ipos].Data2 = (Helpers.NullToFlt(DTTotal[ipos].Data2) + Helpers.NullToFlt(dtr.Data7)).ToString();
                                DTTotal[ipos].Data3 = (Helpers.NullToFlt(DTTotal[ipos].Data3) + Helpers.NullToFlt(dtr.Data8)).ToString();
                                DTTotal[ipos].Data4 = (Helpers.NullToFlt(DTTotal[ipos].Data4) + Helpers.NullToFlt(dtr.Data9)).ToString();

                                // sub data
                                int ipossub = -1;
                                for (int i = 0; i < DTTotal[ipos].SubData.Count; i++)
                                {
                                    if (DTTotal[ipos].SubData[i].Data1 == dtr.Data2)
                                    {
                                        ipossub = i;
                                        break;
                                    }
                                }
                                if (ipossub > -1)
                                {
                                    DTTotal[ipos].SubData[ipossub].Data = (Helpers.NullToFlt(DTTotal[ipos].SubData[ipossub].Data) + 1).ToString();
                                    DTTotal[ipos].SubData[ipossub].Data2 = (Helpers.NullToFlt(DTTotal[ipos].SubData[ipossub].Data2) + Helpers.NullToFlt(dtr.Data7)).ToString();
                                    DTTotal[ipos].SubData[ipossub].Data3 = (Helpers.NullToFlt(DTTotal[ipos].SubData[ipossub].Data3) + Helpers.NullToFlt(dtr.Data8)).ToString();
                                    DTTotal[ipos].SubData[ipossub].Data4 = (Helpers.NullToFlt(DTTotal[ipos].SubData[ipossub].Data4) + Helpers.NullToFlt(dtr.Data9)).ToString();
                                }
                                else
                                {
                                    DTTotal[ipos].SubData.Add(new ReportData("1", dtr.Data2, dtr.Data7, dtr.Data8, dtr.Data9));
                                }
                                // sub data

                            }
                            else
                            {
                                DTTotal.Add(new ReportData("1", dtr.Data1, dtr.Data7, dtr.Data8, dtr.Data9));
                                DTTotal[DTTotal.Count - 1].SubData.Add(new ReportData("1", dtr.Data2, dtr.Data7, dtr.Data8, dtr.Data9));
                            }
                        }
                    }

                    //clear the controls first
                    try
                    {
                        foreach (XRControl ctrl in GroupFooter4.Controls)
                        {
                            if (ctrl.Name == "lblR1C1" || ctrl.Name == "lblR1C2" || ctrl.Name == "lblR1C3" ||
                                ctrl.Name == "lblR1C4" || ctrl.Name == "lblR1C5" || ctrl.Name == "xrPanel1")
                                continue;

                            ctrl.Dispose();
                        }
                    }
                    catch
                    { }


                    if (DTTotal.Count > 0)
                    {
                        //lblR1C1.Text = DTTotal[0].Data;
                        //lblR1C2.Text = DTTotal[0].Data1;
                        //lblR1C3.Text = DTTotal[0].Data2;
                        //lblR1C4.Text = DTTotal[0].Data3;
                        //lblR1C5.Text = DTTotal[0].Data4;

                        float y = lblR1C1.TopF;
                        float[] width = new float[] { lblR1C1.WidthF,
                                              lblR1C2.WidthF,
                                              lblR1C3.WidthF,
                                              lblR1C4.WidthF,
                                              lblR1C5.WidthF };
                        float[] left = new float[] { lblR1C1.LeftF,
                                              lblR1C2.LeftF,
                                              lblR1C3.LeftF,
                                              lblR1C4.LeftF,
                                              lblR1C5.LeftF };
                        DevExpress.XtraPrinting.BorderSide[] borders = new DevExpress.XtraPrinting.BorderSide[] { lblR1C1.Borders,
                                              lblR1C2.Borders,
                                              lblR1C3.Borders,
                                              lblR1C4.Borders,
                                              lblR1C5.Borders };
                        for (int i = 0; i < DTTotal.Count; i++)
                        {
                            for (int j = 1; j <= 5; j++)
                            {
                                XRLabel lbl = new XRLabel();
                                lbl.Name = "lblR" + (i + 1) + "C" + j;
                                lbl.LocationF = new PointF(left[j - 1], y);
                                lbl.SizeF = new SizeF(width[j - 1], lblR1C1.HeightF);
                                lbl.Text = DTTotal[i].DataIndex(j - 1);
                                lbl.Font = lblR1C1.Font;
                                lbl.TextAlignment = (j == 2 ? DevExpress.XtraPrinting.TextAlignment.MiddleLeft : DevExpress.XtraPrinting.TextAlignment.MiddleRight);
                                lbl.XlsxFormatString = lblR1C1.XlsxFormatString;
                                lbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0);
                                //lbl.Borders = lblR1C1.Borders;
                                GroupFooter4.Controls.Add(lbl);
                            }
                            y += lblR1C1.HeightF;

                            // sub data
                            for (int m = 0; m < DTTotal[i].SubData.Count; m++)
                            {
                                for (int j = 1; j <= 5; j++)
                                {
                                    XRLabel lblsub = new XRLabel();
                                    lblsub.Name = "lblR" + (i + 1) + "C" + j + "_Sub" + m;
                                    lblsub.LocationF = new PointF(left[j - 1], y);
                                    lblsub.SizeF = new SizeF(width[j - 1], lblR1C1.HeightF);
                                    lblsub.Text = DTTotal[i].SubData[m].DataIndex(j - 1);
                                    lblsub.Font = new Font(lblR1C1.Font.FontFamily, lblR1C1.Font.Size, FontStyle.Regular);
                                    lblsub.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    lblsub.XlsxFormatString = lblR1C1.XlsxFormatString;
                                    lblsub.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0);
                                    lblsub.Borders = borders[j - 1];
                                    if (m == DTTotal[i].SubData.Count - 1)
                                        lblsub.Borders = borders[j - 1] | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    lblsub.BorderColor = lblR1C1.BorderColor;
                                    GroupFooter4.Controls.Add(lblsub);
                                }
                                y += lblR1C1.HeightF + (m == DTTotal[i].SubData.Count - 1 ? 5 : 0);
                            }
                            // sub data

                            //total
                            lblT3.Text = (Helpers.NullToFlt(lblT3.Text) + Helpers.NullToFlt(DTTotal[i].Data2)).ToString();
                            lblT4.Text = (Helpers.NullToFlt(lblT4.Text) + Helpers.NullToFlt(DTTotal[i].Data3)).ToString();
                            lblT5.Text = (Helpers.NullToFlt(lblT5.Text) + Helpers.NullToFlt(DTTotal[i].Data4)).ToString();
                            xrPanel1.TopF = y;
                        }
                    }

                    bTotalLoaded = true;
                }
            }
            catch { }
        }
    }
}
