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

    public class ReportData
    {
        public ReportData(string data)
        {
            this.Data = data;
        }
        public ReportData(string data, string data1)
        {
            this.Data = data;
            this.Data1 = data1;
        }
        public ReportData(string data, string data1, string data2)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
        }
        public ReportData(string data, string data1, string data2, string data3)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data6 = data7;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
        }

        public string Data { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }
        public string Data7 { get; set; }
        public string Data8 { get; set; }
        public string Data9 { get; set; }
        public string Data10 { get; set; }
        public string Data11 { get; set; }
        public string Data12 { get; set; }
        public string Data13 { get; set; }
        public string Data14 { get; set; }
        public string Data15 { get; set; }
        public string Data16 { get; set; }
        public string Data17 { get; set; }
        public string Data18 { get; set; }
        public string Data19 { get; set; }
        public string Data20 { get; set; }
    }
}
