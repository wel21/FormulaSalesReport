using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraPrinting.Preview;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraReports.UI;

namespace FormulaSalesReportLib
{
    public class CHistory_CardPaymentsByType : CReport
    {
        public CHistory_CardPaymentsByType(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT SUBSTRING(a.tendertype,8) AS data, COUNT(a.subtotal) AS data1, SUM(a.subtotal) AS data2, SUM(b.tipsAdded) AS data3 " +
                    "FROM paymenthistory AS a INNER JOIN tickethistory AS b ON a.`ticketNumber` = b.`TicketNumber` " +
                    "INNER JOIN employees AS c ON a.employeeID = c.id " +
                    "WHERE @myparam AND a.tenderType LIKE 'CREDIT%' " +
                    "GROUP BY a.tendertype ";

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
                            _ParamDate += "DateFormatConvert(a.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                string Query1 = Query;

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
                                                        data.Rows[i]["data2"].ToString(),
                                                        data.Rows[i]["data3"].ToString(),
                                                        (Convert.ToDouble(data.Rows[i]["data2"].ToString()) + 
                                                         Convert.ToDouble(data.Rows[i]["data3"].ToString())).ToString()
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
                    MessageBox.Show("No records retreived.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CHistory_PaymentsBySrvcType : CReport
    {
        public CHistory_PaymentsBySrvcType(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT a.ticketNumber, SUBSTRING(a.tendertype,1,6) AS tendertype, c.serviceTypeName, a.subtotal " +
                    "FROM paymenthistory AS a INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND DateFormatConvert(a.date) = DateFormatConvert(b.date) " +
                    "INNER JOIN servicetypes AS c ON b.serviceTypeID = c.id " +
                    "WHERE @myparam ";

        }

        /// <summary>
        /// The query should have 5 columns. Set the columns name as (data, data1, data2, ... , data4), the table should be @mytable and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field6 AS data5 FROM table WHERE @myparam.
        /// </summary>
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
                            _ParamDate += "DateFormatConvert(a.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
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
                            OptionalColumnsToSomeReports = new List<ColumnHeader>();
                            //List<string> Col1RowVal = new List<string>();
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                // prepare columns
                                bool b1 = false;
                                foreach (ColumnHeader s in OptionalColumnsToSomeReports)
                                {
                                    if (s.Text == data.Rows[i]["serviceTypeName"].ToString())
                                    { b1 = true; break; }
                                }
                                if (b1 == false)
                                    OptionalColumnsToSomeReports.Add(new ColumnHeader(data.Rows[i]["serviceTypeName"].ToString()));

                                // prepare rows
                                bool b2 = false;
                                foreach (ReportData s in list)
                                {
                                    if (s.Data == data.Rows[i]["tendertype"].ToString())
                                    { b2 = true; break; }
                                }
                                if (b2 == false)
                                    list.Add(new ReportData(data.Rows[i]["tendertype"].ToString()));
                            }
                            OptionalColumnsToSomeReports.Add(new ColumnHeader("Total"));


                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                int icol = -1;
                                int irow = -1;
                                // get column index
                                for (int j = 0; j < OptionalColumnsToSomeReports.Count; j++)
                                {
                                    if (OptionalColumnsToSomeReports[j].Text == data.Rows[i]["serviceTypeName"].ToString())
                                    { icol = j; break; }
                                }
                                // get row index
                                for (int j = 0; j < list.Count; j++)
                                {
                                    if (list[j].Data == data.Rows[i]["tendertype"].ToString())
                                    { irow = j; break; }
                                }

                                // assign value
                                list[irow].InserByIndex(icol + 1, data.Rows[i]["subtotal"].ToString(), true);

                                // set total
                                list[irow].InserByIndex(OptionalColumnsToSomeReports.Count, data.Rows[i]["subtotal"].ToString(), true);
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
                    MessageBox.Show("No records retreived.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CHistory_SalesBySrvcType : CReport
    {
        public CHistory_SalesBySrvcType(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT B.serviceTypeName AS data, COUNT(*) AS data1, SUM(A.SubTotal) AS data2, SUM(A.Tax) AS data3, SUM(A.DeliveryCharge) AS data4, SUM(A.tipsAdded) AS data5 " +
                    "FROM @mytable AS A INNER JOIN servicetypes AS B ON A.serviceTypeID = B.id " +
                    "WHERE @myparam " +
                    "GROUP BY B.serviceTypeName";

        }

        /// <summary>
        /// The query should have 5 columns. Set the columns name as (data, data1, data2, ... , data4), the table should be @mytable and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field6 AS data5 FROM table WHERE @myparam.
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
                            _ParamDate += "DateFormatConvert(a.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
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
                                                        data.Rows[i]["data2"].ToString(),
                                                        data.Rows[i]["data3"].ToString(),
                                                        data.Rows[i]["data4"].ToString(),
                                                        data.Rows[i]["data5"].ToString(),
                                                        (Convert.ToDecimal(data.Rows[i]["data2"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["data3"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["data4"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["data5"].ToString())).ToString()
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
                    MessageBox.Show("No records retreived.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }

    public class CHistory_Voids : CReport
    {
        public CHistory_Voids(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            Query = "SELECT DateFormatconvert(a.date) AS data, a.voidreason AS data1, a.TicketNumber AS data3, CONCAT(c.firstname, ' ', c.lastname) AS data4, a.SubTotal AS data5, a.Time AS data6 " +
                    "FROM @mytable AS a INNER JOIN employees AS c ON a.employeeID = c.id " +
                    "WHERE @myparam AND a.status = 'VOIDED' " +
                    "ORDER BY a.date, a.ticketnumber";

        }

        /// <summary>
        /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3, ... , data6), the table should be @mytable and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3, ... , field7 AS data6 FROM @mytable WHERE @myparam.
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
                            _ParamDate += "DateFormatConvert(a.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
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
                                list.Add(new ReportData(data.Rows[i]["data"].ToString().Substring(5) + "/" + data.Rows[i]["data"].ToString().Substring(0, 4),
                                                        data.Rows[i]["data1"].ToString(),
                                                        "",
                                                        data.Rows[i]["data3"].ToString(),
                                                        data.Rows[i]["data4"].ToString(),
                                                        data.Rows[i]["data5"].ToString(),
                                                        data.Rows[i]["data6"].ToString()
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

    public class CHistory_SalesUnitQty : CReport
    {
        public CHistory_SalesUnitQty(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            ReportHelper.MyActiveReport = this;

            Query = "SELECT a.ticketNumber, SUBSTRING(a.tendertype,1,6) AS tendertype, c.serviceTypeName, a.subtotal " +
                    "FROM paymenthistory AS a INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber AND DateFormatConvert(a.date) = DateFormatConvert(b.date) " +
                    "INNER JOIN servicetypes AS c ON b.serviceTypeID = c.id " +
                    "WHERE @myparam ";

        }

        /// <summary>
        /// The query should have 5 columns. Set the columns name as (data, data1, data2, ... , data4), the table should be @mytable and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field6 AS data5 FROM table WHERE @myparam.
        /// </summary>
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
                            _ParamDate += "DateFormatConvert(a.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(a.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(a.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
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
                            OptionalColumnsToSomeReports = new List<ColumnHeader>();
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                // prepare columns
                                bool b1 = false;
                                foreach (ColumnHeader s in OptionalColumnsToSomeReports)
                                {
                                    if (s.Text == data.Rows[i]["serviceTypeName"].ToString())
                                    { b1 = true; break; }
                                }
                                if (b1 == false)
                                    OptionalColumnsToSomeReports.Add(new ColumnHeader(data.Rows[i]["serviceTypeName"].ToString()));

                                // prepare rows
                                bool b2 = false;
                                foreach (ReportData s in list)
                                {
                                    if (s.Data == data.Rows[i]["tendertype"].ToString())
                                    { b2 = true; break; }
                                }
                                if (b2 == false)
                                    list.Add(new ReportData(data.Rows[i]["tendertype"].ToString()));
                            }
                            //add total column
                            OptionalColumnsToSomeReports.Add(new ColumnHeader("Total"));


                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                int icol = -1;
                                int irow = -1;
                                // get column index
                                for (int j = 0; j < OptionalColumnsToSomeReports.Count; j++)
                                {
                                    if (OptionalColumnsToSomeReports[j].Text == data.Rows[i]["serviceTypeName"].ToString())
                                    { icol = j; break; }
                                }
                                // get row index
                                for (int j = 0; j < list.Count; j++)
                                {
                                    if (list[j].Data == data.Rows[i]["tendertype"].ToString())
                                    { irow = j; break; }
                                }

                                // assign value
                                list[irow].InserByIndex((icol + 1) * 2 - 1, 1.ToString(), true);
                                list[irow].InserByIndex((icol + 1) * 2, data.Rows[i]["subtotal"].ToString(), true);

                                // set the total
                                list[irow].InserByIndex(OptionalColumnsToSomeReports.Count * 2 - 1, 1.ToString(), true);
                                list[irow].InserByIndex(OptionalColumnsToSomeReports.Count * 2 , data.Rows[i]["subtotal"].ToString(), true);
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
                    MessageBox.Show("No records retreived.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
    }
    
    public class CHistory_SalesByDay : CReport
    {
        public CHistory_SalesByDay(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            //Query = "SELECT DateFormatconvert(a.date) AS data, a.voidreason AS data1, a.TicketNumber AS data3, CONCAT(c.firstname, ' ', c.lastname) AS data4, a.SubTotal AS data5, a.Time AS data6 " +
            //        "FROM @mytable AS a INNER JOIN employees AS c ON a.employeeID = c.id " +
            //        "WHERE @myparam AND a.status = 'VOIDED' " +
            //        "ORDER BY a.date, a.ticketnumber";

        }

        /// <summary>
        /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3, ... , data6), the table should be @mytable and the date parameter as @myparam.
        /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3, ... , field7 AS data6 FROM @mytable WHERE @myparam.
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
                            _ParamDate += "DateFormatConvert(date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    sfield1.Add("@closed" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();
                DataTable databankhistory = new DataTable();
                DataTable dataDelChrge = new DataTable();
                DataTable datavoid = new DataTable();

                Query = "SELECT DateFormatconvert(date) AS date, SUM(subtotal) AS subtotal, SUM(tax) AS tax " +
                        "FROM paymenthistory " +
                        "WHERE @myparam " +
                        "GROUP BY DateFormatconvert(date)";                
                Query = Query.Replace("@myparam", _ParamDate);
                data = reportdata.ProcessReportData(Query, sfield, svalue);

                Query = "SELECT DateFormatconvert(closed) AS DATE, SUM(CCSales) AS ccsales, SUM(tips) AS tips, SUM(payouts) AS payout, SUM(payin) AS payin " +
                        "FROM bankhistory " +
                        "WHERE @myparam " +
                        "GROUP BY DateFormatconvert(closed)";
                Query = Query.Replace("@myparam", _ParamDate.Replace("date", "closed"));
                databankhistory = reportdata.ProcessReportData(Query, sfield1, svalue);
                
                Query = "SELECT DateFormatconvert(DATE) AS DATE, SUM(deliverycharge) AS deliverycharge " +
                        "FROM tickethistory " +
                        "WHERE @myparam " +
                        "GROUP BY DateFormatconvert(DATE)";
                Query = Query.Replace("@myparam", _ParamDate);
                dataDelChrge = reportdata.ProcessReportData(Query, sfield, svalue);

                Query = "SELECT DateFormatconvert(DATE) AS DATE, COUNT(ticketnumber) AS VoidCount, SUM(subtotal) AS VoidAmount " +
                        "FROM tickethistory " +
                        "WHERE @myparam AND status = 'VOIDED' " +
                        "GROUP BY DateFormatconvert(DATE)";
                Query = Query.Replace("@myparam", _ParamDate);
                datavoid = reportdata.ProcessReportData(Query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {

                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                string delchrge = Helpers.LookUpDataTable(dataDelChrge, "deliverycharge", "DATE='" + data.Rows[i]["DATE"].ToString() + "'");
                                List<string> bankhistory = Helpers.LookUpDataTable(databankhistory, new List<string>() { "ccsales","tips","payout","payin" }, "DATE='" + data.Rows[i]["DATE"].ToString() + "'");
                                List<string> voids = Helpers.LookUpDataTable(datavoid, new List<string>() { "VoidCount", "VoidAmount" }, "DATE='" + data.Rows[i]["DATE"].ToString() + "'");

                                list.Add(new ReportData(data.Rows[i]["DATE"].ToString(),
                                                        (Helpers.NullToFlt(data.Rows[i]["subtotal"]) + Helpers.NullToFlt(data.Rows[i]["tax"]) + Helpers.NullToFlt(delchrge)).ToString(),
                                                        data.Rows[i]["subtotal"].ToString(),
                                                        data.Rows[i]["tax"].ToString(),
                                                        delchrge,
                                                        "0",
                                                        bankhistory[0],
                                                        bankhistory[1],
                                                        bankhistory[2],
                                                        bankhistory[3],
                                                        voids[0],
                                                        voids[1],
                                                        "0"
                                                        ));

                                //list.Add(new ReportData(data.Rows[i]["data"].ToString(),
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0",
                                //                        "0"
                                //                        ));

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
