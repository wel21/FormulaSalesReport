﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraPrinting.Preview;

namespace FormulaReportsLib
{

    #region commented
    //public class CEmployee_ActivityLog : CReport
    //{
    //    public CEmployee_ActivityLog(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
    //        : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
    //    {
    //        this.DV = DV;
    //        this.StoreData = StoreData;
    //        this.ParamDate = ParamDate;
    //        this.report = ReportInstance;
    //        this.MyType = ReportType;

    //    }

    //    /// <summary>
    //    /// The query should have 4 columns. Set the columns name as (data, data1, data2, data3) and the date parameter as @myparam.
    //    /// ex: SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam.
    //    /// </summary>
    //    private string Query { get; set; }
    //    private string Query2 { get; set; }

    //    /*
    //    public override List<ReportData> DataSourceToBind()
    //    {
    //        try
    //        {
    //            List<string> sfield = new List<string>();
    //            List<string> svalue = new List<string>();

    //            // parameters
    //            string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
    //            //string _ParamDate = "WHERE ";
    //            for (int i = 0; i < ParamDate.Count; i++)
    //            {
    //                if (ParamDate.Count == 2)
    //                {
    //                    if (i == 0)
    //                        _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
    //                    else
    //                        _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
    //                }
    //                else
    //                {
    //                    _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
    //                }

    //                sfield.Add("@date" + i.ToString());
    //                svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
    //            }

    //            string Query1 = Query;

    //            // table
    //            if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
    //                Query1 = Query1.Replace("@mytable", "tickets");
    //            else
    //                Query1 = Query1.Replace("@mytable", "tickethistory");

    //            // param
    //            Query1 = Query1.Replace("@myparam", _ParamDate);

    //            List<ReportData> list = new List<ReportData>();
    //            CReportData reportdata = new CReportData();
    //            DataTable data = new DataTable();

    //            data = reportdata.ProcessReportData(Query1, sfield, svalue);

    //            if (data != null)
    //            {
    //                try
    //                {
    //                    if (data.Rows.Count > 0)
    //                    {

    //                        for (int i = 0; i < data.Rows.Count; i++)
    //                        {
    //                            list.Add(new ReportData(data.Rows[i]["data"].ToString(),
    //                                                    data.Rows[i]["data1"].ToString(),
    //                                                    data.Rows[i]["data2"].ToString(),
    //                                                    data.Rows[i]["data3"].ToString()
    //                                                    ));

    //                        }

    //                    }
    //                    else
    //                    { MessageBox.Show("No records retreived."); }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Console.WriteLine(ex.Message);
    //                    MessageBox.Show(ex.Message);
    //                }
    //            }

    //            return list;

    //        }
    //        catch (Exception ex)
    //        { MessageBox.Show(ex.Message); }
    //        return null;
    //    }
    //    */


    //    public override List<ReportData> DataSourceToBind()
    //    {
    //        try
    //        {
    //            List<string> sfield = new List<string>();
    //            List<string> svalue = new List<string>();

    //            // parameters
    //            string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
    //            //string _ParamDate = "WHERE ";
    //            for (int i = 0; i < ParamDate.Count; i++)
    //            {
    //                if (ParamDate.Count == 2)
    //                {
    //                    if (i == 0)
    //                        _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
    //                    else
    //                        _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
    //                }
    //                else
    //                {
    //                    _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
    //                }

    //                sfield.Add("@date" + i.ToString());
    //                svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
    //            }

    //            Query = "SELECT a.ID AS data, CONCAT(a.LastName,', ', a.FirstName) AS data1 " +
    //                    "FROM employees AS a ORDER BY a.LastName, a.FirstName";

    //            //Query = "SELECT a.ID AS DATA, CONCAT(a.LastName,', ', a.FirstName) AS data1, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.time) AS data2, CONCAT(b.action, ' - ', b.jobtype) AS data3 " +
    //            //        "FROM employees AS a INNER JOIN employeetimeclockhistory AS b ON a.id = b.empid " +
    //            //        "WHERE @myparam " +
    //            //        "ORDER BY a.LastName, a.FirstName, date, time";

    //            string Query1 = Query;

    //            // table
    //            if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
    //                Query1 = Query1.Replace("@mytable", "tickets");
    //            else
    //                Query1 = Query1.Replace("@mytable", "tickethistory");

    //            // param
    //            Query1 = Query1.Replace("@myparam", _ParamDate);

    //            List<ReportData> list = new List<ReportData>();
    //            CReportData reportdata = new CReportData();
    //            DataTable data = new DataTable();

    //            data = reportdata.ProcessReportData(Query1, sfield, svalue);

    //            if (data != null)
    //            {
    //                try
    //                {
    //                    if (data.Rows.Count > 0)
    //                    {

    //                        for (int i = 0; i < data.Rows.Count; i++)
    //                        {
    //                            list.Add(new ReportData(data.Rows[i]["data"].ToString(),
    //                                                    data.Rows[i]["data1"].ToString()
    //                                                    ));

    //                        }

    //                    }
    //                    else
    //                    { MessageBox.Show("No records retreived."); }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Console.WriteLine(ex.Message);
    //                    MessageBox.Show(ex.Message);
    //                }
    //            }

    //            return list;

    //        }
    //        catch (Exception ex)
    //        { MessageBox.Show(ex.Message); }
    //        return null;
    //    }


    //    public override SqlDataSource DataSourceToBindDS()
    //    {
    //        try
    //        {
    //            //List<string> sfield = new List<string>();
    //            //List<string> svalue = new List<string>();

    //            // parameters
    //            string _ParamDate = "";
    //            for (int i = 0; i < ParamDate.Count; i++)
    //            {
    //                if (ParamDate.Count == 2)
    //                {
    //                    if (i == 0)
    //                        _ParamDate += "FormatDate(b.date,0) >= '" + Helpers.ConvertMyDate(ParamDate[i].date) + "' AND ";
    //                    else
    //                        _ParamDate += "FormatDate(b.date,0) <= '" + Helpers.ConvertMyDate(ParamDate[i].date) + "' ";
    //                }
    //                else
    //                {
    //                    _ParamDate += "FormatDate(b.date,0) = '" + Helpers.ConvertMyDate(ParamDate[i].date) + "' " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
    //                }

    //                //sfield.Add("@date" + i.ToString());
    //                //svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
    //            }

    //            //Query = "SELECT b.empid AS data, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.time) AS data2, CONCAT(b.action, ' - ', b.jobtype) AS data3 " +
    //            //        "FROM employeetimeclockhistory AS b " +
    //            //        "WHERE @myparam " +
    //            //        "ORDER BY b.date, b.time";

    //            //string Query1 = Query;

    //            //// table
    //            //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
    //            //    Query1 = Query1.Replace("@mytable", "tickets");
    //            //else
    //            //    Query1 = Query1.Replace("@mytable", "tickethistory");

    //            //// param
    //            //Query1 = Query1.Replace("@myparam", _ParamDate);



    //            MySqlConnectionParameters connectionParameters = new MySqlConnectionParameters(DatabaseConnectionSettings.Server,
    //                                                                                           DatabaseConnectionSettings.Database,
    //                                                                                           DatabaseConnectionSettings.User,
    //                                                                                           DatabaseConnectionSettings.Password,
    //                                                                                           DatabaseConnectionSettings.Port);
    //            SqlDataSource ds = new SqlDataSource(connectionParameters);
    //            DevExpress.DataAccess.Sql.CustomSqlQuery query;

    //            query = new CustomSqlQuery();
    //            query.Name = "emp";
    //            query.Sql = "SELECT a.ID AS data, CONCAT(a.LastName,', ', a.FirstName) AS data1 " +
    //                        "FROM employees AS a ORDER BY a.LastName, a.FirstName";
    //            ds.Queries.Add(query);
    //            DataMember1 = "emp";

    //            query = new CustomSqlQuery();
    //            query.Name = "act";
    //            query.Sql = "SELECT b.empid AS data, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.time) AS data2, CONCAT(b.action, ' - ', b.jobtype) AS data3 " +
    //                        "FROM employeetimeclockhistory AS b " +
    //                        "WHERE " + _ParamDate + " ORDER BY b.date, b.time";
    //            ds.Queries.Add(query);
    //            DataMember2 = DataMember1 + "." + DataMember1 + "act";

    //            ds.Relations.Add("emp", "act", "data", "data");


    //            return ds;

    //        }
    //        catch (Exception ex)
    //        { MessageBox.Show(ex.Message); }
    //        return null;
    //    }
    //}
    #endregion

    public class CEmployee_ActivityLog : CReport
    {
        public CEmployee_ActivityLog(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;
            
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
                            _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                Query = "SELECT a.ID AS DATA, CONCAT(a.LastName,', ', a.FirstName) AS data1, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.targettime) AS data2, CONCAT(b.action, ' - ', b.jobtype) AS data3 " +
                        "FROM employees AS a INNER JOIN employeetimeclock AS b ON a.id = b.empid " +
                        "WHERE @myparam " +
                        "ORDER BY a.LastName, a.FirstName, date, time";

                string Query1 = Query;

                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");

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
                                                        data.Rows[i]["data3"].ToString()
                                                        ));

                            }

                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    #region commented
    /*
    public class CEmployee_LaborReport_old : CReport
    {
        public CEmployee_LaborReport_old(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

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
                List<string> svalue = new List<string>();

                // parameters
                string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                Query = "SELECT a.ID, CONCAT(a.LastName,', ', a.FirstName) AS empname, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.time) AS date_time, b.action, b.jobtype, b.time, b.punchpairid " +
                        "FROM employees AS a INNER JOIN employeetimeclock AS b ON a.id = b.empid " +
                        "WHERE @myparam AND b.punchpairid IS NOT NULL " +
                        "ORDER BY a.LastName, a.FirstName, b.punchpairid, b.action, DATE, TIME";

                string Query1 = Query;

                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");

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
                                if (data.Rows[i]["ID"].ToString() == "26")
                                    Console.Write("");

                                if (list.Count != 0)
                                {
                                    int irow = -1;

                                    //locate irow
                                    for (int j = 0; j < list.Count; j++)
                                    {
                                        if (list[j].Data7 == data.Rows[i]["punchpairid"].ToString())
                                        {
                                            irow = j;
                                            break;
                                        }
                                    }

                                    if (irow > -1)
                                    {
                                        if (data.Rows[i]["action"].ToString() == "Clock-In")
                                        {
                                            if (list[irow].Data3 != "")
                                            {
                                                //add new item instead because Clock-In is already filled out
                                                list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                                            data.Rows[i]["empname"].ToString(),
                                                            data.Rows[i]["jobtype"].ToString(),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["date_time"].ToString() : ""),
                                                            (data.Rows[i]["action"].ToString() == "Clock-Out" ? data.Rows[i]["date_time"].ToString() : ""),
                                                            "0",
                                                            "",
                                                            data.Rows[i]["punchpairid"].ToString(),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : ""),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : "")
                                                            ));
                                            }
                                            else
                                            {
                                                list[irow].Data3 = data.Rows[i]["date_time"].ToString();
                                                list[irow].Data8 = data.Rows[i]["time"].ToString();

                                                if (list[irow].Data9 != null || list[irow].Data9 != "")
                                                {
                                                    list[irow].Data5 = Helpers.GetDurationInterval(list[irow].Data8, list[irow].Data9, Helpers.eReturnTime.Hours);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (list[irow].Data4 != "")
                                            {
                                                //locate irow excluding the current irow value
                                                for (int j = 0; j < list.Count; j++)
                                                {
                                                    if (list[j].Data7 == data.Rows[i]["punchpairid"].ToString() && j != irow)
                                                    {
                                                        irow = j;
                                                        break;
                                                    }
                                                }
                                                if (irow < 0)
                                                {
                                                    //add new item instead because Clock-In is already filled out
                                                    list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                                                data.Rows[i]["empname"].ToString(),
                                                                data.Rows[i]["jobtype"].ToString(),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["date_time"].ToString() : ""),
                                                                (data.Rows[i]["action"].ToString() == "Clock-Out" ? data.Rows[i]["date_time"].ToString() : ""),
                                                                "0",
                                                                "",
                                                                data.Rows[i]["punchpairid"].ToString(),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : ""),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : "")
                                                                ));
                                                }
                                                else
                                                {
                                                    list[irow].Data4 = data.Rows[i]["date_time"].ToString();
                                                    list[irow].Data9 = data.Rows[i]["time"].ToString();

                                                    if (list[irow].Data9 != null || list[irow].Data9 != "")
                                                    {
                                                        list[irow].Data5 = Helpers.GetDurationInterval(list[irow].Data8, list[irow].Data9, Helpers.eReturnTime.Hours);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                list[irow].Data4 = data.Rows[i]["date_time"].ToString();
                                                list[irow].Data9 = data.Rows[i]["time"].ToString();

                                                if (list[irow].Data9 != null || list[irow].Data9 != "")
                                                {
                                                    list[irow].Data5 = Helpers.GetDurationInterval(list[irow].Data8, list[irow].Data9, Helpers.eReturnTime.Hours);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                                                data.Rows[i]["empname"].ToString(),
                                                                data.Rows[i]["jobtype"].ToString(),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["date_time"].ToString() : ""),
                                                                (data.Rows[i]["action"].ToString() == "Clock-Out" ? data.Rows[i]["date_time"].ToString() : ""),
                                                                "0",
                                                                "",
                                                                data.Rows[i]["punchpairid"].ToString(),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : ""),
                                                                (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : "")
                                                                ));
                                    }
                                }
                                else
                                {
                                    list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                                            data.Rows[i]["empname"].ToString(),
                                                            data.Rows[i]["jobtype"].ToString(),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["date_time"].ToString() : ""),
                                                            (data.Rows[i]["action"].ToString() == "Clock-Out" ? data.Rows[i]["date_time"].ToString() : ""),
                                                            "0",
                                                            "",
                                                            data.Rows[i]["punchpairid"].ToString(),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : ""),
                                                            (data.Rows[i]["action"].ToString() == "Clock-In" ? data.Rows[i]["time"].ToString() : "")
                                                            ));
                                }
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
    */
    #endregion

    public class CEmployee_LaborReport : CReport
    {
        public CEmployee_LaborReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

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
                            _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                Query = "SELECT a.ID, CONCAT(a.LastName,', ', a.FirstName) AS empname, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.ClockIn) AS ClockIn, CONCAT(FormatDate(b.date,0), CAST(' ' AS CHAR CHARACTER SET utf8), b.ClockOut) AS ClockOut, b.jobCodeName, c.PayRate, b.HoursWorked, b.TotalSales, c.payrate, c.paytype " +
                        "FROM employees AS a INNER JOIN employee_completedshifts AS b ON a.id = b.employeeid " +
                        "INNER JOIN employeejobs AS c ON b.EmployeeID = c.EmpID AND b.jobcodename = c.jobdescription " +
                        "WHERE @myparam " +
                        "ORDER BY a.LastName, a.FirstName, DATE";

                string Query1 = Query;

                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");

                // param
                Query1 = Query1.Replace("@myparam", _ParamDate);

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();
                DataTable dataTotal = new DataTable();

                data = reportdata.ProcessReportData(Query1, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            dataTotal.Columns.Add(new DataColumn("Val"));

                            float totalhourly = 0;
                            float totalsalary = 0;
                            float totalsales = 0;
                            float totalhours = 0;

                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                string hrs = Helpers.GetDurationInterval("00:00:00", data.Rows[i]["HoursWorked"].ToString(), Helpers.eReturnTime.Hours);
                                list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                                        data.Rows[i]["empname"].ToString(),
                                                        data.Rows[i]["jobCodeName"].ToString(),
                                                        data.Rows[i]["ClockIn"].ToString(),
                                                        data.Rows[i]["ClockOut"].ToString(),
                                                        hrs
                                                        ));

                                if (data.Rows[i]["payrate"].ToString() == "Hourly")
                                    totalhourly += (float)hrs.ToDecimal() * (float)data.Rows[i]["payrate"].ToString().ToDecimal();
                                else
                                    totalsalary += (float)hrs.ToDecimal() * (float)data.Rows[i]["payrate"].ToString().ToDecimal();

                                totalsales += Helpers.NullToFlt(data.Rows[i]["TotalSales"].ToString());
                                totalhours += (float)hrs.ToDecimal();
                            }

                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());

                            dataTotal.Rows[0][0] = totalhourly.ToString();
                            dataTotal.Rows[1][0] = totalsalary.ToString();
                            dataTotal.Rows[2][0] = (totalhourly + totalsalary).ToString();
                            dataTotal.Rows[3][0] = (((totalhourly + totalsalary) / totalsales) * 100).DecimalPlace() + "%";
                            dataTotal.Rows[4][0] = totalsales.ToString();
                            dataTotal.Rows[5][0] = totalhours.ToString();

                            report.MyObject = dataTotal;
                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public class CEmployee_PayrollReport : CReport
    {
        public CEmployee_PayrollReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

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
                            _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                Query = "SELECT a.empid, a.employee, b.jobtype, b.date, b.time timein, c.time timeout, a.payrate, c.declaredtips " +
                        "FROM employeejobs a " +
                        "  LEFT JOIN employeetimeclock b ON a.empid = b.empid  " +
                        "  LEFT JOIN employeetimeclock c ON b.empid = c.empid " +
                        "WHERE b.action = 'Clock-In' AND c.action = 'Clock-Out' AND b.punchpairid = c.punchpairid AND @myparam " +
                        "ORDER BY a.employee";
                
                string Query1 = Query;
                
                // param
                Query1 = Query1.Replace("@myparam", _ParamDate);

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();
                DataTable dataTotal = new DataTable();

                data = reportdata.ProcessReportData(Query1, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            dataTotal.Columns.Add(new DataColumn("Val"));
                            
                            float total2Hours = 0;
                            float total2Labor = 0;

                            string _empid = "";
                            double OTHours = 0;
                            double OTLabor = 0;
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                if (_empid == "" || _empid != data.Rows[i]["empid"].ToString())
                                {
                                    GetOT(data.Rows[i]["empid"].ToString(), out OTHours, out OTLabor);
                                    //OTHours = 4;
                                    //OTLabor = 400;
                                }
                                else
                                {
                                    OTHours = 0;
                                    OTLabor = 0;
                                }
                                _empid = data.Rows[i]["empid"].ToString();

                                TimeSpan ts = Convert.ToDateTime(data.Rows[i]["timeout"].ToString()) - Convert.ToDateTime(data.Rows[i]["timein"].ToString());
                                string hrs = Helpers.GetDurationInterval("00:00:00", ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds, Helpers.eReturnTime.Hours);
                                float lbr = (float)(ts.TotalHours * Helpers.NullToFlt(data.Rows[i]["payrate"].ToString()));

                                list.Add(new ReportData(data.Rows[i]["employee"].ToString(),
                                                        data.Rows[i]["jobtype"].ToString(),
                                                        data.Rows[i]["date"].ToString(),
                                                        data.Rows[i]["timein"].ToString(),
                                                        data.Rows[i]["timeout"].ToString(),
                                                        ts.TotalHours.ToString("#0.00"),
                                                        OTHours.ToString(),
                                                        lbr.ToString("#0.00"),
                                                        OTLabor.ToString(),
                                                        data.Rows[i]["declaredtips"].ToString()
                                                        ));
                                
                                total2Hours += (float)hrs.ToDecimal();
                                total2Labor += lbr;
                            }

                            dataTotal.Rows.Add(dataTotal.NewRow());
                            dataTotal.Rows.Add(dataTotal.NewRow());

                            dataTotal.Rows[0][0] = total2Hours.ToString("#0.00");
                            dataTotal.Rows[1][0] = total2Labor.ToString("#0.00");

                            report.MyObject = dataTotal;
                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
            return null;
        }

        private void GetOT(string EmpID, out double OTHours, out double OTLabor)
        {
            string Query = "";
            CReportData reportdata = new CReportData();
            DataTable payrollSett = new DataTable();

            Query = "SELECT StartingDay, Cycle, OvertimeType, OvertimeHours FROM payreollsettings";
            payrollSett = reportdata.ProcessReportData(Query, null, null);
            
            float totalOTHours = 0;
            float totalOTLabor = 0;

            int dayPos = -1;
            List<string> days = new List<string>();
            days.Add("SUNDAY");     // 0
            days.Add("MONDAY");     // 1
            days.Add("TUESDAY");    // 2
            days.Add("WEDNESDAY");  // 3
            days.Add("THURSDAY");   // 4
            days.Add("FRIDAY");     // 5
            days.Add("SATURDAY");   // 6

            // arrange days order based on starting date
            dayPos = days.IndexOf(payrollSett.Rows[0]["StartingDay"].ToString().ToUpper());
            if (dayPos > 0)
            {
                for (int i = 0; i < dayPos; i++)
                {
                    string s = days[i];
                    days.RemoveAt(i);
                    days.Add(s);
                }
            }

            dayPos = days.IndexOf(ParamDate[0].date.ToString("dddd").ToUpper());
            DateTime _firstDate = ParamDate[0].date.AddDays(-dayPos);
            DateTime _currentDate = ParamDate[0].date.AddDays(-dayPos);
            DateTime _lastDate = ParamDate[ParamDate.Count - 1].date.AddDays(6 - dayPos);

            // generate whole week per week
            while (_currentDate < _lastDate)
            //for (int j = 0; j < ParamDate.Count; j++)
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();

                //dayPos = days.IndexOf(ParamDate[j].date.ToString("dddd").ToUpper());

                // parameters
                string _ParamDate = "";
                _ParamDate += "FormatDate(b.date,0) >= " + "@date0 AND ";
                sfield.Add("@date0");
                svalue.Add(Helpers.ConvertMyDate(_currentDate));
                _ParamDate += "FormatDate(b.date,0) <= " + "@date1 ";
                sfield.Add("@date1");
                svalue.Add(Helpers.ConvertMyDate(_currentDate.AddDays(6)));
                

                Query = "SELECT a.employee, b.jobtype, b.date, b.time timein, c.time timeout, a.payrate, c.declaredtips " +
                        "FROM employeejobs a " +
                        "  LEFT JOIN employeetimeclock b ON a.empid = b.empid  " +
                        "  LEFT JOIN employeetimeclock c ON b.empid = c.empid " +
                        "WHERE b.action = 'Clock-In' AND c.action = 'Clock-Out' AND b.punchpairid = c.punchpairid AND a.empid = @empid AND @myparam" +
                        "ORDER BY a.employee";
                
                Query = Query.Replace("@myparam", _ParamDate);
                Query = Query.Replace("@empid", EmpID);

                List<ReportData> list = new List<ReportData>();
                DataTable data = new DataTable();
                data = reportdata.ProcessReportData(Query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            float _totalHours = 0;
                            float _totalOTHours = 0;
                            float _totalOTLabor = 0;

                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                TimeSpan ts = Convert.ToDateTime(data.Rows[i]["timeout"].ToString()) - Convert.ToDateTime(data.Rows[i]["timein"].ToString());
                                string hrs = Helpers.GetDurationInterval("00:00:00", ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds, Helpers.eReturnTime.Hours);
                                float lbr = (float)(ts.TotalHours * Helpers.NullToFlt(data.Rows[i]["payrate"].ToString()));

                                _totalHours += (float)hrs.ToDecimal();
                                
                                if (_totalHours > Helpers.NullToFlt(payrollSett.Rows[0]["OvertimeHours"].ToString()))
                                {
                                    _totalOTHours += (float)hrs.ToDecimal();
                                    _totalOTLabor += lbr;
                                }
                            }

                            totalOTHours += _totalOTHours;
                            totalOTLabor += _totalOTLabor;
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // move to next week
                _currentDate = _currentDate.AddDays(7);
            }

            OTHours = totalOTHours;
            OTLabor = totalOTLabor;
        }
    }

    public class CEmployee_DriverReimbursement : CReport
    {
        public CEmployee_DriverReimbursement(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

            MessageBox.Show("Not yet complete.");
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
                            _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable dttickt = new DataTable();
                DataTable dtEmp = new DataTable();
                DataTable dtJob = new DataTable();
                DataTable dtshft = new DataTable();
                DataTable dttimeclk = new DataTable();

                Query = "SELECT a.employeeid, B.serviceTypeName, A.SubTotal AS Amount, a.tipsAdded, A.DeliveryCharge, FormatDate(A.date,0) AS mdate " +
                        "FROM tickethistory A INNER JOIN servicetypes B ON A.serviceTypeID = B.id " +
                        "WHERE @myparam AND B.isDelivery = 1 " +
                        "ORDER BY employeeid";
                Query = Query.Replace("@myparam", _ParamDate.Replace("b.date", "a.date"));
                dttickt = reportdata.ProcessReportData(Query, null, null);

                Query = "SELECT id, displayname, firstname, lastname FROM employees";
                dtEmp = reportdata.ProcessReportData(Query, null, null);

                Query = "SELECT empid, jobdescription, payrate, paytype FROM employeejobs";
                dtJob = reportdata.ProcessReportData(Query, null, null);

                Query = "SELECT b.clockin, b.clockout, b.jobcodename, b.hoursworked, b.payrate, FormatDate(b.date,0), b.employeeid, b.employeename, b.totalsales, b.cctips, b.punchid " +
                        "FROM employee_completedshifts AS b WHERE @myparam";
                Query = Query.Replace("@myparam", _ParamDate);
                dtshft = reportdata.ProcessReportData(Query, null, null);

                Query = "SELECT empid, jobdescription, payrate, paytype FROM employeejobs";
                dttimeclk = reportdata.ProcessReportData(Query, null, null);


                //// table
                //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                //    Query1 = Query1.Replace("@mytable", "tickets");
                //else
                //    Query1 = Query1.Replace("@mytable", "tickethistory");

                // param
                //Query = Query.Replace("@myparam", _ParamDate);

                if (dttickt != null)
                {
                    try
                    {
                        if (dttickt.Rows.Count > 0)
                        {
                            //float totalsales = 0;
                            //float totalhours = 0;

                            for (int i = 0; i < dttickt.Rows.Count; i++)
                            {
                                //string hrs = Helpers.GetDurationInterval("00:00:00", data.Rows[i]["HoursWorked"].ToString(), Helpers.eReturnTime.Hours);
                                //list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
                                //                        data.Rows[i]["empname"].ToString(),
                                //                        data.Rows[i]["jobCodeName"].ToString(),
                                //                        data.Rows[i]["ClockIn"].ToString(),
                                //                        data.Rows[i]["ClockOut"].ToString(),
                                //                        hrs
                                //                        ));

                                //totalsales += Helpers.NullToFlt(data.Rows[i]["TotalSales"].ToString());
                                //totalhours += (float)hrs.ToDecimal();
                            }
                            
                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
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

    }

    #region commented
    //public class CEmployee_PayrollReport : CReport
    //{
    //    public CEmployee_PayrollReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
    //        : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
    //    {
    //        this.DV = DV;
    //        this.StoreData = StoreData;
    //        this.ParamDate = ParamDate;
    //        this.report = ReportInstance;
    //        this.MyType = ReportType;

    //    }

    //    private string Query { get; set; }

    //    public override List<ReportData> DataSourceToBind()
    //    {
    //        try
    //        {
    //            List<string> sfield = new List<string>();
    //            List<string> svalue = new List<string>();

    //            // parameters
    //            string _ParamDate = "";// (ParamDate.Count == 0 ? "" : "WHERE ");
    //            //string _ParamDate = "WHERE ";
    //            for (int i = 0; i < ParamDate.Count; i++)
    //            {
    //                if (ParamDate.Count == 2)
    //                {
    //                    if (i == 0)
    //                        _ParamDate += "FormatDate(b.date,0) >= " + "@date" + i.ToString() + " AND ";
    //                    else
    //                        _ParamDate += "FormatDate(b.date,0) <= " + "@date" + i.ToString() + " ";
    //                }
    //                else
    //                {
    //                    _ParamDate += "FormatDate(b.date,0) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
    //                }

    //                sfield.Add("@date" + i.ToString());
    //                svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
    //            }

    //            List<ReportData> list = new List<ReportData>();
    //            CReportData reportdata = new CReportData();
    //            DataTable data = new DataTable();
    //            DataTable data1 = new DataTable();

    //            Query = "SELECT a.ID, CONCAT(a.LastName,', ', a.FirstName) AS empname, b.jobCodeName, c.PayRate, c.PayType, b.HoursWorked, b.TotalSales, b.CCTips " +
    //                    "FROM employees AS a INNER JOIN employee_completedshifts AS b ON a.id = b.employeeid " +
    //                    "INNER JOIN employeejobs AS c ON b.EmployeeID = c.EmpID AND b.jobcodename = c.jobdescription " +
    //                    "WHERE @myparam " +
    //                    "ORDER BY a.LastName, a.FirstName, DATE";

    //            //// table
    //            //if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
    //            //    Query1 = Query1.Replace("@mytable", "tickets");
    //            //else
    //            //    Query1 = Query1.Replace("@mytable", "tickethistory");

    //            Query = Query.Replace("@myparam", _ParamDate);
    //            data = reportdata.ProcessReportData(Query, sfield, svalue);

    //            Query = "SELECT ticketnumber, employeeid, date, tipsadded, subtotal " +
    //                    "FROM tickethistory " +
    //                    "WHERE @myparam AND status NOT LIKE 'VOIDED' " +
    //                    "ORDER BY date";

    //            Query = Query.Replace("@myparam", _ParamDate);
    //            data1 = reportdata.ProcessReportData(Query, sfield, svalue);

    //            if (data != null)
    //            {
    //                try
    //                {
    //                    if (data.Rows.Count > 0)
    //                    {
    //                        float totalhourly = 0;
    //                        float totalsalary = 0;
    //                        float totalsales = 0;
    //                        float totalhours = 0;

    //                        for (int i = 0; i < data.Rows.Count; i++)
    //                        {
    //                            string hrs = Helpers.GetDurationInterval("00:00:00", data.Rows[i]["HoursWorked"].ToString(), Helpers.eReturnTime.Hours);
    //                            //float 

    //                            list.Add(new ReportData(data.Rows[i]["ID"].ToString(),
    //                                                    data.Rows[i]["empname"].ToString(),
    //                                                    data.Rows[i]["jobCodeName"].ToString(),
    //                                                    data.Rows[i]["PayRate"].ToString(),
    //                                                    data.Rows[i]["PayType"].ToString(),
    //                                                    hrs,
    //                                                    ((float)hrs.ToDecimal() * (float)data.Rows[i]["payrate"].ToString().ToDecimal()).ToString(),
    //                                                    data.Rows[i]["CCTips"].ToString()
    //                                                    ));

    //                            if (data.Rows[i]["payrate"].ToString() == "Hourly")
    //                                totalhourly += (float)hrs.ToDecimal() * (float)data.Rows[i]["payrate"].ToString().ToDecimal();
    //                            else
    //                                totalsalary += (float)hrs.ToDecimal() * (float)data.Rows[i]["payrate"].ToString().ToDecimal();

    //                            totalsales += Helpers.NullToFlt(data.Rows[i]["TotalSales"].ToString());
    //                            totalhours += (float)hrs.ToDecimal();
    //                        }

    //                    }
    //                    else
    //                    {
    //                        if (!Parent.HideMessages)
    //                            MessageBox.Show("No records retreived.");
    //                        else
    //                            Console.WriteLine("No records retreived.");
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    if (!Parent.HideMessages)
    //                        MessageBox.Show(ex.Message);
    //                    else
    //                        Console.WriteLine(ex.Message);
    //                }
    //            }

    //            return list;

    //        }
    //        catch (Exception ex)
    //        {
    //            if (!Parent.HideMessages)
    //                MessageBox.Show(ex.Message);
    //            else
    //                Console.WriteLine(ex.Message);
    //        }
    //        return null;
    //    }

    //    private float GetTotal(DataTable DT, string Field, string EmpID)
    //    {
    //        float ftotal = 0;
    //        DataRow[] drarray = null;
    //        drarray = DT.Select("employeeid=" + EmpID);
    //        for (int i = 0; i < drarray.Count(); i++)
    //        {
    //            ftotal += Helpers.NullToFlt(drarray[i][Field]);
    //        }

    //        return ftotal;
    //    }

    //}
    #endregion

    public class CEmployee_CashDrawerActivity : CReport
    {
        public CEmployee_CashDrawerActivity(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

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

                Query = "SELECT FormatDate(a.DATE,0) AS DATA, a.time AS data1, a.user AS data2, a.station AS data3, a.tendertype AS data4, IF(a.amount=0,'Yes','No') AS data5 " +
                        "FROM bankactivityhistory AS a " +
                        "WHERE @myparam ";

                string Query1 = Query;

                // table
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    Query1 = Query1.Replace("@mytable", "bankactivity");
                else
                    Query1 = Query1.Replace("@mytable", "bankactivityhistory");

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
                                                        data.Rows[i]["data5"].ToString()
                                                        ));

                            }

                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public class CEmployee_List : CReport
    {
        public CEmployee_List(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
            : base(DV, StoreData, ParamDate, ReportInstance, ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;

        }

        private string Query { get; set; }

        public override List<ReportData> DataSourceToBind()
        {
            try
            {
                List<string> sfield = new List<string>();
                List<string> svalue = new List<string>();
                
                Query = "SELECT id as data, lastname as data1, firstname as data2, CONCAT(phone1, IF(LENGTH(phone2) > 0, CONCAT(' / ', phone2), '')) AS data3 FROM employees";
                
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
                                                        data.Rows[i]["data3"].ToString()
                                                        ));

                            }

                        }
                        else
                        {
                            if (!Parent.HideMessages)
                                MessageBox.Show("No records retreived.");
                            else
                                Console.WriteLine("No records retreived.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!Parent.HideMessages)
                            MessageBox.Show(ex.Message);
                        else
                            Console.WriteLine(ex.Message);
                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

}
