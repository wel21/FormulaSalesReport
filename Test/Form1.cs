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
        
        ReportsControl rptcontrol = new ReportsControl();
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;

            WindowState = FormWindowState.Maximized;
            dtpFrom.Value = Convert.ToDateTime("12/08/2017");
            dtpTo.Value = Convert.ToDateTime("12/11/2017");

            // #### USAGE ####

            //optional : update connection string
            DatabaseConnectionSettings.User = "root";
            DatabaseConnectionSettings.Password = "root";
            DatabaseConnectionSettings.Database = comboBox3.SelectedItem.ToString();
            DatabaseConnectionSettings.Port = "3306";


            //add document viewer to the form
            this.Controls.Add(rptcontrol);
            rptcontrol.Dock = DockStyle.Fill;
            rptcontrol.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if (dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                rptcontrol.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            rptcontrol.ShowPrintButton = true;
            
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptcontrol.ParamDate.Clear();
            rptcontrol.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if (dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                rptcontrol.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));

            rptcontrol.ShowPreview(comboBox1.SelectedIndex);            
        }
        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnectionSettings.Database = comboBox3.SelectedItem.ToString();
        }
    }
}
