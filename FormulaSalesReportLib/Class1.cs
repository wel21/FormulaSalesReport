using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Preview;

namespace FormulaSalesReportLib
{

    #region Base Class

    public class rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public StoreInfo StoreInformation { get; set; }

        public List<ParamDate> ParamDate { get; set; }

        public object MyObject { get; set; }

        public List<ColumnHeader> OptionalColumnsToSomeReports = new List<ColumnHeader>();


    }

    public class CReport
    {
        public DevExpress.XtraPrinting.Preview.DocumentViewer DV { get; set; }
        public CRStoreData StoreData { get; set; }
        public List<ParamDate> ParamDate { get; set; }
        public ReportType MyType { get; set; }
        public rpt rpt { get; set; }
        public rpt report { get; set; }
        //public rpt_Employee_ActivityLog1 reportEmpAct { get; set; }
        public rpt_AllSalesReport reportAllSales { get; set; }
        public List<ColumnHeader> OptionalColumnsToSomeReports = new List<ColumnHeader>();

        //public bool EmpActivity { get; set; }

        public CReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
        {
            this.DV = DV;
            this.StoreData = StoreData;
            this.ParamDate = ParamDate;
            this.report = ReportInstance;
            this.MyType = ReportType;
            //if (ReportType == ReportType.Employee_ActivityLog) EmpActivity = true;
        }

        //public CReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, bool empactivity, ReportType ReportType)
        //{
        //    this.DV = DV;
        //    this.StoreData = StoreData;
        //    this.ParamDate = ParamDate;
        //    this.MyType = ReportType;
        //    EmpActivity = empactivity;
        //}

        #region Report Action
        public void ShowReport()
        {
            ReportHelper.ActiveReport = MyType;
            if (MyType == ReportType.AllSalesReport)
                rpt = CreateReportAllSales();
            else
                rpt = CreateReport();
            DV.DocumentSource = rpt;
            rpt.CreateDocument();
        }

        public void ShowReportDesigner()
        {
            ReportHelper.ActiveReport = MyType;
            if (MyType == ReportType.AllSalesReport)
                rpt = CreateReportAllSales();
            else
                rpt = CreateReport();
            rpt.ShowDesigner();
        }

        public void ShowPreviewReport()
        {
            ReportHelper.ActiveReport = MyType;
            if (MyType == ReportType.AllSalesReport)
                rpt = CreateReportAllSales();
            else
                rpt = CreateReport();
            rpt.ShowPreview();
        }

        public void ShowPreviewReportDialog()
        {
            ReportHelper.ActiveReport = MyType;
            if (MyType == ReportType.AllSalesReport)
                rpt = CreateReportAllSales();
            else
                rpt = CreateReport();
            rpt.ShowPreviewDialog();
        }

        public void Print()
        {
            try { rpt.PrintDialog(); }
            catch { }
        }

        #endregion


        public virtual List<ReportData>  DataSourceToBind()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind1()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind2()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind3()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind4()
        {
            return null;
        }

        public string DataMember1 { get; set; }
        public string DataMember2 { get; set; }
        public string DataMember3 { get; set; }
        public string DataMember4 { get; set; }
        public virtual SqlDataSource DataSourceToBindDS()
        {
            return null;
        }
        
        public virtual rpt CreateReport()
        {
            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";

                //MessageBox.Show(StoreData.StoreInformation.StoreName);
                report.StoreInformation = StoreData.StoreInformation;
                report.OptionalColumnsToSomeReports = OptionalColumnsToSomeReports;
                //MessageBox.Show(ParamDate[0].date.ToString());
                report.ParamDate = ParamDate;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }

        //public virtual rpt_Employee_ActivityLog1 CreateReportEmpAct()
        //{
        //    try
        //    {
        //        SqlDataSource ds = DataSourceToBindDS();
        //        reportEmpAct = new rpt_Employee_ActivityLog1();

        //        // Assign the data source to the report. 
        //        reportEmpAct.DataSource = ds;
        //        reportEmpAct.DataMember = DataMember1;

        //        DetailBand detail = (DetailBand)reportEmpAct.Bands["Detail"];
        //        DetailReportBand detailreport1 = (DetailReportBand)reportEmpAct.Bands["DetailReport"];

        //        detailreport1.DataSource = ds;
        //        detailreport1.DataMember = DataMember2;
                
        //        reportEmpAct.StoreInformation = StoreData.StoreInformation;
        //        reportEmpAct.OptionalColumnsToSomeReports = OptionalColumnsToSomeReports;
        //        reportEmpAct.ParamDate = ParamDate;

        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message); }

        //    return reportEmpAct;
        //}

        public virtual rpt_AllSalesReport CreateReportAllSales()
        {
            try
            {
                SqlDataSource ds = DataSourceToBindDS();
                reportAllSales = new rpt_AllSalesReport();

                // Assign the data source to the report. 
                //reportAllSales.DataSource = ds;
                //reportAllSales.DataMember = DataMember1;

                //DetailBand detail = (DetailBand)reportAllSales.Bands["Detail"];

                DetailReportBand detailreport1 = (DetailReportBand)reportAllSales.Bands["DetailReport"];
                if (DataMember1 != null)
                {
                    detailreport1.DataSource = ds;
                    detailreport1.DataMember = DataMember1;
                }
                else
                    detailreport1.DataSource = DataSourceToBind1();

                DetailReportBand detailreport2 = (DetailReportBand)reportAllSales.Bands["DetailReport1"];
                if (DataMember2 != null)
                {
                    detailreport2.DataSource = ds;
                    detailreport2.DataMember = DataMember2;
                }
                else
                    detailreport2.DataSource = DataSourceToBind2();

                DetailReportBand detailreport3 = (DetailReportBand)reportAllSales.Bands["DetailReport2"];
                if (DataMember3 != null)
                {
                    detailreport3.DataSource = ds;
                    detailreport3.DataMember = DataMember3;
                }
                else
                    detailreport3.DataSource = DataSourceToBind3();

                    DetailReportBand detailreport4 = (DetailReportBand)reportAllSales.Bands["DetailReport3"];
                if (DataMember4 != null)
                {
                    detailreport4.DataSource = ds;
                    detailreport4.DataMember = DataMember4;
                }
                else
                    detailreport4.DataSource = DataSourceToBind4();

                        reportAllSales.StoreInformation = StoreData.StoreInformation;
                reportAllSales.OptionalColumnsToSomeReports = OptionalColumnsToSomeReports;
                reportAllSales.ParamDate = ParamDate;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return reportAllSales;
        }
    }

    #endregion

    public class SalesReportControl : Control
    {
        //CReportTotals RptSalesTotals = new CReportTotals();
        DevExpress.XtraPrinting.Preview.DocumentViewer DV = new DevExpress.XtraPrinting.Preview.DocumentViewer();
        public List<ParamDate> ParamDate = new List<ParamDate>();
        //public List<ParamString> ParamString = new List<ParamString>();
        //public List<ParamNumbers> ParamNumbers = new List<ParamNumbers>();
        CRStoreData StoreData = new CRStoreData();
        LoadReportType reportType;
        DataTable DTDiscounts;
        DataTable DTNetSales;

        Panel pnlHead = new Panel();
        Button btnPrint = new Button();

        double DTotalQty = 0;
        double DTotalOrders = 0;
        double DTotalAmt = 0;
        double DDeliveryChrge = 0;
        double DTax = 0;
        double DTotalPercent = 0;
        double DTotalDiscountQty = 0;
        double DTotalDiscountAmt = 0;
        double DNetSales = 0;
        string SBestHour = "";

        rpt1 Rpt1;
        rpt2 Rpt2;
        rpt3 Rpt3;
        rpt4 Rpt4;

        //public string StoreName { get; set; }
        //public string StoreAddress { get; set; }
        //public string StoreNumber { get; set; }

        private bool _ShowPrintButton = true;
        public bool ShowPrintButton
        {
            get
            {
                return _ShowPrintButton;
            }
            set
            {
                _ShowPrintButton = value;
                pnlHead.Visible = _ShowPrintButton;
            }
        }

        private string GetPrintIcon()
        { return "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"; }
        
        public SalesReportControl()
        {
            this.Controls.Add(DV);
            this.Controls.Add(pnlHead);
            pnlHead.Height = 50;
            pnlHead.Dock = DockStyle.Top;
            pnlHead.Controls.Add(btnPrint);
            pnlHead.BackColor = Color.FromArgb(235, 236, 239);
            btnPrint.Location = new Point(pnlHead.Width - 44-3, 3);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.BackgroundImage = Helpers.ConvBase64ToImage(GetPrintIcon());
            btnPrint.BackgroundImageLayout = ImageLayout.Zoom;
            btnPrint.Size = new Size(44, 44);
            btnPrint.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnPrint.BackColor = Color.FromArgb(235, 236, 239);
            btnPrint.Click += btnPrint_Click;
            DV.Dock = DockStyle.Fill;

            StoreData.StoreInfoPopulate();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void LoadReport(LoadReportType ReportType)
        {
            if (ParamDate.Count == 0)
            {
                MessageBox.Show("No date parameter selected.", "Parameter Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                reportType = ReportType;
                switch (ReportType)
                {
                    case LoadReportType.NetSalesByServiceType:
                        Rpt1 = CreateReport1();
                        DV.DocumentSource = Rpt1;
                        Rpt1.CreateDocument();

                        break;

                    case LoadReportType.GrossSalesSummarybyHours:
                        Rpt2 = CreateReport2();
                        DV.DocumentSource = Rpt2;
                        Rpt2.CreateDocument();

                        break;

                    case LoadReportType.NetSalesByCategory:
                        Rpt3 = CreateReport3();
                        DV.DocumentSource = Rpt3;
                        Rpt3.CreateDocument();

                        break;

                    case LoadReportType.DiscountSummary:
                        Rpt4 = CreateReport4();
                        DV.DocumentSource = Rpt4;
                        Rpt4.CreateDocument();

                        break;
                }
            }
        }

        public void ShowPreview(LoadReportType ReportType)
        {
            if (ParamDate.Count == 0)
            {
                MessageBox.Show("No date parameter selected.", "Parameter Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                reportType = ReportType;
                switch (ReportType)
                {
                    case LoadReportType.NetSalesByServiceType:
                        Rpt1 = CreateReport1();
                        Rpt1.ShowPreview();

                        break;

                    case LoadReportType.GrossSalesSummarybyHours:
                        Rpt2 = CreateReport2();
                        Rpt2.ShowPreview();

                        break;

                    case LoadReportType.NetSalesByCategory:
                        Rpt3 = CreateReport3();
                        Rpt3.ShowPreview();

                        break;

                    case LoadReportType.DiscountSummary:
                        Rpt4 = CreateReport4();
                        Rpt4.ShowPreview();

                        break;
                }
            }
        }

        public void ShowPreviewDialog(LoadReportType ReportType)
        {
            if (ParamDate.Count == 0)
            {
                MessageBox.Show("No date parameter selected.", "Parameter Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                reportType = ReportType;
                switch (ReportType)
                {
                    case LoadReportType.NetSalesByServiceType:
                        Rpt1 = CreateReport1();
                        Rpt1.ShowPreviewDialog();

                        break;

                    case LoadReportType.GrossSalesSummarybyHours:
                        Rpt2 = CreateReport2();
                        Rpt2.ShowPreviewDialog();

                        break;

                    case LoadReportType.NetSalesByCategory:
                        Rpt3 = CreateReport3();
                        Rpt3.ShowPreviewDialog();

                        break;

                    case LoadReportType.DiscountSummary:
                        Rpt4 = CreateReport4();
                        Rpt4.ShowPreviewDialog();

                        break;
                }
            }
        }
        
        private List<Data> DataSourceToBind()
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
                            _ParamDate += "DateFormatConvert(A.date) >= " + "@date" + i.ToString() + " AND "; //_ParamDate += "DateFormatConvert(A.date) >= '" + Helpers.ConvertMyDate(ParamDate[i].date) + "' AND ";
                        else
                            _ParamDate += "DateFormatConvert(A.date) <= " + "@date" + i.ToString() + " ";//_ParamDate += "DateFormatConvert(A.date) <= '" + Helpers.ConvertMyDate(ParamDate[i].date) + "' ";
                    }
                    else
                    {
                        _ParamDate += "DateFormatConvert(A.date) = " + "@date" + i.ToString() + " " + (i == ParamDate.Count - 1 ? "" : ParamDate[i].paramCondition.ToString() + " ");
                    }

                    sfield.Add("@date" + i.ToString());
                    svalue.Add(Helpers.ConvertMyDate(ParamDate[i].date));
                }

                // table
                string _Table = "";
                string _TableDc = "";
                if (ParamDate.Count == 1 && ParamDate[0].date.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    _Table = "ticketitems";
                    _TableDc = "used_coupons";
                }
                else
                {
                    _Table = "ticketitemshistory";
                    _TableDc = "used_couponshistory";
                }


                // Create an SQL query to access the table. 
                string sparamticketnotmp = " ";// "AND A.TicketNumber < 80000 ";
                string query = "";

                switch (reportType)
                {
                    case LoadReportType.NetSalesByServiceType:
                        //query = "SELECT B.serviceTypeName as ServiceTypeName, Count(*) as Quantity, Sum(A.SubTotal) as Amount, Sum(A.DeliveryCharge) as DeliveryCharge, Sum(A.Tax) as Tax, DateFormatConvert(A.date) as mdate " +
                        //            "FROM tickethistory A INNER JOIN servicetypes B on A.serviceTypeID = B.id " +
                        //            "WHERE " + _ParamDate + " " + sparamticketnotmp +
                        //            "GROUP BY B.serviceTypeName";

                        break;

                    case LoadReportType.GrossSalesSummarybyHours:
                        query = "SELECT B.Time, COUNT(B.ItemName) AS Orders, SUM(B.ItemPrice) AS Amount " +
                                "FROM (SELECT CONCAT(SUBSTRING(A.Time,1,LOCATE(':',A.Time)),'00',SUBSTRING(A.Time,LENGTH(A.Time)-2)) AS Time, A.ItemName, A.ItemPrice, A.date " +
                                "FROM " + _Table + " as A " +
                                "WHERE " + _ParamDate + " " + sparamticketnotmp +
                                ") AS B " +
                                "GROUP BY B.Time";

                        break;

                    case LoadReportType.NetSalesByCategory:
                        query = "SELECT B.name as Category, COUNT(A.ItemName) as Quantity, SUM(A.ItemPrice) AS Amount , DateFormatConvert(A.date) as mdate " +
                                    "FROM " + _Table + " A INNER JOIN menucategories B ON A.ItemCategory = B.id " +
                                    "WHERE " + _ParamDate + " " + sparamticketnotmp +
                                    "GROUP BY B.name";

                        break;

                    case LoadReportType.DiscountSummary:
                        //query = "SELECT A.CouponName, COUNT(A.CouponName) AS Quantity, SUM(A.Amount) AS Amount, DateFormatConvert(A.date) as mdate " +
                        //        "FROM " + _TableDc + " A " +
                        //        "WHERE " + _ParamDate + " " + sparamticketnotmp +
                        //        "GROUP BY A.CouponName";

                        break;
                }


                List<Data> list = new List<Data>();

                CReportData reportdata = new CReportData();
                DataTable data = new DataTable();

                //get the discounts
                if (DTDiscounts == null)
                {
                    string querydiscount = "SELECT A.CouponName, COUNT(A.CouponName) AS Quantity, SUM(A.Amount) AS Amount, DateFormatConvert(A.date) as mdate " +
                                    "FROM " + _TableDc + " A " +
                                    "WHERE " + _ParamDate + " " + sparamticketnotmp +
                                    "GROUP BY A.CouponName";

                    //frmMessage frm = new frmMessage();
                    //frm.ShowForm(querydiscount);

                    DTotalDiscountQty = 0;
                    DTotalDiscountAmt = 0;
                    DTDiscounts = reportdata.ProcessReportData(querydiscount, sfield, svalue);
                    if (DTDiscounts.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTDiscounts.Rows.Count; i++)
                        {
                            DTotalDiscountQty += Convert.ToDouble(DTDiscounts.Rows[i]["Quantity"]);
                            DTotalDiscountAmt += Convert.ToDouble(DTDiscounts.Rows[i]["Amount"]);
                        }
                    }
                }

                //get the net sales
                if (DTNetSales == null)
                {
                    string querynet = "SELECT B.serviceTypeName as ServiceTypeName, Count(*) as Quantity, Sum(A.SubTotal) as Amount, Sum(A.DeliveryCharge) as DeliveryCharge, Sum(A.Tax) as Tax, DateFormatConvert(A.date) as mdate " +
                                    "FROM tickethistory A INNER JOIN servicetypes B on A.serviceTypeID = B.id " +
                                    "WHERE " + _ParamDate + " " + sparamticketnotmp +
                                    "GROUP BY B.serviceTypeName";

                    //frmMessage frm = new frmMessage();
                    //frm.ShowForm(querynet);

                    DNetSales = 0;
                    DTNetSales = reportdata.ProcessReportData(querynet, sfield, svalue);
                    if (DTNetSales.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTNetSales.Rows.Count; i++)
                        {
                            DNetSales += Convert.ToDouble(DTNetSales.Rows[i]["Amount"]);
                        }
                    }
                }

                //if (reportType != LoadReportType.DiscountSummary)
                //    data = reportdata.ProcessReportData(query, sfield, svalue);
                //else
                //    data = DTDiscounts;

                if (reportType == LoadReportType.NetSalesByServiceType)
                    data = DTNetSales;
                else if (reportType == LoadReportType.NetSalesByCategory || reportType == LoadReportType.GrossSalesSummarybyHours)
                    data = reportdata.ProcessReportData(query, sfield, svalue);
                else
                    data = DTDiscounts;


                try
                {
                    if (data.Rows.Count > 0)
                    {
                        double fTotalOrders = 0;
                        double fTotalQty = 0;
                        double fTotalAmt = 0;
                        double fDeliveryChrge = 0;
                        double fTax = 0;
                        
                        switch (reportType)
                        {
                            case LoadReportType.NetSalesByServiceType:

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    fTotalQty += Convert.ToDouble(data.Rows[i]["Quantity"]);
                                    fTotalAmt += Convert.ToDouble(data.Rows[i]["Amount"]);
                                    fDeliveryChrge += Convert.ToDouble(data.Rows[i]["DeliveryCharge"]);
                                    fTax += Convert.ToDouble(data.Rows[i]["Tax"]);
                                }

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    list.Add(new Data(data.Rows[i]["ServiceTypeName"].ToString(),
                                                      Convert.ToDouble(data.Rows[i]["Quantity"]),
                                                      Convert.ToDouble(data.Rows[i]["Amount"]),
                                                      (fTotalQty > 0 ? Convert.ToDouble(data.Rows[i]["Quantity"]) / fTotalQty : 0),
                                                      (Convert.ToDouble(data.Rows[i]["Quantity"]) > 0 ? Convert.ToDouble(data.Rows[i]["Amount"]) / Convert.ToDouble(data.Rows[i]["Quantity"]) : 0)));
                                    
                                }

                                break;

                            case LoadReportType.GrossSalesSummarybyHours:
                                
                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    fTotalAmt += Convert.ToDouble(data.Rows[i]["Amount"]);
                                }

                                double tmpbest = 0;
                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    string s = "";
                                    double d = Convert.ToDouble(data.Rows[i]["Amount"]);
                                    for (int j = 0; j < Math.Floor(d / 10); j++)
                                    { s += "$"; }

                                    if (Math.Floor(d / 10) > tmpbest)
                                    {
                                        tmpbest = Math.Floor(d / 10);
                                        SBestHour = data.Rows[i]["Time"].ToString();
                                    }
                                    list.Add(new Data(data.Rows[i]["Time"].ToString(),
                                                      Convert.ToDouble(data.Rows[i]["Orders"]),
                                                      d,
                                                      s));

                                }

                                break;

                            case LoadReportType.NetSalesByCategory:

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    fTotalQty += Convert.ToDouble(data.Rows[i]["Quantity"]);
                                    fTotalAmt += Convert.ToDouble(data.Rows[i]["Amount"]);
                                    //fDeliveryChrge += Convert.ToDouble(data.Rows[i]["DeliveryCharge"]);
                                    //fTax += Convert.ToDouble(data.Rows[i]["Tax"]);
                                }

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    list.Add(new Data(data.Rows[i]["Category"].ToString(),
                                                      Convert.ToDouble(data.Rows[i]["Quantity"]),
                                                      Convert.ToDouble(data.Rows[i]["Amount"]),
                                                      (fTotalQty > 0 ? Convert.ToDouble(data.Rows[i]["Quantity"]) / fTotalQty : 0)));

                                }

                                break;

                            case LoadReportType.DiscountSummary:

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    fTotalQty += Convert.ToDouble(data.Rows[i]["Quantity"]);
                                    fTotalAmt += Convert.ToDouble(data.Rows[i]["Amount"]);
                                    //fDeliveryChrge += Convert.ToDouble(data.Rows[i]["DeliveryCharge"]);
                                    //fTax += Convert.ToDouble(data.Rows[i]["Tax"]);
                                }

                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    list.Add(new Data(data.Rows[i]["CouponName"].ToString(),
                                                      Convert.ToDouble(data.Rows[i]["Quantity"]),
                                                      Convert.ToDouble(data.Rows[i]["Amount"]),
                                                      (DNetSales > 0 ? Convert.ToDouble(data.Rows[i]["Amount"]) / DNetSales : 0)));

                                }
                                
                                break;

                        }

                        DTotalOrders = fTotalOrders;
                        DTotalQty = fTotalQty;
                        DTotalAmt = fTotalAmt;
                        DDeliveryChrge = fDeliveryChrge;
                        DTax = fTax;
                    }
                    else
                    { MessageBox.Show("No records retreived."); }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }

        private rpt1 CreateReport1()
        {
            // Create a new report instance. 
            rpt1 report = new rpt1();

            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";

                report.TotalQty = DTotalQty;// RptSalesTotals.TotalQty;
                report.TotalAmt = DTotalAmt;//RptSalesTotals.TotalAmt;

                //MessageBox.Show(StoreData.StoreInformation.StoreName);
                report.StoreInformation = StoreData.StoreInformation;
                report.ParamDate = ParamDate;

                report.DeliveryCharge = DDeliveryChrge;
                report.Tax = DTax;

                report.DiscountQty = DTotalDiscountQty;
                report.DiscountAmt = DTotalDiscountAmt;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }
        
        private rpt2 CreateReport2()
        {
            // Create a new report instance. 
            rpt2 report = new rpt2();

            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";
                
                report.TotalAmt = DTotalAmt;//RptSalesTotals.TotalAmt;

                report.StoreInformation = StoreData.StoreInformation;
                report.ParamDate = ParamDate;

                report.BestHour = SBestHour;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }

        private rpt3 CreateReport3()
        {
            // Create a new report instance. 
            rpt3 report = new rpt3();

            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";

                report.TotalQty = DTotalQty;// RptSalesTotals.TotalQty;
                report.TotalAmt = DTotalAmt;//RptSalesTotals.TotalAmt;

                report.StoreInformation = StoreData.StoreInformation;
                report.ParamDate = ParamDate;

                report.TotalPercent = DTotalPercent;
                report.DiscountAmt = DTotalDiscountAmt;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }

        private rpt4 CreateReport4()
        {
            // Create a new report instance. 
            rpt4 report = new rpt4();

            try
            {
                // Assign the data source to the report. 
                report.DataSource = DataSourceToBind();
                //report.DataMember = "customQuery";

                report.TotalQty = DTotalQty;// RptSalesTotals.TotalQty;
                report.TotalAmt = DTotalAmt;//RptSalesTotals.TotalAmt;

                report.StoreInformation = StoreData.StoreInformation;
                report.ParamDate = ParamDate;

                report.TotalPercent = DTotalPercent;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return report;
        }

        public void Print()
        {

            switch (reportType)
            {
                case LoadReportType.NetSalesByServiceType:
                    Rpt1.PrintDialog();
                    break;

                case LoadReportType.GrossSalesSummarybyHours:
                    Rpt2.PrintDialog();
                    break;

                case LoadReportType.NetSalesByCategory:
                    Rpt3.PrintDialog();
                    break;

                case LoadReportType.DiscountSummary:
                    Rpt4.PrintDialog();
                    break;
            }

        }

    }

    public static class ReportHelper
    {
        public static ReportType ActiveReport;
        
        public static CReport MyActiveReport;
    }

    public class ReportsControl : Control
    {
        //CReportTotals RptSalesTotals = new CReportTotals();
        DocumentViewer DV = new DocumentViewer();
        public List<ParamDate> ParamDate = new List<ParamDate>();
        //public List<ParamString> ParamString = new List<ParamString>();
        //public List<ParamNumbers> ParamNumbers = new List<ParamNumbers>();
        CRStoreData StoreData = new CRStoreData();
         
        Panel pnlHead = new Panel();
        Button btnPrint = new Button();
        
        public CSales_CreditCardTrans Sales_CreditCardTrans;
        public CSales_OverShortByBusinessDay Sales_OverShortByBusinessDay;
        public CSales_SalesBySrvcType Sales_SalesBySrvcType;
        public CSales_SalesSummary Sales_SalesSummary;
        public CSales_Voids Sales_Voids;

        public CHistory_CardPaymentsByType History_CardPaymentsByType;
        public CHistory_PaymentsBySrvcType History_PaymentsBySrvcType;
        public CHistory_SalesBySrvcType History_SalesBySrvcType;
        public CHistory_SalesOverview History_SalesOverview;
        public CHistory_SalesUnitQty History_SalesUnitQty;
        public CHistory_Voids History_Voids;
        public CHistory_SalesByDay History_SalesByDay;

        public CEmployee_ActivityLog Employee_ActivityLog;
        public CEmployee_LaborReport Employee_LaborReport;


        public CAllSalesReport AllSalesReport;

        public List<CReport> MyReports = new List<CReport>();

        private bool _ShowPrintButton = true;
        public bool ShowPrintButton
        {
            get
            {
                return _ShowPrintButton;
            }
            set
            {
                _ShowPrintButton = value;
                pnlHead.Visible = value;
                btnPrint.Visible = value;
            }
        }

        private string GetPrintIcon()
        { return "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"; }
        
        public ReportsControl()
        {
            this.Controls.Add(DV);
            this.Controls.Add(pnlHead);
            pnlHead.Height = 50;
            pnlHead.Dock = DockStyle.Top;
            pnlHead.Controls.Add(btnPrint);
            pnlHead.BackColor = Color.FromArgb(235, 236, 239);
            btnPrint.Location = new Point(pnlHead.Width - 44-3, 3);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.BackgroundImage = Helpers.ConvBase64ToImage(GetPrintIcon());
            btnPrint.BackgroundImageLayout = ImageLayout.Zoom;
            btnPrint.Size = new Size(44, 44);
            btnPrint.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnPrint.BackColor = Color.FromArgb(235, 236, 239);
            btnPrint.Click += btnPrint_Click;
            DV.Dock = DockStyle.Fill;

            StoreData.StoreInfoPopulate();

            MyReports.Clear();
            InitializeReports();
        }

        private void InitializeReports()
        {
            InitializeSales();

            InitializeHistory();

            InitializeEmployee();

            AllSalesReport = new CAllSalesReport(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_AllSalesReport(),
                                                            ReportType.AllSalesReport);

            MyReports.Add(AllSalesReport);
        }

        private void InitializeSales()
        {
            // Sales ------------------------------------------------------------------------------

            Sales_CreditCardTrans = new CSales_CreditCardTrans(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Sales_CreditCardTrans(),
                                                            ReportType.Sales_CreditCardTrans);

            Sales_OverShortByBusinessDay = new CSales_OverShortByBusinessDay(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Sales_OverShortByBusinessDay(),
                                                            ReportType.Sales_OverShortByBusinessDay);

            Sales_SalesBySrvcType = new CSales_SalesBySrvcType(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Sales_SalesBySrvcType(),
                                                            ReportType.Sales_SalesBySrvcType);

            Sales_SalesSummary = new CSales_SalesSummary(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Sales_SalesSummary(),
                                                            ReportType.Sales_SalesSummary);

            Sales_Voids = new CSales_Voids(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Sales_Voids(),
                                                            ReportType.Sales_Voids);

            // Sales ------------------------------------------------------------------------------

            MyReports.Add(Sales_CreditCardTrans);
            MyReports.Add(Sales_OverShortByBusinessDay);
            MyReports.Add(Sales_SalesBySrvcType);
            MyReports.Add(Sales_SalesSummary);
            MyReports.Add(Sales_Voids);
        }

        private void InitializeHistory()
        {
            // History ----------------------------------------------------------------------------

            History_CardPaymentsByType = new CHistory_CardPaymentsByType(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_CardPaymentsByType(), 
                                                            ReportType.History_CardPaymentsByType);

            History_PaymentsBySrvcType = new CHistory_PaymentsBySrvcType(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_PaymentsBySrvcType(), 
                                                            ReportType.History_PaymentsBySrvcType);

            History_SalesBySrvcType = new CHistory_SalesBySrvcType(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_SalesBySrvcType(),
                                                            ReportType.History_SalesBySrvcType);

            History_SalesOverview = new CHistory_SalesOverview(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_SalesOverview(),
                                                            ReportType.History_SalesOverview);

            History_SalesUnitQty = new CHistory_SalesUnitQty(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_SalesUnitQty(),
                                                            ReportType.History_SalesUnitQty);

            History_Voids = new CHistory_Voids(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_Voids(),
                                                            ReportType.History_Voids);

            History_SalesByDay = new CHistory_SalesByDay(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_SalesByDay(),
                                                            ReportType.History_SalesByDay);

            // History ----------------------------------------------------------------------------

            MyReports.Add(History_CardPaymentsByType);
            MyReports.Add(History_PaymentsBySrvcType);
            MyReports.Add(History_SalesBySrvcType);
            MyReports.Add(History_SalesOverview);
            MyReports.Add(History_SalesUnitQty);
            MyReports.Add(History_Voids);
            MyReports.Add(History_SalesByDay);
        }

        private void InitializeEmployee()
        {
            // Employee ------------------------------------------------------------------------------

            Employee_ActivityLog = new CEmployee_ActivityLog(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_ActivityLog(),
                                                            ReportType.Employee_ActivityLog);

            Employee_LaborReport = new CEmployee_LaborReport(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_LaborReport(),
                                                            ReportType.Employee_LaborReport);

            // Employee ------------------------------------------------------------------------------

            MyReports.Add(Employee_ActivityLog);
            MyReports.Add(Employee_LaborReport);
        }

        public void ShowPreview(int Index)
        {
            ReportType rptype = (ReportType)Index;

            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == rptype)
                    MyReports[i].ShowReport();
            }
        }

        public void ShowPreview(ReportType ReportType)
        {
            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == ReportType)
                    MyReports[i].ShowReport();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ReportHelper.ActiveReport.ToString());
            //CReport rptclass;

            //System.Reflection.PropertyInfo propertyInfo = typeof(T).GetProperty(fieldName);
            //Type propertyType = propertyInfo.PropertyType;

            //TypeConverter converter = TypeDescriptor.GetConverter(type);
            //if (converter.CanConvertFrom(typeof(string)))
            //{
            //    var a = converter.ConvertFrom(fieldValue, type);

            //}

            ////System.Reflection.PropertyInfo pinfo = typeof(CReport).GetProperty(ReportHelper.ActiveReport.ToString());
            ////CReport value = pinfo.GetValue(new rptclass(), null);
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //rptclass = assembly.CreateInstance("C" + ReportHelper.ActiveReport.ToString()) as CReport;
            ////rptclass = (CReport)Convert.ChangeType(ReportHelper.ActiveReport.ToString(), typeof(CReport));

            //rptclass = (CReport)TypeDescriptor.GetConverter(typeof(CReport)).ConvertFromString("C" + ReportHelper.ActiveReport.ToString());
            //rptclass = Convert.ChangeType()
            //rptclass.Print();


            switch (ReportHelper.ActiveReport)
            {
                //0
                case ReportType.Sales_CreditCardTrans:
                    Sales_CreditCardTrans.Print();
                    break;
                case ReportType.Sales_OverShortByBusinessDay:
                    Sales_OverShortByBusinessDay.Print();
                    break;
                //2
                case ReportType.Sales_SalesBySrvcType:
                    Sales_SalesBySrvcType.Print();
                    break;
                case ReportType.Sales_SalesSummary:
                    Sales_SalesSummary.Print();
                    break;
                //4
                case ReportType.Sales_Voids:
                    Sales_Voids.Print();
                    break;
                case ReportType.History_CardPaymentsByType:
                    History_CardPaymentsByType.Print();
                    break;
                // ----------------------------------------------------------------------------
                //6
                case ReportType.History_PaymentsBySrvcType:
                    History_PaymentsBySrvcType.Print();
                    break;
                case ReportType.History_SalesBySrvcType:
                    History_SalesBySrvcType.Print();
                    break;
                //8
                case ReportType.History_SalesOverview:
                    History_SalesOverview.Print();
                    break;
                case ReportType.History_SalesUnitQty:
                    History_SalesUnitQty.Print();
                    break;
                //10
                case ReportType.History_Voids:
                    History_Voids.Print();
                    break;
                case ReportType.History_EndOfDaySalesNumbers:
                    break;
                case ReportType.History_SalesByDay:
                    History_SalesByDay.Print();
                    break;
                
                // ----------------------------------------------------------------------------
                case ReportType.Employee_PayrollReport:
                    break;
                case ReportType.Employee_ActivityLog:
                    Employee_ActivityLog.Print();
                    break;
                case ReportType.Employee_CashDrawerActivity:
                    break;
                case ReportType.Employee_DriverReimbursement:
                    break;
                case ReportType.Employee_LaborReport:
                    break;
                case ReportType.Customer_306090DaysSinceLastOrder:
                    break;
                case ReportType.Customer_CustomerCredits:
                    break;
                case ReportType.Customer_Customers:
                    break;
                case ReportType.Customer_NewCustomers:
                    break;
                case ReportType.Items_ItemSalesSummary:
                    break;
                case ReportType.Items_SalesByGroup:
                    break;

                // ----------------------------------------------------------------------------
                case ReportType.AllSalesReport:
                    break;
            }
        }
        

    }
    
    public enum ReportType
    {
        Sales_CreditCardTrans,
        Sales_OverShortByBusinessDay,
        Sales_SalesBySrvcType,
        Sales_SalesSummary,
        Sales_Voids,
        History_CardPaymentsByType,
        History_PaymentsBySrvcType,
        History_SalesBySrvcType,
        History_SalesOverview,
        History_SalesUnitQty,
        History_Voids,
        History_EndOfDaySalesNumbers,
        History_SalesByDay,
        Employee_PayrollReport,
        Employee_ActivityLog,
        Employee_CashDrawerActivity,
        Employee_DriverReimbursement,
        Employee_LaborReport,
        Customer_306090DaysSinceLastOrder,
        Customer_CustomerCredits,
        Customer_Customers,
        Customer_NewCustomers,
        Items_ItemSalesSummary,
        Items_SalesByGroup,
        AllSalesReport
    }
    public enum LoadReportType
    {
        NetSalesByServiceType,
        GrossSalesSummarybyHours,
        NetSalesByCategory,
        DiscountSummary
    }
    public enum ParameterCondition
    {
        AND,
        OR
    }

    public class ParamDate
    {
        public ParamDate(DateTime Date, ParameterCondition ParamCondition)
        {
            this.date = Date;
            this.paramCondition = ParamCondition;
        }
        public DateTime date { get; set; }
        
        public ParameterCondition paramCondition { get; set; }


    }
    public class ParamString
    {
        public ParamString(string StringVal, ParameterCondition ParamCondition)
        {
            this.stringVal = StringVal;
            this.paramCondition = ParamCondition;
        }
        public string stringVal { get; set; }
        public ParameterCondition paramCondition { get; set; }
    }
    public class ParamNumbers
    {
        public ParamNumbers(float Numbers, ParameterCondition ParamCondition)
        {
            this.numbers = Numbers;
            this.paramCondition = ParamCondition;
        }
        public float numbers { get; set; }
        public ParameterCondition paramCondition { get; set; }
    }

}
