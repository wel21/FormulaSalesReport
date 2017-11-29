using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting.Preview;

namespace FormulaSalesReportLib
{

    public class CSales_CreditCardTrans : CReport
    {
        public CSales_CreditCardTrans(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            if (DatabaseConnectionSettings.Database == "purepos")
            {
                Query = "SELECT FormatDate(a.date,0) AS DATA, a.paymenttype AS data1, SUBSTRING(a.tendertype,8) AS data2, SUBSTRING(c.Account,LENGTH(c.Account)-3) AS data3, a.time AS data4, a.ticketnumber AS data5, a.refno AS data6, a.subtotal AS data7, b.tipsAdded AS data8 " +
                        "FROM paymenthistory AS a " +
                        "INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND FormatDate(a.date,0) = FormatDate(b.date,0) " +
                        "LEFT JOIN ccreceipts AS c ON a.uniqueid = c.PaymentID " +
                        "WHERE @myparam AND a.tenderType LIKE 'CREDIT%' " +
                        "GROUP BY a.ticketnumber, a.paymentType, a.time, a.subtotal, b.tipsadded";
            }
            else
            {
                Query = "SELECT FormatDate(a.date,0) AS DATA, a.paymenttype AS data1, SUBSTRING(a.tendertype,8) AS data2, a.time AS data3, a.time AS data4, a.ticketnumber AS data5, a.refno AS data6, a.subtotal AS data7, b.tipsAdded AS data8 " +
                        "FROM paymenthistory AS a " +
                        "INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND FormatDate(a.date,0) = FormatDate(b.date,0) " +
                        "WHERE @myparam AND a.tenderType LIKE 'CREDIT%' " +
                        "GROUP BY a.ticketnumber, a.paymentType, a.time, a.subtotal, b.tipsadded";
            }
        }

        /// <summary>
        /// The query should have 9 columns. Set the columns name as (data, data1, data2, ... , data8) and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field9 AS data8 FROM table WHERE @myparam.
        /// </summary>
        public string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "FormatDate(a.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(a.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(a.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                if (DatabaseConnectionSettings.Database == "purepos")
                {
                    Query = "SELECT FormatDate(a.date,0) AS DATA, a.paymenttype AS data1, SUBSTRING(a.tendertype,8) AS data2, SUBSTRING(c.Account,LENGTH(c.Account)-3) AS data3, a.time AS data4, a.ticketnumber AS data5, a.refno AS data6, a.subtotal AS data7, b.tipsAdded AS data8 " +
                            "FROM paymenthistory AS a " +
                            "INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND FormatDate(a.date,0) = FormatDate(b.date,0) " +
                            "LEFT JOIN ccreceipts AS c ON a.uniqueid = c.PaymentID " +
                            "WHERE @myparam AND a.tenderType LIKE 'CREDIT%' " +
                            "GROUP BY a.ticketnumber, a.paymentType, a.time, a.subtotal, b.tipsadded";
                }
                else
                {
                    Query = "SELECT FormatDate(a.date,0) AS DATA, a.paymenttype AS data1, SUBSTRING(a.tendertype,8) AS data2, a.time AS data3, a.time AS data4, a.ticketnumber AS data5, a.refno AS data6, a.subtotal AS data7, b.tipsAdded AS data8 " +
                            "FROM paymenthistory AS a " +
                            "INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND FormatDate(a.date,0) = FormatDate(b.date,0) " +
                            "WHERE @myparam AND a.tenderType LIKE 'CREDIT%' " +
                            "GROUP BY a.ticketnumber, a.paymentType, a.time, a.subtotal, b.tipsadded";
                }

                Query = Query.Replace("@myparam", _ParamDate);
                
                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(Query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["data"].ToString(),
                                                        data.Rows[i]["data1"].ToString(),
                                                        data.Rows[i]["data2"].ToString(),
                                                        "",
                                                        Convert.ToDateTime(data.Rows[i]["data"]).ToString("MM/dd ") + data.Rows[i]["data4"].ToString(),
                                                        data.Rows[i]["data5"].ToString(),
                                                        data.Rows[i]["data6"].ToString(),
                                                        data.Rows[i]["data7"].ToString(),
                                                        data.Rows[i]["data8"].ToString(),
                                                        (Convert.ToDecimal(data.Rows[i]["data7"].ToString()) + Convert.ToDecimal(data.Rows[i]["data8"].ToString())).ToString()
                                                        ));

                            }
                            
                        }
                        else
                        { MessageBox.Show("No records retreived."); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CSales_OverShortByBusinessDay : CReport
    {
        public CSales_OverShortByBusinessDay(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT DateFormatconvert(a.Closed) AS data, a.cashcollected AS data1, a.cashsales AS data2, a.ccsales AS data3, a.overshort AS data4, a.closed AS data5, a.closed AS data6, a.closed AS data7, a.closed AS data8 " +
                    "FROM bankhistory AS a " +
                    "WHERE @myparam ";

        }

        /// <summary>
        /// The query should have 9 columns. Set the columns name as (data, data1, data2, ... , data8) and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field9 AS data8 FROM table WHERE @myparam.
        /// </summary>
        public string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "DateFormatConvert(a.closed) >= " + "@closed" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.closed) <= " + "@closed" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.closed) = " + "@closed" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@closed" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                Query = Query.Replace("@myparam", _ParamDate);

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                //MessageBox.Show(query);
                data = reportdata.ProcessReportData(Query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["data"].ToString().Substring(5) + "/" + data.Rows[i]["data"].ToString().Substring(0, 4),
                                                        data.Rows[i]["data1"].ToString(),
                                                        data.Rows[i]["data2"].ToString(),
                                                        "",
                                                        data.Rows[i]["data4"].ToString(),
                                                        Convert.ToDateTime(data.Rows[i]["data5"].ToString()).ToShortTimeString(),
                                                        "",
                                                        "",
                                                        ""
                                                        ));

                            }
                            
                        }
                        else
                        { MessageBox.Show("No records retreived."); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Data is empty.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CSales_SalesBySrvcType : CReport
    {
        public CSales_SalesBySrvcType(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT B.serviceTypeName AS data, COUNT(*) AS Quantity, SUM(A.SubTotal) AS data1, SUM(A.Tax) AS data2, SUM(A.DeliveryCharge) AS data3 " +
                    "FROM tickethistory AS A INNER JOIN servicetypes AS B ON A.serviceTypeID = B.id " +
                    "WHERE @myparam " +
                    "GROUP BY B.serviceTypeName";

        }

        /// <summary>
        /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3) and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam.
        /// </summary>
        public string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "FormatDate(a.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(a.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(a.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                string Query1 = Query;

                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query1 = Query1.Replace("@mytable", "tickets");
                else
                    Query1 = Query1.Replace("@mytable", "tickethistory");

                // param
                Query = Query.Replace("@myparam", _ParamDate);

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(Query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {                            
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["data"].ToString(),
                                                        data.Rows[i]["data1"].ToString(),
                                                        data.Rows[i]["data2"].ToString(),
                                                        data.Rows[i]["data3"].ToString(),
                                                        (Convert.ToDecimal(data.Rows[i]["data1"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["data2"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["data3"].ToString())).ToString()
                                                        ));

                            }
                            
                        }
                        else
                        { MessageBox.Show("No records retreived."); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CSales_SalesSummary : CReport
    {
        public CSales_SalesSummary(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            //Query = "SELECT B.serviceTypeName AS data, COUNT(*) AS Quantity, SUM(A.SubTotal) AS data1, SUM(A.Tax) AS data2, SUM(A.DeliveryCharge) AS data3 " +
            //        "FROM tickethistory AS A INNER JOIN servicetypes AS B ON A.serviceTypeID = B.id " +
            //        "WHERE @myparam " +
            //        "GROUP BY B.serviceTypeName";

        }

        /// <summary>
        /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3) and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam.
        /// </summary>
        //public string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> sfield1 = new List<string>();
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "FormatDate(date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    sfield1.Add("@closed" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data1 = new DataTable();
                DataTable data2 = new DataTable();
                //DataTable data3 = new DataTable();

                string Query1 = "SELECT SUM(payin) AS PayIn, SUM(ccsales) AS CCPayment, SUM(OverShort) AS OverShort, SUM(cctips) AS CCTips, SUM(payouts) AS PayOuts, SUM(expectedcash) AS Expected, SUM(totalcashcounted) AS Actual " +
                                "FROM bankhistory " +
                                "WHERE @myparam";
                Query1 = Query1.Replace("@myparam", _ParamDate.Replace("date","closed"));

                //string Query2 = "SELECT SUM(tenderamount) AS Total FROM paymenthistory WHERE tendertype='HOUSEACCOUNT'";
                string Query2 = "SELECT tenderamount, tendertype FROM paymenthistory WHERE @myparam";
                Query2 = Query2.Replace("@myparam", _ParamDate);

                //string Query3 = "SELECT SUM(tenderamount) AS Total FROM paymenthistory WHERE tendertype='GC'";
                //Query3 = Query3.Replace("@myparam", _ParamDate);

                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");

                data1 = reportdata.ProcessReportData(Query1, sfield1, svalue);
                data2 = reportdata.ProcessReportData(Query2, sfield, svalue);
                //data3 = reportdata.ProcessReportData(Query3, sfield, svalue);

                float fpayins = 0;
                float fccpay = 0;
                float fcctip = 0;
                float fpayout = 0;
                float fexp = 0;
                float fact = 0;
                float fhouse = 0;
                float fgc = 0;
                float fovrshrt = 0;

                if (data1 != null)
                {
                    fpayins = Helpers.NullToFlt(data1.Rows[0]["PayIn"]);
                    fccpay = Helpers.NullToFlt(data1.Rows[0]["CCPayment"]);
                    fcctip = Helpers.NullToFlt(data1.Rows[0]["CCTips"]);
                    fpayout = Helpers.NullToFlt(data1.Rows[0]["PayOuts"]);
                    fexp = Helpers.NullToFlt(data1.Rows[0]["Expected"]);
                    fact = Helpers.NullToFlt(data1.Rows[0]["Actual"]);
                    fovrshrt = Helpers.NullToFlt(data1.Rows[0]["OverShort"]);
                }

                if (data2 != null)
                {
                    fhouse = GetHouseAccount(data2);// 0;//(data2.Rows[0]["Total"] != null ? (float)Convert.ToDouble(data2.Rows[0]["Total"]) : 0);
                    fgc = GetGC(data2);// 0;//(data3.Rows[0]["Total"] != null ? (float)Convert.ToDouble(data3.Rows[0]["Total"]) : 0);
                }

                //if (data3 != null)
                //    fgc = GetGC(data2);// 0;//(data3.Rows[0]["Total"] != null ? (float)Convert.ToDouble(data3.Rows[0]["Total"]) : 0);

                list.Add(new ReportData("Total Sales", (fpayins + fccpay + fcctip + fpayout + fexp + fact + fhouse + fgc).ToString()));
                list.Add(new ReportData("Total Payins (+)", fpayins.ToString()));
                list.Add(new ReportData("Total Credit Card Payments (-)", fccpay.ToString()));
                list.Add(new ReportData("Total Credit Card Tips (-)", fcctip.ToString()));
                list.Add(new ReportData("Total House Account Charges (-)", fhouse.ToString()));
                list.Add(new ReportData("Total Gift Cards Payments (-)", fgc.ToString()));
                list.Add(new ReportData("Total Payouts (-)", fpayout.ToString()));
                list.Add(new ReportData("Total Driver Reimbursement (-)", "0"));
                list.Add(new ReportData("Adjusted Sales (Expected Cash/Cks)", fexp.ToString()));
                list.Add(new ReportData("Adjusted Sales (Actual Cash)", fact.ToString()));
                list.Add(new ReportData("Over / Short Amount", fovrshrt.ToString()));
                list.Add(new ReportData("Labor Percentage", "0"));

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }

        private float GetHouseAccount(DataTable DT)
        {
            float ftotal = 0;
            DataRow[] drarray = null;
            drarray = DT.Select("tendertype='GC'");
            for (int i = 0; i < drarray.Count(); i++)
            {
                ftotal += Helpers.NullToFlt(drarray[0]["tenderamount"]);
            }

            return ftotal;
        }

        private float GetGC(DataTable DT)
        {
            float ftotal = 0;
            DataRow[] drarray = null;
            drarray = DT.Select("tendertype='HOUSEACCOUNT'");
            for (int i = 0; i < drarray.Count(); i++)
            {
                ftotal += Helpers.NullToFlt(drarray[0]["tenderamount"]);
            }

            return ftotal;
        }

    }

    public class CSales_Voids : CReport
    {
        public CSales_Voids(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            //Query = "SELECT b.TicketNumber, FormatDate(a.date,0) AS data, a.voidreason AS data1, b.itemname AS data2, b.itemname AS data3, CONCAT(c.firstname, ' ', c.lastname) AS data4, b.ItemPrice AS data5 " +
            //        "FROM tickethistory AS a INNER JOIN ticketitemshistory AS b ON a.ticketnumber = b.ticketnumber " +
            //        "INNER JOIN employees AS c ON a.employeeID = c.id " +
            //        "WHERE @myparam AND a.status = 'VOIDED' " +
            //        "GROUP BY ticketnumber, itemname " +
            //        "ORDER BY a.date, a.ticketnumber, b.itemname";

            Query = "SELECT a.Time AS data, a.voidreason AS data1, a.TicketNumber AS data3, CONCAT(c.firstname, ' ', c.lastname) AS data4, a.SubTotal AS data5 " +
                    "FROM @mytable AS a INNER JOIN employees AS c ON a.employeeID = c.id " +
                    "WHERE @myparam AND a.status = 'VOIDED' " +
                    "ORDER BY a.date, a.ticketnumber";

        }

        /// <summary>
        /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3) and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam.
        /// </summary>
        public string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "FormatDate(a.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(a.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(a.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                string Query1 = Query;

                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query1 = Query1.Replace("@mytable", "tickets");
                else
                    Query1 = Query1.Replace("@mytable", "tickethistory");

                // param
                Query1 = Query1.Replace("@myparam", _ParamDate);

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(Query1, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {

                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["data"].ToString(),
                                                        data.Rows[i]["data1"].ToString(),
                                                        "",
                                                        data.Rows[i]["data3"].ToString(),
                                                        data.Rows[i]["data4"].ToString(),
                                                        data.Rows[i]["data5"].ToString()
                                                        ));

                            }
                            
                        }
                        else
                        { MessageBox.Show("No records retreived."); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }
}
