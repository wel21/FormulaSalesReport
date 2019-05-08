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
using System.IO;

using DevExpress.XtraPrinting;

namespace FormulaReportsLib
{

    #region Base Class

    public class rpt : DevExpress.XtraReports.UI.XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;

        public StoreInfo StoreInformation { get; set; }

        public List<ParamDate> ParamDate { get; set; }

        public object MyObject { get; set; }

        public List<ColumnHeader> OptionalColumnsToSomeReports = new List<ColumnHeader>();

        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 100F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.HeightF = 100F;
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 100F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // rpt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Version = "17.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
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

        private bool NullReport;
        public ReportsControl Parent { get; set; }

        //public bool EmpActivity { get; set; }

        public CReport(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType, bool NullReport = false)
        {
            this.NullReport = NullReport;
            if (NullReport != true)
            {
                this.DV = DV;
                this.StoreData = StoreData;
                this.ParamDate = ParamDate;
                this.report = ReportInstance;
            }
            this.MyType = ReportType;
            //if (ReportType == ReportType.Employee_ActivityLog) EmpActivity = true;
        }
        

        #region Report Action
        public void ShowReport()
        {
            if (NullReport != true)
            {
                ReportHelper.ActiveReport = MyType;
                if (MyType == ReportType.AllSalesReport)
                    rpt = CreateReportAllSales();
                else
                    rpt = CreateReport();
                rpt.CreateDocument();
                rpt.ShowPreviewMarginLines = false;
                DV.DocumentSource = rpt;
                DV.ShowPageMargins = false;
            }
        }

        public void ShowReportDesigner()
        {
            if (NullReport != true)
            {
                ReportHelper.ActiveReport = MyType;
                if (MyType == ReportType.AllSalesReport)
                    rpt = CreateReportAllSales();
                else
                    rpt = CreateReport();
                rpt.ShowPreviewMarginLines = false;
                rpt.ShowDesigner();
            }
        }

        public void ShowPreviewReport()
        {
            if (NullReport != true)
            {
                ReportHelper.ActiveReport = MyType;
                if (MyType == ReportType.AllSalesReport)
                    rpt = CreateReportAllSales();
                else
                    rpt = CreateReport();
                rpt.ShowPreviewMarginLines = false;
                rpt.ShowPreview();
            }
        }

        public void ShowPreviewReportDialog()
        {
            if (NullReport != true)
            {
                ReportHelper.ActiveReport = MyType;
                if (MyType == ReportType.AllSalesReport)
                    rpt = CreateReportAllSales();
                else
                    rpt = CreateReport();
                rpt.ShowPreviewMarginLines = false;
                rpt.ShowPreviewDialog();
            }
        }

        public void Print()
        {
            if (NullReport != true)
            {
                try { rpt.PrintDialog(); }
                catch { }
            }
        }

        public byte[] ExportToPDF()
        {
            if (NullReport != true)
            {
                byte[] pdfArr;
                using (MemoryStream memStream = new MemoryStream())
                {
                    if (rpt == null)
                    {
                        ReportHelper.ActiveReport = MyType;
                        if (MyType == ReportType.AllSalesReport)
                            rpt = CreateReportAllSales();
                        else
                            rpt = CreateReport();
                        rpt.CreateDocument();
                        rpt.ShowPreviewMarginLines = false;
                    }
                    rpt.ExportToPdf(memStream);
                    pdfArr = memStream.ToArray();
                }
                return pdfArr;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region DataSource
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

        public virtual List<ReportData> DataSourceToBind5()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind6()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind7()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind8()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind9()
        {
            return null;
        }

        public virtual List<ReportData> DataSourceToBind10()
        {
            return null;
        }

        public string DataMember1 { get; set; }
        public string DataMember2 { get; set; }
        public string DataMember3 { get; set; }
        public string DataMember4 { get; set; }
        public string DataMember5 { get; set; }
        public string DataMember6 { get; set; }
        public string DataMember7 { get; set; }
        public string DataMember8 { get; set; }
        public string DataMember9 { get; set; }
        public string DataMember10 { get; set; }

        public virtual SqlDataSource DataSourceToBindDS()
        {
            return null;
        }
        #endregion

        public virtual rpt CreateReport()
        {
            if (NullReport != true)
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
                {
                    if (!Parent.HideMessages)
                        MessageBox.Show(ex.Message);
                    else
                        Console.WriteLine(ex.Message);
                }

                return report;
            }
            else
            {
                return null;
            }
        }

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
            {
                if (!Parent.HideMessages)
                    MessageBox.Show(ex.Message);
                else
                    Console.WriteLine(ex.Message);
            }

            return reportAllSales;
        }
    }

    #endregion
    
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
        public CSales_PayIn_PayOut Sales_PayIn_PayOut;
        public CSales_Coupons Sales_Coupons;

        public CHistory_CardPaymentsByType History_CardPaymentsByType;
        public CHistory_PaymentsBySrvcType History_PaymentsBySrvcType;
        public CHistory_SalesBySrvcType History_SalesBySrvcType;
        public CHistory_SalesOverview History_SalesOverview;
        public CHistory_SalesUnitQty History_SalesUnitQty;
        public CHistory_Voids History_Voids;
        public CHistory_SalesByDay History_SalesByDay;
        public CHistory_SquareReport History_SquareReport;

        public CEmployee_ActivityLog Employee_ActivityLog;
        public CEmployee_LaborReport Employee_LaborReport;
        public CEmployee_List Employee_List;
        public CEmployee_CashDrawerActivity Employee_CashDrawerActivity;

        public CAllSalesReport AllSalesReport;

        public COthers_MonetaryBatch Others_MonetaryBatch;


        public List<CReport> MyReports = new List<CReport>();

        public bool HideMessages { get; set; }

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
            HideMessages = false;
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

            InitializeCustomer();

            InitializeItem();

            InitializeOthers();
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

            Sales_PayIn_PayOut = new CSales_PayIn_PayOut(DV,            StoreData,
                                                                        ParamDate,
                                                                        new rpt_Sales_PayIn_PayOut(),
                                                                        ReportType.Sales_PayIn_PayOut);
            Sales_Coupons = new CSales_Coupons(DV,                      StoreData,
                                                                        ParamDate,
                                                                        new rpt_Sales_Coupons(),
                                                                        ReportType.Sales_Coupons);

            // Sales ------------------------------------------------------------------------------

            MyReports.Add(Sales_CreditCardTrans);
            MyReports.Add(Sales_OverShortByBusinessDay);
            MyReports.Add(Sales_SalesBySrvcType);
            MyReports.Add(Sales_SalesSummary);
            MyReports.Add(Sales_Voids);
            MyReports.Add(Sales_PayIn_PayOut);
            MyReports.Add(Sales_Coupons);

            Sales_CreditCardTrans.Parent = this;
            Sales_OverShortByBusinessDay.Parent = this;
            Sales_SalesBySrvcType.Parent = this;
            Sales_SalesSummary.Parent = this;
            Sales_Voids.Parent = this;
            Sales_PayIn_PayOut.Parent = this;
            Sales_Coupons.Parent = this;
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
            History_SquareReport = new CHistory_SquareReport(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_History_SquareReport(),
                                                            ReportType.History_SquareReport);

            // History ----------------------------------------------------------------------------

            MyReports.Add(History_CardPaymentsByType);
            MyReports.Add(History_PaymentsBySrvcType);
            MyReports.Add(History_SalesBySrvcType);
            MyReports.Add(History_SalesOverview);
            MyReports.Add(History_SalesUnitQty);
            MyReports.Add(History_Voids);
            MyReports.Add(new CReport(null, null, null, null, ReportType.History_EndOfDaySalesNumbers, true)); //History_EndOfDaySalesNumbers = 11
            MyReports.Add(History_SalesByDay);
            MyReports.Add(History_SquareReport);

            History_CardPaymentsByType.Parent = this;
            History_PaymentsBySrvcType.Parent = this;
            History_SalesBySrvcType.Parent = this;
            History_SalesOverview.Parent = this;
            History_SalesUnitQty.Parent = this;
            History_Voids.Parent = this;
            History_SalesByDay.Parent = this;
            History_SquareReport.Parent = this;
        }

        private void InitializeEmployee()
        {
            // Employee ------------------------------------------------------------------------------

            Employee_ActivityLog = new CEmployee_ActivityLog(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_ActivityLog(),
                                                            ReportType.Employee_ActivityLog);

            Employee_CashDrawerActivity = new CEmployee_CashDrawerActivity(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_CashDrawerActivity(),
                                                            ReportType.Employee_CashDrawerActivity);

            Employee_LaborReport = new CEmployee_LaborReport(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_LaborReport(),
                                                            ReportType.Employee_LaborReport);

            Employee_List = new CEmployee_List(DV,
                                                            StoreData,
                                                            ParamDate,
                                                            new rpt_Employee_List(),
                                                            ReportType.Employee_List);

            // Employee ------------------------------------------------------------------------------

            MyReports.Add(new CReport(null, null, null, null, ReportType.Employee_PayrollReport, true)); //Employee_PayrollReport = 14
            MyReports.Add(Employee_ActivityLog);
            MyReports.Add(Employee_CashDrawerActivity);
            MyReports.Add(new CReport(null, null, null, null, ReportType.Employee_DriverReimbursement, true)); //Employee_DriverReimbursement = 17
            MyReports.Add(Employee_LaborReport);
            MyReports.Add(Employee_List);

            Employee_ActivityLog.Parent = this;
            Employee_CashDrawerActivity.Parent = this;
            Employee_LaborReport.Parent = this;
            Employee_List.Parent = this;
        }

        private void InitializeCustomer()
        {
            MyReports.Add(new CReport(null, null, null, null, ReportType.Customer_306090DaysSinceLastOrder, true)); //Customer_306090DaysSinceLastOrder = 20
            MyReports.Add(new CReport(null, null, null, null, ReportType.Customer_CustomerCredits, true)); //Customer_CustomerCredits = 21
            MyReports.Add(new CReport(null, null, null, null, ReportType.Customer_Customers, true)); //Customer_Customers = 22
            MyReports.Add(new CReport(null, null, null, null, ReportType.Customer_NewCustomers, true)); //Customer_NewCustomers = 23
        }

        private void InitializeItem()
        {
            MyReports.Add(new CReport(null, null, null, null, ReportType.Items_ItemSalesSummary, true)); //Items_ItemSalesSummary = 24
            MyReports.Add(new CReport(null, null, null, null, ReportType.Items_SalesByGroup, true)); //Items_SalesByGroup = 25
        }

        private void InitializeOthers()
        {

            AllSalesReport = new CAllSalesReport(DV,
                                                StoreData,
                                                ParamDate,
                                                new rpt_AllSalesReport(),
                                                ReportType.AllSalesReport);

            Others_MonetaryBatch = new COthers_MonetaryBatch(DV,
                                                StoreData,
                                                ParamDate,
                                                new rpt_Others_MonetaryBatch(),
                                                ReportType.Others_MonetaryBatch);

            MyReports.Add(AllSalesReport);
            MyReports.Add(Others_MonetaryBatch);

            AllSalesReport.Parent = this;
            Others_MonetaryBatch.Parent = this;
        }

        public void ShowPreview(int Index)
        {
            ReportType rptype = (ReportType)Index;

            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == rptype)
                    if (MyReports[i].report != null)
                        MyReports[i].ShowReport();
            }
        }

        public void ShowPreview(ReportType ReportType)
        {
            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == ReportType)
                    if (MyReports[i].report != null)
                        MyReports[i].ShowReport();
            }
        }

        public void Print(ReportType ReportType)
        {
            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == ReportType)
                    if (MyReports[i].report != null)
                        MyReports[i].Print();
            }
        }

        public byte[] GetPDF(int Index)
        {
            ReportType rptype = (ReportType)Index;

            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == rptype)
                    if (MyReports[i].report != null)
                        return MyReports[i].ExportToPDF();
            }

            return null;
        }

        public byte[] GetPDF(ReportType ReportType)
        {
            for (int i = 0; i < MyReports.Count; i++)
            {
                if (MyReports[i].MyType == ReportType)
                    if (MyReports[i].report != null)
                        return MyReports[i].ExportToPDF();
            }

            return null;
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

                // ----------------------------------------------------------------------------
                case ReportType.Sales_PayIn_PayOut:
                    Sales_PayIn_PayOut.Print();
                    break;
                case ReportType.Sales_Coupons:
                    Sales_Coupons.Print();
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
                case ReportType.History_SquareReport:
                    History_SquareReport.Print();
                    break;
                // ----------------------------------------------------------------------------
                case ReportType.Employee_PayrollReport:
                    break;
                case ReportType.Employee_ActivityLog:
                    Employee_ActivityLog.Print();
                    break;
                case ReportType.Employee_CashDrawerActivity:
                    Employee_CashDrawerActivity.Print();
                    break;
                case ReportType.Employee_DriverReimbursement:
                    break;
                case ReportType.Employee_LaborReport:
                    Employee_LaborReport.Print();
                    break;
                case ReportType.Employee_List:
                    Employee_List.Print();
                    break;

                // ----------------------------------------------------------------------------
                case ReportType.Customer_306090DaysSinceLastOrder:
                    break;
                case ReportType.Customer_CustomerCredits:
                    break;
                case ReportType.Customer_Customers:
                    break;
                case ReportType.Customer_NewCustomers:
                    break;

                // ----------------------------------------------------------------------------
                case ReportType.Items_ItemSalesSummary:
                    break;
                case ReportType.Items_SalesByGroup:
                    break;

                // ----------------------------------------------------------------------------
                case ReportType.AllSalesReport:
                    AllSalesReport.Print();
                    break;

                // ----------------------------------------------------------------------------
                case ReportType.Others_MonetaryBatch:
                    AllSalesReport.Print();
                    break;
            }
        }
        

    }

    #region Enum
    public enum ReportType
    {
        Sales_CreditCardTrans,
        Sales_OverShortByBusinessDay,
        Sales_SalesBySrvcType,
        Sales_SalesSummary,
        Sales_Voids,
        Sales_PayIn_PayOut,
        Sales_Coupons,
        History_CardPaymentsByType,
        History_PaymentsBySrvcType,
        History_SalesBySrvcType,
        History_SalesOverview,
        History_SalesUnitQty,
        History_Voids,
        History_EndOfDaySalesNumbers,
        History_SalesByDay,
        History_SquareReport,
        Employee_PayrollReport,
        Employee_ActivityLog,
        Employee_CashDrawerActivity,
        Employee_DriverReimbursement,
        Employee_LaborReport,
        Employee_List,
        Customer_306090DaysSinceLastOrder,
        Customer_CustomerCredits,
        Customer_Customers,
        Customer_NewCustomers,
        Items_ItemSalesSummary,
        Items_SalesByGroup,
        AllSalesReport,
        Others_MonetaryBatch
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
    #endregion

    #region Parameters
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
    #endregion
}
