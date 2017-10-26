using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormulaSalesReportLib;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SalesReportControl SR = new SalesReportControl();
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            // #### USAGE ####

            //optional : update connection string
            SalesReportDatabaseConnectionSettings.User = "root";
            SalesReportDatabaseConnectionSettings.Password = "root";
            SalesReportDatabaseConnectionSettings.Database = "purepos";
            SalesReportDatabaseConnectionSettings.Port = "3306";


            //add document viewer to the form
            this.Controls.Add(SR);
            SR.Dock = DockStyle.Fill;
            //SR.StoreName = "Papa's Pizza To Go";
            SR.ParamDate.Add(new ParamDate(Convert.ToDateTime("09/01/2017"), ParameterCondition.AND));
            SR.ParamDate.Add(new ParamDate(Convert.ToDateTime("09/03/2017"), ParameterCondition.AND));
            SR.ShowPrintButton = true;
            SR.LoadReport(LoadReportType.NetSalesByServiceType);
            //SR.ShowPreview(LoadReportType.NetSalesByServiceType);


            ////show report preview dialog
            //SR.StoreName = "Papa's Pizza To Go";
            //SR.ParamDate.Add(new ParamDate(Convert.ToDateTime("09/01/2017"), ParameterCondition.AND));
            //SR.ShowPrintButton = true;
            //SR.ShowPreviewDialog(LoadReportType.NetSalesByServiceType);


            ////show report preview
            //SR.StoreName = "Papa's Pizza To Go";
            //SR.ParamDate.Add(new ParamDate(Convert.ToDateTime("09/01/2017"), ParameterCondition.AND));
            //SR.ShowPrintButton = true;
            //SR.ShowPreview(LoadReportType.NetSalesByServiceType);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SR.LoadReport(LoadReportType.NetSalesByServiceType);
            //SR.ShowPreview(LoadReportType.NetSalesByServiceType);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SR.LoadReport(LoadReportType.GrossSalesSummarybyHours);
            //SR.ShowPreview(LoadReportType.GrossSalesSummarybyHours);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SR.LoadReport(LoadReportType.NetSalesByCategory);
            //SR.ShowPreview(LoadReportType.NetSalesByCategory);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SR.LoadReport(LoadReportType.DiscountSummary);
            //SR.ShowPreview(LoadReportType.DiscountSummary);
        }
    }
}
