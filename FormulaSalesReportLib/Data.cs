using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaSalesReportLib
{
    class Data
    {
        public Data(string name, double orders, double amount, string averageSalesRepresentation)
        {
            this.Name = name;
            this.Quantity = 0;
            this.Orders = orders;
            this.Amount = amount;
            this.Percent = 0;
            this.Average = 0;
            this.AverageSalesRepresentation = averageSalesRepresentation;
        }

        public Data(string name, double quantity, double amount, double percent)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Orders = 0;
            this.Amount = amount;
            this.Percent = percent;
            this.Average = 0;
            this.AverageSalesRepresentation = "";
        }

        public Data(string name, double quantity, double amount, double percent, double average)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Orders = 0;
            this.Amount = amount;
            this.Percent = percent;
            this.Average = average;
            this.AverageSalesRepresentation = "";
        }

        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Orders { get; set; }
        public double Amount { get; set; }
        public double Percent { get; set; }
        public double Average { get; set; }
        public string AverageSalesRepresentation { get; set; }
    }
}
