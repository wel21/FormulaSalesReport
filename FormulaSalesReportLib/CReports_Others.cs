using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraPrinting.Preview;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraReports.UI;
using FPOSMonetaryBatchReport;

namespace FormulaReportsLib
{
    public class COthers_MonetaryBatch : CReport
    {
        public COthers_MonetaryBatch(DocumentViewer DV, CRStoreData StoreData, List<ParamDate> ParamDate, rpt ReportInstance, ReportType ReportType)
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
                List<ReportData> list = new List<ReportData>();
                CReportData reportdata = new CReportData();
                DataTable dtmerchantinfo = new DataTable();

                Query = "SELECT * FROM merchantinfo";
                dtmerchantinfo = reportdata.ProcessReportData(Query, null, null);


                // parameters
                string _StartDate = "";
                string _EndDate = "";
                //string _ParamDate = "WHERE ";
                for (int i = 0; i < ParamDate.Count; i++)
                {
                    if (ParamDate.Count == 2)
                    {
                        if (i == 0)
                            _StartDate = ParamDate[i].date.ToString("yyyy-MM-dd");
                        else
                            _EndDate = ParamDate[i].date.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        _StartDate = ParamDate[i].date.ToString("yyyy-MM-dd");
                        _EndDate = ParamDate[i].date.ToString("yyyy-MM-dd");
                    }
                }


                if (dtmerchantinfo != null)
                {
                    if (dtmerchantinfo.Rows[0]["Platform"].ToString() == "Monetary")
                    {

                        MonetaryReporting mrpt = new MonetaryReporting();
                        //"cert_secret63C7P14W0WT9"
                        List<TransactionInfo> TransactionInfos = mrpt.LoadCreditTransactions(dtmerchantinfo.Rows[0]["creditMerchantID"].ToString(), _StartDate, _EndDate);

                        try
                        {
                            ReportDataComparer rdc = new ReportDataComparer();

                            for (int i = 0; i < TransactionInfos.Count; i++)
                            {
                                list.Add(new ReportData(TransactionInfos[i].Reference,
                                                        TransactionInfos[i].DateTime,
                                                        TransactionInfos[i].Transcode,
                                                        TransactionInfos[i].Status,
                                                        TransactionInfos[i].CardBrand,
                                                        TransactionInfos[i].Last4,
                                                        TransactionInfos[i].Expiration,
                                                        TransactionInfos[i].Invoice,
                                                        TransactionInfos[i].Amount,
                                                        TransactionInfos[i].Captured
                                                        ));
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to generate report. CC Platform should be set to Monetary.");
                    }
                }
                else
                {
                    MessageBox.Show("Merchant Info table is empty.");
                }

                return list;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;
        }
        

    }

}
