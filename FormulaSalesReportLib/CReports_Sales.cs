using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using System.Data;
using DevExpress.XtraPrinting.Preview;
using System.Diagnostics;

namespace FormulaReportsLib
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
            
        }
        
        private string Query { get; set; }

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
                            _ParamDate += "FormatDate(date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }
                
                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable datapay = new DataTable();
                DataTable dataticket = new DataTable();
                DataTable datacc = new DataTable();


                // PaymentHistory #########################
                Query = "SELECT FormatDate(date,0) AS date, ticketnumber, paymenttype, SUBSTRING(tendertype,8) AS tendertype, time, authCode, total " +
                        "FROM paymenthistory  " +
                        "WHERE @myparam AND tenderType LIKE 'CREDIT%' ";
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                datapay = reportdata.ProcessReportData(Query, sfield, svalue);


                // tickethistory #########################
                Query = "SELECT FormatDate(DATE,0) AS DATE, TicketNumber, tipsAdded " +
                        "FROM tickethistory  " +
                        "WHERE @myparam ";
                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query = Query.Replace("@mytable", "tickets");
                else
                    Query = Query.Replace("@mytable", "tickethistory");
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                dataticket = reportdata.ProcessReportData(Query, sfield, svalue);


                // ccreceipts #########################
                Query = "SELECT FormatDate(DATE,0) AS DATE, TicketNumber, SUBSTRING(Account,LENGTH(Account)-3) AS account " +
                        "FROM ccreceipts  " +
                        "WHERE @myparam ";
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                datacc = reportdata.ProcessReportData(Query, sfield, svalue);

                if (datapay != null)
                {
                    try
                    {
                        if (datapay.Rows.Count > 0)
                        {
                            for (int i = 0; i < datapay.Rows.Count; i++)
                            {
                                float tips = Gettips(dataticket, "date='" + datapay.Rows[i]["date"].ToString() + "' AND ticketnumber='" + datapay.Rows[i]["ticketnumber"].ToString() + "'");
                                list.Add(new ReportData(datapay.Rows[i]["date"].ToString(),
                                                        "",
                                                        datapay.Rows[i]["tendertype"].ToString(),
                                                        "***" + Getacct(datacc,"date='" + datapay.Rows[i]["date"].ToString() + "' AND ticketnumber='" + datapay.Rows[i]["ticketnumber"].ToString() + "'"),
                                                        Convert.ToDateTime(datapay.Rows[i]["date"]).ToString("MM/dd ") + datapay.Rows[i]["time"].ToString(),
                                                        datapay.Rows[i]["ticketnumber"].ToString(),
                                                        datapay.Rows[i]["authCode"].ToString(),
                                                        datapay.Rows[i]["total"].ToString(),
                                                        tips.ToString(),
                                                        (Convert.ToDecimal(datapay.Rows[i]["total"].ToString()) + Convert.ToDecimal(tips)).ToString()
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

        private float Gettips(DataTable DT, string param)
        {
            float ftotal = 0;
            DataRow[] drarray = null;
            drarray = DT.Select(param);
            for (int i = 0; i < drarray.Count(); i++)
            {
                ftotal += Helpers.NullToFlt(drarray[i]["tipsadded"]);
            }

            return ftotal;
        }

        private string Getacct(DataTable DT, string param)
        {
            if (DT == null) return "";

            DataRow[] drarray = null;
            drarray = DT.Select(param);

            if (drarray != null && drarray.Count() > 0)
                return drarray[0]["account"].ToString();
            else
                return "";
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

            Query = "SELECT DateFormatconvert(a.Closed) AS data, a.cashcollected AS data1, a.cashsales AS data2, a.ccsales AS data3, a.overshort AS data4, a.closed AS data5, a.owner AS data6, a.closed AS data7, a.closed AS data8 " +
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
                                                        data.Rows[i]["data6"].ToString(),
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
        private string Query { get; set; }

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
                DataTable data3 = new DataTable();
                DataTable data4 = new DataTable();

                Query = "SELECT SUM(payin) AS PayIn, SUM(ccsales) AS CCPayment, SUM(OverShort) AS OverShort, SUM(cctips) AS CCTips, SUM(payouts) AS PayOuts, SUM(expectedcash) AS Expected, SUM(totalcashcounted) AS Actual " +
                                "FROM bankhistory " +
                                "WHERE @myparam";
                Query = Query.Replace("@myparam", _ParamDate.Replace("date","closed"));
                data1 = reportdata.ProcessReportData(Query, sfield1, svalue);

                Query = "SELECT tenderamount, tendertype FROM paymenthistory WHERE @myparam";
                Query = Query.Replace("@myparam", _ParamDate);
                data2 = reportdata.ProcessReportData(Query, sfield, svalue);

                Query = "SELECT a.ID, CONCAT(a.LastName,', ', a.FirstName) AS empname, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.ClockIn) AS ClockIn, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.ClockOut) AS ClockOut, b.jobCodeName, b.PayRate, b.HoursWorked, b.TotalSales, c.payrate, c.paytype " +
                                "FROM employees AS a INNER JOIN employee_completedshifts AS b ON a.id = b.employeeid " +
                                "INNER JOIN employeejobs AS c ON b.EmployeeID = c.EmpID AND b.jobcodename = c.jobdescription " +
                                "WHERE @myparam " +
                                "ORDER BY a.LastName, a.FirstName, DATE";
                Query = Query.Replace("@myparam", _ParamDate.Replace("(date", "(b.date"));
                data3 = reportdata.ProcessReportData(Query, sfield, svalue);

                Query = "SELECT SUM(amountdriverkeeps) AS total FROM history_driversummary WHERE @myparam";
                Query = Query.Replace("@myparam", _ParamDate);
                data4 = reportdata.ProcessReportData(Query, sfield, svalue);

                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");


                float fpayins = 0;
                float fccpay = 0;
                float fcctip = 0;
                float fpayout = 0;
                float fexp = 0;
                float fact = 0;
                float fhouse = 0;
                float fgc = 0;
                float fovrshrt = 0;
                float flbrperc = 0;
                float freim = 0;

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
                    fhouse = GetHouseAccount(data2);
                    fgc = GetGC(data2);
                }

                if (data3 != null)
                    flbrperc = GetLaborPerc(data3) * 100;

                if (data4 != null)
                    freim = Helpers.NullToFlt(data4.Rows[0]["total"]);


                list.Add(new ReportData("Total Sales", (fpayins + fccpay + fcctip + fpayout + fexp + fact + fhouse + fgc).ToString()));
                list.Add(new ReportData("Total Payins (+)", fpayins.ToString()));
                list.Add(new ReportData("Total Credit Card Payments (-)", fccpay.ToString()));
                list.Add(new ReportData("Total Credit Card Tips (-)", fcctip.ToString()));
                list.Add(new ReportData("Total House Account Charges (-)", fhouse.ToString()));
                list.Add(new ReportData("Total Gift Cards Payments (-)", fgc.ToString()));
                list.Add(new ReportData("Total Payouts (-)", fpayout.ToString()));
                list.Add(new ReportData("Total Driver Reimbursement (-)", freim.ToString()));
                list.Add(new ReportData("Adjusted Sales (Expected Cash/Cks)", fexp.ToString()));
                list.Add(new ReportData("Adjusted Sales (Actual Cash)", fact.ToString()));
                list.Add(new ReportData("Over / Short Amount", fovrshrt.ToString()));
                list.Add(new ReportData("Labor Percentage", flbrperc.DecimalPlace() + "%"));

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
                ftotal += Helpers.NullToFlt(drarray[i]["tenderamount"]);
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
                ftotal += Helpers.NullToFlt(drarray[i]["tenderamount"]);
            }

            return ftotal;
        }

        private float GetLaborPerc(DataTable DT)
        {
            float totalhourly = 0;
            float totalsalary = 0;
            float totalsales = 0;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string hrs = Helpers.GetDurationInterval("00:00:00", DT.Rows[i]["HoursWorked"].ToString(), Helpers.eReturnTime.Hours);

                if (DT.Rows[i]["payrate"].ToString() == "Hourly")
                    totalhourly += (float)hrs.ToDecimal() * (float)DT.Rows[i]["payrate"].ToString().ToDecimal();
                else
                    totalsalary += (float)hrs.ToDecimal() * (float)DT.Rows[i]["payrate"].ToString().ToDecimal();

                totalsales += Helpers.NullToFlt(DT.Rows[i]["TotalSales"].ToString());
            }

            return (totalhourly + totalsalary) / totalsales;
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

    // #######################----------------------------------------#######################

    public class CAllSalesReport : CReport
    {
        public CAllSalesReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

        }
        
        private string Query { get; set; }

        DataTable data1 = new DataTable();
        DataTable data1DlvryStt = new DataTable();
        DataTable data2 = new DataTable();
        DataTable data3 = new DataTable();
        DataTable data4 = new DataTable();

        AllSalesReport AllSalesTotals = new AllSalesReport();

        public override List<ReportData> DataSourceToBind1()
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

                CReportData reportdata = new CReportData();

                // Net Sales By Service Type ############################
                Query = "SELECT B.serviceTypeName as ServiceTypeName, Count(*) as Quantity, Sum(A.SubTotal) as Amount, Sum(A.DeliveryCharge) as DeliveryCharge, Sum(A.Tax) as Tax, FormatDate(A.date,0) as mdate, B.isDelivery " +
                        "FROM @mytable A INNER JOIN servicetypes B on A.serviceTypeID = B.id " +
                        "WHERE @myparam " +
                        "GROUP BY B.serviceTypeName";
                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query = Query.Replace("@mytable", "tickets");
                else
                    Query = Query.Replace("@mytable", "tickethistory");
                // param
                Query = Query.Replace("@myparam", _ParamDate);                
                data1 = reportdata.ProcessReportData(Query, sfield, svalue);

                Query = "SELECT DeliveryFeeAmount, DriverReimbursementAmount FROM deliverysettings";
                data1DlvryStt = reportdata.ProcessReportData(Query, null, null);



                // Gross Sales Summary by Hours #########################
                Query = "SELECT B.Time, COUNT(B.TicketNumber) AS Orders, SUM(B.Total) AS Amount " +
                        "FROM (SELECT CONCAT(SUBSTRING(A.Time,1,LOCATE(':',A.Time)),'00',SUBSTRING(A.Time,LENGTH(A.Time)-2)) AS TIME, A.TicketNumber, A.Total, A.date " +
                        "FROM @mytable as A " +
                        "WHERE @myparam " + 
                        ") AS B " +
                        "GROUP BY B.Time";
                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query = Query.Replace("@mytable", "tickets");
                else
                    Query = Query.Replace("@mytable", "tickethistory");
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                data2 = reportdata.ProcessReportData(Query, sfield, svalue);


                // Net Sales By Category ################################
                Query = "SELECT B.name AS Category, COUNT(A.ItemName) AS Quantity, SUM(A.ItemPrice) AS Amount, a.date " +
                        "FROM @mytable A INNER JOIN menucategories B ON A.ItemCategory = B.id " +
                        "WHERE @myparam AND a.itemtype = 'Item' " +
                        "GROUP BY B.name";
                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query = Query.Replace("@mytable", "ticketitems");
                else
                    Query = Query.Replace("@mytable", "ticketitemshistory");
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                data3 = reportdata.ProcessReportData(Query, sfield, svalue);


                // Discount Summary ################################
                Query = "SELECT A.CouponName, COUNT(A.CouponName) AS Quantity, SUM(A.Amount) AS Amount, DateFormatConvert(A.date) AS mdate " +
                        "FROM @mytable AS a " +
                        "WHERE @myparam " +
                        "GROUP BY A.CouponName";
                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query = Query.Replace("@mytable", "used_coupons");
                else
                    Query = Query.Replace("@mytable", "used_couponshistory");
                // param
                Query = Query.Replace("@myparam", _ParamDate);
                data4 = reportdata.ProcessReportData(Query, sfield, svalue);

                List<ReportData> list1 = new List<ReportData>();
                if (data1.Rows.Count > 0)
                {
                    double fTotalQty = 0;
                    double fTotalAmt = 0;
                    double fDeliveryChrge = 0;
                    double fTax = 0;
                    double fTotalDelivery = 0;

                    for (int i = 0; i < data1.Rows.Count; i++)
                    {
                        fTotalQty += Convert.ToDouble(data1.Rows[i]["Quantity"]);
                        fTotalAmt += Convert.ToDouble(data1.Rows[i]["Amount"]) - Convert.ToDouble(data1.Rows[i]["DeliveryCharge"]);
                        fDeliveryChrge += Convert.ToDouble(data1.Rows[i]["DeliveryCharge"]);
                        fTax += Convert.ToDouble(data1.Rows[i]["Tax"]);
                    }

                    for (int i = 0; i < data1.Rows.Count; i++)
                    {
                        string perc = "";
                        if (fTotalQty > 0)
                            perc = Convert.ToDecimal((Convert.ToDouble(data1.Rows[i]["Quantity"]) / fTotalQty) * 100).DecimalPlace();
                        else
                            perc = "0";
                        string perc1 = "";
                        if (Convert.ToDouble(data1.Rows[i]["Quantity"]) > 0)
                            perc1 = Convert.ToDecimal((Convert.ToDouble(data1.Rows[i]["Amount"]) / Convert.ToDouble(data1.Rows[i]["Quantity"]))).DecimalPlace();
                        else
                            perc1 = "0";
                        if (data1.Rows[i]["isDelivery"].ToString() == "1")
                            fTotalDelivery = Convert.ToDouble(data1.Rows[i]["Quantity"]);

                        list1.Add(new ReportData(data1.Rows[i]["ServiceTypeName"].ToString(),
                                          Convert.ToDouble(data1.Rows[i]["Quantity"]).ToString(),
                                          (Convert.ToDouble(data1.Rows[i]["Amount"]) - Convert.ToDouble(data1.Rows[i]["DeliveryCharge"])).ToString(),
                                          perc + "%",
                                          perc1));

                    }

                    AllSalesTotals.rpt1TotalQty = fTotalQty;
                    AllSalesTotals.rpt1TotalAmt = fTotalAmt;
                    AllSalesTotals.rpt1Delivery = (Convert.ToDouble(data1DlvryStt.Rows[0]["DeliveryFeeAmount"]) * fTotalDelivery);
                    AllSalesTotals.rpt1Reimbursement = (Convert.ToDouble(data1DlvryStt.Rows[0]["DriverReimbursementAmount"]) * fTotalDelivery);
                    AllSalesTotals.rpt1NetSales = fTotalAmt + AllSalesTotals.rpt1Delivery - AllSalesTotals.rpt1Reimbursement;
                    AllSalesTotals.rpt1SalesTax = fTax;
                    AllSalesTotals.rpt1GrossQty = fTotalQty;
                    AllSalesTotals.rpt1GrossAmt = Convert.ToDouble(fTotalAmt) + Convert.ToDouble(fTax) + Convert.ToDouble(fDeliveryChrge);

                    return list1;
                }
            }
            catch (Exception ex)
            { Debug.Print(ex.Message); }
            return null;
        }

        public override List<ReportData> DataSourceToBind2()
        {
            try
            {
                double fTotalAmt = 0;
                string SBestHour = "";
                for (int i = 0; i < data2.Rows.Count; i++)
                {
                    fTotalAmt += Convert.ToDouble(data2.Rows[i]["Amount"]);
                }

                List<ReportData> list2 = new List<ReportData>();
                double tmpbest = 0;
                for (int i = 0; i < data2.Rows.Count; i++)
                {
                    string s = "";
                    double d = Convert.ToDouble(data2.Rows[i]["Amount"]);
                    for (int j = 0; j < Math.Floor(d / 10); j++)
                    { s += "$"; }

                    if (Math.Floor(d / 10) > tmpbest)
                    {
                        tmpbest = Math.Floor(d / 10);
                        SBestHour = data2.Rows[i]["Time"].ToString();
                    }
                    list2.Add(new ReportData(data2.Rows[i]["Time"].ToString(),
                                      Convert.ToDouble(data2.Rows[i]["Orders"]).ToString(),
                                      d.ToString(),
                                      s));

                }

                AllSalesTotals.rpt2TotalAmt = fTotalAmt;
                AllSalesTotals.rpt2BestHour = SBestHour;

                return list2;
            }
            catch (Exception ex)
            { Debug.Print(ex.Message); }
            return null;
        }

        public override List<ReportData> DataSourceToBind3()
        {
            try
            {
                double fTotalAmt = 0;
                double fTotalQty = 0;
                //double fDiscount = 0;
                //for (int i = 0; i < data3.Rows.Count; i++)
                //{
                //    fTotalAmt += Convert.ToDouble(data3.Rows[i]["Amount"]);
                //}

                List<ReportData> list3 = new List<ReportData>();
                for (int i = 0; i < data3.Rows.Count; i++)
                {
                    fTotalQty += Convert.ToDouble(data3.Rows[i]["Quantity"]);
                    fTotalAmt += Convert.ToDouble(data3.Rows[i]["Amount"]);
                    //fDeliveryChrge += Convert.ToDouble(data.Rows[i]["DeliveryCharge"]);
                    //fTax += Convert.ToDouble(data.Rows[i]["Tax"]);
                }

                for (int i = 0; i < data3.Rows.Count; i++)
                {
                    string perc = "";
                    if (fTotalQty > 0)
                        perc = Convert.ToDecimal((Convert.ToDouble(data3.Rows[i]["Quantity"]) / fTotalQty) * 100).DecimalPlace();
                    else
                        perc = "0";
                    list3.Add(new ReportData(data3.Rows[i]["Category"].ToString(),
                                      Convert.ToDouble(data3.Rows[i]["Quantity"]).ToString(),
                                      Convert.ToDouble(data3.Rows[i]["Amount"]).ToString(),
                                      perc + "%"));

                }

                AllSalesTotals.rpt3TotalQty = fTotalQty;
                AllSalesTotals.rpt3TotalAmt = fTotalAmt;
                AllSalesTotals.rpt3DiscountAmt = AllSalesTotals.rpt1DiscountAmt;
                AllSalesTotals.rpt3NetSales = fTotalAmt - AllSalesTotals.rpt1DiscountAmt;

                //AllSalesTotals.rpt1DiscountQty = fTotalQty;
                //AllSalesTotals.rpt1DiscountAmt = fTotalAmt;

                return list3;
            }
            catch (Exception ex)
            { Debug.Print(ex.Message); }
            return null;
        }

        public override List<ReportData> DataSourceToBind4()
        {
            try
            {
                double fTotalAmt = 0;
                double fTotalQty = 0;
                double fPercent = 0;
                for (int i = 0; i < data4.Rows.Count; i++)
                {
                    fTotalQty += Convert.ToDouble(data4.Rows[i]["Quantity"]);
                    fTotalAmt += Convert.ToDouble(data4.Rows[i]["Amount"]);
                    //fDeliveryChrge += Convert.ToDouble(data.Rows[i]["DeliveryCharge"]);
                    //fTax += Convert.ToDouble(data.Rows[i]["Tax"]);
                }

                List<ReportData> list4 = new List<ReportData>();
                for (int i = 0; i < data4.Rows.Count; i++)
                {
                    string perc = "";
                    if (AllSalesTotals.rpt1NetSales > 0)
                        perc = Convert.ToDecimal((Convert.ToDouble(data4.Rows[i]["Amount"]) / AllSalesTotals.rpt1NetSales) * 100).DecimalPlace(2,true);
                    else
                        perc = "0";
                    list4.Add(new ReportData(data4.Rows[i]["CouponName"].ToString(),
                                      Convert.ToDouble(data4.Rows[i]["Quantity"]).ToString(),
                                      Convert.ToDouble(data4.Rows[i]["Amount"]).ToString(),
                                      perc + "%"));

                }

                AllSalesTotals.rpt4TotalQty = fTotalQty;
                AllSalesTotals.rpt4TotalAmt = fTotalAmt;
                AllSalesTotals.rpt4TotalPercent = fPercent;

                AllSalesTotals.rpt3DiscountAmt = fTotalAmt;
                //AllSalesTotals.rpt3NetSales = fTotalAmt;

                AllSalesTotals.rpt1DiscountQty = fTotalQty;
                AllSalesTotals.rpt1DiscountAmt = fTotalAmt;

                reportAllSales.MyObject = AllSalesTotals;

                return list4;
            }
            catch (Exception ex)
            { Debug.Print(ex.Message); }
            return null;
        }
        
    }

}
