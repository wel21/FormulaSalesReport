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

    #region Base Class

    public class rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public StoreInfo StoreInformation { get; set; }

        public List<ParamDate> ParamDate { get; set; }

    }

    public class CReport
    {
        public DevExpress.XtraPrinting.Preview.DocumentViewer DV { get; set; }
        public CRStoreData StoreData { get; set; }
        public List<ParamDate> ParamDate { get; set; }

        public ReportType MyType { get; set; }
        public rpt rpt { get; set; }
        public rpt report { get; set; }// = new rpt_History_CardPaymentsByType();
        
        public CReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;
            //PrintButton.Click -= Print;
            //PrintButton.Click += Print;
        }

        #region Report Action
        public void ShowReport()
        {
            ReportHelper.ActiveReport = MyType;
            //if (rpt == null && RecordsRetrieved == false) rpt = CreateReport();
            rpt = CreateReport();
            DV.DocumentSource = rpt;
            rpt.CreateDocument();
        }

        public void ShowPreviewReport()
        {
            ReportHelper.ActiveReport = MyType;
            //if (rpt == null && RecordsRetrieved == false) rpt = CreateReport();
            rpt = CreateReport();
            rpt.ShowPreview();
        }

        public void ShowPreviewReportDialog()
        {
            ReportHelper.ActiveReport = MyType;
            //if (rpt == null && RecordsRetrieved == false) rpt = CreateReport();
            rpt = CreateReport();
            rpt.ShowPreviewDialog();
        }

        public void Print()
        {
            try { rpt.PrintDialog(); }
            catch { }
        }
        #endregion

        
        public virtual List<ReportData> DataSourceToBind()
        {            
            return null;
        }

        public rpt CreateReport()
        {
            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";

                //MessageBox.Show(StoreData.StoreInformation.StoreName);
                report.StoreInformation = StoreData.StoreInformation;
                //MessageBox.Show(ParamDate[0].date.ToString());
                report.ParamDate = ParamDate;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }
    }

    #endregion

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
        }

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
                            _ParamDate += "DateFormatConvert(A.date) >= " + "@date" + i.ToString() + " AND ";
                        else
                            _ParamDate += "DateFormatConvert(A.date) <= " + "@date" + i.ToString() + " ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(A.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + "");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                string query = "";

                query = "SELECT DateFormatconvert(a.date) AS DATE, a.paymenttype, SUBSTRING(a.tendertype,8) AS CardType, a.time, a.ticketnumber, a.subtotal, b.tipsAdded " +
                        "FROM paymenthistory AS a " +
                        "INNER JOIN tickethistory AS b ON a.ticketNumber = b.TicketNumber " +
                        "WHERE " + (_ParamDate == "" ? "" : _ParamDate + " AND ") + " a.tenderType LIKE 'CREDIT%' " +
                        "GROUP BY a.ticketnumber, a.paymentType, a.time, a.subtotal, b.tipsadded";

                //MessageBox.Show(query);


                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["DATE"].ToString(),
                                                        data.Rows[i]["paymenttype"].ToString(),
                                                        data.Rows[i]["CardType"].ToString(),
                                                        "",
                                                        Convert.ToDateTime(data.Rows[i]["DATE"]).ToString("MM/dd ") + data.Rows[i]["time"].ToString(),
                                                        "",
                                                        data.Rows[i]["ticketnumber"].ToString(),
                                                        data.Rows[i]["subtotal"].ToString(),
                                                        data.Rows[i]["tipsAdded"].ToString(),
                                                        (Convert.ToDecimal(data.Rows[i]["subtotal"].ToString()) + Convert.ToDecimal(data.Rows[i]["tipsAdded"].ToString())).ToString()
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
        }

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

                string query = "";

                query = "SELECT DateFormatconvert(a.Closed) AS date, a.cashcollected, a.cashsales, a.overshort, a.closed " +
                        "FROM bankhistory AS a " +
                        "WHERE " + _ParamDate;// (_ParamDate == "" ? "" : _ParamDate + " AND ");

                //MessageBox.Show(query);


                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                //MessageBox.Show(query);
                data = reportdata.ProcessReportData(query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["date"].ToString().Substring(5) + "/" + data.Rows[i]["date"].ToString().Substring(0, 4),
                                                        data.Rows[i]["cashcollected"].ToString(),
                                                        data.Rows[i]["cashsales"].ToString(),
                                                        "",
                                                        data.Rows[i]["overshort"].ToString(),
                                                        Convert.ToDateTime(data.Rows[i]["closed"].ToString()).ToShortTimeString(),
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
        }

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

                string query = "";

                query = "SELECT B.serviceTypeName AS ServiceTypeName, COUNT(*) AS Quantity, SUM(A.SubTotal) AS SubTotal, SUM(A.Tax) AS Tax, SUM(A.DeliveryCharge) AS DeliveryCharge, DateFormatConvert(A.date) AS mdate " +
                        "FROM tickethistory AS A INNER JOIN servicetypes AS B ON A.serviceTypeID = B.id " +
                        "WHERE " + _ParamDate +
                        "GROUP BY B.serviceTypeName";

                //frmMessage frm = new frmMessage();
                //frm.ShowForm(query);


                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {                            
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["ServiceTypeName"].ToString(),
                                                        data.Rows[i]["SubTotal"].ToString(),
                                                        data.Rows[i]["Tax"].ToString(),
                                                        data.Rows[i]["DeliveryCharge"].ToString(),
                                                        (Convert.ToDecimal(data.Rows[i]["SubTotal"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["Tax"].ToString()) +
                                                         Convert.ToDecimal(data.Rows[i]["DeliveryCharge"].ToString())).ToString()
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
        }

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

                string query = "";

                query = "SELECT a.id, b.TicketNumber, DateFormatconvert(a.date) AS date, a.voidreason, b.itemname, CONCAT(c.firstname, ' ', c.lastname) AS employee, b.ItemPrice " +
                        "FROM tickethistory AS a INNER JOIN ticketitemshistory AS b ON a.ticketnumber = b.ticketnumber " +
                        "INNER JOIN employees AS c ON a.employeeID = c.id " +
                        "WHERE " + _ParamDate + " AND a.status = 'VOIDED' " +
                        "GROUP BY ticketnumber, itemname " +
                        "ORDER BY a.date, a.ticketnumber, b.itemname";

                //frmMessage frm = new frmMessage();
                //frm.ShowForm(query);


                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {

                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["date"].ToString().Substring(5) + "/" + data.Rows[i]["date"].ToString().Substring(0, 4),
                                                        data.Rows[i]["voidreason"].ToString(),
                                                        data.Rows[i]["itemname"].ToString(),
                                                        "",
                                                        data.Rows[i]["employee"].ToString(),
                                                        data.Rows[i]["ItemPrice"].ToString()
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
        }
        
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

                string query = "";

                query = "SELECT SUBSTRING(a.tendertype,8) AS CardType, COUNT(a.subtotal) AS Qty, SUM(a.subtotal) AS PaymentAmount, SUM(b.tipsAdded) AS TipAmount " +
                        "FROM paymenthistory AS a INNER JOIN tickethistory AS b ON a.`ticketNumber` = b.`TicketNumber` " +
                        "INNER JOIN employees AS c ON a.employeeID = c.id " +
                        "WHERE " + _ParamDate + " AND a.tenderType LIKE 'CREDIT%' " +
                        "GROUP BY a.tendertype ";

                //frmMessage frm = new frmMessage();
                //frm.ShowForm(query);


                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                data = reportdata.ProcessReportData(query, sfield, svalue);

                if (data != null)
                {
                    try
                    {
                        if (data.Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                list.Add(new ReportData(data.Rows[i]["CardType"].ToString(),
                                                        data.Rows[i]["Qty"].ToString(),
                                                        data.Rows[i]["PaymentAmount"].ToString(),
                                                        data.Rows[i]["TipAmount"].ToString(),
                                                        (Convert.ToDouble(data.Rows[i]["PaymentAmount"].ToString()) + Convert.ToDouble(data.Rows[i]["TipAmount"].ToString())).ToString()
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
}
