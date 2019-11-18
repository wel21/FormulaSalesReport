using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormulaReportsLib;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // instantiate ReportsControl
        ReportsControl rptcontrol = new ReportsControl();
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;

            WindowState = FormWindowState.Maximized;
            dtpFrom.Value = Convert.ToDateTime("3/10/2019");
            dtpTo.Value = Convert.ToDateTime("4/17/2019");

            // #### USAGE ####

            //optional : update connection string
            DatabaseConnectionSettings.User = "root";
            DatabaseConnectionSettings.Password = "root";
            DatabaseConnectionSettings.Database = comboBox3.SelectedItem.ToString();
            DatabaseConnectionSettings.Port = "3306";
            //DatabaseConnectionSettings.SetConnectionString = "server=Localhost;User Id=root;password=root;Persist Security Info=True;database=purepos";


            //add control to the form
            this.Controls.Add(rptcontrol);
            rptcontrol.Dock = DockStyle.Fill;
            rptcontrol.ShowPrintButton = true;
            rptcontrol.BackColor = this.BackColor;

            //add date parameter
            rptcontrol.ParamDate.Add(new ParamDate(dtpFrom.Value, ParameterCondition.AND));
            if (dtpFrom.Value.ToShortDateString() != dtpTo.Value.ToShortDateString())
                rptcontrol.ParamDate.Add(new ParamDate(dtpTo.Value, ParameterCondition.AND));
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] pdfArr = rptcontrol.GetPDF(comboBox1.SelectedIndex);
        }
    }
}
