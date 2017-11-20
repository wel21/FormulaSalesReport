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
            comboBox3.SelectedIndex = 0;

            WindowState = FormWindowState.Maximized;
            dtpFrom.Value = Convert.ToDateTime("11/14/2017");
            dtpTo.Value = Convert.ToDateTime("11/14/2017");

            // #### USAGE ####

            //optional : update connection string
            SalesReportDatabaseConnectionSettings.User = "root";
            SalesReportDatabaseConnectionSettings.Password = "root";
            SalesReportDatabaseConnectionSettings.Database = comboBox3.SelectedItem.ToString();
            SalesReportDatabaseConnectionSettings.Port = "3306";


            //add document viewer to the form
            this.Controls.Add(SR);
            SR.Dock = DockStyle.Fill;
            //SR.StoreName = "Papa's Pizza To Go";
            SR.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if(dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                SR.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            SR.ShowPrintButton = true;
            //SR.LoadReport(LoadReportType.NetSalesByServiceType);

            this.Controls.Add(SR1);
            SR1.Dock = DockStyle.Fill;
            //SR.StoreName = "Papa's Pizza To Go";
            SR1.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if (dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                SR1.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
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
            if (dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                SR1.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    SR1.Sales_CreditCardTrans.ShowReport();
                    //custom query format
                    //SR1.Sales_CreditCardTrans.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field9 AS data8 FROM table WHERE @myparam";
                    break;
                case 1:
                    SR1.Sales_OverShortByBusinessDay.ShowReport();
                    //custom query format
                    //SR1.Sales_OverShortByBusinessDay.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field9 AS data8 FROM table WHERE @myparam";
                    break;
                case 2:
                    SR1.Sales_SalesBySrvcType.ShowReport();
                    //custom query format
                    //SR1.Sales_SalesBySrvcType.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam";
                    break;
                case 3:
                    SR1.Sales_SalesSummary.ShowReport();
                    break;
                case 4:
                    SR1.Sales_Voids.ShowReport();
                    //custom query format
                    //SR1.Sales_Voids.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field6 AS data5 FROM table WHERE @myparam";
                    break;
                case 5:
                    SR1.History_CardPaymentsByType.ShowReport();
                    //custom query format
                    //SR1.History_CardPaymentsByType.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3 FROM table WHERE @myparam";
                    break;
                case 6:
                    SR1.History_PaymentsBySrvcType.ShowReport();
                    //custom query format
                    //SR1.History_PaymentsBySrvcType.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, field4 AS data3, field5 AS data4 FROM table WHERE @myparam";
                    break;
                case 7:
                    SR1.History_SalesBySrvcType.ShowReport();
                    //custom query format
                    //SR1.History_SalesBySrvcType.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field6 AS data5 FROM table WHERE @myparam";
                    break;
                case 8:
                    break;
                case 9:
                    SR1.History_SalesUnitQty.ShowReport();
                    break;
                case 10:
                    SR1.History_Voids.ShowReport();
                    //custom query format
                    //SR1.History_SalesBySrvcType.Query = "SELECT field1 AS data, field2 AS data1, field3 AS data2, ... , field7 AS data6 FROM table WHERE @myparam";
                    break;
                case 11:
                    break;
                case 12:
                    SR1.History_SalesByDay.ShowReport();
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SalesReportDatabaseConnectionSettings.Database = comboBox3.SelectedItem.ToString();
        }
    }
}
