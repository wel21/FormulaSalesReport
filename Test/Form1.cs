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
        ReportsControl SR1 = new ReportsControl();
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            dtpFrom.Value = Convert.ToDateTime("08/02/2017");
            dtpTo.Value = Convert.ToDateTime("08/02/2017");

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
            SR.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if(dtpFrom.Value != dtpTo.Value) SR.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            SR.ShowPrintButton = true;
            //SR.LoadReport(LoadReportType.NetSalesByServiceType);

            this.Controls.Add(SR1);
            SR1.Dock = DockStyle.Fill;
            //SR.StoreName = "Papa's Pizza To Go";
            SR1.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if (dtpFrom.Value != dtpTo.Value) SR1.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            SR1.ShowPrintButton = true;
            //SR1.Sales_CreditCardTrans.ShowReport();
            
            comboBox1.GotFocus += comboBox1_GotFocus;
            comboBox2.GotFocus += comboBox2_GotFocus;
        }
        
        private void comboBox1_GotFocus(object sender, EventArgs e)
        {
            SR.Visible = false;
            SR1.Visible = true;
            checkBox2.Checked = true;
            checkBox1.Checked = false;
        }

        private void comboBox2_GotFocus(object sender, EventArgs e)
        {
            SR1.Visible = false;
            SR.Visible = true;
            checkBox1.Checked = true;
            checkBox2.Checked = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SR1.ParamDate.Clear();
            SR1.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            SR1.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    SR1.Sales_CreditCardTrans.ShowReport();
                    break;
                case 1:
                    SR1.Sales_OverShortByBusinessDay.ShowReport();
                    break;
                case 2:
                    SR1.Sales_SalesBySrvcType.ShowReport();
                    break;
                case 3:
                    break;
                case 4:
                    SR1.Sales_Voids.ShowReport();
                    break;
                case 5:
                    SR1.History_CardPaymentsByType.ShowReport();
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SR.ParamDate.Clear();
            SR.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            SR.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    SR.LoadReport(LoadReportType.NetSalesByServiceType);
                    break;
                case 1:
                    SR.LoadReport(LoadReportType.GrossSalesSummarybyHours);
                    break;
                case 2:
                    SR.LoadReport(LoadReportType.NetSalesByCategory);
                    break;
                case 3:
                    SR.LoadReport(LoadReportType.DiscountSummary);
                    break;
            }
        }

    }
}
